using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TerrariumTraits;
using LerpingUtils;
using System.Linq;
using Unity.VisualScripting;

public class AnimalAI : MonoBehaviour
{
    [Header("Animal AI Fields")]
    public float speed;
    public ITerrariumProduct? target = null;

    public Grid terrainGrid;

    public Vector3[] AvailableTiles(int terrainData)
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

        return results.ToArray();
    }

    public void Move(Vector3[] availableTiles)
    {
        if(availableTiles.Length > 0)
        {
            int randomTile = Random.Range(0, availableTiles.Length + 1);

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

    public ITerrariumProduct FindWithTrait(int terrainData, int[] searchData, float range)
    {
        ITerrariumProduct target = null;

        int maxColliders = 10;

        Collider[] nearbyColliders = new Collider[maxColliders];
        List<Vector3> nearbyCollidersPositions = new List<Vector3>();

        Physics.OverlapSphereNonAlloc(transform.position, range, nearbyColliders);

        foreach(var collider in nearbyColliders)
        {
            print(collider);

            if(TryGetComponent<ITerrariumProduct>(out target))
            {
                foreach(var trait in searchData)
                {
                    return target;

                    //if(TraitConstants.HasTrait(target.TraitData, trait))
                    //{
                    //    return target;
                    //} else
                    //{
                    //    target = null;
                    //}
                }
            }
        }

        return target;
    }

    public Vector3 ClosestTileToTarget(int terrainData)
    {
        Vector3[] nonFilteredAvailableTiles = AvailableTiles(terrainData);

        int tileIndex = 0;
        float tempDistance = float.MaxValue;

        for (int i = 0; i < nonFilteredAvailableTiles.Length; ++i)
        {
            if (Vector3.Distance(nonFilteredAvailableTiles[i], target.PositionalData.position) < tempDistance)
            {
                tileIndex = i;
                tempDistance = Vector3.Distance(nonFilteredAvailableTiles[i], target.PositionalData.position);
            }
        }

        return nonFilteredAvailableTiles[tileIndex];
    }

    public bool CheckForTrait(int[] searchData)
    {
        List<Vector3> directions = new List<Vector3>() { transform.forward , -transform.forward, transform.right, -transform.right };

        RaycastHit[] hitData = new RaycastHit[4];

        for(int i = 0; i < 4; ++i)
        {
            Physics.Raycast(transform.position, directions[i], out hitData[i], 1f);

            ITerrariumProduct terrariumProduct;

            foreach (int trait in searchData)
            {
                if (hitData[i].collider.TryGetComponent<ITerrariumProduct>(out terrariumProduct))
                {
                    if(TraitConstants.HasTrait(terrariumProduct.TraitData, trait))
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    public void Consume()
    {
        print("Consumed Target");
    }

    public void Eat(int[] nutritionData, int terrainData, float range)
    {
        print(CheckForTrait(nutritionData));

        if(target == null)
        {
            FindWithTrait(terrainData, nutritionData, range);
            Move(AvailableTiles(terrainData));
        } else
        {
            if(!CheckForTrait(nutritionData))
            {
                Move(ClosestTileToTarget(terrainData));
            } else
            {
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
