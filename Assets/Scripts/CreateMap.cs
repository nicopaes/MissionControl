using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MapLoader))]
public class CreateMap : MonoBehaviour
{ 
    public List<GameObject> AllTileTypes;

	public GameObject Tile;
	public GameObject Player;
	public float tileSize;
	[Range(1,100)]
	public int Width;
	[Range(1,100)]
	public int Height;

	[SerializeField]
	public float[,] MapMatrixPos;
	public Vector3[,] MapMatrixVec;

	public GameObject[,] MapMatrixGameObj;
	public Vector2 initialPlayerPos;

    [SerializeField]
    public MapLoader mapLoader;

	void Start ()
	{
		GenerateMap();
        mapLoader = GetComponent<MapLoader>();
	}
    /*
	public void GenerateMap()
	{
		if(transform.childCount == 0)
		{
			MapMatrixPos = new float[Width,Height];
			MapMatrixVec = new Vector3[Width,Height];
			MapMatrixGameObj = new GameObject[Width,Height];

			for(int x = 0; x < Width; x++ )
				{
					for(int y = 0; y < Height; y++ )
					{
						MapMatrixVec[x,y] = new Vector3(x*tileSize,y*tileSize,0f);
                        //MapMatrixGameObj[x,y] = Instantiate(Tile,MapMatrixVec[x,y],Quaternion.identity,this.transform);
                        MapMatrixGameObj[x,y] = Instantiate(AllTileTypes[(int)Random.Range(0,AllTileTypes.Count)],MapMatrixVec[x,y],Quaternion.identity,this.transform);
                }
            }
		}
		initialPlayerPos = new Vector2 (Width/2,Height/2);
		
		var player = Instantiate(Player,new Vector3(initialPlayerPos.x*tileSize,initialPlayerPos.x*tileSize,0),Quaternion.identity);
		player.GetComponent<PlayerMovement>().SetInitialPos(initialPlayerPos);
        ActivateTile(initialPlayerPos);
	}
    */
    public void GenerateMap()
    {
        Map newMap = mapLoader.loadedMap;
        if (transform.childCount == 0)
        {
            MapMatrixPos = new float[Width, Height];
            MapMatrixVec = new Vector3[Width, Height];
            MapMatrixGameObj = new GameObject[Width, Height];

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
                    if (layer.name == "Base")
                    {
                        switch (layer.data[pos])
                        {
                            case 20:
                                Debug.Log("WHAT " + y + ":" + x);
                                MapMatrixVec[x, y] = new Vector3(x, -y, 0f);
                                Debug.Log(x + y);
                                MapMatrixGameObj[x, y] = Instantiate(AllTileTypes[(int)Random.Range(0, AllTileTypes.Count)], MapMatrixVec[x, y], Quaternion.identity, layerEmptyObjt.transform);
                                break;
                        }
                    }
                    if (layer.name == "Interactive")
                    {
                        switch (layer.data[pos])
                        {
                            case 3:
                                initialPlayerPos = new Vector2(x,y);

                                var player = Instantiate(Player, new Vector3(initialPlayerPos.x, initialPlayerPos.y, 0), Quaternion.identity);
                                player.GetComponent<PlayerMovement>().SetInitialPos(initialPlayerPos);
                                ActivateTile(initialPlayerPos);
                                break;
                        }
                    }
                }
            }
        }
    }
    public bool CheckNextPos(Vector2 nextPos)
    {
        if(nextPos.x > Width || nextPos.x < 0 || nextPos.y > Height || nextPos.y < 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public void ActivateTile(Vector2 tilePosition)
    {
        MapMatrixGameObj[(int)tilePosition.x,(int)tilePosition.y].GetComponent<TileComponent>().active = true;
    }
    public void DeactivateTile(Vector2 tilePosition)
    {
        MapMatrixGameObj[(int)tilePosition.x, (int)tilePosition.y].GetComponent<TileComponent>().active = false;
    }

    public void DestroyMap()
	{
		for(int i =0;i < transform.childCount;i++)
		{
            if(Application.isPlaying)
			    Destroy(transform.GetChild(i).gameObject);
            else
                DestroyImmediate(transform.GetChild(i).gameObject);
        }
	}
}
