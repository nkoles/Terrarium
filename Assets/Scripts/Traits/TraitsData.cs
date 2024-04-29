using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[Serializable]
//public enum NutritionTraits
//{
//    Herbivore,
//    Carnivore,
//    Scavenger
//}

//[Serializable]
//public enum FoodTraits
//{
//    Plant,
//    Meat,
//    Fertilizer
//}

//[Serializable]
//public enum TerrainTraits
//{
//    Ground,
//    Water,
//    Air
//}

//[Serializable]
//public enum MiscTraits
//{
//    Pickupable
//}

namespace TerrariumTraits
{
    using TraitType = Int32;

    static public class TraitConstants
    {
        //Nutrition Traits
        public const TraitType NUTRITION_SHIFT = 0;
        public const TraitType NUTRITION_MASK = 0xF << NUTRITION_SHIFT;

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

        public const TraitType TERRAIN_TRAITS_ALL = TERRAIN_GROUND | TERRAIN_WATER | TERRAIN_AIR;

        //MiscTraits
        public const int MISC_TRAIT_SHIFT = 24;
        public const TraitType MISC_MASK = 0xFF << MISC_TRAIT_SHIFT;

        public const TraitType MISC_PICKUPABLE = (int)1 << MISC_MASK;

        public const TraitType MISC_TRAITS_ALL = MISC_PICKUPABLE;

        public const TraitType ALL_TRAITS = NUTRITION_TRAITS_ALL | FOOD_TRAITS_ALL | TERRAIN_TRAITS_ALL | MISC_TRAITS_ALL;

        static public TraitType[] GenerateFlagArray(int end, TraitType mask)
        {
            TraitType[] results = new TraitType[end];

            for (int i = 1; i <= end; ++i)
            {
                results[i-1] = (int)i << mask;
            }

            return results;
        }

        static public TraitType CreateTraitData(params TraitType[] traits)
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
            TraitType removalTraits = CreateTraitData(traitsToRemove);

            traitData &= ~removalTraits;

            return traitData;
        }

        static public bool HasTrait(TraitType traitData, params TraitType[] traitComparisons)
        {
            for(int i = 0; i < traitComparisons.Length; ++i)
            {
                if((traitData & traitComparisons[i]) != 0)
                {
                    //Debug.Log("Containts traits");

                    if (i ==traitComparisons.Length-1)
                    {

                        return true;
                    }
                }
            }

            Debug.Log("Doesn't contain the traits");

            return false;
        }

        static public TraitType[] ReturnTraits(TraitType traitData, TraitType traitCategory)
        {
            List<TraitType> results = new List<TraitType>();
            TraitType[] comparison = new TraitType[0];

            switch (traitCategory)
            {
                case NUTRITION_TRAITS_ALL:
                    comparison = GenerateFlagArray(3, NUTRITION_MASK);
                    break;
                case FOOD_TRAITS_ALL:
                    comparison = GenerateFlagArray(3, FOOD_MASK);
                    break;

                case MISC_TRAITS_ALL:
                    comparison = GenerateFlagArray(1, MISC_MASK);
                    break;
            }

            foreach(var trait in comparison)
            {
                if(HasTrait(traitData, trait))
                {
                    results.Add(trait);
                }
            }

            return results.ToArray();
        }
    }
}

//public class Trait : ScriptableObject
//{
//    public string traitName;
//    public int traitID;
//}

//[Serializable]
//public struct TraitData
//{
//    public 
//}
