using System.Collections;
using System.Collections.Generic;
using TerrariumTraits;
using UnityEngine;

public enum PlantStates
{
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

    public void Initialise()
    {

    }

    public void Age()
    {
    }

    public void Lifecycle()
    {
    }

    public void Awake()
    {
        _self = gameObject;
        GameTimeManager.Tick.AddListener(Lifecycle);
    }
}
