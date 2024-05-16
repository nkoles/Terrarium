using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftingSlot : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    // This script should not actually change any of the inventory data directly - it should only make a duplicate of the item its given
    private GameObject currentItemObj;
    [SerializeField] private CraftingSystem craftingSystem;
    private InventoryItemV2 currentItem;
    public InventoryItemV2 otherItemLink;

    public void OnDrop(PointerEventData eventData)
    {
        craftingSystem.RemoveEmptyFromList();
        // make a duplicate of the inventoryItem dropped and display it here
        if (currentItemObj == null && eventData.pointerDrag.GetComponent<InventoryItemV2>() != null)
        {
            currentItemObj = Instantiate(eventData.pointerDrag.gameObject, transform);
            currentItem = currentItemObj.GetComponent<InventoryItemV2>();
            otherItemLink = eventData.pointerDrag.gameObject.GetComponent<InventoryItemV2>();
            craftingSystem.itemsInCrafting.Add(currentItem);
        }
        else if (currentItemObj != null && eventData.pointerDrag.GetComponent<InventoryItemV2>() != null)
        {
            Debug.Log("Crafting slot is occupied");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // delete the duplicate inventoryItem
        if (currentItemObj == null)
        {
            Debug.Log("No item to remove from crafting slot");
        }
        else
        {
            craftingSystem.itemsInCrafting.Remove(currentItem);
            Destroy(currentItemObj);
        }
    }
}
