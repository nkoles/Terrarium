using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFadeHighlighter : MonoBehaviour
{
    public GameObject targetGameObject;

    private GameObject _currentCamera;
    private GameObject _currentHitObject;
    void Start()
    {
        _currentCamera = GameObject.FindGameObjectWithTag("MainCamera");

    }

    // Update is called once per frame
    void Update()
    {
        if(targetGameObject)
        {
            Ray ray = new Ray(_currentCamera.transform.position, (targetGameObject.transform.position - _currentCamera.transform.position).normalized);
            RaycastHit raycastHit;

            if(Physics.Raycast(ray,out raycastHit))
            {
                if(raycastHit.collider.GetComponent<DitherMatScript>())
                {
                    raycastHit.collider.GetComponent<DitherMatScript>().isFaded = true;
                }

                if (_currentHitObject != null)
                {
                    if(raycastHit.collider.gameObject != _currentHitObject && _currentHitObject.GetComponent<DitherMatScript>())
                    {
                        _currentHitObject.GetComponent<DitherMatScript>().isFaded = false;
                    }
                }

                _currentHitObject = raycastHit.collider.gameObject;
            }
        }
    }
}
