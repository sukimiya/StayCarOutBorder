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
    public float startTime;
    public float lastTime;
	bool isPause = false;
    Vector3 v1, v2, v3;
    // Use this for initialization
    void Start () {
        new GEOLocation(117.25251548d, 31.71685460d);
    }
    public int steps = 0;
	// Update is called once per frame
	void Update () {
		if (!isPause)
			if (car != null) {
				//ca.transform.SetPositionAndRotation(lines[0].displayObject.transform.position, ca.transform.rotation);
				v1 = car.line.transform.position;
				if ((ca.transform.position.x != v1.x && ca.transform.position.y != v1.y)) {
					//v3 = new Vector3(v1.x, v1.y, ca.transform.position.z);
					//Tween.MoveTo(ca.gameObject, v3, 0.2f);
				}
				if (runpath != null)
				if (steps < runpath.path.Length) {
					GameObject otext = GameObject.Find ("OutText");
					GameObject otext2 = GameObject.Find ("OutText2");
					Text txt = otext.GetComponent<Text> ();
					Text txt2 = otext2.GetComponent<Text> ();
					PP first = runpath.path [0].parsePP ();
					PP p = runpath.path [steps].parsePP ();
                    PP pp = runpath.path[steps-1].parsePP();
                        float curTime = float.Parse((p.time - pp.time).ToString());
					float timed = (getTimestamp (DateTime.Now) - startTime);
					if (timed>= curTime) {
	                       
						float duration = curTime - lastTime;
                            //car.line.transform.position = m1;

                        iTween.MoveTo(ca.gameObject, GetTweenMove( new Vector3( p.position.x,p.position.y,-10), curTime));
                            iTween.MoveTo(car.displayObject, GetTweenMove(p.position, curTime));
                            car.line.transform.position = p.position;
						Quaternion q = Quaternion.AngleAxis ((295.27f - p.anglez + 180.0f) % 360.0f, Vector3.forward);
						car.line.transform.rotation = q;
                            iTween.RotateTo(car.displayObject, GetTweenRotation( q.eulerAngles, curTime));
                        Debug.Log("duration:" + (curTime).ToString()+" time Delta:"+ timed.ToString());
						steps++;
						lastTime = curTime;
						startTime = getTimestamp (DateTime.Now);
						txt.text = p.time.ToString ();
						txt2.text = p.anglez.ToString ();
					}
	                    
				}
			}
	}
    Hashtable GetTweenRotation(Vector3 p,float time)
    {
        Hashtable h = new Hashtable();
        h.Add("time",time);
        h.Add("rotation", p);
        h.Add("easetype", iTween.EaseType.linear);
        return h;
    }
    Hashtable GetTweenMove(Vector3 p, float time)
    {
        Hashtable h = new Hashtable();
        h.Add("time", time);
        h.Add("position", p);
        h.Add("easetype", iTween.EaseType.linear);
        return h;
    }

    private float getTimestamp(DateTime dt)
    {
        return float.Parse((dt.Millisecond + dt.Second * 1000 + dt.Minute * 600000 + dt.Hour * 3600000).ToString()) / 1000.0f;
    }
    private void OnGUI()
    {
        if (Input.GetMouseButtonDown(0))
        {

            
        }
    }
	void gamePause(){
		if (isPause) {
			Time.timeScale = 1;
			isPause = false;
		} else {
			Time.timeScale = 0;
			isPause = true;
		}
	}
	void restart(){
        steps = 1;
    }
    void createGame()
    {
		steps = 0;
        GPath path = PathLoader.LoadJsonFromFile();
        lines = new List<DisplayLineation>(path.lines.Length);
        Lineation last = null;
        GameObject go = GameObject.Find("lineTemplete");
        ca = GameObject.Find("Main Camera").GetComponent<Camera>();
        foreach (Lineation gp in path.lines)
        {
            
            lines.Add(new DisplayLineation(go, gp.points, gp.type,gp.loop));
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
        lastTime = float.Parse(first.time.ToString());
        v2 = first.position;
        steps++;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        createGame();
    }
}
