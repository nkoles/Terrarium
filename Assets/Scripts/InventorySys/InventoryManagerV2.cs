using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManagerV2 : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private int maxStack;
    [Header("Inventory Slots")]
    public InventorySlotV2[] inventorySlots;
    [Header("Item Types")]
    public ItemData[] itemTypes;
    [Header("Other")]
    public GameObject inventoryItemV2Prefab;
    [SerializeField] private bool debugMode;

    private void Update()
    {
        if (debugMode)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) { AddItem(itemTypes[0]); }
            else if (Input.GetKeyDown(KeyCode.Alpha2)) { AddItem(itemTypes[1]); }
            else if (Input.GetKeyDown(KeyCode.Alpha3)) { AddItem(itemTypes[2]); }
        }
    }

    public void AddItem(ItemData item)
    {
        // Check if we can stack anywhere in the inventory
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlotV2 slot = inventorySlots[i];
            InventoryItemV2 itemInSlot = slot.GetComponentInChildren<InventoryItemV2>();
            if (itemInSlot != null && itemInSlot.itemData == item && itemInSlot.count < maxStack && itemInSlot.itemData.isStackable)
            { // Check if there's an existing stack with same item type & isnt at max stack capacity / is stackable
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                Debug.Log("Successfully added item to stack!");
                return;
            }
        }

        // find empty slot
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlotV2 slot = inventorySlots[i];
            InventoryItemV2 itemInSlot = slot.GetComponentInChildren<InventoryItemV2>();
            if (itemInSlot == null) // if the item slot is empty
            {
                SpawnNewItem(item, slot);
                Debug.Log("Successfully added new item to inventory!");
                return;
            }
        }
        Debug.Log("Failed trying to add item to inventory - inventory full!");
    }

    public void RemoveItem(ItemData item)
    {

    }

    void SpawnNewItem(ItemData itemData, InventorySlotV2 slot)
    {
        GameObject newItemGO = Instantiate(inventoryItemV2Prefab, slot.transform);
        InventoryItemV2 invItem = newItemGO.GetComponent<InventoryItemV2>();
        invItem.InitialiseItem(itemData);
    }
}
