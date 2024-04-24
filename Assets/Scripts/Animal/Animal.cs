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

    //[SerializeField]
    //private TraitData traits;
    private int _traitData;
    public int TraitData { get { return _traitData; } set { _traitData = value; } }

    public AnimalStates currentState;

    public bool isInitialised = false;
    public void Initialise()
    {
        maxAge = UnityEngine.Random.Range(120, 180);
        currentAge = 0;

        maxHunger = 10;

        isBaby = true;

        isInitialised = true;
    }
    
    public void Age()
    {

    }

    public void Lifecycle()
    {
        if(isInitialised)
            Age();

        switch(currentState){
            case AnimalStates.Idle:
                Move(AvailableTiles(TraitConstants.TERRAIN_GROUND));
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

    public void Awake()
    {
        TraitData = TraitConstants.TERRAIN_GROUND;
        GameTimeManager.Tick.AddListener(Lifecycle);
    }
}
