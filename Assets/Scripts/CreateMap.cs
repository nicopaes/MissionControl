using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMap : MonoBehaviour {

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



	void Start ()
	{
		GenerateMap();		
	}
	
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
			Destroy(transform.GetChild(i).gameObject);
		}
	}
}
