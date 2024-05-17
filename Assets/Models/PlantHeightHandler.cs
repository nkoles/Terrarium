using System;
using System.Collections;
using System.Collections.Generic;
using TerrariumTraits;
using UnityEngine;

public class PlantHeightHandler : MonoBehaviour
{
    private float _baseHeightY;

    public float waterHeightY;

    public bool isWater;

    public void Start()
    {
        isWater = GetComponentInParent<Plant>().Traits.terrainTraits.HasFlag(TerrainTraits.Water);

        _baseHeightY = transform.localPosition.y;
        
        UpdateWaterHeight();
    }

    public void UpdateWaterHeight()
    {
        
        
        if (isWater) transform.localPosition = new Vector3(0, waterHeightY, 0);
        else transform.localPosition = new Vector3(0, _baseHeightY, 0);
    }
}
