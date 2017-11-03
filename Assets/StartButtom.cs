using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartButtom : MonoBehaviour, IPointerClickHandler{

    public GameObject prefabToCreate;
    public int numberToCreate;
    public List<DisplayLineation> lines = null;
    public bool isDrawLines = false;
    public DisplayCar car = null;
    public Camera ca = null;
    public RunPath runpath;
    public int startTime;
    public float lastTime;
    Vector3 v1, v2, v3;
    // Use this for initialization
    void Start () {
        new GEOLocation(117.25251548f, 31.71685460f);
    }
    public int steps = 0;
	// Update is called once per frame
	void Update () {
        
        if (car != null)
        {
            //ca.transform.SetPositionAndRotation(lines[0].displayObject.transform.position, ca.transform.rotation);
            v1 = car.line.transform.position;
            if((ca.transform.position.x!=v1.x&& ca.transform.position.y != v1.y))
            {
                //v3 = new Vector3(v1.x, v1.y, ca.transform.position.z);
                //Tween.MoveTo(ca.gameObject, v3, 0.2f);
            }
            if(runpath!=null)


                if (steps < runpath.path.Length)
                {
                    GameObject otext =  GameObject.Find("OutText");
                    Text txt = otext.GetComponent<Text>();
                    PP first = runpath.path[0].parsePP();
                    PP p = runpath.path[steps].parsePP();
                    float curTime = float.Parse(getTimestamp(p).ToString()) / 1000.0f;
                    float timed = (getTimestamp(DateTime.Now) - startTime) / 1000.0f;
                    if((curTime - lastTime)==0|| (curTime-lastTime) <= timed)
                    {
                       
                        float duration = curTime - lastTime;
                        Vector3 m1 = GEOLocation.TranslateGPoint2Vector3(p.position);
                        //car.line.transform.position = m1;
                        //iTween.MoveTo(ca.gameObject, m1, 0.2f);
                        car.line.transform.position = m1;
                        car.line.transform.rotation = new Quaternion(p.anglex, p.angley, p.anglez, 0);
                        steps++;
                        lastTime = curTime;
                        startTime = getTimestamp(DateTime.Now);
                        txt.text = duration.ToString();
                    }
                    
                }
        }
	}
    private int getTimestamp(PP p)
    {
        return (p.time.Millisecond + p.time.Second * 1000 + p.time.Minute * 600000 + p.time.Hour * 3600000);
    }
    private int getTimestamp(DateTime dt)
    {
        return (dt.Millisecond + dt.Second * 1000 + dt.Minute * 600000 + dt.Hour * 3600000);
    }
    private void OnGUI()
    {
        if (Input.GetMouseButtonDown(0))
        {

            
        }
    }
    void createGame()
    {
        GPath path = PathLoader.LoadJsonFromFile();
        lines = new List<DisplayLineation>(path.lines.Length);
        Lineation last = null;
        GameObject go = GameObject.Find("lineTemplete");
        ca = GameObject.Find("Main Camera").GetComponent<Camera>();
        foreach (Lineation gp in path.lines)
        {
            lines.Add(new DisplayLineation(go, gp.points, gp.type));
            last = gp;
        }
        //iTween.MoveTo(ca.gameObject, v3, 6.6f);

        carData cardata = PathLoader.loadCarData();
        car = new DisplayCar(go, cardata, new GPoint[0]);
        prefabToCreate = car.displayObject;
        //ca.WorldToScreenPoint(GEOLocation.TranslateGPoint2Vector3(car.cdata.antennaB));
        v1 = car.line.transform.position;
        v3 = new Vector3(v1.x, v1.y, ca.transform.position.z);

        iTween.MoveTo(ca.gameObject, v3, 0.2f);

        runpath = PathLoader.loadPath();

        startTime = getTimestamp(DateTime.Now);
        PP first = runpath.path[0].parsePP();
        lastTime = float.Parse(getTimestamp(first).ToString()) / 1000.0f;
        v2 = GEOLocation.TranslateGPoint2Vector3(first.position);
        //iTween.MoveTo(car.displayObject, v2, 0.2f);
        steps++;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        createGame();
    }
}
