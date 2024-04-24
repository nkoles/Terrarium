using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TerrariumTraits;
using LerpingUtils;
using System.Linq;

public class AnimalAI : MonoBehaviour
{
    [Header("Animal AI Fields")]
    public float speed;

    public Grid terrainGrid;

    public List<Vector3> AvailableTiles(int terrainData)
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

                if (groundHits[i].collider != null && TraitConstants.HasTrait(groundHits[i].collider.GetComponent<TerrariumTerrain>().traitData, terrainData))    
                {
                    Vector3Int cellPosition = terrainGrid.WorldToCell(groundHits[i].collider.gameObject.transform.position);
                    results.Add(new Vector3(cellPosition.x, cellPosition.z, cellPosition.y));
                }
            }
        }

        return results;
    }

    public void Move(List<Vector3> availableTiles)
    {
        if(availableTiles.Count > 0)
        {
            int randomTile = Random.Range(0, availableTiles.Count + 1);

            Vector3 target = transform.position;

            if(randomTile != availableTiles.Count)
            {
                target = new Vector3(availableTiles[randomTile].x + 0.5f, transform.position.y, availableTiles[randomTile].z + 0.5f);
                print(target);
            }

            transform.position = target;
        }
    }

    public void Move(Vector3 availableTile)
    {
        List<Vector3> tile = new List<Vector3>() { availableTile };

        Move(tile);
    }

    public Vector3 Find(int terrainData, int searchData, float range, AnimalStates state)
    {
        List<Vector3> nonFilteredAvailableTiles = AvailableTiles(terrainData);

        Collider[] nearbyColliders = new Collider[0];
        List<Vector3> nearbyCollidersPositions = new List<Vector3>();

        Physics.OverlapSphereNonAlloc(transform.position, range, nearbyColliders);

        foreach(var collider in nearbyColliders)
        {
            Plant plant = new Plant();
            Animal animal = new Animal();

            if(collider.TryGetComponent<Plant>(out plant) || collider.TryGetComponent<Animal>(out animal))
            {
                if(TryGetComponent<Plant>(out plant))
                {
                    if (TraitConstants.HasTrait(plant.TraitData, searchData))
                        nearbyCollidersPositions.Add(collider.transform.position);
                }

                if (TryGetComponent<Animal>(out animal))
                {
                    if (TraitConstants.HasTrait(animal.TraitData, searchData))
                        nearbyCollidersPositions.Add(collider.transform.position);
                }
            }
        }

        int tileIndex = 0;
        float tempDistance = float.MaxValue;

        for (int i = 0; i < nonFilteredAvailableTiles.Count ; ++i)
        {
            for (int j = 0; j < nearbyCollidersPositions.Count; ++j)
            {
                if (Vector3.Distance(nonFilteredAvailableTiles[i], nearbyCollidersPositions[j]) < tempDistance)
                {
                    tileIndex = i;
                    tempDistance = Vector3.Distance(nonFilteredAvailableTiles[i], nearbyCollidersPositions[j]);
                }
            }
        }

        return nonFilteredAvailableTiles[tileIndex];
    }

    public bool CheckForFood(int traitData)
    {
        List<Vector3> directions = new List<Vector3>() { transform.forward , -transform.forward, transform.right, -transform.right };

        RaycastHit[] hitData = new RaycastHit[4];

        for(int i = 0; i < 4; ++i)
        {
            Physics.Raycast(transform.position, directions[i], out hitData[i], 1f);

            
        }

        return true;
    }

    public void Eat()
    {
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
    }
}
