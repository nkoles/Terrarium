using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TerrariumFactory : MonoBehaviour
{
    public abstract ITerrariumProduct CreateTerrariumObject(Vector3 position, TraitData traitData);
    public abstract ITerrariumProduct CreateTerrariumObject(Vector3 position, List<Enum> traitList);
    public abstract ITerrariumProduct CreateTerrariumObject(Vector3 position, UInt32 traitListID);
}
