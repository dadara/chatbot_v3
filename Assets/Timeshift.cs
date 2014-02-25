using UnityEngine;
using System.Collections;

public class Timeshift : MonoBehaviour 
{
	GameObject timeshift;
	// Use this for initialization
	void Start () 
	{
		timeshift = GameObject.Find("Timeshift");
		timeshift.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	public void ActivateTimeShift(bool active)
	{
		timeshift.SetActive(active);
	}
}
