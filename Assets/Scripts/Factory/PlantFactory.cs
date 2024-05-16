using System.Collections;
using System.Collections.Generic;
using TerrariumTraits;
using UnityEngine;

public class PlantFactory : TerrariumFactory
{
    static public PlantFactory instance;

    public Plant plantPrefab;

    public override ITerrariumProduct CreateTerrariumObject(Vector3 position, TraitData traitData)
    {
        GameObject instance = Instantiate(plantPrefab.gameObject, position, Quaternion.identity);

        Plant newPlant = instance.GetComponent<Plant>();
        newPlant.Traits = traitData;

        return newPlant;
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
