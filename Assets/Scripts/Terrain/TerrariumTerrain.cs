using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TerrariumTraits;

public class TerrariumTerrain : MonoBehaviour
{
    public int traitData = TraitConstants.TERRAIN_GROUND;

    private void Awake()
    {
        traitData = TraitConstants.TERRAIN_GROUND;
    }
}
