using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameLoader;

public class StartButtom : MonoBehaviour {

    public GameObject prefabToCreate;
    public int numberToCreate;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(LoadManager.instance.progress>0)
            Debug.Log(LoadManager.instance.progress);
	}
    private void OnGUI()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Start");
            LoadManager.instance.RegisterEnumerator(EnumeratorToLoad());
            LoadManager.instance.LoadRegistered(OnLoadComplete);
            
        }
    }
    private void OnLoadComplete()
    {
        Debug.Log("Load Complete!");
        Debug.Log(numberToCreate);
    }
    private IEnumerator EnumeratorToLoad()
    {
        for (int i = 0; i < numberToCreate; i++)
        {
            Instantiate(prefabToCreate);
            yield return null;
        }

        // Can call into other enumerators
        yield return LoadOtherEnumerator();
    }
    private IEnumerator LoadOtherEnumerator()
    {
        // Can do whatever loading here too
        yield return null;
    }
}
