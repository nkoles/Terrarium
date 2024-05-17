using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainShaderHandler : MonoBehaviour
{
    public Renderer rend;

    private TerrariumTerrain _terrariumTerrain;

    private float _fertilty;
    private bool _bloody;

    public void Start()
    {
        _terrariumTerrain = GetComponentInParent<TerrariumTerrain>();
    }

    public void UpdateShader()
    {
        _fertilty = _terrariumTerrain.fertility;
        _bloody = _terrariumTerrain.isBloody;
        
        rend.material.SetFloat("_Fertility", _fertilty);
        
        if (_bloody) rend.material.SetFloat("_Bloody", 1);
        else rend.material.SetFloat("_Bloody", 0);
    }

    public void Update()
    {
        UpdateShader();
    }
}
