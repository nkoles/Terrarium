using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    
    [SerializeField] private Camera cam;

    private Vector3 dragOrigin;

    void Update() 
    {
        PanCamera();
    }

    public void PanCamera() 
    {

        if(Input.GetMouseButtonDown(0)) 
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if(Input.GetMouseButton(0)) 
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);

            cam.transform.position += difference;
        }



    }

}
