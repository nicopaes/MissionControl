using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapLoader))]
public class MapLoaderEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();

        MapLoader MG = target as MapLoader;
        if (GUILayout.Button("LoadMap"))
        {
            MG.LoadGameData();
        }
        if (GUILayout.Button("GenerateMap"))
        {
            MG.LoadGameData();
        }
    }
}
