using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TerrariumTraits;

public class AnimalAI : MonoBehaviour
{
    [Header("Animal AI Fields")]
    public float speed;

    public Grid terrainGrid;

    private List<Vector3> AvailableTiles(int terrainData)
    {
        List<Vector3> results = new List<Vector3>();

        Vector3Int currentAnimalPosition = terrainGrid.WorldToCell(transform.position);

        Ray[] directionalRays = new Ray[4]
        {
            new Ray(transform.position, transform.forward),
            new Ray(transform.position, -transform.forward),
            new Ray(transform.position, transform.right),
            new Ray(transform.position, -transform.right)
        };

        Ray[] groundRays = new Ray[4]
        {
            new Ray(transform.position + transform.forward, -transform.up),
            new Ray(transform.position - transform.forward, -transform.up),
            new Ray(transform.position + transform.right, -transform.up),
            new Ray(transform.position - transform.right, -transform.up)
        };

        RaycastHit[] directionalHits = new RaycastHit[4];
        RaycastHit[] groundHits = new RaycastHit[4];

        for(int i = 0; i < directionalHits.Length; ++i)
        {
            Physics.Raycast(directionalRays[i], out directionalHits[i], 1f);

            if (directionalHits[i].collider == null)
            {
                Physics.Raycast(groundRays[i], out groundHits[i], 1f);

                if (TraitConstants.HasTrait(groundHits[i].collider.GetComponent<TerrariumTerrain>().traitData, terrainData))
                {
                    results.Add(terrainGrid.WorldToCell(groundHits[i].collider.transform.position));
                }
            }
        }

        return results;
    }

    public void Move(int traitData)
    {

        //List<Vector3> availableTiles = AvailableTiles(traitData);
    }

//    private void OnDrawGizmos()
//    {
//        Ray[] directionalRays = new Ray[4]
//{
//            new Ray(transform.position, transform.forward),
//            new Ray(transform.position, -transform.forward),
//            new Ray(transform.position, transform.right),
//            new Ray(transform.position, -transform.right)
//};

//        Ray[] groundRays = new Ray[4]
//        {
//            new Ray(transform.position + transform.forward, -transform.up),
//            new Ray(transform.position - transform.forward, -transform.up),
//            new Ray(transform.position + transform.right, -transform.up),
//            new Ray(transform.position - transform.right, -transform.up)
//        };

//        for(int i = 0; i < 4; i++)
//        {
//            Gizmos.DrawRay(groundRays[i]);
//            Gizmos.DrawRay(directionalRays[i]);
//        }
//    }
}
