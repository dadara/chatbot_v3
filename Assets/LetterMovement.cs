using UnityEngine;
using System.Collections;

public class LetterMovement : MonoBehaviour {

	float maxX = -20f;
	float minX = -25f;
	float maxY = 2f;
	float minY = -2f;
	
	private float tChange = 0; // force new direction in the first Update
	private float randomX;
	private float randomY;


	public Texture2D cursorTexture;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot = Vector2.zero;


	void Start () 
	{

	}

	void Update () 
	{
		if(this.transform.parent.parent.parent.GetComponent<WordPuzzle>().gameActive)
		{

			// change to random direction at random intervals
			if (Time.time >= tChange){
				randomX = Random.Range(-1.0f,1.0f); // with float parameters, a random float
				randomY = Random.Range(-1.0f,1.0f); //  between -2.0 and 2.0 is returned
				// set a random interval between 0.5 and 1.5
				tChange = Time.time + Random.Range(0.5f,1.5f);
			}
			transform.Translate(new Vector3(randomX,randomY,0f) * Time.deltaTime);
			// if object reached any border, revert the appropriate direction
			if (transform.position.x >= maxX || transform.position.x <= minX) {
				randomX = -randomX;
			}
			if (transform.position.y >= maxY || transform.position.y <= minY) {
				randomY = -randomY;
			}
			// make sure the position is inside the borders
			transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), 
			                                Mathf.Clamp(transform.position.y, minY, maxY),
			                                transform.position.z);
		}
	}

	void OnMouseOver()
	{
		Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
		
	}
	
	void OnMouseExit()
	{
		Cursor.SetCursor(null, hotSpot, cursorMode);	
	}
	
	void OnMouseDown()
	{
		string letter = this.transform.GetComponent<TextMesh>().text;
		if(this.transform.parent.parent.parent.GetComponent<WordPuzzle>().AddLetter(letter))
		{
			this.transform.GetComponent<TextMesh>().text = "";
			this.transform.GetComponent<BoxCollider>().enabled = true;
		}
	}
}
