using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TerrariumTraits
{
    [Flags]
    public enum NutritionalTraits
    {
        None = 0,
        Herbivore = 1,
        Carnivore = 2,
        Scavenger = 4,
        Everything = 8
    }

    [Flags]
    public enum FoodTraits
    {
        None = 0,
        Plant = 1,
        Meat = 2,
        Fertilizer = 4,
        Everything = 8
    }


    [Flags]
    public enum TerrainTraits
    {
        None = 0,
        Ground = 1,
        Water = 2,
        Air = 4,
        Everything = 8
    }

    [Flags]
    public enum MiscTraits
    {
        None = 0,
        Pickupable = 1,
        Everything
    }

    static public class TraitUtils
    {
        static public void AddTrait<T>(T traitData, params T[] traitFlags) where T: Enum
        {
            foreach (var t in traitFlags)
            {
                int traitDataValue = (int)(object)traitData;
                int traitFlagValue = (int)(object)t;

                traitData = (T)(object)(traitDataValue | traitFlagValue);

                //Debug.Log("Added Trait: " + Enum.GetName(typeof(T), traitFlagValue));
            }
        }

        static public void RemoveTrait<T>(T traitData, params T[] traitFlags) where T: Enum
        {
            foreach (var t in traitFlags)
            {
                int traitDataValue = (int)(object)traitData;
                int traitFlagValue = (int)(object)t;

                traitData = (T)(object)(traitDataValue & (~traitFlagValue));

                //Debug.Log("Removed Trait: " + Enum.GetName(typeof(T), traitFlagValue));
            }
        }

        static public bool HasTrait<T>(T traitData, T traitFlags) where T: Enum
        {
            int traitDataValue = (int)(object)traitData;
            int traitFlagValue = (int)(object)traitFlags;

            if ((traitDataValue & traitFlagValue) != 0)
            {
                //Debug.Log("Does have trait: " + Enum.GetName(typeof(T), traitFlagValue));

                return true;
            }

            //Debug.Log("Does not have trait: " + Enum.GetName(typeof(T), traitFlagValue));

            return false;
        }
    }

    [Serializable]
    public class TraitData
    {
        public NutritionalTraits nutritionTraits;
        public FoodTraits foodTraits;
        public TerrainTraits terrainTraits;
        public MiscTraits miscTraits;

        public TEnum GetTraitFlags<TEnum>() where TEnum : Enum
        {
            object result = null;

            if (typeof(TEnum) == typeof(NutritionalTraits))
                result = nutritionTraits;
            else if (typeof(TEnum) == typeof(FoodTraits))
                result = foodTraits;
            else if (typeof(TEnum) == typeof(TerrainTraits))
                result = terrainTraits;
            else if (typeof (TEnum) == typeof(MiscTraits))
                result = miscTraits;

            return (TEnum)result;
        }

        public TraitData(NutritionalTraits nTraits, FoodTraits fTraits, TerrainTraits tTraits, MiscTraits mTraits)
        {
            this.nutritionTraits = nTraits;
            this.foodTraits = fTraits;
            this.terrainTraits = tTraits;
            this.miscTraits = mTraits;
        }

        public TraitData(TerrainTraits tTraits)
        {
            this.nutritionTraits = NutritionalTraits.None;
            this.foodTraits = FoodTraits.None;
            this.terrainTraits = tTraits;
            this.miscTraits = MiscTraits.None;
        }
    }
}
