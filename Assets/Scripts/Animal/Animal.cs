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
    public bool isDead;

    public int maxHunger = 20;
    public int currentHunger = 0;

    [SerializeField]
    private int _traitData;
    public int TraitData { get { return _traitData; } set { _traitData = value; } }

    [SerializeField]
    private Transform _transform;
    public Transform PositionalData { get { return _transform; } set { _transform = value; } }

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

        print(target);
    }

    public void Lifecycle()
    {
        if (isInitialised)
            Age();

        PositionalData = transform;

        switch (currentState){
            case AnimalStates.Idle:
                if (currentHunger > maxHunger / 2)
                {
                    currentState = AnimalStates.Hungry;
                }

                Move(AvailableTiles(TraitConstants.TERRAIN_GROUND));
                break;
            case AnimalStates.Hungry:
                int[] nutritionalTraits = TraitConstants.ReturnTraits(TraitData, TraitConstants.NUTRITION_TRAITS_ALL);
                List<int> searchTraits = new List<int>();

                foreach(var trait in nutritionalTraits)
                {
                    switch (trait)
                    {
                        case TraitConstants.NUTRITION_HERBIVORE:
                            searchTraits.Add(TraitConstants.FOOD_PLANT);
                            break;
                        case TraitConstants.NUTRITION_CARNIVORE:
                            searchTraits.Add(TraitConstants.FOOD_MEAT);
                            break;
                        case TraitConstants.NUTRITION_SCAVENGER:
                            searchTraits.Add(TraitConstants.FOOD_FERTILISER);
                            break;
                    }
                }

                Eat(searchTraits.ToArray(), TraitConstants.TERRAIN_GROUND, 10);
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
        TraitData = TraitConstants.CreateTraitData(TraitConstants.TERRAIN_GROUND, TraitConstants.NUTRITION_HERBIVORE);
        GameTimeManager.Tick.AddListener(Lifecycle);
    }
}
