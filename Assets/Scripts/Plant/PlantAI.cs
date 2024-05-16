using System.Collections;
using System.Collections.Generic;
using TerrariumTraits;
using UnityEngine;

public class PlantAI : MonoBehaviour
{
    [Header("Plant AI Fields")]
    public TerrariumTerrain? targetTerrain;
    public GameObject seedPrefab;

    public bool CheckForCorrectGround(TraitData plantTraits)
    {
        RaycastHit hit;

        if (Physics.Linecast(transform.position, transform.position + Vector3.down, out hit, 1<<6))
        {
            print(hit.collider);

            TerrariumTerrain tempTerrain;

            if(hit.collider.TryGetComponent<TerrariumTerrain>(out tempTerrain) && plantTraits.HasTrait<TerrainTraits>(tempTerrain.traits.terrainTraits))
            {
                targetTerrain = tempTerrain;
                print("test");
                return true;
            }
        }

        return false;
    }

    public List<Vector3> CalculateAvailableBloomTiles(int range)
    {
        print("entered");

        List<Vector3> result = new List<Vector3>();

        Vector3 selfPos = transform.position;

        for(int i = -range; i < range; ++i)
        {
            for(int j = -range; j < range; ++j)
            {
                if (i == 0 && j == 0)
                    continue;

                Collider[] hitColliders = new Collider[1];

                Vector3 offset = new Vector3(i, 0, j);
                Vector3 size = new Vector3(0.4f, 0.4f, 0.4f);

                print(i);
                print(j);
                
                Physics.OverlapBoxNonAlloc(transform.position + offset, size, hitColliders);

                if (hitColliders[0] == null)
                {
                    if(Physics.Linecast(transform.position + offset, transform.position + offset + Vector3.down, 1 << 6))
                    {
                        print("hello?");
                        result.Add(transform.position + offset);
                    }
                }
            }
        }

        return result;
    }

    public bool Bloom(TraitData plantData)
    {
        bool hasBloomed = false;

        int range = (int)(3 * targetTerrain.fertility);
        int[] posIdx = new int[range];

        if (range != 0)
        {
            print("enough fertility");

            List<Vector3> availableTiles = CalculateAvailableBloomTiles(range);
            List<int> takenTiles = new List<int>();

            print("availableTiles" + availableTiles.Count);

            if (availableTiles.Count > 0)
            {
                for (int i = 0; i < range; ++i)
                {
                    int randomPos = Random.Range(0, availableTiles.Count);

                    while (takenTiles.Contains(randomPos))
                    {
                        randomPos = Random.Range(0, availableTiles.Count);
                    }

                    takenTiles.Add(randomPos);
                    PlantFactory.instance.CreateTerrariumObject(availableTiles[randomPos]);
                }

                hasBloomed = true;
            }
        }

        return hasBloomed;
    }

    //private void OnDrawGizmos()
    //{
    //    for (int i = -3; i < 3; ++i)
    //    {
    //        for (int j = -3; j < 3; ++j)
    //        {
    //            Vector3 offset = new Vector3(i, 0, j);

    //            Gizmos.DrawCube(transform.position + offset, new Vector3(0.5f, 0.5f, 0.5f));
    //        }
    //    }
    //}
}
