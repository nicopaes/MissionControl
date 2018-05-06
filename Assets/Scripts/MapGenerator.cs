/* 
* Copyright (c) Bravarda Game Studio
* John K. Paul Project 2017
*
*/
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;

[ExecuteInEditMode]
public class MapGenerator : MonoBehaviour
{
    [Header("Load File")]
    public string fileName; // The name of the file that will be loaded 

    [Header("Generated Prefabs")] // The prefabs that the script will Instantiate
    public GameObject CellTiles;

    [Header("Map Randomization")]
    public static string MapsFolder = "StreamingAssets/jsonMaps";

    [Space]

    //private Vector2 mapSize;
    private string jsonString;

    // The Position that the objects will be Instantiated
    private float posX;
    private float posY;
    private float posZ;

    private List<GameObject> TileList = new List<GameObject>();

    //Counters
    int c;
    int t;

    int maxSize = 0;

    Layers AllLayers;

    public void GenerateMap()
    {
        LoadMap();
        Debug.Log(AllLayers.layers);
        if (AllLayers != null)
        {
            foreach (Layer layer in AllLayers.layers) // For each layer of the AllLayers variable
            {
                c = 0;
                if (GameObject.Find(layer.name) == null) // Create a new Empity object to hold the objects of that layer
                {
                    var layerEmptyObjt = new GameObject(layer.name);
                    layerEmptyObjt.transform.parent = GameObject.Find("MAP").transform;
                    layerEmptyObjt.transform.position = Vector3.zero;
                    layerEmptyObjt.transform.localPosition = Vector3.zero;
                    layerEmptyObjt.transform.localRotation = Quaternion.identity;
                    //Debug.Log(layer.name + ":" + layerEmptyObjt.transform.localPosition);
                }
                for (int i = 0; i < layer.data.Length; i++)
                {
                    if (i % layer.height == 0 && i != 0)
                    {
                        c++;
                    }
                    t = i - layer.width * c;

                    if (c > maxSize)
                    {
                        maxSize = c;
                    }

                    //This is the next position of the Tiles base on the counters, so the first will be (3,0,0) -> (6,0,-3) -> (9,0,-6),etc. So the tiles size will have to be changed for each game, in this case 3x3 square
                    posX = (1.1f * t + 0.34f);
                    posY = -0.55f;
                    posZ = (-1.1f * c);

                    if (layer.name == "Base") // The name of the layer inside the json file, and Tiled
                    {
                        switch (layer.data[i])
                        {
                            case 0:
                                Debug.Log("oi");
                                break;
                        }

                    }
                }
            }
        }
    }
    public void DeleteMap() // Deletes the map that was loaded 
    {
        foreach (Transform child in GameObject.Find("GeneratedTiles").transform)
        {
            DestroyImmediate(child.gameObject);
            //Destroy(child.gameObject);
        }
    }
    public bool CheckChildZero()
    {
        return (transform.childCount == 0);
    }
    /// <summary>
    /// Function that loads the map file, and dumps it to the AllLayers variable
    /// </summary>
    public void LoadMap()
    {
        jsonString = File.ReadAllText(Application.dataPath + "/Maps/" + fileName + ".json");
        if (jsonString != null)
        {
            AllLayers = JsonUtility.FromJson<Layers>(jsonString);
            Debug.Log("Successfully Loaded: " + fileName);
        }
        else
        {
            Debug.Log("MAP FILE NOT FOUND, TRY CHANGING FILE NAME");
        }
    }

    [Serializable]
    public class Layers
    {
        public List<Layer> layers;
    }

    //Class that hold all the information of the json File
    [Serializable]
    public class Layer
    {
        public int[] data;
        public string name;
        public int opacity;
        public string type;
        public bool visible;
        public int width;
        public int height;
        public int x;
        public int y;
    }
}