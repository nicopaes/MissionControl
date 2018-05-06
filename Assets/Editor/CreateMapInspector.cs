using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CreateMap))]
public class CreateMapInspector : Editor
{
	public override void OnInspectorGUI()
	{
		base.DrawDefaultInspector();

		CreateMap CM = target as CreateMap;
		if(GUILayout.Button("CreateMap"))
		{
			CM.GenerateMap();
		}
		if(GUILayout.Button("DestroyMap"))
		{
			CM.DestroyMap();
		}
	}
	
}
