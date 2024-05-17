using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIToolStates;
using TerrariumTraits;

public class ToolManager : MonoBehaviour
{
    static public ToolManager instance;

    public RaycastHit hit;
    public TerrainFactory terrainFactory;

    public GameObject outlines = null;

    public InventoryManagerV2 inventoryManager;

    public InventoryItemV2 currentItem = null;

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
                if (hit.collider != null && ((hit.collider.TryGetComponent<Animal>(out Animal anim) && anim.Traits.miscTraits.HasFlag(MiscTraits.Pickupable)) ||
                                            (hit.collider.TryGetComponent<Plant>(out Plant plant) && plant.Traits.miscTraits.HasFlag(MiscTraits.Pickupable)) ||
                                            (hit.collider.TryGetComponent<TerrariumTerrain>(out TerrariumTerrain terra) && (terra.traits.terrainTraits.HasFlag(TerrainTraits.Water) || terra.isBloody))))
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
                        ItemData itemType = new ItemData();
                        bool isDestroyed = true;

                        if(hit.collider.GetComponent<Animal>() != null)
                        {
                            Animal a =  hit.collider.GetComponent<Animal>();

                            itemType = inventoryManager.itemTypes[1];
                            itemType.itemType = inventoryManager.itemTypes[1].itemType;
                            itemType.traits = a.Traits;
                            itemType.displayName = UIManager.UpdateCorpseName(a);
                        }

                        if(hit.collider.GetComponent<Plant>() != null)
                        {
                            Plant p = hit.collider.GetComponent<Plant>();

                            itemType = inventoryManager.itemTypes[3];
                            itemType.itemType = inventoryManager.itemTypes[3].itemType;
                            itemType.traits = p.Traits;
                            itemType.displayName = UIManager.UpdateSeedName(p);
                        }

                        if(hit.collider.GetComponent<TerrariumTerrain>() != null)
                        {
                            TerrariumTerrain t = hit.collider.GetComponent<TerrariumTerrain>();

                            if(t.traits.terrainTraits.HasFlag(TerrainTraits.Water))
                            {
                                itemType = inventoryManager.itemTypes[4];
                                itemType.itemType = inventoryManager.itemTypes[4].itemType;
                                itemType.traits = t.traits;

                                itemType.displayName = "Water";
                            } else
                            {
                                itemType = inventoryManager.itemTypes[0];
                                itemType.itemType = inventoryManager.itemTypes[0].itemType;
                                itemType.traits = t.traits;
                                itemType.traits.foodTraits |= FoodTraits.Meat;
                                itemType.traits.nutritionTraits |= NutritionalTraits.Carnivore;

                                itemType.displayName = "Traits";
                            }

                            isDestroyed = false;
                        }

                        

                        inventoryManager.AddItem(itemType, 1);
                        Destroy(hit.collider.gameObject.GetComponent<Outline>());
                        outlines = null;

                        if (isDestroyed)
                            Destroy(hit.collider.gameObject);

                        UIManager.instance.UIState = UIInteractionType.None;
                    }
                }
                else
                {
                    if (outlines != null)
                        Destroy(outlines.GetComponent<Outline>());
                }
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
            case UIInteractionType.PlaceItem:
                if (hit.collider != null && hit.collider.TryGetComponent<TerrariumTerrain>(out TerrariumTerrain ter))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        Vector3 pos = hit.collider.transform.position + Vector3.up;


                        switch (currentItem.itemData.itemType)
                        {
                            case ItemType.Seed:
                                PlantFactory.instance.CreateTerrariumObject(pos, currentItem.itemData.traits);
                                break;
                            case ItemType.Egg:
                                AnimalFactory.instance.CreateTerrariumObject(pos, currentItem.itemData.traits);
                                break;
                            default:
                                currentItem = null;
                                break;
                        }

                        inventoryManager.RemoveItem(currentItem.itemData, 1);
                        UIManager.instance.UIState = UIInteractionType.None;
                    }
                }
                else
                {
                    if (outlines != null)
                        Destroy(outlines.GetComponent<Outline>());
                }
                break;
        }       
    }

    private void Awake()
    {
        instance = this;
    }
}
