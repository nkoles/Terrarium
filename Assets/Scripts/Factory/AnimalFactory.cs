using System.Collections;
using System.Collections.Generic;
using TerrariumTraits;
using UnityEngine;
using UnityEngine.UI;

public class AnimalFactory : TerrariumFactory
{
    static public AnimalFactory instance;

    public int count;
    public int maxCount;

    public Animal animalPrefab;

    public Transform container;

    public GameObject Restart;
    public GameTimeManager gameTimeManager;

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

    private void Update()
    {
        if (count <= 0)
        {
            Restart.SetActive(false);
            gameTimeManager.SetPlayState(false);
        }
    }
}
