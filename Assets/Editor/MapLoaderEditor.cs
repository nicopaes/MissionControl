using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();

        MapGenerator MG = target as MapGenerator;
        if (GUILayout.Button("CreateMap"))
        {
            MG.GenerateMap();
        }
    }
}
