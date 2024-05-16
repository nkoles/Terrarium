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
}