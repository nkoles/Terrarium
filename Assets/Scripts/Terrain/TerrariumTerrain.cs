using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TerrariumTraits;
using System.Drawing;

public class TerrariumTerrain : MonoBehaviour
{
    public TraitData traits; 

    public float fertility = 0;
    public float bloody = 0;



    public void CheckNearbyWaterTiles()
    {
        Vector3 size = new Vector3(1f, 0.25f, 1f);

        Collider[] colliders = new Collider[9];

        int num = Physics.OverlapBoxNonAlloc(transform.position, size, colliders, Quaternion.identity, 1<<6);

        print(num);

        float tempFertility = 0;

        foreach(var col in colliders)
        {
            if(col!=null)
            {
                if (col.TryGetComponent<TerrariumTerrain>(out TerrariumTerrain terrain) && terrain.traits.terrainTraits.HasFlag(TerrainTraits.Water))
                    tempFertility++;
            }
        }

        if(tempFertility >= 1)
        {
            fertility = 1;
        }else
        {
            fertility = tempFertility / 8;
        }
    }

    public void Awake()
    {
        if(traits.terrainTraits.HasFlag(TerrainTraits.Ground))
        {
            GameTimeManager.PreTick.AddListener(CheckNearbyWaterTiles);
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Vector3 offset = new Vector3(0, 0.25f, 0);
    //    Vector3 size = new Vector3(2f, 0.5f, 2f);
    //    Gizmos.DrawCube(transform.position - offset, size);
    //}
}
