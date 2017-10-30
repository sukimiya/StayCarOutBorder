using UnityEngine;
using System.Collections;

public class GEOLocation : MonoBehaviour
{
    public static GPoint center;
    public static float scale = 10000.0f;
    public GEOLocation(float lng,float lat)
    {
        center = new GPoint();
        center.lng = lng;
        center.lat = lat;
    }
    public static Vector3 TranslateGPoint2Vector3(GPoint gPoint)
    {
        return new Vector3((gPoint.lng - center.lng)*scale, (gPoint.lat - center.lat)*scale);
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
