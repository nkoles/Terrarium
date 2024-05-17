using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainFactory : MonoBehaviour
{
    public TerrariumTerrain groundPrefab;
    public TerrariumTerrain waterPrefab;

    public Transform terrainContainer;

    public void CreateTerrain(Vector3 pos, bool isGround, bool isBloody = false)
    {
        if(!isGround)
        {
            Instantiate(waterPrefab, pos, Quaternion.identity, terrainContainer);
        }
        else
        {
            TerrariumTerrain terrain = Instantiate(groundPrefab, pos, Quaternion.identity, terrainContainer);

            terrain.isBloody = isBloody;
        }

    }
}
