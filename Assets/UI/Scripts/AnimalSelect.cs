using System.Collections;
using System.Collections.Generic;
using TerrariumTraits;
using UnityEngine;
using UnityEngine.Events;

public class AnimalSelect : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float rayDepth;
    [SerializeField] private LayerMask gameLayer;
    
    public Animal currentAnimal { get; private set; }

    public Plant currentPlant { get; private set; }

    private Camera cam;

    public UnityEvent animalSelectionNulled;


    void Awake() 
    {
        cam = Camera.main;
        currentAnimal = null;
        currentPlant = null;

    }

    void Update() 
    {

        var mousePos = Input.mousePosition;
        mousePos.z = rayDepth;
        mousePos = cam.ScreenToWorldPoint(mousePos);
        Debug.DrawRay(transform.position, mousePos-transform.position, Color.blue);

        if(Input.GetMouseButtonDown(0) && UIManager.instance.UIState == UIManager.UIInteractionType.None) 
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if(Physics.Raycast(ray, out hit, 100, gameLayer)) 
            {
                if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Animal")) 
                {
                    Debug.Log(hit.transform.gameObject);
                    currentAnimal = hit.transform.gameObject.GetComponent<Animal>();
                    currentPlant = null;
                    currentAnimal.gameObject.AddComponent<Outline>();
                    UIManager.instance.UpdateTraitsUI<NutritionalTraits>(currentAnimal.Traits.nutritionTraits);
                    UIManager.instance.UpdateTraitsUI<FoodTraits>(currentAnimal.Traits.foodTraits);
                    UIManager.instance.UpdateTraitsUI<TerrainTraits>(currentAnimal.Traits.terrainTraits);
                    UIManager.instance.UpdateAnimalName(currentAnimal);
                }

                if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Plant")) 
                {
                    Debug.Log(hit.transform.gameObject);
                    currentPlant = hit.transform.gameObject.GetComponent<Plant>();
                    currentAnimal = null;
                    currentPlant.gameObject.AddComponent<Outline>();
                    UIManager.instance.UpdateTraitsUI<NutritionalTraits>(currentPlant.Traits.nutritionTraits);
                    UIManager.instance.UpdateTraitsUI<FoodTraits>(currentPlant.Traits.foodTraits);
                    UIManager.instance.UpdateTraitsUI<TerrainTraits>(currentPlant.Traits.terrainTraits);
                    UIManager.instance.UpdatePlantName(currentPlant);

                }
                    
            }

            if(!Physics.Raycast(ray, out hit, 100, gameLayer)) 
            {
                Destroy(currentAnimal.GetComponent<Outline>());
                Destroy(currentPlant.GetComponent<Outline>());
                currentAnimal = null;
                currentPlant = null;
                animalSelectionNulled.Invoke();
            }
        }
        
        //Debug.LogWarning(currentAnimal.Traits.nutritionTraits.ToString());

    }


}
