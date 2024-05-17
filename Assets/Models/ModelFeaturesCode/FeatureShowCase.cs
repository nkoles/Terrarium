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

    //USING FOR DEBUG MAKE SURE TO DELETE AFTER!!!!!
    private void Update()
    {
        //UpdateFeatures();
    }

    public void UpdateFeatures()
    {
        currentTraitData = GetComponentInParent<Animal>().Traits;

        traitModels[0].SetActive(currentTraitData.terrainTraits.HasFlag(TerrainTraits.Water));
        traitModels[1].SetActive(currentTraitData.nutritionTraits.HasFlag(NutritionalTraits.Scavenger));
        traitModels[2].SetActive(currentTraitData.foodTraits.HasFlag(FoodTraits.Fertilizer));
        traitModels[3].SetActive(currentTraitData.nutritionTraits.HasFlag(NutritionalTraits.Carnivore));
        traitModels[4].SetActive(currentTraitData.nutritionTraits.HasFlag(NutritionalTraits.Herbivore));
    }
    
}
