using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TerrariumTraits;
using UnityEngine.Events;

public class TerrariumTerrain : MonoBehaviour
{
    public TraitData traits; 

    public float fertility = 0;
    public bool isBloody;

    public float bloodTimer = 5;

    static public UnityEvent OnTerrainCreation = new UnityEvent(); 
    public UnityEvent OnBlood = new UnityEvent();
    public void CheckNearbyWaterTiles()
    {
        Vector3 size = new Vector3(0.5f, 0.5f, 0.5f);

        Collider[] colliders = new Collider[18];

        int num = Physics.OverlapBoxNonAlloc(transform.position, size, colliders);

        print(num);

        float tempFertility = 0;

        List<Collider> tempCol = new List<Collider>();

        foreach(var col in colliders)
        {
            if(col!=null)
            {
                print(col.gameObject.name);

                if ((col.TryGetComponent<TerrariumTerrain>(out TerrariumTerrain terrain) && terrain.traits.terrainTraits.HasFlag(TerrainTraits.Water))
                    || (col.TryGetComponent<ITerrariumProduct>(out ITerrariumProduct plantAnim) && plantAnim.Traits.foodTraits.HasFlag(FoodTraits.Fertilizer)))
                {
                    if(!tempCol.Contains(col))
                        tempCol.Add(col);
                }
            }
        }

        fertility = tempCol.Count*0.25f;

        if(fertility >= 1)
        {
            fertility = 1;
        }
    }

    public bool CheckForBlood()
    {
        RaycastHit hit;
        Physics.Linecast(transform.position, transform.position + transform.up, out hit, ~(1<<6));

        if (hit.collider != null && hit.collider.TryGetComponent<Animal>(out Animal deadAnim) && deadAnim.IsDead)
        {
            OnBlood.Invoke();
            return true;            
        }

        return false;
    }

    private void OnTick()
    {
        CheckForBlood();
    }

    private void OnBloody()
    {
        StopAllCoroutines();
        StartCoroutine(BloodTimer());
    }

    private IEnumerator BloodTimer()
    {
        yield return new WaitForSeconds(bloodTimer);

        isBloody = false;
    }

    public void Awake()
    {
        if(traits.terrainTraits.HasFlag(TerrainTraits.Ground))
        {
            OnTerrainCreation.AddListener(CheckNearbyWaterTiles);
            OnBlood.AddListener(OnBloody);
            GameTimeManager.Tick.AddListener(OnTick);
        }

        OnTerrainCreation.Invoke();
    }
}
