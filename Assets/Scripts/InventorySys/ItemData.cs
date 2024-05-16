using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TerrariumTraits;
using UnityEditor;
using UnityEngine;

public enum ItemType
{
    Seed,
    LiquidWater,
    LiquidBlood,
    Corpse,
    Egg
}

[CreateAssetMenu(menuName = "ScriptableObjects/ItemDataType")]
public class ItemData : ScriptableObject
{
    public ItemType itemType;
    public string displayName;
    public Sprite icon;
    public bool isStackable = true; // just in case we want items that dont stack
    public TraitData traits;
    // OTHER ITEM VARIABLES HERE

    public void InitialiseNewType(ItemType itemType, string displayName, Sprite icon, bool isStackable, TraitData traits)
    {
        this.itemType = itemType;
        this.displayName = displayName;
        this.icon = icon;
        this.isStackable = isStackable;
        this.traits = traits;
    }

    public static ItemData CreateInstance(ItemType itemType, string displayName, Sprite icon, bool isStackable, TraitData traits)
    {
        Debug.Log("Creating new data type! Yippeeee");
        var data = CreateInstance<ItemData>();
        data.InitialiseNewType(itemType, displayName, icon, isStackable, traits);
        return data;
    }
}