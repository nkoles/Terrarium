using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TerrariumTraits;
using UnityEngine.UI;
using System.Linq;
using System;

public class UIManager : MonoBehaviour
{

    public static UIManager instance { get; set; }


    [Header("UI Elements")]
    [SerializeField] private List<GameObject> traitsDisplay;

    private AnimalSelect animalSelect;

    void Awake() 
    {
        if(instance != null) 
        {
            Debug.Log("panic");
        }

        instance = this;
        animalSelect = Camera.main.GetComponent<AnimalSelect>();
    }

    void Start() 
    {
       
    }

    public void UpdateTraitsUI<TEnum>(TEnum input) where TEnum : Enum
    {

        foreach(Enum values in Enum.GetValues(input.GetType())) 
        {
            if(values.ToString() == "None" || values.ToString() == "Everything") continue;

            if (input.HasFlag(values)) 
            {
                    Debug.LogWarning(values);
                    traitsDisplay.Find(x => x.name.Contains(values.ToString())).SetActive(true);
            }
            else 
            {
                    traitsDisplay.Find(x => x.name.Contains(values.ToString())).SetActive(false);
            }
        }
    }

    public void HideTraits() 
    {
        foreach(GameObject ga in traitsDisplay) 
        {
            ga.SetActive(false);
        }
    }
}
