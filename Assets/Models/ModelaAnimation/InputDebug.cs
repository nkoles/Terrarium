using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class InputDebug : MonoBehaviour
{
    public UnityEvent onClickW, onClickA, onClickS, onClickD;

    public TickMovement[] guah;

    public void Start()
    {
        guah = FindObjectsByType<TickMovement>(FindObjectsSortMode.None);
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.W)) moveArmy(0);
        if(Input.GetKeyDown(KeyCode.A)) moveArmy(1);
        if(Input.GetKeyDown(KeyCode.S)) moveArmy(2);
        if(Input.GetKeyDown(KeyCode.D)) moveArmy(3);
    }

    public void moveArmy(int dir)
    {
        foreach (var animation in guah)
        {
            animation.startStepAnimation(dir);
        }
    }
}
