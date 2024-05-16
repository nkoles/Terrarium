using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class DebugMovement : MonoBehaviour
{
    public UnityEngine.Vector3 direction;
    public void moveForwardDebug()
    {
        gameObject.transform.position += direction;
    }
}
