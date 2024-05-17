using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using TMPro;
using UnityEngine.Events;

public class InventoryItemV2 : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Image icon; // image of the item
    [SerializeField] private TextMeshProUGUI countText, nameText;

    [Header("DEBUGGING - DONT CHANGE THESE VALUES")]
    public ItemData itemData;
    public Transform parentAfterDrag; // the new inventory slot for this item
    public int count;
    public UnityEvent OnItemUsed = new UnityEvent();

    private void Awake()
    {
        icon = GetComponent<Image>();
        OnItemUsed.AddListener(CheckCount);
    }

    public void RefreshCount() // Update count text
    {
        countText.enabled = true; nameText.enabled = true; icon.enabled = true;
        countText.text = count.ToString();
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive); // If count <= 1, disable count text
        nameText.text = itemData.displayName;
        icon.sprite = itemData.icon;
    }

    public void InitialiseItem(ItemData newItemData) // Initialise an item not currently in inventory
    {
        itemData = newItemData;
        icon.sprite = newItemData.icon;
        nameText.text = newItemData.displayName;
        RefreshCount();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        icon.raycastTarget = false; // prevents dropping on own slot
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        CheckCount();
    }

    public void OnDrag(PointerEventData eventData) 
    {
        transform.position = Input.mousePosition; // while being dragged, follows mouse pos
        CheckCount();
    }

    public void OnEndDrag(PointerEventData eventData) // NOT OnDrop, which is called on InvSlotV2
    {
        icon.raycastTarget = true; // reset 
        transform.SetParent(parentAfterDrag);
        CheckCount();
    }

    public void CheckCount()
    {
        RefreshCount();
        if (count == 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTransformParentChanged()
    {
        Debug.Log("Parent changed");
        CheckCount();
    }
}
