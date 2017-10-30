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
        GEOLocation.center = new GPoint();
        GEOLocation.center.lng = 117.25251548f;
        GEOLocation.center.lat = 31.71685460f;

    }
	
	// Update is called once per frame
	void Update () {
        
	}
    private void OnGUI()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Lineation lin = PathLoader.LoadJsonFromFile();
            lines = new List<DisplayLineation>(lin.points.Length-1);
            GPoint last = null;
            GameObject go = GameObject.Find("GameObject");
            foreach (GPoint gp in lin.points)
            {
                if (last != null)
                {
                    
                    lines.Add(new DisplayLineation(go,last, gp));
                }
                last = gp;
            }

        }
    }
}
