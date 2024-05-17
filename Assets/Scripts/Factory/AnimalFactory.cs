using System.Collections;
using System.Collections.Generic;
using TerrariumTraits;
using UnityEngine;

public class AnimalFactory : TerrariumFactory
{
    static public AnimalFactory instance;

    public Transform animalContainer;
    public Animal animalPrefab;

    public int currentAnimalCount;
    public int maxAnimalCount;

    public override ITerrariumProduct CreateTerrariumObject(Vector3 position, TraitData traitData)
    {
        if(currentAnimalCount < maxAnimalCount)
        {
            GameObject instance = Instantiate(animalPrefab.gameObject, position, Quaternion.identity, animalContainer);

            Animal newAnimal = instance.GetComponent<Animal>();
            newAnimal.Traits = traitData;

            currentAnimalCount++;

            return newAnimal;
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
