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

    public bool Bloom(float fertilityLevel, TraitData plantData)
    {
        bool hasBloomed = false;

        if(targetTerrain.fertility >= fertilityLevel)
        {
            int range = (int)(3 * fertilityLevel);
            int[] posIdx = new int[range];

            if(range!=0)
            {
                print("enough fertility");

                List<Vector3> availableTiles = CalculateAvailableBloomTiles(range); 
                List<int> takenTiles = new List<int>();

                print("availableTiles" + availableTiles.Count);

                if (availableTiles.Count > 0)
                {
                    for(int i = 0; i < range; ++i)
                    {
                        int randomPos = Random.Range(0, availableTiles.Count);

                        while (takenTiles.Contains(randomPos))
                        {
                            randomPos = Random.Range(0, availableTiles.Count);
                        }

                        takenTiles.Add(randomPos);

                        StartCoroutine(LerpParabola(GenerateParabolaPoints(transform.position, availableTiles[randomPos]), Instantiate(seedPrefab, transform), plantData)); 
                    }

                    hasBloomed = true;
                }
            }
        }

        return hasBloomed;
    }

    public Vector3[] GenerateParabolaPoints(Vector3 start, Vector3 end, int numPoints = 3)
    {
        List<Vector3> points = new List<Vector3>();

        float startEndDist = Vector3.Distance(start, end);

        float xDiff = end.x - start.x;
        float zDiff = end.z - start.z;

        Vector3 quarterPoint = new Vector3(end.x - 3 * ((end.x - start.x) / 4), start.y + startEndDist / 4, end.z - 3 * ((end.z - start.z) / 4));

        Vector3 midPoint = new Vector3(end.x - (end.x - start.x) / 2, start.y + startEndDist / 2, end.z - (end.z - start.z) / 2);

        Vector3 secondQuarterPoint = new Vector3(end.x - 1 * ((end.x - start.x) / 4), start.y + startEndDist / 4, end.z - 1 * ((end.z - start.z) / 4));

        //for (int i = 1; i < numPoints; ++i)
        //{


        //    points.Add(new Vector3(end.x - i * xDiff / numPoints, start.y + 1startEndDisti, end.z - i * zDiff / numPoints));
        //}

        points.Add(quarterPoint);
        points.Add(midPoint);
        points.Add(secondQuarterPoint);
        points.Add(end);

        return points.ToArray();
    }

    public IEnumerator LerpParabola(Vector3[] points, GameObject target, TraitData plantData)
    {
        foreach (var point in points)
        {
            Vector3 targetPos = point;

            float lerp = 0;

            while (lerp < 1)
            {
                lerp += Time.deltaTime * 10;

                target.transform.position = Vector3.Lerp(target.transform.position, point, lerp);

                yield return null;
            }
        }

        Destroy(target.gameObject);
        PlantFactory.instance.CreateTerrariumObject(points[points.Length-1], plantData);
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
