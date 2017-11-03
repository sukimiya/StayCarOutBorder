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
        p.position = new GPoint();
        int fullyear = int.Parse( time.Substring(0, 4));
        int month = int.Parse(time.Substring(4, 2));
        int day = int.Parse(time.Substring(6, 2));
        int hour = int.Parse(time.Substring(8, 2));
        int minute = int.Parse(time.Substring(10, 2));
        int sec = int.Parse(time.Substring(12, 2));
        int msec = int.Parse(time.Substring(15, 2));
        p.time = new DateTime(fullyear,month,day,hour,minute,sec,msec*10);
        p.position.lng = p.lng = double.Parse(lng);
        p.position.lat = p.lat = double.Parse(lat);
        p.position.high = p.high = double.Parse(high);
        p.anglez = float.Parse(anglez);
        p.anglex = float.Parse(anglex);
        p.speed = float.Parse(speed);
        p.speedangle = float.Parse(speedangle);
        p.angley = float.Parse(angley);
        p.sat1 = float.Parse(sat1);
        p.sat2 = float.Parse(sat2);
        p.rotation = new Quaternion(p.anglex * Mathf.Rad2Deg/Mathf.PI, p.angley * Mathf.Rad2Deg / Mathf.PI, p.anglez * Mathf.Rad2Deg / Mathf.PI, 1.0f);
        return p;
    }
}
public class PP
{
    public DateTime time;
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
    public GPoint position;
    public Quaternion rotation;
}