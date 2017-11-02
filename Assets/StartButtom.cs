using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtom : MonoBehaviour {

    public GameObject prefabToCreate;
    public int numberToCreate;
    public List<DisplayLineation> lines = null;
    public bool isDrawLines = false;
    public DisplayCar car = null;
    public Camera ca = null;
    Vector3 v1, v2, v3;
    // Use this for initialization
    void Start () {
        new GEOLocation(117.25251548f, 31.71685460f);
    }
	
	// Update is called once per frame
	void Update () {
        
        if (car != null)
        {
            //ca.transform.SetPositionAndRotation(lines[0].displayObject.transform.position, ca.transform.rotation);
            v1 = car.line.transform.position;
            if((ca.transform.position.x!=v1.x&& ca.transform.position.y != v1.y))
            {
                v3 = new Vector3(v1.x, v1.y, ca.transform.position.z);
                iTween.MoveTo(ca.gameObject, v3, 0.2f);
            }
        }
	}
    private void OnGUI()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GPath path = PathLoader.LoadJsonFromFile();
            lines = new List<DisplayLineation>(path.lines.Length);
            Lineation last = null;
            GameObject go = GameObject.Find("lineTemplete");
            ca = GameObject.Find("Main Camera").GetComponent<Camera>();
            foreach (Lineation gp in path.lines)
            {
                lines.Add(new DisplayLineation(go, gp.points,gp.type));
                last = gp;
            }
            //iTween.MoveTo(ca.gameObject, v3, 6.6f);
            
            carData cardata = PathLoader.loadCarData();
            car = new DisplayCar(go, cardata,new GPoint[0]);
            prefabToCreate = car.displayObject;
            //ca.WorldToScreenPoint(GEOLocation.TranslateGPoint2Vector3(car.cdata.antennaB));

        }
    }
}
