using UnityEngine;
using System.Collections;

public class AlertClose : MonoBehaviour {

	public GameObject alerts;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnPress (bool isPressed)
	{
		if (isPressed)
		{
			alerts.SetActive(false);
		}
	}
}
