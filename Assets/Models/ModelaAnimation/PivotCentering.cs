using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PivotCentering : MonoBehaviour
{
    private Vector3 center;

    public Renderer miteBodyRend;

    public void Start()
    {
        center = miteBodyRend.bounds.center;

        center = new Vector3(0, center.y, 0);

        transform.localPosition = center;
    } 


}
