using System.Collections;
using System.Collections.Generic;
using TerrariumTraits;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEditor.Progress;

public class CraftingSystem : MonoBehaviour
{
    [SerializeField] private CraftingSlot[] craftingSlot;
    public List<InventoryItemV2> itemsInCrafting;
    [SerializeField] private GameObject resultSlot;
    private InventoryManagerV2 inventoryManager;
    //[SerializeField] private GameObject itemPrefab;

    private void Awake()
    {
        inventoryManager = GetComponent<InventoryManagerV2>();
    }

    public void Craft()
    {
        if (itemsInCrafting.Count == 2)
        {
            // first combine traits of first and second and output result into resultSlot
            // then remove 1 from itemsInCrafting items
            // then remove 1 of the same type from inventory
            ItemData itemData1 = itemsInCrafting[0].itemData;
            ItemData itemData2 = itemsInCrafting[1].itemData;
            ItemData newItemData = itemData1; // the item data for the output item
            newItemData.traits += itemData2.traits;

            InventoryItemV2 outputItem = itemsInCrafting[0];
            outputItem.itemData = newItemData;

            for (int i = 0; i < itemsInCrafting.Count; i++)
            {
                Debug.Log("Foreach running on item " + itemsInCrafting[i] + " of type " + itemsInCrafting[i].itemData.displayName);
                itemsInCrafting[i].count--;
                inventoryManager.RemoveItem(itemsInCrafting[i].itemData, 1);
                itemsInCrafting[i].RefreshCount();
                if (itemsInCrafting[i].count == 0) // if out of item, remove it from crafting
                {
                    itemsInCrafting.Remove(itemsInCrafting[i]);
                    Destroy(itemsInCrafting[i].gameObject);
                }
            }

            if (resultSlot.GetComponentInChildren<InventoryItemV2>() != null) // if theres an item in the result slot
            {
                if (resultSlot.GetComponentInChildren<InventoryItemV2>().itemData != outputItem.itemData) // if the item in result slot is different from this one
                {
                    resultSlot.GetComponent<ResultSlot>().SendToInventory(); // send old item to inventory
                    GameObject prefabOutput = Instantiate(itemsInCrafting[0].gameObject, resultSlot.transform); // create new item gameobject
                    InventoryItemV2 prefabItem = prefabOutput.GetComponent<InventoryItemV2>();
                    prefabItem = outputItem;
                    Debug.Log("Item in result slot is different from one crafted, sent to inventory.");
                }
                else if (resultSlot.GetComponentInChildren<InventoryItemV2>().itemData == outputItem.itemData) // if the item in result slot is the same as this item
                {
                    resultSlot.GetComponentInChildren<InventoryItemV2>().count++; // add to the count
                    resultSlot.GetComponentInChildren<InventoryItemV2>().RefreshCount(); // refresh count
                    Debug.Log("Item in result slot is same as one crafted, count updated.");
                }
            }
            else // if theres no item in the result slot
            {
                GameObject prefabOutput = Instantiate(itemsInCrafting[0].gameObject, resultSlot.transform); // create new item gameobject
                InventoryItemV2 prefabItem = prefabOutput.GetComponent<InventoryItemV2>();
                prefabItem = outputItem;
                Debug.Log("No item in result slot, item instantiated.");
            }

            /*foreach (InventoryItemV2 item in itemsInCrafting) // updating counts
            {
                Debug.Log("Foreach running on item " + item);
                item.count--;
                inventoryManager.RemoveItem(item.itemData, 1);
                item.RefreshCount();
                if (item.count == 0) // if out of item, remove it from crafting
                {
                    itemsInCrafting.Remove(item);
                    Destroy(item.gameObject);
                }
            }*/

        }
        else
        {
            Debug.Log("Wrong amount of items in crafting somehow :/");
        }
    }
}
