using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    //public GameObject slotPrefab;
    public List<InventorySlot> inventorySlots = new List<InventorySlot>(18);
    private Inventory inv;
    public bool isCraftingPanel;

    private void Awake()
    {
        inv = FindObjectOfType<Inventory>();
    }

    private void OnEnable()
    {
        Inventory.OnInventoryChange += UpdateInventory;
        UpdateInventory(inv.inventory);
    }

    private void OnDisable()
    {
        Inventory.OnInventoryChange -= UpdateInventory;
    }

    public void UpdateInventory(List<InventoryItem> inventory)
    {
        if (inventory.Count > 0)
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                inventorySlots[i].DrawSlot(inventory[i]);
            }
        }
    }
}