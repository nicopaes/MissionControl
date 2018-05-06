using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileComponent : MonoBehaviour {

    public bool active;
    public bool path;
    public bool danger;

    public Vector2 position;

    private void Update()
    {
       if(active)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else if(danger)
        {
            GetComponent<SpriteRenderer>().color = Color.yellow;
        }
       else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
    public void SetPosition(Vector2 startPos)
    {
        position = startPos;
    }
}
