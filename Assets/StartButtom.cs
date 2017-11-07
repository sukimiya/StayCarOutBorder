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
	bool isPause = false;
    Vector3 v1, v2, v3;
    // Use this for initialization
    void Start () {
        new GEOLocation(117.25251548f, 31.71685460f);
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
					float curTime = float.Parse (getTimestamp (p).ToString ()) / 1000.0f;
					float timed = (getTimestamp (DateTime.Now) - startTime) / 1000.0f;
					if ((curTime - lastTime) == 0 || (curTime - lastTime) <= timed) {
	                       
						float duration = curTime - lastTime;
						//car.line.transform.position = m1;
						//iTween.MoveTo(ca.gameObject, m1, 0.2f);

						car.line.transform.position = p.position;
						Quaternion q = Quaternion.AngleAxis ((295.27f - p.anglez + 180.0f) % 360.0f, Vector3.forward);
						car.line.transform.rotation = q;
						steps++;
						lastTime = curTime;
						startTime = getTimestamp (DateTime.Now);
						txt.text = p.time.ToString ();
						txt2.text = p.anglez.ToString ();
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
		steps = 0;
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
        v2 = first.position;
        //iTween.MoveTo(car.displayObject, v2, 0.2f);
		for (float i = 0; i < 360.0f; i+=1.0f) {
			Quaternion q = Quaternion.AngleAxis (i, Vector3.forward);

			Debug.Log("["+i.ToString()+"] x:"+q.x.ToString()+" y:"+q.y.ToString()+" z:"+q.z.ToString());
		}
        steps++;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        createGame();
    }
}
