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

    public List<AttributeBaseClass> attributes;

    public enum LifecycleStates
    {
        Idle,
        Hungry,
        Horny,
        Dead
    }

    public LifecycleStates currentState;

    public AnimalBaseClass InitialiseAnimal()
    {
        AnimalBaseClass result = new AnimalBaseClass();

        return result;
    }

    private void Update()
    {
        switch (currentState)
        {
            case LifecycleStates.Idle:
                break;
            case LifecycleStates.Hungry:
                break;
            case LifecycleStates.Horny:
                break;
            case LifecycleStates.Dead:
                break;
        }
    }
}
