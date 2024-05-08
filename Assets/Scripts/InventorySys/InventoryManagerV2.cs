using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManagerV2 : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private int maxStack;
    [Header("Inventory Slots")]
    public InventorySlotV2[] inventorySlots;
    public Transform[] parentTransforms;
    [Header("Item Types")]
    public ItemData[] itemTypes;
    [Header("Other")]
    public GameObject inventoryItemV2Prefab;
    [Header("Debug")]
    [SerializeField] private bool debugMode;
    [SerializeField] private int debugAmountToAdd;

    //public Dictionary<InventorySlotV2, InventoryItemV2> itemDictionary;

    private void Awake()
    {
        LoadData();
    }

    private void Update()
    {
        if (debugMode)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) { AddItem(itemTypes[0], debugAmountToAdd); }
            else if (Input.GetKeyDown(KeyCode.Alpha2)) { AddItem(itemTypes[1], debugAmountToAdd); }
            else if (Input.GetKeyDown(KeyCode.Alpha3)) { AddItem(itemTypes[2], debugAmountToAdd); }
            else if (Input.GetKeyDown(KeyCode.Alpha4)) { RemoveItem(itemTypes[0], debugAmountToAdd); }
            else if (Input.GetKeyDown(KeyCode.Alpha5)) { RemoveItem(itemTypes[1], debugAmountToAdd); }
            else if (Input.GetKeyDown(KeyCode.Alpha6)) { RemoveItem(itemTypes[2], debugAmountToAdd); }
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveData();
        }
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    public void AddItem(ItemData item, int amountToAdd)
    {
        // Check if we can stack anywhere in the inventory
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            //InventorySlotV2 slot = inventorySlots[i];
            InventoryItemV2 itemInSlot = inventorySlots[i].GetComponentInChildren<InventoryItemV2>();
            if (itemInSlot != null && itemInSlot.itemData == item && itemInSlot.count < maxStack && itemInSlot.itemData.isStackable)
            { // Check if there's an existing stack with same item type & isnt at max stack capacity / is stackable
                if (itemInSlot.count + amountToAdd > maxStack) // If going over max stack
                {
                    Debug.Log("Going over maxStack");
                    itemInSlot.count = maxStack; // Lazy workaround because I crashed unity
                }
                else { itemInSlot.count += amountToAdd; }
                itemInSlot.RefreshCount();
                //Debug.Log("Successfully added item to stack!");
                return;
            }
        }

        // find empty slot
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventoryItemV2 itemInSlot = inventorySlots[i].GetComponentInChildren<InventoryItemV2>();
            if (itemInSlot == null) // if the item slot is empty
            {
                SpawnNewItem(item, inventorySlots[i], amountToAdd);
                //Debug.Log("Successfully added new item to inventory!");
                return;
            }
        }
        //Debug.Log("Failed trying to add item to inventory - inventory full!");
    }

    public void RemoveItem(ItemData item, int amountToRemove)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventoryItemV2 itemInSlot = inventorySlots[i].GetComponentInChildren<InventoryItemV2>();
            if (itemInSlot != null && itemInSlot.itemData != null && itemInSlot.count >= amountToRemove)
            {
                itemInSlot.count -= amountToRemove;
                if (itemInSlot.count == 0)
                {
                    Destroy(itemInSlot.gameObject);
                    //Debug.Log("Removed " + amountToRemove + " of " + item.name + " from inventory");
                }
                else
                {
                    itemInSlot.RefreshCount();
                }
                return;
            }
        }
    }

    void SpawnNewItem(ItemData itemData, InventorySlotV2 slot, int amount)
    {
        GameObject newItemGO = Instantiate(inventoryItemV2Prefab, slot.transform);
        InventoryItemV2 invItem = newItemGO.GetComponent<InventoryItemV2>();
        invItem.InitialiseItem(itemData);
        invItem.count += amount;
        invItem.RefreshCount();
    }

    public void SwitchSlotParents(int index)
    {
        foreach (InventorySlotV2 slot in inventorySlots)
        {
            slot.transform.SetParent(parentTransforms[index], false);
        }
    }

    void SaveData()
    {
        Debug.Log("The game would save if it had a function to do so");
        // SAVE DATA HERE
    }

    void LoadData()
    {
        Debug.Log("The game would load if it had a function to do so");
        // LOAD DATA HERE
    }
}
