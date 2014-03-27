using UnityEngine;
using System.Collections;

public class StartscreenEndBtn : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick ()
	{
		GameObject startscreen = GameObject.Find("Startscreen");
		startscreen.SetActive(false);
	}
}
