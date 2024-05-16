using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour, ICollectible
{
    [Header("Inventory Contents")]
    public List<InventoryItem> inventory = new List<InventoryItem>();
    private Dictionary<ItemData, InventoryItem> itemDictionary = new Dictionary<ItemData, InventoryItem>();

    [Header("Item Types")]
    public ItemData[] itemArray;

    public static event Action OnItemCollected;
    public static event Action<List<InventoryItem>> OnInventoryChange;

    private PanelManager[] panelManager;

    private void OnEnable()
    {
        panelManager = FindObjectsOfType<PanelManager>();
    }

    private void OnDisable()
    {
        
    }

    private void Update()
    {
        DebugAddItem();
    }

    void DebugAddItem()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Collect(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Collect(1);
        }
    }

    public void Add(ItemData itemData)
    {
        if (itemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            item.AddToStack();
            //Debug.Log(item.itemData.displayName + " total stack is now " + item.stackSize);
            OnInventoryChange?.Invoke(inventory);
        }
        else
        {
            if (inventory.Count <= 18)
            {
                InventoryItem newItem = new InventoryItem(itemData);
                inventory.Add(newItem);
                itemDictionary.Add(itemData, newItem);
                foreach (PanelManager pm in panelManager)
                { pm.UpdateInventory(inventory); }
                
                Debug.Log("Added " + newItem.itemData.displayName + " to inventory for first time");
                
            }
            else { Debug.Log("Inventory Full!"); }
        }
    }

    public void Remove(ItemData itemData)
    {
        if (itemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            item.RemoveFromStack();
            if (item.stackSize == 0)
            {
                inventory.Remove(item);
                itemDictionary.Remove(itemData);
            }
            OnInventoryChange?.Invoke(inventory);
        }
    }

    public void Collect(int i)
    {
        OnItemCollected?.Invoke();
        Add(itemArray[i]);
    }
}
