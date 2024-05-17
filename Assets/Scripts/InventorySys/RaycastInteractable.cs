using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LerpingUtils;

public class RaycastInteractable : MonoBehaviour
{
    private Camera myCamera;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private float cameraSpeed;
    [SerializeField] private InventoryManagerV2 invManager; // change slot parents
    [SerializeField] private Transform tooltipObj;
    [Tooltip("Crafting, Reset")]public string myBehaviourType;
    [SerializeField] private GameObject[] canvas; // MAINUI, MENUUI, CRAFTINGUI
    public static bool behaviourActive;
    //[SerializeField] private 

    private void Awake()
    {
        myCamera = Camera.main;
    }

    private void Update()
    {
        
    }

    public void MyBehaviour()
    {
        if (!behaviourActive)
        {
            switch (myBehaviourType)
            {
                case "Crafting":
                    {
                        StartCoroutine(CraftingBehaviour());
                        break;
                    }
                case "Reset":
                    {
                        StartCoroutine(ResetBehaviour());
                        break;
                    }
            }
        }
    }
    
    public IEnumerator CraftingBehaviour()
    {
        behaviourActive = true;
        StartCoroutine(LerpTools.LerpToPosition(transform.position, targetTransform.position, cameraSpeed));

        // when coroutine is finished
        canvas[2].SetActive(true);
        tooltipObj.SetParent(canvas[2].transform);
        invManager.SwitchSlotParents(2);
        behaviourActive = false;

        yield return null;
    }

    public IEnumerator ResetBehaviour()
    {

        yield return null;
    }
}
