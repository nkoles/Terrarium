using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastInteractor : MonoBehaviour
{
    [SerializeField] private Camera myCamera;
    private bool waitingForRaycast;

    private void Awake()
    {
        myCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            waitingForRaycast = true;
        }
    }

    private void FixedUpdate()
    {
        if (waitingForRaycast)
        {
            PerformRaycast();
        }
    }

    void PerformRaycast()
    {
        Debug.Log("Performing raycast...");
        waitingForRaycast = false;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 50f))
        {
            if (hit.transform.gameObject.GetComponent<RaycastInteractable>() != null)
            {
                Debug.Log("Got a hit on " + hit.transform.gameObject);
                hit.transform.gameObject.GetComponent<RaycastInteractable>().MyBehaviour();
            }
        }
    }
}
