using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisplayLineation {
    public LineRenderer line;
    public EdgeCollider2D edge;
    Vector2[] e2p = null;
    public GPoint[] points;
    public string type;

    public GameObject displayObject;

    public static int numLine = 0;

    public static Shader shader = Shader.Find("Unlit/Color");
    public DisplayLineation(GameObject gameObject, GPoint[] _points, string _type)
    {
        Vector3 v1 = GEOLocation.TranslateGPoint2Vector3(_points[0]);
        displayObject = new GameObject("DisplayLineation_" + numLine.ToString());
        displayObject.transform.position = v1;
        displayObject.SetActive(true);
        Debug.Log(displayObject.name + ":" + displayObject.transform.position.x + "," + displayObject.transform.position.y);
        numLine++;
        line = displayObject.AddComponent<LineRenderer>();
        type = _type;
        __init(_points);
        setActivity();
    }

    void __init(GPoint[] _points)
    {
        points = _points;
        edge = displayObject.AddComponent<EdgeCollider2D>();
        e2p = new Vector2[points.Length];
        int i = 0;
        foreach(GPoint p in points)
        {
            Vector3 v3 = GEOLocation.TranslateGPoint2Vector3(p);
            e2p[i] = new Vector2(v3.x, v3.y);
            i++;
        }
        edge.points = e2p;
    }
    public void setActivity()
    {
        line.material = new Material(shader);
        if(type == Lineation.TYPE_SOLID)
            line.material.color = Color.white;
        if (type == Lineation.TYPE_DASH)
            line.material.color = new Color(0, 0, 0, 0.5f);
        line.positionCount = 2;
        line.startWidth = 0.01f;
        line.endWidth = 0.01f;
        line.startColor = Color.white;
        line.endColor = Color.white;
        int i = 0;
        GPoint first = null;
        GPoint last = null;
        foreach (GPoint p in points)
        {
            if(first == null)
            {
                //displayObject.transform.Translate(GEOLocation.TranslateGPoint2Vector3(p));
                //line.SetPosition(i, new Vector3(0,0,0));
                first = p;
                last = p;
            }
            else
            {
                //line.SetPosition(i, GEOLocation.TranslateGPoint(last, p));
                last = p;
            }
            line.SetPosition(i, GEOLocation.TranslateGPoint2Vector3(p));
            i++;
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	public void Update () {
        Vector3 v1 = GEOLocation.TranslateGPoint2Vector3(points[0]);
        displayObject.transform.Translate(v1);
    }
}
