using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TerrariumTraits;

public class FeatureShowCase : MonoBehaviour
{
    public TraitData currentTraitData;

    public GameObject[] traitModels;

    private void Awake()
    {
        GameTimeManager.Tick.AddListener(UpdateFeatures);
    }

    public void UpdateFeatures()
    {
        traitModels[0].SetActive(currentTraitData.nutritionTraits.HasFlag(NutritionalTraits.Carnivore));
        traitModels[1].SetActive(currentTraitData.nutritionTraits.HasFlag(NutritionalTraits.Herbivore));
    }
    
}
