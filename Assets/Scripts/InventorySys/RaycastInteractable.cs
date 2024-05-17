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
    public static bool coroutineActive;

    private void Awake()
    {
        myCamera = Camera.main;
    }

    private void Update()
    {
        //Debug.Log("Coroutine active = " + coroutineActive);
    }

    public void MyBehaviour()
    {
        if (!coroutineActive)
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
                default:
                    {
                        Debug.Log("PANIC");
                        break;
                    }
            }
        }
    }
    
    public IEnumerator CraftingBehaviour()
    {
        if (RaycastInteractor.state != "Crafting")
        {
            coroutineActive = true;
            StartCoroutine(LerpTools.LerpTransform(myCamera.transform, targetTransform.position, 0, cameraSpeed));
            yield return LerpTools.LerpTransform(myCamera.transform, targetTransform.rotation.eulerAngles, 1, cameraSpeed);

            Debug.Log("bruh");
            canvas[2].SetActive(true);
            tooltipObj.SetParent(canvas[2].transform);
            invManager.SwitchSlotParents(2);
            RaycastInteractor.state = "Crafting";
            coroutineActive = false;

        }
        yield return null;
    }

    public IEnumerator ResetBehaviour()
    {
        if (RaycastInteractor.state != "Reset")
        {
            coroutineActive = true;

            StartCoroutine(LerpTools.LerpTransform(myCamera.transform, targetTransform.position, 0, cameraSpeed));
            yield return LerpTools.LerpTransform(myCamera.transform, targetTransform.rotation.eulerAngles, 1, cameraSpeed);

            foreach (GameObject c in canvas)
            {
                c.SetActive(false);
            }

            RaycastInteractor.state = "Reset";
            coroutineActive = false;
        }
        yield return null;
    }
}
