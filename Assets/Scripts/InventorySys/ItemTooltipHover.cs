using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTooltipHover : MonoBehaviour
{
    private TooltipUI tooltip;
    private ItemData itemData;

    private void Awake()
    {
        tooltip = GameObject.FindGameObjectWithTag("Tooltip").GetComponent<TooltipUI>();
        itemData = GetComponentInParent<ItemData>();
    }

    void OnMouseEnter()
    {
        Debug.Log("OnMouseOver");
        tooltip.ShowTooltip("Item Traits:\nNutrition: " + itemData.traits.nutritionTraits.ToString() + "\nFood: " + itemData.traits.foodTraits.ToString() + "\nTerrain: " + itemData.traits.terrainTraits.ToString() + "\nMisc: " + itemData.traits.miscTraits.ToString());
    }

    void OnMouseExit()
    {
        Debug.Log("OnMouseExit");
        tooltip.HideTooltip();
    }
}
