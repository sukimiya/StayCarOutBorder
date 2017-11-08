using System;
using UnityEngine;

[System.Serializable]
public class PathPoint {

    public string head;
    public string time;
    public string lng;
    public string lat;
    public string high;
    public string anglez;
    public string anglex;
    public string speed;
    public string speedangle;
    public string angley;
    public string sat1;
    public string sat2;
    public string satn1;
    public string satn2;
    public string ve;
    public string vn;
    public string vecc;
    public string se;
    public string sn;
    public string secc;
    public string r1;
    public string r2;
    public string check;
    public PP parsePP()
    {
        PP p = new PP();
        p.position = new Vector3();
        float fullyear = float.Parse( time.Substring(0, 4));
        float month = float.Parse(time.Substring(4, 2));
        float day = float.Parse(time.Substring(6, 2));
        float hour = float.Parse(time.Substring(8, 2));
        float minute = float.Parse(time.Substring(10, 2));
        float sec = float.Parse(time.Substring(12, 2));
        float msec = float.Parse(time.Substring(15, 2));
        p.time = double.Parse(( msec/100.0f+sec+minute*60.0f+hour*3600.0f).ToString());
        p.lng = double.Parse(lng);
        p.lat = double.Parse(lat);
        p.high = double.Parse(high);
        p.position = GEOLocation.TranslateGPoint2Vector3(new GPoint(p.lng, p.lat, p.high));
        p.anglez = float.Parse(anglez);
        p.anglex = float.Parse(anglex);
        p.speed = float.Parse(speed);
        p.speedangle = float.Parse(speedangle);
        p.angley = float.Parse(angley);
        p.sat1 = float.Parse(sat1);
        p.sat2 = float.Parse(sat2);
		p.rotation = new Quaternion();
        return p;
    }
}
public class PP
{
    public double time;
    public double lng;
    public double lat;
    public double high;
    public float anglez;
    public float anglex;
    public float speed;
    public float speedangle;
    public float angley;
    public float sat1;
    public float sat2;
    public int satn1;
    public int satn2;
    public float ve;
    public float vn;
    public float vecc;
    public float se;
    public float sn;
    public float secc;
    public string r1;
    public string r2;
    public string check;
    public Vector3 position;
    public Quaternion rotation;
    public static double F2d(float f)
    {
        return double.Parse(f.ToString());
    }
    public static float D2f(double d)
    {
        return float.Parse(d.ToString());
    }
    public static float I2F(int i)
    {
        return float.Parse(i.ToString());
    }
}