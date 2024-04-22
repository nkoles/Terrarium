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
    using TraitType = UInt32;

    static public class TraitConstants
    {
        //Nutrition Traits
        public const TraitType NUTRITION_MASK = (uint) 0xFFFF;

        public const TraitType NUTRITION_HERBIVORE = (uint)1 << (int)NUTRITION_MASK;
        public const TraitType NUTRITION_CARNIVORE = (uint)2 << (int)NUTRITION_MASK;
        public const TraitType NUTRITION_SCAVENGER = (uint)3 << (int)NUTRITION_MASK;

        static public TraitType[] NUTRITION_TRAITS_ALL = { NUTRITION_CARNIVORE,  NUTRITION_SCAVENGER, NUTRITION_SCAVENGER };

        //Food Traits
        public const int FOOD_TRAIT_SHIFT = 8;
        public const TraitType FOOD_MASK = (uint) 0xFFFF << FOOD_TRAIT_SHIFT;

        public const TraitType FOOD_PLANT = (uint)1 << (int)FOOD_MASK;
        public const TraitType FOOD_MEAT = (uint)2 << (int)FOOD_MASK;
        public const TraitType FOOD_FERTILISER = (uint)3 << (int)FOOD_MASK;

        //TerrainTraits
        public const int TERRAIN_TRAIT_SHIFT = 16;
        public const TraitType TERRAIN_MASK = (uint) 0xFFFF << TERRAIN_TRAIT_SHIFT;

        public const TraitType TERRAIN_GROUND = (UInt32)1 << (int)TERRAIN_MASK;
        public const TraitType TERRAIN_WATER = (uint)2 << (int)TERRAIN_MASK;
        public const TraitType TERRAIN_AIR = (uint)3 << (int)TERRAIN_MASK;

        //MiscTraits
        public const int MISC_TRAIT_MASK = 24;
        public const TraitType MISC_TRAITS = (uint) 0xFFFF << MISC_TRAIT_MASK;

        public const TraitType MISC_PICKUPABLE = MISC_TRAITS << (1 + MISC_TRAIT_MASK);

        static public TraitType CreateTraitDataIndex(params TraitType[] traits)
        {
            TraitType result = new TraitType();

            for(int i =0; i < traits.Length; ++i)
            {
                result |= traits[i];
            }

            return result;
        }

        

        static public bool hasTrait(TraitType traitData, TraitType traitComparison)
        {
            return (traitData & traitComparison)!=0;

            //if ((traitData & traitComparison))
            //{
            //    return false;
            //}
            //else
            //{
            //    Debug.Log("Has Trait");
            //    return true;
            //}
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
