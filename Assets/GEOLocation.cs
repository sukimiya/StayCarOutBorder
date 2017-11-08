using UnityEngine;
using System;
using System.Collections;

public class GEOLocation : MonoBehaviour
{
    public static GPoint center;
    public static double scale = 69900d;
    public GEOLocation(double _lng, double _lat)
    {
        center = new GPoint(_lng,_lat,0.0d);
    }
    public static Vector3 GlobalVector3(GPoint p)
    {
        return new Vector3(float.Parse((p.lng%1.0d* scale).ToString()), float.Parse((p.lat%1.0d* scale).ToString()),float.Parse(p.high.ToString()));
    }
    public static Vector3 TranslateGPoint2Vector3(GPoint gPoint)
    {
        return new Vector3(float.Parse((((gPoint.lng) - (center.lng)) * scale).ToString()), float.Parse(((((gPoint.lat) - (center.lat)) * scale)).ToString()),float.Parse(gPoint.high.ToString()));
        //return new Vector3(Convent.Tofloat((TO_GLNG(gPoint.lng) - TO_GLNG(center.lng))*scale), Convent.Tofloat(((TO_GLAT(gPoint.lat) - TO_GLAT(center.lat))*scale)));
        //Vector3 v1 = TranslateGPoint(center, gPoint);
        //v1.z = float.Parse((gPoint.high).ToString());
        // return v1;
    }
    public static Vector3 TranslateGPoint(GPoint p1,GPoint p2)
    {
        return new Vector3(float.Parse((((p2.lng) - (p1.lng)) * scale).ToString()), float.Parse(((((p2.lat) - (p1.lat)) * scale)).ToString()));
        //return new Vector3(Convent.Tofloat((TO_GLNG(p2.lng) - TO_GLNG(p1.lng)) * scale), Convent.Tofloat(((TO_GLAT(p2.lat) - TO_GLAT(p1.lat)) * scale)));
        //double dy = distance(p1.lat, p1.lng, p1.lat, p2.lng);
        //double dx = distance(p1.lat, p1.lng, p2.lat, p1.lng);
        //Vector3 v1 = new Vector3(float.Parse((dx * scale).ToString()), float.Parse((dy * scale).ToString()));
        //v1.z = float.Parse(((p2.high-p1.high)).ToString());
        //return v1;
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static double TO_BLNG(double lng){return lng+0.0065d;}
    public static double TO_BLAT(double lat) {return lat+0.0060d;}
    public static double TO_GLNG(double lng) {return lng-0.0065d;}
    public static double TO_GLAT(double lat) {return lat-0.0060d;}

    public static Vector3 GPoint2Vector3(GPoint gp)
    {
        double dy = distance(center.lat, center.lng, center.lat, gp.lng);
        double dx = distance(center.lat, center.lng, gp.lat, center.lng);

        return new Vector3(float.Parse((dx * scale).ToString()), float.Parse((dy * scale).ToString()));
    }
    public static double distance(double lat2, double lon2, double lat1, double lon1)
    {
        double theta = lon1 - lon2;
        double dist = Math.Sin(lat1 * Mathf.Deg2Rad) * Math.Sin(lat2 * Mathf.Deg2Rad)
                    + Math.Cos(lat1 * Mathf.Deg2Rad) * Math.Cos(lat2 * Mathf.Deg2Rad)
                    * Math.Cos(theta * Mathf.Deg2Rad);
        dist = Math.Acos(dist);
        dist = dist * Mathf.Rad2Deg;
        double miles = dist * 60.0d * 1.1515d;
        return miles;
    }
}
public class Convent
{
    public static float Tofloat(double d)
    {
        return float.Parse(d.ToString());
    }
}