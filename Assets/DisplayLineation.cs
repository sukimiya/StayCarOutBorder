using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisplayLineation {
    public LineRenderer line;
    public EdgeCollider2D edge;

    public GPoint start;
    public GPoint end;

    Scene scene;

    public GameObject displayObject;

    public static int numLine = 0;

    public static Shader shader = Shader.Find("Unlit/Color");
    public DisplayLineation(GameObject gameObject, GPoint _start,GPoint _end)
    {
        scene = gameObject.scene;
        displayObject = GameObject.Instantiate(gameObject);
        displayObject.name = "DisplayLineation_" + numLine.ToString();
        numLine++;
        line = displayObject.AddComponent<LineRenderer>();
        __init(_start, _end);
        setActivity();
    }

    void __init(GPoint _start, GPoint _end)
    {
        start = _start;
        end = _end;
    }
    public void setActivity()
    {
        line.material = new Material(shader);
        line.material.color = Color.white;
        line.positionCount = 2;
        line.startWidth = 0.03f;
        line.endWidth = 0.03f;
        line.startColor = Color.white;
        line.endColor = Color.white;
        line.SetPosition(0, GEOLocation.TranslateGPoint2Vector3(start));
        line.SetPosition(1, GEOLocation.TranslateGPoint2Vector3(end));
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
