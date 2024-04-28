using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public enum ItemType
{
    Type0,
    Type1
}

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public ItemType itemType;
    [HideInInspector] public string displayName;
    public Sprite icon;
    // OTHER ITEM VARIABLES HERE

    private void OnEnable()
    {
        displayName = itemType.ToString();
    }

    private void OnDisable()
    {
        displayName = null;
    }
}
