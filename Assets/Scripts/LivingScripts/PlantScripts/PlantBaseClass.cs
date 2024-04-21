using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBaseClass : MonoBehaviour
{
    public float age;
    public float ageLimit;

    public string plantName;

    public enum LifecycleStates
    {
        Seed,
        Sapling,
        Bloom,
        Dead
    }

    public LifecycleStates currentState;

    public AttributeData attributes;

    public PlantBaseClass InitialisePlant()
    {
        PlantBaseClass result = new PlantBaseClass();

        return result;
    }
}
