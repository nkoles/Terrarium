using System.Collections;
using System.Collections.Generic;
using TerrariumTraits;
using UnityEngine;

public class PlantAI : MonoBehaviour
{
    [Header("Plant AI Fields")]
    public TerrariumTerrain targetTerrain;

    public bool CheckForCorrectGround(TraitData plantTraits)
    {
        RaycastHit hit;

        if (Physics.Linecast(transform.position, transform.position - Vector3.down, out hit))
        {
            TerrariumTerrain tempTerrain;

            if(hit.collider.TryGetComponent<TerrariumTerrain>(out tempTerrain) && plantTraits.HasTrait<TerrainTraits>(tempTerrain.terrainFlag))
            {
                targetTerrain = tempTerrain;
                return true;
            }
        }

        return false;
    }


}
