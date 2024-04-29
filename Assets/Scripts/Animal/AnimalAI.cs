using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TerrariumTraits;
using LerpingUtils;
using System.Linq;
using System;
using static UnityEngine.ParticleSystem;

public class AnimalAI : MonoBehaviour
{
    [Header("Animal AI Fields")]
    public float speed;
    public ITerrariumProduct? target = null;

    public Grid terrainGrid;

    public Vector3[] AvailableTiles(TerrainTraits terrainTraits)
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

                if (groundHits[i].collider != null && TraitUtils.HasTrait(terrainTraits, groundHits[i].collider.GetComponent<TerrariumTerrain>().traitData.terrainTraits))
                {
                    Vector3Int cellPosition = terrainGrid.WorldToCell(groundHits[i].collider.gameObject.transform.position);
                    results.Add(new Vector3(cellPosition.x, cellPosition.z, cellPosition.y));
                }
            }
        }

        return results.ToArray();
    }

    public void Move(Vector3[] availableTiles)
    {
        if(availableTiles.Length > 0)
        {
            int randomTile = UnityEngine.Random.Range(0, availableTiles.Length + 1);

            Vector3 target = transform.position;

            if(randomTile != availableTiles.Length)
            {
                target = new Vector3(availableTiles[randomTile].x + 0.5f, transform.position.y, availableTiles[randomTile].z + 0.5f);
                //print(target);
            }

            transform.position = target;
        }
    }

    public void Move(Vector3 availableTile)
    {
        Vector3[] tile = new Vector3[] { availableTile };

        Move(tile);
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

                if (TraitUtils.HasTrait<TEnum>(collider.GetComponent<ITerrariumProduct>().Traits.GetTraitFlags<TEnum>(), searchTrait))
                {
                    nearbyTargets.Add(collider.GetComponent<ITerrariumProduct>());
                }
            }
        }

        float distance = float.MaxValue;

        print(nearbyTargets.Count);

        foreach(var targ in nearbyTargets)
        {
            if(Vector3.Distance(transform.position, targ.PositionalData) < distance)
            {
                target = targ;
                distance = Vector3.Distance(transform.position, targ.PositionalData);
            }
        }

        return target;
    }

    public Vector3 ClosestTileToTarget(TerrainTraits terrainData)
    {
        Vector3[] nonFilteredAvailableTiles = AvailableTiles(terrainData);

        int tileIndex = 0;
        float tempDistance = float.MaxValue;

        for (int i = 0; i < nonFilteredAvailableTiles.Length; ++i)
        {
            if (Vector3.Distance(nonFilteredAvailableTiles[i], target.PositionalData) < tempDistance)
            {
                tileIndex = i;
                tempDistance = Vector3.Distance(nonFilteredAvailableTiles[i], target.PositionalData);
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

    public void Consume()
    {
    }

    public void Eat(TerrainTraits terrainTraits, FoodTraits searchTraits, float range)
    {
        if(target == null)
        {
            Move(AvailableTiles(terrainTraits));
            target = FindTargetWithTrait<FoodTraits>(searchTraits, range);
        } else
        {
            print("Target Found");

            if (!CheckForTarget())
            {
                print("Getting to Target");
                Move(ClosestTileToTarget(terrainTraits));
            }
            else
            {
                print("Consuming");
                Consume();
            }
        }
    }

    private void OnDrawGizmos()
    {
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

        for (int i = 0; i < 4; i++)
        {
            Gizmos.DrawRay(groundRays[i]);
            Gizmos.DrawRay(directionalRays[i]);
        }

        Gizmos.DrawWireSphere(transform.position, 5);
    }
}
