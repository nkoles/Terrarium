using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class InputDebug : MonoBehaviour
{
    public UnityEvent onClickW, onClickA, onClickS, onClickD;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.W)) onClickW.Invoke();
        if(Input.GetKeyDown(KeyCode.A)) onClickA.Invoke();
        if(Input.GetKeyDown(KeyCode.S)) onClickS.Invoke();
        if(Input.GetKeyDown(KeyCode.D)) onClickD.Invoke();
    }
}
