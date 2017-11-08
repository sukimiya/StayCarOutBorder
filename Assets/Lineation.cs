using System;

[System.Serializable]
public class Lineation
{
    public const string TYPE_SOLID = "S";
    public const string TYPE_DASH = "D";
    public string type;
    public int loop = 0;
    public GPoint[] points;
}
[System.Serializable]
public class GPoint
{
    public int id;
    public string sn;
    public double lng;
    public double lat;
    public double high;
    public GPoint(double _lng,double _lat,double _high)
    {
        lng = _lng;
        lat = _lat;
        high = _high;
    }
    public GPoint()
    {
        id = -1;
        sn = "";
        lng = 0.000000d;
        lat = 0.000000d;
        high = 0.000000d;
    }
}