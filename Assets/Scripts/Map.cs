using System.Collections.Generic;
using System;
[System.Serializable]
public class Map
{
    public int height;
    public bool infinite;
    public List<Layer> layers;
    public int nextobjectid;
    public string orientation;
    public string renderorder;
    public string tiledversion;
    public int tileheight;
    public int tilewidth;
    public string type;
    public int version;
    public int width;
}
