using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TerrariumTraits;

using TraitType = System.Int32;

public interface ITerrainProduct
{
    public void CheckNeighbour();

    public void Initialise();
}
