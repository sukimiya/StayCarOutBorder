using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisplayLineation {
    public LineRenderer line;
    public EdgeCollider2D edge;
    protected Vector2[] e2p = null;
    public GPoint[] points;
    public string type;
    public int loop;

    public GameObject displayObject;

    public static int numLine = 0;

    public static Shader shader = Shader.Find("Unlit/Color");
    public DisplayLineation(GameObject gameObject, GPoint[] _points, string _type,int _loop=0)
    {
        loop = _loop;
        displayObject = new GameObject("DisplayLineation_" + numLine.ToString());
        numLine++;
        line = displayObject.AddComponent<LineRenderer>();
        line.useWorldSpace = false;
        type = _type;
        __init(_points);
        setActivity();
    }

    protected void __init(GPoint[] _points)
    {
        points = _points;
        edge = displayObject.AddComponent<EdgeCollider2D>();
        e2p = new Vector2[points.Length];
        int i = 0;
        foreach(GPoint p in points)
        {
            Vector3 v3 = GEOLocation.TranslateGPoint(_points[0], p);
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
        line.positionCount = points.Length;
        line.startWidth = 0.01f;
        line.endWidth = 0.01f;
        line.startColor = Color.white;
        line.endColor = Color.white;
        for(int i=1;i< points.Length;i++)
        {
            GPoint p = points[i];
            line.SetPosition(i, GEOLocation.TranslateGPoint(points[0], p));
        }
        if (loop==1)
            line.loop = true;
        Vector3 v1 = GEOLocation.TranslateGPoint2Vector3(points[0]);

        //displayObject.transform.position = v1;
        line.transform.position = v1;
        //Debug.Log(displayObject.name + ":" + v1.x + "," + v1.y);
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	public void Update () {

    }
}
