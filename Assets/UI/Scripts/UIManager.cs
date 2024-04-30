using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TerrariumTraits;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    private static UIManager instance { get; set; }


    [Header("UI Elements")]
    [SerializeField] private Image[] traitSlots = new Image[3];

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
        foreach(Image image in traitSlots) 
        {
            image.sprite = Resources.Load<Sprite>("TraitImages/Default");
        }
    }

    public void UpdateTraitsUI() 
    {
        //nutrition traits

        traitSlots[0].sprite = Resources.Load<Sprite>("TraitImages/NutritionTraits/" + animalSelect.currentAnimal.Traits.nutritionTraits.ToString()); 

        //food traits

        traitSlots[1].sprite = Resources.Load<Sprite>("TraitImages/FoodTraits/" + animalSelect.currentAnimal.Traits.foodTraits.ToString());

        //terrain traits

        traitSlots[2].sprite = Resources.Load<Sprite>("TraitImages/TerrainTraits/" + animalSelect.currentAnimal.Traits.terrainTraits.ToString());

        // Debug.LogWarning("Function Called");
        // Debug.LogWarning("TraitImages/NutritionTraits/" + animalSelect.currentAnimal.Traits.nutritionTraits.ToString());
    }

    public void HideTraits() 
    {
        foreach(Image image in traitSlots) 
        {
            image.sprite = Resources.Load<Sprite>("TraitImages/Default");
        }
    }
}
