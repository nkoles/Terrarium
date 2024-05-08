using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public enum ItemType
{
    Type0,
    Type1,
    Type2
}

[CreateAssetMenu(menuName = "ScriptableObjects/ItemDataType")]
public class ItemData : ScriptableObject
{
    public ItemType itemType;
    public string displayName;
    public Sprite icon;
    public bool isStackable = true; // just in case we want items that dont stack
    // OTHER ITEM VARIABLES HERE
}