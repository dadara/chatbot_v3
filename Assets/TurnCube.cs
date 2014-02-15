using UnityEngine;
using System.Collections;

public class TurnCube : MonoBehaviour 
{
	private bool selected = false;
	private bool rotating = false;
	private float speed = 5f;

	Quaternion oldRotation;
	Quaternion lastRotation;
	Quaternion newRotation;

	public bool Selected {
		get {
			return selected;
		}
	}

	public void SetSelected (bool boolean)
	{
		selected = boolean;
	}
	
	void Start () 
	{
		selected = false;
		rotating = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(selected)
		{

			if(Input.GetKeyDown ("up"))
			{
				StartCoroutine("RotateUp");
			} else if(Input.GetKeyDown ("down"))
			{
				StartCoroutine("RotateDown");
			} else if(Input.GetKeyDown ("left"))
			{
				StartCoroutine("RotateLeft");
			} else if(Input.GetKeyDown ("right"))
			{
				StartCoroutine("RotateRight");
			}
		}
	}

	IEnumerator RotateLeft()
	{
		if(!rotating) 
		{
			rotating = true;
			
			oldRotation = transform.rotation; 
			newRotation = transform.rotation * Quaternion.Euler(0, 90, 0); 
			
			for (float t = 0.0f; t <= 1.0f; t += Time.deltaTime) 			
			{ 
				transform.rotation = Quaternion.Slerp(oldRotation, newRotation, t); 
				yield return null;
			} 

			transform.rotation = newRotation; // To make it come out at exactly 90 degrees
			
			rotating = false;	
		}
	}

	IEnumerator RotateRight()
	{
		if(!rotating) 
		{
			rotating = true;
			
			oldRotation = transform.rotation; 
			newRotation = transform.rotation * Quaternion.Euler(0, -90, 0); 
			
			for (float t = 0.0f; t <= 1.0f; t += Time.deltaTime) 			
			{ 
				transform.rotation = Quaternion.Slerp(oldRotation, newRotation, t); 
				yield return null;
			} 
			
			transform.rotation = newRotation; // To make it come out at exactly 90 degrees
			
			rotating = false;	
		}
	}
	IEnumerator RotateDown()
	{
		if(!rotating) 
		{
			rotating = true;
			
			oldRotation = transform.rotation; 
			newRotation = transform.rotation * Quaternion.Euler(-90, 0, 0); 
			
			for (float t = 0.0f; t <= 1.0f; t += Time.deltaTime) 			
			{ 
				transform.rotation = Quaternion.Slerp(oldRotation, newRotation, t); 
				yield return null;
			} 
			
			transform.rotation = newRotation; // To make it come out at exactly 90 degrees
			
			rotating = false;	
		}
	}
	IEnumerator RotateUp()
	{
		if(!rotating) 
		{
			rotating = true;
			
			oldRotation = transform.rotation; 
			newRotation = transform.rotation * Quaternion.Euler(90, 0, 0); 
			
			for (float t = 0.0f; t <= 1.0f; t += Time.deltaTime) 			
			{ 
				transform.rotation = Quaternion.Slerp(oldRotation, newRotation, t); 
				yield return null;
			} 
			
			transform.rotation = newRotation; // To make it come out at exactly 90 degrees
			
			rotating = false;	
		}
	}
}
