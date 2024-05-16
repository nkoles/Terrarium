using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Destroy : MonoBehaviour, IDropHandler
{
    //[SerializeField] private InventoryManagerV2 inventoryManager;
    [SerializeField] private CraftingSlot[] craftingSlots;

    public void OnDrop(PointerEventData eventData)
    {
        InventoryItemV2 itemToDestroy = eventData.pointerDrag.GetComponent<InventoryItemV2>();
        if (itemToDestroy != null)
        {
            Debug.Log("bruh 1");
            for (int i = 0; i < 2; i++)
            {
                InventoryItemV2 temp = craftingSlots[i].GetComponentInChildren<InventoryItemV2>();
                if (temp != null && temp.itemData == itemToDestroy.itemData)
                {
                    Debug.Log("object in crafting slots wowee");
                    Destroy(temp.gameObject);
                    i = 2;
                }
            }
            Destroy(itemToDestroy.gameObject);
       }
    }
}
