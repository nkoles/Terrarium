using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlotV2 : MonoBehaviour, IDropHandler, IPointerDownHandler
{
    public void OnDrop(PointerEventData eventData) // When a dragged item is released on this slot
    {
        if (transform.childCount == 0 && eventData.pointerDrag.GetComponent<InventoryItemV2>() != null) // make sure that we don't double stack inv items
        {
            InventoryItemV2 inventoryItem = eventData.pointerDrag.GetComponent<InventoryItemV2>();
            inventoryItem.parentAfterDrag = transform;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (GetComponentInChildren<InventoryItemV2>() != null)
        {
            InventoryItemV2 item = GetComponentInChildren<InventoryItemV2>();

            UIManager.instance.UIState = UIToolStates.UIInteractionType.PlaceItem;
            ToolManager.instance.currentItem = item;

            InvOpen.instance.OpenInv();
        }
    }
}
