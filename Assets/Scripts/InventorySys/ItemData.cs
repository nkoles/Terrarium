using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public enum ItemType
{
    Type0,
    Type1
}

[CreateAssetMenu]
[CanEditMultipleObjects]
public class ItemData : ScriptableObject
{
    public ItemType itemType;
    public string displayName;
    public Sprite icon;
    // OTHER ITEM VARIABLES HERE
}