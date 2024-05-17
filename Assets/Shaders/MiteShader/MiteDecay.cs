using System.Collections;
using System.Collections.Generic;
using TerrariumTraits;
using UnityEngine;

public class MiteDecay : MonoBehaviour
{
    public Animal animal;

    public Shader miteShader;

    private List<Material> _validMaterials;

    private void Start()
    {
        _validMaterials = new List<Material>();


        Renderer[] tempRend = GetComponentsInChildren<Renderer>();

        animal = GetComponentInParent<Animal>();
        
        foreach (var x in tempRend)
        {
            if (x.material.shader == miteShader)
            {
                _validMaterials.Add(x.material);

                x.material.SetFloat("_MaxDecay", animal.maxDecay);
            }
        }
    }

    private void Update()
    {
        foreach(var x in _validMaterials)
        {
            x.SetFloat("_DecayValue", animal.CurrentDecay);
        }
    }
}
