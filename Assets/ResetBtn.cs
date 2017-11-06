using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResetBtn : MonoBehaviour , IPointerClickHandler{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void OnPointerClick(PointerEventData eventData)
	{
		GameObject go = GameObject.Find ("Button");
		go.SendMessage ("restart");
	}
}
