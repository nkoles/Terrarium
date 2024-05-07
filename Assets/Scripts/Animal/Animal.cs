using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TerrariumTraits;
using System;
using System.Linq;
using System.Runtime.CompilerServices;

public enum AnimalStates
{
    Idle,
    Hungry,
    Horny,
    Decomposing
}

public class Animal : AnimalAI, ITerrariumProduct
{
    [Header("Animal General Fields")]

    public int maxAge;
    [SerializeField]
    private int _currentAge;
    public int CurrentAge {  get { return _currentAge; } set { _currentAge = value; } }

    public int maxVariable;
    [SerializeField]
    private int _currentVariable;
    public int AgeVariable { get { return _currentVariable; } set {  _currentVariable = value; } }

    public int maxDecay;
    [SerializeField]
    private int _currentDecay;
    public int CurrentDecay { get { return _currentDecay; } set { _currentDecay = value; } }

    [SerializeField]
    private bool _isBaby;
    [SerializeField]
    private bool _isDead;

    public bool IsBaby {  get { return _isBaby; } set { _isBaby = value; } }
    public bool IsDead { get { return _isDead; } set { _isDead = value; } }

    [SerializeField]
    private GameObject _self;
    public GameObject SelfObject { get { return _self; } }

    [SerializeField]
    private TraitData _traitData;
    public TraitData Traits { get { return _traitData; } set { _traitData = value; } }

    public AnimalStates currentState;

    public bool isInitialised = false;

    public Material herb, carn;

    public void Initialise()
    {
        TraitUtils.AddTrait<MiscTraits>(ref Traits.miscTraits, MiscTraits.Pickupable);

        _self = gameObject;

        maxAge = UnityEngine.Random.Range(180, 241);
        maxVariable = 40;

        IsBaby = true;
        currentState = AnimalStates.Idle;

        isInitialised = true;
    }
    
    public void Age()
    {
        if (!IsDead && CurrentAge < maxAge)
        {
            if (CurrentAge > (int)(maxAge / 4))
            {
                TraitUtils.RemoveTrait<MiscTraits>(ref Traits.miscTraits, MiscTraits.Pickupable);

                IsBaby = false;
            }

            CurrentAge++;
        }
        else
        {
            currentState = AnimalStates.Decomposing;
            IsDead = true;
        }
    }

    public void Evolve()
    {
        GetComponent<Renderer>().material = herb;

        if (TraitUtils.HasTrait<NutritionalTraits>(Traits.nutritionTraits, NutritionalTraits.Carnivore))
        {
            GetComponent<Renderer>().material = carn;
        }
    }

    public void Lifecycle()
    {
        if (target != null)
        {
            if(target.SelfObject != null)
                print(target.SelfObject.name);
        }

        if (isInitialised)
            Age();

        switch (currentState){
            case AnimalStates.Idle:
                if (CurrentAge % 20 == 0)
                    currentState = AnimalStates.Hungry;

                Move(AvailableTiles(Traits), 50);
                break;
            case AnimalStates.Hungry:
                FoodTraits searchTraits = new FoodTraits();

                if (TraitUtils.HasTrait(Traits.nutritionTraits, NutritionalTraits.Herbivore))
                    searchTraits |= FoodTraits.Plant;
                if (TraitUtils.HasTrait(Traits.nutritionTraits, NutritionalTraits.Carnivore))
                    searchTraits |= FoodTraits.Meat;
                if (TraitUtils.HasTrait(Traits.nutritionTraits, NutritionalTraits.Scavenger))
                    searchTraits |= FoodTraits.Fertilizer;

                if(AgeVariable < maxVariable)
                {
                    AgeVariable++;
                } else
                {
                    AgeVariable = 0;
                    currentState = AnimalStates.Decomposing;
                }

                bool isFed = Eat(Traits, searchTraits, 10);

                if (isFed)
                {
                    AgeVariable = 0;
                    Evolve();
                    currentState = AnimalStates.Horny;
                }

                break;
            case AnimalStates.Horny:
                if(AgeVariable < maxVariable)
                {
                    AgeVariable++;

                } else
                {
                    currentState = AnimalStates.Idle;
                }

                bool isBred = Breed(Traits, 1, 10);

                if (isBred)
                {
                    AgeVariable = 0;
                    currentState = AnimalStates.Idle;
                }

                break;
            case AnimalStates.Decomposing:
                TraitUtils.AddTrait<MiscTraits>(ref Traits.miscTraits, MiscTraits.Pickupable);
                Traits.foodTraits = FoodTraits.Fertilizer;

                if (CurrentDecay < maxDecay)
                {
                    CurrentDecay++;
                }
                else
                    Destroy(gameObject);

                break;
            default:
                break;
        }
    }

    public void Awake()
    {
        terrainGrid = FindObjectOfType<Grid>();
        Initialise();
        GameTimeManager.Tick.AddListener(Lifecycle);
    }
}
