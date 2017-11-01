using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtom : MonoBehaviour {

    public GameObject prefabToCreate;
    public int numberToCreate;
    public List<DisplayLineation> lines = null;
    public bool isDrawLines = false;
    // Use this for initialization
    void Start () {
        new GEOLocation(117.25251548f, 31.71685460f);
    }
	
	// Update is called once per frame
	void Update () {
        
        if (prefabToCreate != null)
        {
            //Camera ca = GameObject.Find("Main Camera").GetComponent<Camera>();
            //ca.transform.SetPositionAndRotation(lines[0].displayObject.transform.position, ca.transform.rotation);
            foreach (DisplayLineation gp in lines)
            {
                gp.Update();
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
            Camera ca = GameObject.Find("Main Camera").GetComponent<Camera>();
            foreach (Lineation gp in path.lines)
            {
                lines.Add(new DisplayLineation(go, gp.points,gp.type));
                last = gp;
            }
            prefabToCreate = lines[0].displayObject;
            //ca.ScreenToViewportPoint(GEOLocation.TranslateGPoint2Vector3(lines[0].points[0]));
            //ca.transform.Translate(lines[0].displayObject.transform.position,Space.Self);
            //ca.transform.SetPositionAndRotation(lines[0].displayObject.transform.position, ca.transform.rotation);
            Vector3 v1 = GEOLocation.TranslateGPoint2Vector3(path.lines[0].points[0]);
            Vector3 v2 = new Vector3(0 - v1.x, 0 - v1.y, v1.z);
            Vector3 v3 =new Vector3(v1.x,v1.y, ca.transform.position.z);
            ca.transform.position = v3;

        }
    }
}
