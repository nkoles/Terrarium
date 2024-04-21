using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalBaseClass : MonoBehaviour
{
    public float age;
    public float ageLimit;

    public float hunger;
    public float hungerLimit;

    public string animalName;

    public bool isBaby;

    public enum LifecycleStates
    {
        Idle,
        Hungry,
        Horny,
        Dead
    }

    public LifecycleStates currentState;

    public AttributeData attributes;

    public AnimalBaseClass InitialiseAnimal()
    {
        A nimalBaseClass result = new AnimalBaseClass();

        return result;
    }
}
