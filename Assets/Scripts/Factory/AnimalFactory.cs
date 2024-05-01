using System.Collections;
using System.Collections.Generic;
using TerrariumTraits;
using UnityEngine;

public class AnimalFactory : TerrariumFactory
{
    static public AnimalFactory instance;

    public Animal animalPrefab;

    public override ITerrariumProduct CreateTerrariumObject(Vector3 position, TraitData traitData)
    {
        GameObject instance = Instantiate(animalPrefab.gameObject, position, Quaternion.identity);

        Animal newAnimal = instance.GetComponent<Animal>();
        newAnimal.Traits = traitData;

        //newAnimal.Initialise();

        return newAnimal;
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
