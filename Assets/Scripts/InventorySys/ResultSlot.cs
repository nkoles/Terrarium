using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResultSlot : MonoBehaviour, IPointerClickHandler
{
    public InventoryItemV2 craftedItem;
    [SerializeField] private InventoryManagerV2 inventoryManager;

    void Update()
    {
        if (transform.childCount > 0)
        {
            transform.GetComponentInChildren<InventoryItemV2>().OnItemUsed.Invoke();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("OnPointerclick");
        SendToInventory();
    }

    public void SendToInventory()
    {
        if (GetComponentInChildren<InventoryItemV2>() != null)
        {
            Debug.Log("Sending to inventory");
            craftedItem = GetComponentInChildren<InventoryItemV2>();
            inventoryManager.AddItem(craftedItem.itemData, craftedItem.count);
            Destroy(craftedItem.gameObject);
        }
        else
        {
            Debug.Log("No item to add to inventory");
        }
    }
}
