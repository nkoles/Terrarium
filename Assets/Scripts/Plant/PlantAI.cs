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

        if (Physics.Linecast(transform.position, transform.position - Vector3.down, out hit))
        {
            TerrariumTerrain tempTerrain;

            if(hit.collider.TryGetComponent<TerrariumTerrain>(out tempTerrain) && plantTraits.HasTrait<TerrainTraits>(tempTerrain.terrainFlag))
            {
                targetTerrain = tempTerrain;
                return true;
            }
        }

        return false;
    }

    public List<Vector3> CalculateAvailableBloomTiles(int range)
    {
        List<Vector3> result = new List<Vector3>();

        Vector3 selfPos = transform.position;

        for(int i = 0; i < range; ++i)
        {
            for(int j = 0; j < range; ++j)
            {
                if (i == 0 && j == 0)
                    continue;

                Collider[] hitColliders = new Collider[1];

                Vector3 offset = new Vector3(i, 0, j);
                Vector3 size = transform.lossyScale / 2;

                if(Physics.OverlapBoxNonAlloc(transform.position + offset, size, hitColliders) == 0 && Physics.Linecast(transform.position+offset, transform.position + offset + Vector3.down, 1 << 6))
                {
                    result.Add(transform.position + offset);
                }
            }
        }

        return result;
    }

    public bool Bloom(float fertilityLevel, TraitData plantData)
    {
        bool hasBloomed = false;

        if(targetTerrain.fertility >= fertilityLevel)
        {
            int range = (int)(3 * fertilityLevel);
            int[] posIdx = new int[range];

            if(range!=0)
            {
                List<Vector3> availableTiles = CalculateAvailableBloomTiles(range);
                List<int> takenTiles = new List<int>();

                if (availableTiles.Count > 0)
                {
                    for(int i = 0; i < range; ++i)
                    {
                        int randomPos = Random.Range(0, availableTiles.Count);

                        while (!takenTiles.Contains(randomPos))
                        {
                            randomPos = Random.Range(0, availableTiles.Count);
                        }

                        takenTiles.Add(randomPos);

                        Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), availableTiles[randomPos], Quaternion.identity);
                    }
                }
            }
        }

        return hasBloomed;
    }
}
