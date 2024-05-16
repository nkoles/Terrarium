using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class InputDebug : MonoBehaviour
{
    public UnityEvent onClickW, onClickA, onClickS, onClickD;

    public TickMovement[] guah;

    private bool fuck = false;

    public void Start()
    {
        guah = FindObjectsByType<TickMovement>(FindObjectsSortMode.None);
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.W)) MoveArmy(0);
        if(Input.GetKeyDown(KeyCode.A)) MoveArmy(1);
        if(Input.GetKeyDown(KeyCode.S)) MoveArmy(2);
        if(Input.GetKeyDown(KeyCode.D)) MoveArmy(3);

        
    }

    public void MoveArmy(int dir)
    {
        foreach (var animation in guah)
        {
            animation.startStepAnimation(dir);
        }
    }

    
}
