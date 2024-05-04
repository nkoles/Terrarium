using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class InputDebug : MonoBehaviour
{
    public UnityEvent onClick;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) onClick.Invoke();
    }
}
