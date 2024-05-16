using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameTimeManager : MonoBehaviour
{
    static public GameTimeManager timeHandlerInstance;

    static public UnityEvent PreTick = new UnityEvent();
    static public UnityEvent Tick = new UnityEvent();

    public float tickFrequency;
    private float _tickTimer = 0;

    private void Awake()
    {
        if(timeHandlerInstance == null)
        {
            timeHandlerInstance = this;
        } else if (timeHandlerInstance != this)
        {
            Destroy(this);
        }
    }
    
    private void TickRate()
    {
        _tickTimer += Time.deltaTime;

        if(_tickTimer >= tickFrequency)
        {
            print("TICK");
            Tick.Invoke();
            _tickTimer = 0;
        }
    } 

    public void Update()
    {
        TickRate();
    }
}
