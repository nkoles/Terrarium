using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Tilemap3D))]
public class Tilemap3DEditor : Editor
{
    private Tilemap3D m_Tilemap;
    public GameObject cursor;

    public void OnEnable()
    {
        m_Tilemap = (Tilemap3D)target;
        m_Tilemap.grid = m_Tilemap.GetComponent<Grid>();
    }

    public override void OnInspectorGUI()
    {
        cursor = (GameObject)EditorGUILayout.ObjectField(new GUIContent("PlacementCursor"), cursor, typeof(GameObject), true);

        //if(GUILayout.Button(new GUIContent("ToggleCursor"))){
         
        //}
    }

    public void OnSceneGUI()
    {
        if(!Application.isPlaying)
        {
            Vector3 worldMousePosition = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition).origin;
            Vector3Int tilemapMousePosition = new Vector3Int(m_Tilemap.grid.WorldToCell((worldMousePosition)).x, m_Tilemap.grid.WorldToCell((worldMousePosition)).y, 0);

            cursor.transform.position = m_Tilemap.grid.CellToWorld(tilemapMousePosition);

            Debug.Log(m_Tilemap.grid.WorldToCell((worldMousePosition)));
        }
    }
}