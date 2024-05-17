using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TerrariumTraits;
using UnityEngine;

public class MiteColorHandler : MonoBehaviour
{
    public Shader miteShader;

    private TraitData _traitData;

    // 1 - 

    public Color[] colors;

    private List<Material> _validMaterials;
    private Color outputColor;

    private void Start()
    {
        _validMaterials = new List<Material>();

        GameTimeManager.Tick.AddListener(ReColor);

        Renderer[] tempRend = GetComponentsInChildren<Renderer>();
        
        foreach (var x in tempRend)
        {
            if (x.material.shader == miteShader)
            {
                _validMaterials.Add(x.material);
            }
        }
    }

    public void ReColor()
    {
        _traitData = GetComponentInParent<Animal>().Traits;

        List<Color> addColors = new List<Color>();

        bool hasColor = false;

        if(_traitData.nutritionTraits.HasFlag(NutritionalTraits.Carnivore))
        {
            addColors.Add(colors[0]);
            hasColor = true;
        } 
        if(_traitData.nutritionTraits.HasFlag(NutritionalTraits.Scavenger))
        {
            addColors.Add(colors[1]);
            hasColor = true;
        } 
        if(_traitData.foodTraits.HasFlag(FoodTraits.Plant))
        {
            addColors.Add(colors[2]);
            hasColor = true;
        } 
        if(_traitData.terrainTraits.HasFlag(TerrainTraits.Water))
        {
            addColors.Add(colors[3]);
            hasColor = true;
        } 

        Vector3 finalColor = new Vector3(); 

        foreach(var c in addColors)
        {
            finalColor += new Vector3(Mathf.Sqrt(c.r), Mathf.Sqrt(c.g), Mathf.Sqrt(c.b));
        }

        if (hasColor)
        {
            finalColor /= addColors.Count;

            outputColor = new Color(Mathf.Pow(finalColor.x, 2), Mathf.Pow(finalColor.y, 2), Mathf.Pow(finalColor.z, 2));

            foreach (var x in _validMaterials)
            {
               x.SetColor("_Color", outputColor);
            }
        }

    }
}
