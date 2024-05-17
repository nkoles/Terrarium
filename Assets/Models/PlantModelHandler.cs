using System;
using System.Collections;
using System.Collections.Generic;
using TerrariumTraits;
using UnityEngine;

public class PlantModelHandler : MonoBehaviour
{
    public MeshFilter mf;
    public Renderer renderer;
    
    public Mesh[] groundPlants;
    public Mesh[] waterPlants;

    private float _currentAge;
    private float _maxAge;

    public bool isWater;

    private Plant _plant;

    private void Start()
    {
        _plant = GetComponentInParent<Plant>();

        if (_plant.Traits.terrainTraits.HasFlag(TerrainTraits.Water)) isWater = true;
        else isWater = false;
        
        renderer.material.SetFloat("_MaxDecay", _plant.maxAge);
        
        GameTimeManager.Tick.AddListener(UpdatePlantAge);
    }

    public void UpdatePlantAge()
    {
        if (!_plant.IsDead)
        {
            _currentAge = _plant.CurrentAge;
            _maxAge = _plant.maxAge;


            if (_currentAge <= _maxAge / 8 && _plant.currentState == PlantStates.Sapling)
            {
                setModel(0);
            }
            else if (_currentAge <= _maxAge / 2)
            {
                setModel(1);
            }
            else
            {
                setModel(2);
            }
        }
        else
        {
            UpdatePlantDecay();
        }
    }

    public void UpdatePlantDecay()
    {
        renderer.material.SetFloat("_DecayValue", _plant.CurrentAge);
    }
    public void setModel(int i)
    {
        if (isWater) mf.mesh = waterPlants[i];
        else mf.mesh = groundPlants[i];
    }
}

