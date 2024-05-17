using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InvOpen : MonoBehaviour
{
    static public InvOpen instance;
    [SerializeField] bool invOpen;

    [SerializeField] Transform closed, open;

    [SerializeField] float lerpValue;


    public void OpenInv() 
    {

        if (!invOpen) 
        {
            
            invOpen = true;

        }
        else 
        {
            invOpen = false;
        }

    }

    public void Update() 
    {

        switch(invOpen) 
        {
            case true:

            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, open.position, lerpValue * Time.deltaTime);

            break;

            case false:

            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, closed.position, lerpValue * Time.deltaTime);

            break;

        }


    }

    private void Awake()
    {
        instance = this;
    }
}
