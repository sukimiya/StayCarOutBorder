using System;

[System.Serializable]
public class Lineation
{
    public const string TYPE_SOLID = "S";
    public const string TYPE_DASH = "D";
    public string type;
    public GPoint[] points;
}
[System.Serializable]
public class GPoint
{
    public int id;
    public int sn;
    public float lng;
    public float lat;
    public float high;
    public GPoint()
    {
        id = -1;
        sn = -1;
        lng = 0.000000f;
        lat = 0.000000f;
        high = 0.000000f;
    }

    public static implicit operator GPoint(GEOLocation v)
    {
        throw new NotImplementedException();
    }
}