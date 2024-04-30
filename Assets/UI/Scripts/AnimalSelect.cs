using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimalSelect : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float rayDepth;
    [SerializeField] private LayerMask animalLayer;
    
    public Animal currentAnimal { get; private set; }

    private Camera cam;

    public UnityEvent animalSelectionChanged, animalSelectionNulled; 

    void Awake() 
    {
        cam = Camera.main;
        currentAnimal = null;

    }

    void Update() 
    {

        var mousePos = Input.mousePosition;
        mousePos.z = rayDepth;
        mousePos = cam.ScreenToWorldPoint(mousePos);
        Debug.DrawRay(transform.position, mousePos-transform.position, Color.blue);

        if(Input.GetMouseButtonDown(0)) 
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if(Physics.Raycast(ray, out hit, 100, animalLayer)) 
            {
                Debug.Log(hit.transform.gameObject);
                currentAnimal = hit.transform.gameObject.GetComponent<Animal>();
                animalSelectionChanged.Invoke();
            }

            if(!Physics.Raycast(ray, out hit, 100, animalLayer)) 
            {
                currentAnimal = null;
                animalSelectionNulled.Invoke();
            }
        }
        
        //Debug.LogWarning(currentAnimal.Traits.nutritionTraits.ToString());

    }


}
