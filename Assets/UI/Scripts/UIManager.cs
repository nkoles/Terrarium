using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TerrariumTraits;
using UnityEngine.UI;
using System.Linq;
using System;
using TMPro;

public class UIManager : MonoBehaviour
{

    public static UIManager instance { get; set; }

    public enum UIInteractionType 
    {
        None,

        Pickup,

        Kill,

        Slorp,

    }    

    public UIInteractionType UIState;
    

    [Header("UI Elements")]
    [SerializeField] private List<GameObject> traitsDisplay;
    [SerializeField] private GameObject UICamera;
    [SerializeField] private GameObject renderTexture;
    [SerializeField] private TextMeshProUGUI nameText;

    private AnimalSelect animalSelect;

    Vector3 offset = new Vector3(1.68f, 2.01f, -0.52f);

    void Awake() 
    {
        if(instance != null) 
        {
            Debug.Log("panic");
        }

        instance = this;
        animalSelect = Camera.main.GetComponent<AnimalSelect>();

        UIState = UIInteractionType.None;
    }

    void Start() 
    {
       nameText.text = "";
    }

    public void UpdateTraitsUI<TEnum>(TEnum input) where TEnum : Enum
    {

        foreach(Enum values in Enum.GetValues(input.GetType())) 
        {
            if(values.ToString() == "None" || values.ToString() == "Everything") continue;

            if (input.HasFlag(values)) 
            {
                //Debug.LogWarning(values);
                traitsDisplay.Find(x => x.name.Contains(values.ToString())).SetActive(true);
            }
            else 
            {
                traitsDisplay.Find(x => x.name.Contains(values.ToString())).SetActive(false);
            }

        }
    }

    public void UpdateAnimalName(Animal currentAnimal) 
    {
        nameText.text = "Mite";

    }

    public void UpdatePlantName(Plant currentPlant) 
    {
        nameText.text = "Plant";

    }

    public void HideTraits() 
    {
        foreach(GameObject ga in traitsDisplay) 
        {
            ga.SetActive(false);
        }

        nameText.text = "";
    }

    void Update() 
    {
        if(animalSelect.currentAnimal) 
        {
            renderTexture.SetActive(true);
            UICamera.transform.position = animalSelect.currentAnimal.transform.position + offset;
        }

        if(animalSelect.currentPlant) 
        {
            renderTexture.SetActive(true);
            UICamera.transform.position = animalSelect.currentPlant.transform.position + offset;
        }

        if(!animalSelect.currentAnimal && !animalSelect.currentPlant) 
        {
            renderTexture.SetActive(false);
        }
    }

    public void SetState(string newState) 
    {
        UIState = (UIInteractionType) Enum.Parse(typeof(UIInteractionType), newState);
    }
}
