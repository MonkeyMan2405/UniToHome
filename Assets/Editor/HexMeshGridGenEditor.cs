using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HexGridMeshGen))]
public class HexMeshGridGenEditor : Editor
{
    //puts buttons in the inspector!!!!!!

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        HexGridMeshGen hexMeshGen = (HexGridMeshGen)target;

        if(GUILayout.Button("Create Hex Mesh"))
        {
            hexMeshGen.CreateHexMesh();
        }

        if (GUILayout.Button("Clear Hex Mesh"))
        {
            hexMeshGen.ClearHexGridMesh();
        }
    }

}
