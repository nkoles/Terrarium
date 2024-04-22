using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TerrariumTraits;
using System;

public enum AnimalStates
{
    Idle,
    Hungry,
    Horny,
    Decomposing
}

public class Animal : AnimalAI, ITerrariumProduct
{
    [Header ("Animal General Fields")]
    public float maxAge;
    public float currentAge;

    public bool isBaby;
    public bool isDead;


    public float maxHunger;
    public float currentHunger;

    [SerializeField]
    private TraitData traits;
    public int traitData;

    public AnimalStates currentState;

    public bool isInitialised = false;
    public void Initialise()
    {
        maxAge = UnityEngine.Random.Range(120, 180);
        currentAge = 0;

        isBaby = true;

    }
    
    public void Age()
    {

    }

    private void Update()
    {
        if(isInitialised)
            Age();

        switch(currentState){
            case AnimalStates.Idle:
                break;
            case AnimalStates.Hungry:
                break;
            case AnimalStates.Horny:
                break;
            case AnimalStates.Decomposing:
                break;
            default:
                break;
        }
    }

    private void Awake()
    {
    }
}
