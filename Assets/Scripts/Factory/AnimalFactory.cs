using System.Collections;
using System.Collections.Generic;
using TerrariumTraits;
using UnityEngine;

public class AnimalFactory : TerrariumFactory
{
    static public AnimalFactory instance;

    public int count;
    public int maxCount;

    public Animal animalPrefab;

    public Transform container;

    public override ITerrariumProduct CreateTerrariumObject(Vector3 position, TraitData traitData)
    {
        if(count < maxCount)
        {
            GameObject instance = Instantiate(animalPrefab.gameObject, position, Quaternion.identity, container);

            Animal newAnimal = instance.GetComponent<Animal>();
            newAnimal.Traits = traitData;

            //newAnimal.Initialise();

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
