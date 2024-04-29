using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TerrariumTraits;
using System;
using System.Linq;

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

    [SerializeField]
    private bool _isDead;
    public bool IsDead { get { return _isDead; } set { _isDead = value; } }

    public int maxHunger = 40;
    public int currentHunger = 0;

    [SerializeField]
    private TraitData _traitData;
    public TraitData Traits { get { return _traitData; } set { _traitData = value; } }

    [SerializeField]
    private Vector3 _position;
    public Vector3 PositionalData { get { return _position; } set { _position = value; } }

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
        currentHunger++;
    }

    public void Lifecycle()
    {
        if (isInitialised)
            Age();

        PositionalData = transform.position;

        switch (currentState){
            case AnimalStates.Idle:
                Move(AvailableTiles(Traits.terrainTraits));
                break;
            case AnimalStates.Hungry:
                FoodTraits searchTraits = new FoodTraits();

                if (TraitUtils.HasTrait(Traits.nutritionTraits, NutritionalTraits.Herbivore))
                    searchTraits |= FoodTraits.Plant;
                if (TraitUtils.HasTrait(Traits.nutritionTraits, NutritionalTraits.Carnivore))
                    searchTraits |= FoodTraits.Meat;
                if (TraitUtils.HasTrait(Traits.nutritionTraits, NutritionalTraits.Scavenger))
                    searchTraits |= FoodTraits.Fertilizer;

                Eat(Traits.terrainTraits, searchTraits, 10);

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
        GameTimeManager.Tick.AddListener(Lifecycle);
    }
}
