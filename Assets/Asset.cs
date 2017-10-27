using UnityEngine;
using System.Collections.Generic;
public class Asset
{
    public const byte TYPE_JSON = 1;
    public const byte TYPE_GAMEOBJECT = 2;
    public Asset()
    {
        //default type is gameobject for json load
        Type = TYPE_GAMEOBJECT;
    }
    public byte Type
    {
        get;
        set;
    }
    public string Name
    {
        get;
        set;
    }
    public string Source
    {
        get;
        set;
    }
    public double[] Bounds
    {
        get;
        set;
    }

    public double[] Position
    {
        get;
        set;
    }
    public double[] Rotation
    {
        get;
        set;
    }
    public List<Asset> AssetList
    {
        get;
        set;
    }
    public bool isLoadFinished
    {
        get;
        set;
    }
    public WWW www
    {
        get;
        set;
    }
    public GameObject gameObject
    {
        get;
        set;
    }
}