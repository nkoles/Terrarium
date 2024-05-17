using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantModelHandler : MonoBehaviour
{
    public MeshFilter mf;
    public Renderer renderer;
    
    public Mesh[] groundPlants;
    public Mesh[] waterPlants;

    private float _currentAge;
    private float _maxAge;

    private Plant _plant;

    private void Start()
    {
        _plant = GetComponentInParent<Plant>();
        
    }

    public void UpdatePlantAge()
    {
        _currentAge = _plant.CurrentAge;
        _maxAge = _plant.maxAge;

        if (_currentAge <= _maxAge)
        {
            
        }
    }
}

