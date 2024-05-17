using System.Collections;
using System.Collections.Generic;
using TerrariumTraits;
using UnityEngine;

public class PlantFactory : TerrariumFactory
{
    static public PlantFactory instance;

    public Transform plantContainer; 
    public Plant plantPrefab;

    public int currentCount;
    public int maxPlantCount = 20;

    public override ITerrariumProduct CreateTerrariumObject(Vector3 position, TraitData traitData)
    {
        if(currentCount < maxPlantCount)
        {
            GameObject instance = Instantiate(plantPrefab.gameObject, position, Quaternion.identity, plantContainer);

            Plant newPlant = instance.GetComponent<Plant>();
            newPlant.Traits = traitData;

            return newPlant;
        }

        return null;
    }

    public override ITerrariumProduct CreateTerrariumObject(Vector3 position)
    {
        throw new System.NotImplementedException();
    }

    private void Awake()
    {
        instance = this;
    }
}
