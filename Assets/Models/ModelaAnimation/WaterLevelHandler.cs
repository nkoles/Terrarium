using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLevelHandler : MonoBehaviour
{
    public bool isOnWater = false;

    public float startHeight;
    public float waterHeight;

    public void Start()
    {
        startHeight = transform.localPosition.y;
    }

    public void Update()
    {
        if(isOnWater)
        {
            transform.localPosition = new Vector3(0, waterHeight + startHeight, 0);
        }
        else
        {
            transform.localPosition = new Vector3(0, startHeight, 0);
        }
    }
}
