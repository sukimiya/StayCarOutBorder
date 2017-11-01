using UnityEngine;
using System.Collections;

public class GEOLocation : MonoBehaviour
{
    public static GPoint center;
    public static float scale = 50000.0f;
    public GEOLocation(float lng,float lat)
    {
        center = new GPoint();
        center.lng = TO_GLNG(lng);
        center.lat = TO_GLAT(lat);
    }
    public static Vector3 TranslateGPoint2Vector3(GPoint gPoint)
    {
        return new Vector3((TO_GLNG(gPoint.lng) - TO_GLNG(center.lng))*scale, ((TO_GLAT(gPoint.lat) - TO_GLAT(center.lat))*scale));
    }
    public static Vector3 TranslateGPoint(GPoint p1,GPoint p2)
    {
        return new Vector3((TO_GLNG(p2.lng) - TO_GLNG(p1.lng)) * scale, ((TO_GLAT(p2.lat) - TO_GLAT(p1.lat)) * scale));
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static float TO_BLNG(float lng){return lng+0.0065f;}
    public static float TO_BLAT(float lat) {return lat+0.0060f;}
    public static float TO_GLNG(float lng) {return lng-0.0065f;}
    public static float TO_GLAT(float lat) {return lat-0.0060f;}

}
