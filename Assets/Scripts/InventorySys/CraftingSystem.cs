using System.Collections;
using System.Collections.Generic;
using TerrariumTraits;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEditor.Progress;
//using static UnityEditor.Progress;

public class CraftingSystem : MonoBehaviour
{
    public List<InventoryItemV2> itemsInCrafting;
    [SerializeField] private GameObject resultSlot;
    private InventoryManagerV2 inventoryManager;

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
            ItemData newItemData;
            TraitData newTraitData = UpdatedTraitData(itemData1.traits, itemData2.traits); // the item data for the output item

            //newItemData.traits = UpdatedTraitData(itemData1.traits, itemData2.traits);
            /*Debug.Log(CompareTraitData(newTraitData, itemData1.traits));

            Debug.Log("newTraitData Nutrition traits = " + newTraitData.nutritionTraits);
            Debug.Log("itemdata1 Nutrition traits = " + itemData1.traits.nutritionTraits);
            Debug.Log("newTraitData Food traits = " + newTraitData.foodTraits);
            Debug.Log("itemdata1 Food traits = " + itemData1.traits.foodTraits);
            Debug.Log("newTraitData Terrain traits = " + newTraitData.terrainTraits);
            Debug.Log("itemdata1 Terrain traits = " + itemData1.traits.terrainTraits);
            Debug.Log("newTraitData Misc traits = " + newTraitData.miscTraits);
            Debug.Log("itemdata1 Misc traits = " + itemData1.traits.miscTraits);*/

            // CHECK IF THIS IS A NEW TYPE OF TRAIT, IF IT IS, INSTANTIATE A NEW SCRIPTABLE OBJECT
            bool newDataType = true;
            for (int i = 0; i < inventoryManager.itemTypes.Count; i++)
            {
                if (itemData1.itemType == inventoryManager.itemTypes[i].itemType && CompareTraitData(newTraitData, inventoryManager.itemTypes[i].traits)) // Check if this itemData type already exists
                {
                    Debug.Log("Not a new item type");
                    newDataType = false;
                    break;
                }
            }
            if (newDataType) // Data type doesnt exist, create a new one
            {
                Debug.Log("Instantiating new data type SO, yippee!!");
                ItemData data = ItemData.CreateInstance(itemData1.itemType, "Custom Data Type", itemData1.icon, itemData1.isStackable, newTraitData);
                inventoryManager.itemTypes.Add(data);
                newItemData = data;
            }
            else
            {
                newItemData = itemData1;
            }

            InventoryItemV2 outputItem = itemsInCrafting[0];
            outputItem.itemData = newItemData;

            if (resultSlot.GetComponentInChildren<InventoryItemV2>() != null) // if theres an item in the result slot
            {
                if (resultSlot.GetComponentInChildren<InventoryItemV2>().itemData != outputItem.itemData) // if the item in result slot is different from this one
                {
                    resultSlot.GetComponent<ResultSlot>().SendToInventory(); // send old item to inventory
                    GameObject prefabOutput = Instantiate(itemsInCrafting[0].gameObject, resultSlot.transform); // create new item gameobject
                    InventoryItemV2 prefabItem = prefabOutput.GetComponent<InventoryItemV2>();
                    prefabItem = outputItem;
                    UpdateOutput();
                    Debug.Log("Item in result slot is different from one crafted, sent to inventory.");
                }
                else if (resultSlot.GetComponentInChildren<InventoryItemV2>().itemData == outputItem.itemData) // if the item in result slot is the same as this item
                {
                    resultSlot.GetComponentInChildren<InventoryItemV2>().count++; // add to the count
                    resultSlot.GetComponentInChildren<InventoryItemV2>().OnItemUsed.Invoke(); // refresh count
                    UpdateOutput();
                    Debug.Log("Item in result slot is same as one crafted, count updated.");
                }
            }
            else // if theres no item in the result slot
            {
                GameObject prefabOutput = Instantiate(itemsInCrafting[0].gameObject, resultSlot.transform); // create new item gameobject
                InventoryItemV2 prefabItem = prefabOutput.GetComponent<InventoryItemV2>();
                prefabItem = outputItem;
                prefabItem.count = 1;
                prefabItem.OnItemUsed.Invoke();
                UpdateOutput();
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

    public void UpdateOutput()
    {
        Debug.Log("Update output");
        for (int i = 0; i < itemsInCrafting.Count; i++)
        {
            itemsInCrafting[i].count--;
            inventoryManager.RemoveItem(itemsInCrafting[i].itemData, 1);
            if (itemsInCrafting[i] != null) { itemsInCrafting[i].OnItemUsed.Invoke(); }

        }
    }

    TraitData UpdatedTraitData(TraitData traitData1, TraitData traitData2)
    {
        NutritionalTraits nutritionalTraits = new NutritionalTraits();
        FoodTraits foodTraits = new FoodTraits();
        TerrainTraits terrainTraits = new TerrainTraits();
        MiscTraits miscTraits = new MiscTraits();

        if (traitData1.nutritionTraits == NutritionalTraits.None) { nutritionalTraits = traitData2.nutritionTraits; }
        else { nutritionalTraits = traitData1.nutritionTraits; }

        if (traitData1.foodTraits == FoodTraits.None) { foodTraits = traitData2.foodTraits; }
        else { foodTraits = traitData1.foodTraits; }

        if (traitData1.terrainTraits == TerrainTraits.None) { terrainTraits = traitData2.terrainTraits; }
        else { terrainTraits = traitData1.terrainTraits; }

        if (traitData1.miscTraits == MiscTraits.None) { miscTraits = traitData2.miscTraits; }
        else { miscTraits = traitData1.miscTraits; }

        return new TraitData(nutritionalTraits, foodTraits, terrainTraits, miscTraits);
    }

    public void RemoveEmptyFromList()
    {
        itemsInCrafting.RemoveAll(item => item == null);
    }

    bool CompareTraitData(TraitData traitData1, TraitData traitData2)
    {
        if (traitData1.foodTraits != traitData2.foodTraits) { return false; }
        if (traitData1.nutritionTraits != traitData2.nutritionTraits) { return false; }
        if (traitData1.terrainTraits != traitData2.terrainTraits) { return false; }
        if (traitData1.miscTraits != traitData2.miscTraits) { return false; }
        return true;
    }
}