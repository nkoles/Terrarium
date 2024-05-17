using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemTooltipHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TooltipUI tooltip;
    private InventoryItemV2 parentItem;
    private ItemData itemData;

    private void Awake()
    {
        tooltip = GameObject.FindGameObjectWithTag("Tooltip").GetComponent<TooltipUI>();
        parentItem = transform.parent.GetComponent<InventoryItemV2>();
        //itemData = parentItem.itemData;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnPointerEnter");
        itemData = parentItem.itemData;
        tooltip.ShowTooltip("Item Traits:\nNutrition: " + itemData.traits.nutritionTraits.ToString() + "\nFood: " + itemData.traits.foodTraits.ToString() + "\nTerrain: " + itemData.traits.terrainTraits.ToString() + "\nMisc: " + itemData.traits.miscTraits.ToString());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!eventData.fullyExited) { return; }
        Debug.Log("OnPointerExit");
        tooltip.HideTooltip();
    }

}
