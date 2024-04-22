using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum NutritionTraits
{
    Herbivore,
    Carnivore,
    Scavenger
}

[Serializable]
public enum FoodTraits
{
    Plant,
    Meat,
    Fertilizer
}

[Serializable]
public enum TerrainTraits
{
    Ground,
    Water,
    Air
}

[Serializable]
public enum MiscTraits
{
    Pickupable
}

namespace TerrariumTraits
{
    using TraitType = Int32;

    static public class TraitConstants
    {
        //Nutrition Traits
        public const TraitType NUTRITION_SHIFT = 0;
        public const TraitType NUTRITION_MASK = 0xFF << NUTRITION_SHIFT;

        public const TraitType NUTRITION_HERBIVORE = (int)1 << NUTRITION_MASK;
        public const TraitType NUTRITION_CARNIVORE = (int)2 << NUTRITION_MASK;
        public const TraitType NUTRITION_SCAVENGER = (int)3 << NUTRITION_MASK;

        public const TraitType NUTRITION_TRAITS_ALL = NUTRITION_CARNIVORE | NUTRITION_SCAVENGER | NUTRITION_SCAVENGER;

        //Food Traits
        public const int FOOD_TRAIT_SHIFT = 8;
        public const TraitType FOOD_MASK = 0xFF << FOOD_TRAIT_SHIFT;

        public const TraitType FOOD_PLANT = (int)1 << FOOD_MASK;
        public const TraitType FOOD_MEAT = (int)2 << FOOD_MASK;
        public const TraitType FOOD_FERTILISER = (int)3 << FOOD_MASK;

        public const TraitType FOOD_TRAITS_ALL = FOOD_PLANT | FOOD_MEAT | FOOD_FERTILISER;

        //TerrainTraits
        public const int TERRAIN_TRAIT_SHIFT = 16;
        public const TraitType TERRAIN_MASK = 0xFF << TERRAIN_TRAIT_SHIFT;

        public const TraitType TERRAIN_GROUND = (int)1 << TERRAIN_MASK;
        public const TraitType TERRAIN_WATER = (int)2 << TERRAIN_MASK;
        public const TraitType TERRAIN_AIR = (int)3 << TERRAIN_MASK;

        //MiscTraits
        public const int MISC_TRAIT_MASK = 24;
        public const TraitType MISC_TRAITS = 0xFF << MISC_TRAIT_MASK;

        public const TraitType MISC_PICKUPABLE = (int)1 << MISC_TRAITS;

        static public TraitType CreateTraitDataIndex(params TraitType[] traits)
        {
            TraitType result = new TraitType();

            foreach(TraitType t in traits)
            {
                AddTrait(result, t);
            }

            return result;
        }

        static public TraitType AddTrait(TraitType traitData, params TraitType[] traitToAdd)
        {
            foreach(TraitType t in traitToAdd)
            {
                traitData |= t;
            }

            return traitData;
        }

        static public TraitType RemoveTrait(TraitType traitData, params TraitType[] traitsToRemove)
        {
            TraitType removalTraits = CreateTraitDataIndex(traitsToRemove);

            traitData &= ~removalTraits;

            return traitData;
        }

        static public bool HasTrait(TraitType traitData, params TraitType[] traitComparisons)
        {
            for(int i = 0; i < traitComparisons.Length; ++i)
            {
                if((traitData & traitComparisons[i]) != 0)
                {
                    Debug.Log("Containts traits");

                    if (i ==traitComparisons.Length-1)
                    {

                        return true;
                    }
                }
            }

            Debug.Log("Doesn't contain the traits");

            return false;
        }
    }
}

[Serializable]
public struct TraitData
{
    public List<FoodTraits> FoodTraits;
    public List<NutritionTraits> NutritionTraits;
    public List<TerrainTraits> TerrainTraits;
    public List<MiscTraits> MiscTraits;

    //public void AddTrait<T>(T trait)
    //    where T : System.Enum
    //{
    //    switch (System.Enum.GetUnderlyingType(typeof(T))
    //    {
    //        case (FoodTraits):
    //            break;
    //    }
    //}

    //public bool HasDuplicateTrait<T>(T trait)
    //{

    //}

    public TraitData CombineTraitData(ref TraitData traits1, ref TraitData traits2)
    {
        TraitData CombinedTraits = new TraitData();

        return CombinedTraits;
    }
}
