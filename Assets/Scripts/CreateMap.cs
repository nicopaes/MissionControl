using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MapLoader))]
public class CreateMap : MonoBehaviour
{ 
    [System.Serializable]
    public struct TiledTile
    {
        public string name;
        public int code;
        public GameObject prefab;
    }
    public List<TiledTile> AllTiles;

	public GameObject Tile;
	public GameObject Player;

    public List<GameObject> MapGameObjList;
	public Vector2 initialPlayerPos;

    [SerializeField]
    public MapLoader mapLoader;

    public Vector3[,] MapMatrixVec;


    void Start ()
	{
		GenerateMap();
        mapLoader = GetComponent<MapLoader>();
	}
    public void GenerateMap()
    {
        Map newMap = mapLoader.loadedMap;

        MapMatrixVec = new Vector3[newMap.width, newMap.height];

        if (transform.childCount == 0)
        {
            foreach (Layer layer in newMap.layers)
            {
                var layerEmptyObjt = new GameObject(layer.name);
                layerEmptyObjt.transform.parent = this.gameObject.transform;
                layerEmptyObjt.transform.position = Vector3.zero;
                layerEmptyObjt.transform.localPosition = Vector3.zero;
                layerEmptyObjt.transform.localRotation = Quaternion.identity;

                int y = 0;
                int x = -1;

                for (int pos = 0; pos < layer.data.Length; pos++)
                {
                    x++;
                    if (pos%layer.height == 0 && pos != 0)
                    {
                        y++;
                        x = 0;
                    }
                    foreach (TiledTile tile in AllTiles)
                    {
                        if(layer.data[pos] == 1)
                        {
                            initialPlayerPos = new Vector2(x, y);

                            var player = Instantiate(Player, new Vector3(initialPlayerPos.x, -initialPlayerPos.y, 0), Quaternion.identity);
                            player.GetComponent<PlayerMovement>().SetInitialPos(initialPlayerPos);
                            ActivateTile(initialPlayerPos);
                            break;
                        }
                        if (tile.code == layer.data[pos])
                        {
                            MapMatrixVec[x, y] = new Vector3(x, -y, 0f);
                            MapGameObjList.Add(Instantiate(tile.prefab, MapMatrixVec[x, y], Quaternion.identity, layerEmptyObjt.transform));
                            break;
                        }
                    }
                    /*
                    if (layer.name == "BG")
                    {
                        foreach (TiledTile tile in AllTiles)
                        {
                            if(tile.code == layer.data[pos])
                            {
                                MapMatrixVec[x, y] = new Vector3(x, -y, 0f);
                                MapGameObjList.Add(Instantiate(tile.prefab,MapMatrixVec[x, y], Quaternion.identity, layerEmptyObjt.transform));
                                break;
                            }
                        }
                    }    
                    if (layer.name == "IN")
                    {
                        switch (layer.data[pos])
                        {
                            case 1:
                                initialPlayerPos = new Vector2(x,y);

                                var player = Instantiate(Player, new Vector3(initialPlayerPos.x, -initialPlayerPos.y, 0), Quaternion.identity);
                                player.GetComponent<PlayerMovement>().SetInitialPos(initialPlayerPos);
                                ActivateTile(initialPlayerPos);
                                break;
                        }
                    }*/
                }
            }
        }
    }
    public Vector3 CheckNextPos(Vector2 nextPos)
    {
        int y = (int)nextPos.y;
        int x = (int)nextPos.x;

        int sum = x + y*5;
        Debug.Log("SUM " + sum);

        Map newMap = mapLoader.loadedMap;

        if ((x < newMap.width || x > 0 || y < newMap.height || y > 0) && MapGameObjList[sum].GetComponent<TileComponent>().path)
        {
            Debug.Log("Okay");
            return MapGameObjList[sum].transform.position;
        }
        else
        {
            Debug.Log("Not okay");
            Terminal.WriteLine("Tile blocked");
            return new Vector3(1,1,1);
        }
    }
    public void ActivateTile(Vector2 tilePosition)
    {
        MapGameObjList[(int)tilePosition.x + (int)tilePosition.y*5].GetComponent<TileComponent>().active = true;
    }
    public void DeactivateTile(Vector2 tilePosition)
    {
        MapGameObjList[(int)tilePosition.x + (int)tilePosition.y * 5].GetComponent<TileComponent>().active = false;
    }
    public void DestroyMap()
	{
        MapGameObjList.Clear();
        if (Application.isPlaying)
            Destroy(GameObject.Find("Player(Clone)"));
        else
            DestroyImmediate(GameObject.Find("Player(Clone)"));

		for(int i =0;i < transform.childCount;i++)
		{
            if(Application.isPlaying)
			    Destroy(transform.GetChild(i).gameObject);
            else
                DestroyImmediate(transform.GetChild(i).gameObject);
        }
	}
}
