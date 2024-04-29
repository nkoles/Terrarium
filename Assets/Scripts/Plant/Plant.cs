using System.Collections;
using System.Collections.Generic;
using TerrariumTraits;
using UnityEngine;

public class Plant : PlantAI, ITerrariumProduct
{
    private int _traitData;
    public int TraitData { get { return _traitData; } set { _traitData = value; } }

    private Transform _transform;
    public Transform PositionalData { get { return _transform; } set { _transform = value; } }

    public void Initialise()
    {

    }

    public void Age()
    {

    }

    public void Lifecycle()
    {
        Age();

        PositionalData = transform;
    }

    public void Awake()
    {
        TraitData = TraitConstants.FOOD_PLANT;
        GameTimeManager.Tick.AddListener(Lifecycle);
    }
}
