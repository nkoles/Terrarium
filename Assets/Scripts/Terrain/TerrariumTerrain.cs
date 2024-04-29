using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TerrariumTraits;

public class TerrariumTerrain : MonoBehaviour
{
    public TraitData traitData;

    private void Awake()
    {
        traitData = new TraitData(TerrainTraits.Ground);
    }
}
