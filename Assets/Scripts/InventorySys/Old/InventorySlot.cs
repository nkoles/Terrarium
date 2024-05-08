using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public Image icon;
    public TextMeshProUGUI labelText;
    public TextMeshProUGUI stackSizeText;
    public bool isOccupied;
    public bool isCraftingSlot;
    public DragAndDrop dragAndDrop;

    public void ClearSlot()
    {
        icon.enabled = false;
        labelText.enabled = false;
        stackSizeText.enabled = false;
        isOccupied = false;

        dragAndDrop.tempItem = null;
    }

    public void DrawSlot(InventoryItem item)
    {
        if (item == null)
        {
            ClearSlot();
            return;
        }
        icon.enabled = true;
        labelText.enabled = true;
        stackSizeText.enabled = true;
        isOccupied = true;

        icon.sprite = item.itemData.icon;
        labelText.text = item.itemData.displayName;
        stackSizeText.text = item.stackSize.ToString();
        dragAndDrop.tempItem = item;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        DragAndDrop otherDrop = eventData.pointerDrag.gameObject.GetComponent<DragAndDrop>();
        if (!isOccupied)
        {
            dragAndDrop.tempItem = otherDrop.tempItem;
            Debug.Log(gameObject + "'s DragAndDrop tempItem = " + dragAndDrop.tempItem);
            DrawSlot(dragAndDrop.tempItem);
            otherDrop.GetComponentInParent<InventorySlot>().ClearSlot();
        }
        otherDrop.transform.position = otherDrop.startingPos;
    }
}
