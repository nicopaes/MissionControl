using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapLoader : MonoBehaviour
{
    public string fileName;
    [SerializeField]
    public Map loadedMap;

    public void LoadGameData()
    {
        // Path.Combine combines strings into a file path
        // Application.StreamingAssets points to Assets/StreamingAssets in the Editor, and the StreamingAssets folder in a build
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName + ".json");

        if (File.Exists(filePath))
        {
            
            // Read the json from the file into a string
            string dataAsJson = File.ReadAllText(filePath);

            
            // Pass the json to JsonUtility, and tell it to create a GameData object from it
            loadedMap = JsonUtility.FromJson<Map>(dataAsJson);
            if(loadedMap != null)
            {
                Debug.Log("Loaded: " + fileName);
            }
        }
        else
        {
            Debug.LogError("Cannot load game data!");
        }
    }

}
