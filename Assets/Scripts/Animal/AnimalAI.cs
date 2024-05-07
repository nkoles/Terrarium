using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TerrariumTraits;
using LerpingUtils;
using System.Linq;
using System;
using static UnityEngine.ParticleSystem;
using UnityEditor;
using Unity.VisualScripting;

public class AnimalAI : MonoBehaviour
{
    [Header("Animal AI Fields")]
    public int consumption;
    public int consumptionTime;
    public ITerrariumProduct? target = null;

    public Grid terrainGrid;
    
    public bool CheckTargetDestruction()
    {
        if(target != null)
        {
            if(target.SelfObject == null)
            {
                target = null;
                return true;
            }
        }

        return false;
    }

    public Vector3[] AvailableTiles(TraitData animalTraits)
    {
        List<Vector3> results = new List<Vector3>();

        Ray[] directionalRays = new Ray[4]
        {
            new Ray(transform.position, transform.forward),
            new Ray(transform.position, -transform.forward),
            new Ray(transform.position, transform.right),
            new Ray(transform.position, -transform.right)
        };

        Ray[] groundRays = new Ray[4]
        {
            new Ray(transform.position + transform.forward, -transform.up),
            new Ray(transform.position - transform.forward, -transform.up),
            new Ray(transform.position + transform.right, -transform.up),
            new Ray(transform.position - transform.right, -transform.up)
        };

        RaycastHit[] directionalHits = new RaycastHit[4];
        RaycastHit[] groundHits = new RaycastHit[4];

        for(int i = 0; i < directionalHits.Length; ++i)
        {
            Physics.Raycast(directionalRays[i], out directionalHits[i], 1f);

            if (directionalHits[i].collider == null)
            {
                Physics.Raycast(groundRays[i], out groundHits[i], 1f);

                if (groundHits[i].collider != null && animalTraits.HasTrait<TerrainTraits>(groundHits[i].collider.GetComponent<TerrariumTerrain>().terrainFlag))
                {
                    Vector3Int cellPosition = terrainGrid.WorldToCell(groundHits[i].collider.gameObject.transform.position);
                    results.Add(new Vector3(cellPosition.x + 0.5f, transform.position.y, cellPosition.y+0.5f));
                }
            }
        }

        return results.ToArray();
    }

    public void Move(Vector3[] availableTiles, int chanceToMove = 100)
    {
        if(availableTiles.Length > 0)
        {
            int movementChance = UnityEngine.Random.Range(0, 101);
            int randomTile = 0;
            Vector3 target = transform.position;

            if (movementChance <= chanceToMove)
                target = availableTiles[UnityEngine.Random.Range(0, availableTiles.Length)];

            transform.position = target;
        }
    }

    public void Move(Vector3 availableTile, int chanceToMove = 100)
    {
        Vector3[] tile = new Vector3[] { availableTile };

        Move(tile, chanceToMove);
    }

    public ITerrariumProduct FindTargetWithTrait<TEnum>(TEnum searchTrait, float range) where TEnum: Enum
    {
        ITerrariumProduct target = null;

        int maxColliders = 5;

        Collider[] nearbyColliders = new Collider[maxColliders];
        List<ITerrariumProduct> nearbyTargets = new List<ITerrariumProduct>();

        Physics.OverlapSphereNonAlloc(transform.position, range, nearbyColliders, ~(1<<6));

        foreach(var collider in nearbyColliders)
        {
            if(collider != null && collider.GetComponent<ITerrariumProduct>() != null)
            {
                print(collider.name);

                print(collider.GetComponent<ITerrariumProduct>().Traits.GetTraitFlags<TEnum>());

                if (TraitUtils.HasTrait<TEnum>(collider.GetComponent<ITerrariumProduct>().Traits.GetTraitFlags<TEnum>(), searchTrait) && !collider.GetComponent<ITerrariumProduct>().IsBaby)
                {
                    nearbyTargets.Add(collider.GetComponent<ITerrariumProduct>());
                }
            }
        }

        float distance = float.MaxValue;

        print(nearbyTargets.Count);

        foreach(var targ in nearbyTargets)
        {
            Vector3 pos = targ.SelfObject.transform.position;

            if (Vector3.Distance(transform.position, pos) < distance)
            {
                target = targ;
                distance = Vector3.Distance(transform.position, pos);
            }
        }

        return target;
    }

    public ITerrariumProduct FindTargetWithSimilarTraits(TraitData traitData, int similarityCount, float range)
    {
        ITerrariumProduct target = null;

        int maxColliders = 5;

        Collider[] nearbyColliders = new Collider[maxColliders];
        List<ITerrariumProduct> nearbyTargets = new List<ITerrariumProduct>();

        Physics.OverlapSphereNonAlloc(transform.position, range, nearbyColliders, ~(1 << 6));

        foreach (var collider in nearbyColliders)
        {
            if (collider != null && collider.GetComponent<Animal>() != null && collider.GetComponent<AnimalAI>() != this)
            {
                print("Similarities" + collider.GetComponent<ITerrariumProduct>().Traits.CompareAllTraitSimilarity(traitData));
                if (collider.GetComponent<ITerrariumProduct>().Traits.CompareAllTraitSimilarity(traitData) > similarityCount && collider.GetComponent<Animal>().currentState == AnimalStates.Horny)
                {
                    nearbyTargets.Add(collider.GetComponent<ITerrariumProduct>());
                }
            }
        }

        if(nearbyTargets.Count > 0)
        {
            foreach(var obj in nearbyTargets)
            {
                print(obj.SelfObject.name);
            }
        }

        float distance = float.MaxValue;

        foreach (var targ in nearbyTargets)
        {
            Vector3 pos = targ.SelfObject.transform.position;

            if (Vector3.Distance(transform.position, pos) < distance)
            {
                print("why not targeting?");
                target = targ;
                distance = Vector3.Distance(transform.position, pos);
            }
        }

        //if(CheckTargetDestruction())
        //    target.SelfObject.GetComponent<AnimalAI>().target = this.target;
        if(target != null)
            print("partner " + target.SelfObject.name);

        return target;
    }

    public Vector3 ClosestTileToTarget(TraitData animalTraits)
    {
        Vector3[] nonFilteredAvailableTiles = AvailableTiles(animalTraits);

        int tileIndex = 0;
        float tempDistance = float.MaxValue;

        for (int i = 0; i < nonFilteredAvailableTiles.Length; ++i)
        {
            Vector3 pos = target.SelfObject.transform.position;

            if (Vector3.Distance(nonFilteredAvailableTiles[i], pos) < tempDistance)
            {
                tileIndex = i;
                tempDistance = Vector3.Distance(nonFilteredAvailableTiles[i], pos);
            }
        }

        return nonFilteredAvailableTiles[tileIndex];
    }

    public bool CheckForTarget()
    {
        List<Vector3> directions = new List<Vector3>() { transform.forward, -transform.forward, transform.right, -transform.right };

        RaycastHit[] hitData = new RaycastHit[4];

        for (int i = 0; i < 4; ++i)
        {
            Physics.Raycast(transform.position, directions[i], out hitData[i], 1f);

            ITerrariumProduct terrariumProduct;

            if (hitData[i].collider != null && hitData[i].collider.TryGetComponent<ITerrariumProduct>(out terrariumProduct))
            {
                if(terrariumProduct == target)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public bool Consume(TraitData traitData)
    {
        if(!target.Traits.foodTraits.HasFlag(FoodTraits.Meat) && (target.Traits.foodTraits.HasFlag(FoodTraits.Plant) || target.Traits.foodTraits.HasFlag(FoodTraits.Fertilizer))){
            print("test");
            consumption++;
        } else
        {
            consumption = consumptionTime;
        }

        if(consumption >= consumptionTime)
        {
            if(TraitUtils.HasTrait<FoodTraits>(target.Traits.foodTraits, FoodTraits.Plant))
            {
                TraitUtils.AddTrait<NutritionalTraits>(ref traitData.nutritionTraits, NutritionalTraits.Herbivore);
            }

            if (TraitUtils.HasTrait<FoodTraits>(target.Traits.foodTraits, FoodTraits.Meat))
            {
                TraitUtils.AddTrait<NutritionalTraits>(ref traitData.nutritionTraits, NutritionalTraits.Carnivore);
            }

            if(TraitUtils.HasTrait<FoodTraits>(target.Traits.foodTraits, FoodTraits.Fertilizer))
            {
                TraitUtils.AddTrait<NutritionalTraits>(ref traitData.nutritionTraits, NutritionalTraits.Scavenger);
            }

            Destroy(target.SelfObject);
            target = null;

            return true;
        }

        return false;
    }

    public bool Eat(TraitData traitData, FoodTraits searchTraits, float range)
    {
        if(target == null)
        {
            consumption = 0;
            Move(AvailableTiles(traitData), 70);
            target = FindTargetWithTrait<FoodTraits>(searchTraits, range);
        } else
        {
            print("Target Found");

            if (!CheckForTarget())
            {
                print("Getting to Target");
                if(!CheckTargetDestruction())
                    Move(ClosestTileToTarget(traitData));
            }
            else
            {
                print("Consuming");
                return Consume(traitData);
            }
        }

        return false;
    }

    public bool FreakyTime(TraitData traitData)
    {
        int randomSpawn = 0;

        if (TraitUtils.HasTrait<MiscTraits>(traitData.miscTraits, MiscTraits.Gender) != TraitUtils.HasTrait<MiscTraits>(target.Traits.miscTraits, MiscTraits.Gender))
        {
            if (TraitUtils.HasTrait<MiscTraits>(traitData.miscTraits, MiscTraits.Gender))
            {
                Vector3[] pos = AvailableTiles(traitData);

                if (pos.Length > 0)
                {
                    randomSpawn = UnityEngine.Random.Range(0, pos.Length);

                    AnimalFactory.instance.CreateTerrariumObject(pos[randomSpawn], traitData + target.Traits);
                }
            }
        }

        //Vector3[] pos = AvailableTiles(traitData.terrainTraits);

        //if (pos.Length > 0)
        //{
        //    randomSpawn = UnityEngine.Random.Range(0, pos.Length);

        //    AnimalFactory.instance.CreateTerrariumObject(pos[randomSpawn], traitData + target.Traits);
        //}

        //target = null;

        return true;
    }

    public bool Breed(TraitData traitData, int similarityCount, float range)
    {
        if(target == null)
        {
            print("finding partner");

            Move(AvailableTiles(traitData), 70);
            target = FindTargetWithSimilarTraits(traitData, similarityCount, range);
        } 
        else
        {
            print("Freakyyyyyyyyy");

            if(!CheckForTarget())
            {
                print("looking to get freaky");
                if(!CheckTargetDestruction())
                    Move(ClosestTileToTarget(traitData));
            } else
            {
                return FreakyTime(traitData);
            }
        }

        return false;
    }

//    private void OnDrawGizmos()
//    {
//        Ray[] directionalRays = new Ray[4]
//{
//                new Ray(transform.position, transform.forward),
//                new Ray(transform.position, -transform.forward),
//                new Ray(transform.position, transform.right),
//                new Ray(transform.position, -transform.right)
//};

//        Ray[] groundRays = new Ray[4]
//        {
//                new Ray(transform.position + transform.forward, -transform.up),
//                new Ray(transform.position - transform.forward, -transform.up),
//                new Ray(transform.position + transform.right, -transform.up),
//                new Ray(transform.position - transform.right, -transform.up)
//        };

//        for (int i = 0; i < 4; i++)
//        {
//            Gizmos.DrawRay(groundRays[i]);
//            Gizmos.DrawRay(directionalRays[i]);
//        }

//        Gizmos.DrawWireSphere(transform.position, 5);
//    }
}
