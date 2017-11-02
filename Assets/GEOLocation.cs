using UnityEngine;
using System.Collections;

public class GEOLocation : MonoBehaviour
{
    public static GPoint center;
    public static double scale = 50000.0d;
    public GEOLocation(double lng, double lat)
    {
        center = new GPoint();
        center.lng = TO_GLNG(lng);
        center.lat = TO_GLAT(lat);
    }
    public static Vector3 TranslateGPoint2Vector3(GPoint gPoint)
    {
        return new Vector3(Convent.Tofloat((TO_GLNG(gPoint.lng) - TO_GLNG(center.lng))*scale), Convent.Tofloat(((TO_GLAT(gPoint.lat) - TO_GLAT(center.lat))*scale)));
    }
    public static Vector3 TranslateGPoint(GPoint p1,GPoint p2)
    {
        return new Vector3(Convent.Tofloat((TO_GLNG(p2.lng) - TO_GLNG(p1.lng)) * scale), Convent.Tofloat(((TO_GLAT(p2.lat) - TO_GLAT(p1.lat)) * scale)));
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

}
public class Convent
{
    public static float Tofloat(double d)
    {
        return float.Parse(d.ToString());
    }
}