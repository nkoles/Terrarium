using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLevelHandler : MonoBehaviour
{
    public bool isOnWater = false;

    public float waterHeight;

    public void Update()
    {
        if(isOnWater)
        {
            transform.localPosition = new Vector3(0, waterHeight, 0);
        }
        else
        {
            transform.localPosition = Vector3.zero;
        }
    }
}
