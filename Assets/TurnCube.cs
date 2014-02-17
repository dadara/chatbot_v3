using UnityEngine;
using System.Collections;

public class TurnCube : MonoBehaviour 
{
	public GameObject PuzzlePiece;

	private bool selected = false;
	private bool rotating = false;
	private bool puzzlePieceCorrectPosition = false;

	Quaternion oldRotation;
	Quaternion newRotation;

	Vector3 initialPosition;

	public bool Selected {
		get {
			return selected;
		}
	}

	public void SetSelected (bool boolean)
	{
		selected = boolean;
	}

	public bool PuzzlePieceCorrectPosition {
		get {
			return puzzlePieceCorrectPosition;
		}
	}
	
	void Start () 
	{
		selected = false;
		rotating = false;

		resetRotation();

		initialPosition = transform.position;
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

			transform.position = new Vector3(transform.position.x, transform.position.y, 1f);
		} else 
		{
			transform.position = initialPosition;
		}

		checkPuzzlePieceCorrectPosition();
	}

	void checkPuzzlePieceCorrectPosition()
	{
		float angle = Quaternion.Angle(PuzzlePiece.transform.rotation, Quaternion.Euler(270,0,0));

		if(Mathf.Abs(angle) < 1)
		{
			puzzlePieceCorrectPosition = true;
			transform.position = initialPosition;
			selected = false;
		} else 
		{
			puzzlePieceCorrectPosition = false;
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

			resetRotation();
			
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

			resetRotation();
			
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

			resetRotation();

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

			resetRotation();

			rotating = false;	
		}
	}


	void resetRotation()
	{
		Quaternion[] partsRotation = new Quaternion[6];
		Vector3[] partsPosition = new Vector3[6];

		int i = 0;

		//Copy pieces rotation and position
		foreach (Transform child in transform)
		{
			partsRotation[i] = child.rotation;
			partsPosition[i] = child.position;
			i++;
		}

		//reset Cube rotation
		transform.rotation = Quaternion.identity;

		i = 0;

		//Paste pieces rotation and position
		foreach (Transform child in transform)
		{
			child.rotation = partsRotation[i];
			child.position = partsPosition[i];
			i++;
		}
	}
}
