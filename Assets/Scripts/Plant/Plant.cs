using System.Collections;
using System.Collections.Generic;
using TerrariumTraits;
using UnityEngine;

public enum PlantStates
{
    Sapling,
    Grow,
    Bloom,
    Wither
}

public class Plant : PlantAI, ITerrariumProduct
{
    public int maxAge;
    [SerializeField]
    private int _currentAge;
    public int CurrentAge { get { return _currentAge; } set { _currentAge = value; } }

    public int bloomTime;
    [SerializeField]
    private int _currentBloom;
    public int AgeVariable { get { return _currentBloom; } set { _currentBloom = value; } }

    public int maxDecay;
    [SerializeField]
    private int _currentDecay;
    public int CurrentDecay { get { return _currentDecay; } set { _currentDecay = value; } }

    [SerializeField]
    private bool _isBaby;
    public bool IsBaby { get { return _isBaby; } set { _isBaby = value; } }

    [SerializeField]
    private bool _isDead;
    public bool IsDead { get { return _isDead; } set { _isDead = value; } }

    [SerializeField]
    private GameObject _self;
    public GameObject SelfObject { get { return _self; } }

    [SerializeField]
    private TraitData _traitData;
    public TraitData Traits { get { return _traitData; } set { _traitData = value; } }

    public PlantStates currentState = PlantStates.Sapling;

    public bool isBlooming = false;

    public void Initialise()
    {
        currentState = PlantStates.Sapling;

        maxAge = 180;
        maxDecay = 90;
        bloomTime = 20;

        _currentBloom = 0;
        _currentDecay = 0;
        _isBaby = true;
        TraitUtils.AddTrait<MiscTraits>(ref Traits.miscTraits, MiscTraits.Pickupable);

        _self = gameObject;

        isBlooming = false;

        Evolve();



    }

    public void Age()
    {
        if (!IsDead && CurrentAge < maxAge)
        {
            if (CurrentAge > (int)(maxAge / 8))
            {
                TraitUtils.RemoveTrait<MiscTraits>(ref Traits.miscTraits, MiscTraits.Pickupable);
                IsBaby = false;
            }

            CurrentAge++;
        }
        else
        {
            TraitUtils.AddTrait(ref Traits.foodTraits, FoodTraits.Fertilizer);
            currentState = PlantStates.Wither;
            IsDead = true;
        }
    }

    public void Evolve()
    {

    }

    public void Lifecycle()
    {
        Age();

        CheckForCorrectGround(Traits);

        Evolve();

        if(targetTerrain == null || targetTerrain.fertility <= 0.125)
        {
            if(currentState == PlantStates.Sapling)
            {
                Destroy(gameObject);
            }else if (currentState == PlantStates.Grow)
            {
                IsDead = true;
                currentState = PlantStates.Wither;
            }
        }

        switch (currentState)
        {
            case PlantStates.Sapling:
                if(CurrentAge > (int)maxAge / 8)
                    currentState = PlantStates.Grow;

                if (targetTerrain.isBloody)
                    Traits.foodTraits |= FoodTraits.Meat;
                break;
            case PlantStates.Grow:
                if(CurrentAge % 20 == 0)
                {
                    if(AgeVariable >= bloomTime)
                    {
                        if (!isBlooming)
                            currentState = PlantStates.Bloom;
                        else
                        {
                            TraitUtils.AddTrait(ref Traits.foodTraits, FoodTraits.Fertilizer);
                            IsDead = true;
                            currentState = PlantStates.Wither;
                        }

                        AgeVariable = 0;
                    }
                } else
                {
                    AgeVariable++;
                }

                break;
            case PlantStates.Bloom:
                if(!isBlooming || AgeVariable < bloomTime)
                {
                    isBlooming = Bloom(targetTerrain.fertility, Traits);

                    if (isBlooming)
                    {
                        AgeVariable = 0;

                        currentState = PlantStates.Grow;
                    }
                    else
                        AgeVariable++;
                } else
                {
                    AgeVariable = 0;

                    currentState = PlantStates.Grow;
                }

                break;
            case PlantStates.Wither:
                CurrentDecay++;

                if(CurrentDecay > maxDecay)
                {
                    Destroy(gameObject);
                }

                break;
        }
    }

    public void Awake()
    {
        _self = gameObject;
        GameTimeManager.Tick.AddListener(Lifecycle);

        Initialise();
    }
}
