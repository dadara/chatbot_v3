using UnityEngine;
using System.Collections;

public class DragPhoto : MonoBehaviour 
{

	private Vector3 screenPoint;
	private Vector3 offset;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnPress (bool isPressed)
	{
		if (isPressed)
		{
			Debug.Log("Is Pressed");
		}
	}

}
