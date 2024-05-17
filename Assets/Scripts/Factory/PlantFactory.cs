using System.Collections;
using System.Collections.Generic;
using TerrariumTraits;
using UnityEngine;

public class PlantFactory : TerrariumFactory
{
    static public PlantFactory instance;

    public int count;
    public int maxCount;

    public Plant plantPrefab;

    public Transform container;

    public override ITerrariumProduct CreateTerrariumObject(Vector3 position, TraitData traitData)
    {
        if(count < maxCount)
        {
            GameObject instance = Instantiate(plantPrefab.gameObject, position, Quaternion.identity, container);

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
