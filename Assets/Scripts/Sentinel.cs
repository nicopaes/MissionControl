using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TileComponent))]
public class Sentinel : MonoBehaviour
{
    public CreateMap cmap;
    public List<Vector2> dir = new List<Vector2>{Vector2.right, Vector2.left, Vector2.up, Vector2.down };

    private int intDir = 0;

    private void OnEnable()
    {
        cmap = GameObject.FindObjectOfType<CreateMap>();
        StartCoroutine(ChangeDir());
    }
    void WatchTile(Vector2 dir, Vector2 lastDir)
    {
        Vector2 tileToWatchVec = GetComponent<TileComponent>().position + dir;
        Vector2 tileToUnwatchVec = GetComponent<TileComponent>().position + lastDir;

        int y = (int)tileToWatchVec.y;
        int x = (int)tileToWatchVec.x;

        int ly = (int)tileToUnwatchVec.y;
        int lx = (int)tileToUnwatchVec.x;

        int sum = x + y * 5;
        int lsum = lx + ly * 5;

        Debug.Log("COUNT " + sum);
        Debug.Log("LCOUNT " + lsum);

        if (cmap.MapGameObjList[sum].GetComponent<TileComponent>().path)
        {
            cmap.MapGameObjList[lsum].GetComponent<TileComponent>().danger = false;
            cmap.MapGameObjList[sum].GetComponent<TileComponent>().danger = true;
        }
    }
    IEnumerator ChangeDir()
    {
        while(true)
        {
            WatchTile(dir[intDir],intDir==0?dir[3]:dir[intDir-1]);
            yield return new WaitForSeconds(4);
            intDir++;
            if (intDir > dir.Count -1)
                intDir = 0;
        }
    }
}
