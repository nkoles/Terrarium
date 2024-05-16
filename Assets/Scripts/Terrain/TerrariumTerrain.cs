using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TerrariumTraits;

public class TerrariumTerrain : MonoBehaviour
{
    public TerrainTraits terrainFlag;

    public float fertility = 0;
    public float bloody = 0;

    public void CheckNearbyWaterTiles()
    {

    }

    public void Awake()
    {
        GameTimeManager.Tick.AddListener(CheckNearbyWaterTiles);
    }
}
