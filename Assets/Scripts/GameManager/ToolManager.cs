using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIToolStates;
using TerrariumTraits;

public class ToolManager : MonoBehaviour
{
    public RaycastHit hit;
    public TerrainFactory terrainFactory;

    public GameObject outlines = null;

    private void Update()
    {
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, float.MaxValue);

        switch (UIManager.instance.UIState)
        {
            case UIInteractionType.Kill:
                if(hit.collider != null && hit.collider.TryGetComponent<ITerrariumProduct>(out ITerrariumProduct product))
                {
                    if (!hit.collider.TryGetComponent(out Outline outline))
                    {
                        outline = hit.collider.gameObject.AddComponent<Outline>();
                        outline.OutlineMode = Outline.Mode.OutlineAndSilhouette;
                        outline.OutlineColor = Color.white;
                        outline.OutlineWidth = 5f;

                        outlines = hit.collider.gameObject;
                    }

                    if (Input.GetMouseButtonDown(0))
                    {
                        Destroy(hit.collider.gameObject.GetComponent<Outline>());
                        outlines = null;

                        product.IsDead = true;
                        UIManager.instance.UIState = UIInteractionType.None;
                    }
                } else
                {
                    if (outlines != null)
                        Destroy(outlines.GetComponent<Outline>());
                }
                break;
            case UIInteractionType.Pickup:
                break;
            case UIInteractionType.Slorp:
                if(hit.collider != null && hit.collider.TryGetComponent<TerrariumTerrain>(out TerrariumTerrain terrain))
                {
                    if (Input.GetMouseButtonDown(0) && terrain.traits.terrainTraits.HasFlag(TerrainTraits.Ground))
                    {
                        Vector3 pos = hit.collider.transform.position;

                        Destroy(hit.collider.gameObject);

                        terrainFactory.CreateTerrain(pos, false);
                        UIManager.instance.UIState = UIInteractionType.None;

                    } else if (Input.GetMouseButtonDown(1) && terrain.traits.terrainTraits.HasFlag(TerrainTraits.Water))
                    {
                        Vector3 pos = hit.collider.transform.position;

                        Destroy(hit.collider.gameObject);

                        terrainFactory.CreateTerrain(pos, true);
                        UIManager.instance.UIState = UIInteractionType.None;

                    }
                }
                break;
        }       
    }
}
