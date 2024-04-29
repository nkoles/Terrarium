using System.Collections;
using System.Collections.Generic;
using TerrariumTraits;
using UnityEngine;

public class Plant : PlantAI, ITerrariumProduct
{
    [SerializeField]
    private TraitData _traitData;
    public TraitData Traits { get { return _traitData; } set { _traitData = value; } }

    private Vector3 _position;
    public Vector3 PositionalData { get { return _position; } set { _position = value; } }

    private bool _isDead;
    public bool IsDead {  get { return _isDead; } set { _isDead = value; } }

    public void Initialise()
    {

    }

    public void Age()
    {
        print(PositionalData);
        PositionalData = transform.position;
    }

    public void Lifecycle()
    {
    }

    public void Awake()
    {
        GameTimeManager.PreTick.AddListener(Age);
        GameTimeManager.Tick.AddListener(Lifecycle);
    }
}
