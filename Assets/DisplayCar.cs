using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisplayCar {

    public LineRenderer line;
    public EdgeCollider2D edge;
    protected Vector2[] e2p = null;
    public GPoint[] points;
    public string type;

    public GameObject displayObject;
    public carData cdata;
    public static int numLine = 0;

    public static Shader shader = Shader.Find("Unlit/Color");


    public DisplayCar(GameObject gameObject, carData cardata,GPoint[] _points, string _type="S")
    {
        _points = cardata.body;
        cdata = cardata;
        Vector3 v1 = GEOLocation.TranslateGPoint2Vector3(cdata.antennaB);
        displayObject = new GameObject("DisplayLineation_" + numLine.ToString());
        displayObject.SetActive(true);
        Debug.Log(displayObject.name + ":" + displayObject.transform.position.x + "," + displayObject.transform.position.y);
        numLine++;
        line = displayObject.AddComponent<LineRenderer>();
        line.useWorldSpace = false;
        line.transform.position = v1;
        type = _type;
        __init(cardata.body);
        setActivity();
    }
    protected void __init(GPoint[] _points)
    {
        points = _points;
        edge = displayObject.AddComponent<EdgeCollider2D>();
        e2p = new Vector2[points.Length];
        int i = 0;
        foreach (GPoint p in points)
        {
            Vector3 v3 = GEOLocation.TranslateGPoint(cdata.antennaB, p);
            e2p[i] = new Vector2(v3.x, v3.y);
            i++;
        }
        edge.points = e2p;
    }
    public void setActivity()
    {
        line.material = new Material(shader);
        if (type == Lineation.TYPE_SOLID)
            line.material.color = Color.white;
        if (type == Lineation.TYPE_DASH)
            line.material.color = new Color(0, 0, 0, 0.5f);
        line.positionCount = points.Length;
        line.startWidth = 0.01f;
        line.endWidth = 0.01f;
        line.startColor = Color.white;
        line.endColor = Color.white;
        int i = 0;
        foreach (GPoint p in points)
        {
            line.SetPosition(i, GEOLocation.TranslateGPoint(cdata.antennaB, p));
            i++;
        }
        line.loop = true;
    }
}
