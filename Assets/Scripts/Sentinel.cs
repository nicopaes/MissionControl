using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TileComponent))]
public class Sentinel : MonoBehaviour
{
    public GameObject cmap;

    public Transform BG;

    public float rotateTime;
    public GameObject dangerProbe;

    public float dangerTime = 2;

    [Space]
    public List<Vector2> dir = new List<Vector2>{Vector2.right, Vector2.left, Vector2.up, Vector2.down };   
    [SerializeField]
    private int intDir = 0;

    private void OnEnable()
    {
        cmap = GameObject.Find("MAP");
        BG = cmap.transform.GetChild(0);

        StartCoroutine(ChangeDir(rotateTime));
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

        //Debug.Log("COUNT " + sum);
        //Debug.Log("LCOUNT " + lsum);
        /*
        if (cmap.MapGameObjList[sum].GetComponent<TileComponent>().path)
        {
            cmap.MapGameObjList[lsum].GetComponent<TileComponent>().danger = false;
            cmap.MapGameObjList[sum].GetComponent<TileComponent>().danger = true;
        }*/
    }

    void WatchTile(Vector2 dir)
    {
        dangerProbe.transform.position = (Vector2)transform.position + dir;
        //dangerProbe.transform.localPosition = dir;        
    }
    private void Update()
    {
        for (int i = 0; i < BG.childCount; i++)
        {
            if ((Vector2)BG.GetChild(i).position == (Vector2)this.transform.position + dir[intDir])//dangerProbe.transform.position)
            {
                //BG.GetChild(i).GetComponent<TileComponent>().danger = true;
                BG.GetChild(i).GetComponent<TileComponent>().turnDanger(dangerTime);
            }
            else
            {
                //BG.GetChild(i).GetComponent<TileComponent>().danger = false;
            }

        }
    }
    IEnumerator ChangeDir(float rotateTime)
    {
        while(true)
        {
            WatchTile(dir[intDir]);
            yield return new WaitForSeconds(rotateTime);
            intDir++;
            if (intDir > dir.Count -1)
                intDir = 0;
        }
    }
}
