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

    public string targetName;

    static public int totalAnimalCount;

    public String TargetName
    {
        get
        {
            if(target != null)
            {
                return target.SelfObject.name;
            }

            return "";
        }
    }

    public AnimalStates currentState;

    public bool isInitialised = false;

    public void Initialise()
    {
        TraitUtils.AddTrait<MiscTraits>(ref Traits.miscTraits, MiscTraits.Pickupable);

        if((int)UnityEngine.Random.Range(0, 2) % 2 == 0)
        {
            TraitUtils.AddTrait<MiscTraits>(ref Traits.miscTraits, MiscTraits.Gender);
        }

        _self = gameObject;

        maxAge = UnityEngine.Random.Range(300, 600);

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
    }

    public void Lifecycle()
    {
        if (isInitialised)
            Age();

        //if(target != null)
        //    targetName = TargetName;

        Evolve();

        switch (currentState){
            case AnimalStates.Idle:
                if (CurrentAge % 40 == 0)
                {
                    target = null;
                    currentState = AnimalStates.Hungry;
                }


                Move(availableTiles.ToArray(), 50);
                break;
            case AnimalStates.Hungry:
                List<FoodTraits> searchTraits = new List<FoodTraits>();

                if (TraitUtils.HasTrait(Traits.nutritionTraits, NutritionalTraits.Herbivore))
                    searchTraits.Add(FoodTraits.Plant);
                if (TraitUtils.HasTrait(Traits.nutritionTraits, NutritionalTraits.Carnivore))
                    searchTraits.Add(FoodTraits.Meat);
                if (TraitUtils.HasTrait(Traits.nutritionTraits, NutritionalTraits.Scavenger))
                    searchTraits.Add(FoodTraits.Fertilizer);

                if(AgeVariable < maxVariable)
                {
                    AgeVariable++;
                } else
                {
                    AgeVariable = 0;
                    target = null;
                    currentState = AnimalStates.Decomposing;
                }

                bool isFed = Eat(Traits, searchTraits.ToArray(), 10);

                if (isFed)
                {
                    AgeVariable = 0;
                    Evolve();
                    target = null;
                    currentState = AnimalStates.Horny;
                }

                break;
            case AnimalStates.Horny:
                if(AgeVariable < maxVariable/2)
                {
                    AgeVariable++;

                } else
                {
                    AgeVariable = 0;
                    target = null;
                    currentState = AnimalStates.Idle;
                }

                bool isBred = Breed(Traits, 1, 10);

                if (isBred)
                {
                    AgeVariable = 0;
                    target = null;
                    currentState = AnimalStates.Idle;
                }

                break;
            case AnimalStates.Decomposing:
                IsDead = true;
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
        _self = gameObject;
        selfTraits = Traits;
        _lastPosition = transform.position;
        GameTimeManager.PreTick.AddListener(OnPreTick);
        terrainGrid = FindObjectOfType<Grid>();
        GameTimeManager.Tick.AddListener(Lifecycle);

        Initialise();
    }

    private void OnDestroy()
    {
        AnimalFactory.instance.currentAnimalCount--;
    }
}
