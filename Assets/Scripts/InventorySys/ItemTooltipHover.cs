using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemTooltipHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TooltipUI tooltip;
    private InventoryItemV2 parentItem;
    private ItemData itemData;
    public bool isOnItemPrefab;

    private void Awake()
    {
        tooltip = GameObject.FindGameObjectWithTag("Tooltip").GetComponent<TooltipUI>();
        if (transform.parent.GetComponent<InventoryItemV2>() != null)
        {
            parentItem = transform.parent.GetComponent<InventoryItemV2>();
            isOnItemPrefab = true;
        }
        else
        {
            isOnItemPrefab = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnPointerEnter");
        if (isOnItemPrefab)
        {
            itemData = parentItem.itemData;
            tooltip.ShowTooltip("Item Traits:\nNutrition: " + itemData.traits.nutritionTraits.ToString() + "\nFood: " + itemData.traits.foodTraits.ToString() + "\nTerrain: " + itemData.traits.terrainTraits.ToString() + "\nMisc: " + itemData.traits.miscTraits.ToString());
        }
        else
        {
            tooltip.ShowTooltip(eventData.pointerDrag.gameObject.name);
            tooltip.ShowTooltip(gameObject.name);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!eventData.fullyExited) { return; }
        Debug.Log("OnPointerExit");
        tooltip.HideTooltip();
    }

}
