using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FoodAttributes
{
    Meat,
    Plant,
    Fertilizer
}

public enum NutritionAttributes
{
    Herbivore,
    Carnivore,
    Scavenger
}

public enum TerrainAttributes
{
    Ground,
    Water,
    Air
}

public enum TechnicalAttributes
{
    Killable,
    Pickupable
}

[Serializable]
public struct AttributeData
{
    public List<FoodAttributes> foodAttributes;
    public List<NutritionAttributes> nutritionAttributes;
    public List<TerrainAttributes> terrainAttributes;
    public List<TechnicalAttributes> technicalAttributes;
}