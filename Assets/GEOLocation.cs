using UnityEngine;
using System;
using System.Collections;

public class GEOLocation : MonoBehaviour
{
    public static GPoint center;
    public static double scale = 500.0d;
    public GEOLocation(double lng, double lat)
    {
        center = new GPoint();
        center.lng = TO_GLNG(lng);
        center.lat = TO_GLAT(lat);
    }
    public static Vector3 TranslateGPoint2Vector3(GPoint gPoint)
    {
        //return new Vector3(Convent.Tofloat((TO_GLNG(gPoint.lng) - TO_GLNG(center.lng))*scale), Convent.Tofloat(((TO_GLAT(gPoint.lat) - TO_GLAT(center.lat))*scale)));
        return TranslateGPoint(center,gPoint);
    }
    public static Vector3 TranslateGPoint(GPoint p2,GPoint p1)
    {
        //return new Vector3(Convent.Tofloat((TO_GLNG(p2.lng) - TO_GLNG(p1.lng)) * scale), Convent.Tofloat(((TO_GLAT(p2.lat) - TO_GLAT(p1.lat)) * scale)));
        double dy = distance(p1.lat, p1.lng, p2.lat, p1.lng);
        double dx = distance(p1.lat, p1.lng, p1.lat, p2.lng);

        return new Vector3(float.Parse((dx * scale).ToString()), float.Parse((dy * scale).ToString()));
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
        double dy = distance(center.lat, center.lng, gp.lat, center.lng);
        double dx = distance(center.lat, center.lng, center.lat, gp.lng);

        return new Vector3(float.Parse((dx * scale).ToString()), float.Parse((dy * scale).ToString()));
    }
    public static double distance(double lat1, double lon1, double lat2, double lon2)
    {
        double theta = lon1 - lon2;
        double dist = Math.Sin(lat1 * Mathf.Deg2Rad) * Math.Sin(lat2 * Mathf.Deg2Rad)
                    + Math.Cos(lat1 * Mathf.Deg2Rad) * Math.Cos(lat2 * Mathf.Deg2Rad)
                    * Math.Cos(theta * Mathf.Deg2Rad);
        dist = Math.Acos(dist);
        dist = dist * Mathf.Rad2Deg;
        double miles = dist * 60 * 1.1515d;
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