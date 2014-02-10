using UnityEngine;
using System.Collections;

public class FindJanePuzzleClickOnFace : MonoBehaviour {

	public Texture2D cursorTexture;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot = Vector2.zero;
	private FindJanePuzzle findJanePuzzle;

	// Use this for initialization
	void Start () 
	{
		findJanePuzzle = transform.parent.GetComponent<FindJanePuzzle>();
		this.renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	void OnMouseOver()
	{
		if(findJanePuzzle.GameActive)
		{
			Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
		}
	}
	
	void OnMouseExit()
	{
		Cursor.SetCursor(null, hotSpot, cursorMode);	
	}

	void OnMouseDown()
	{
		if(findJanePuzzle.GameActive)
		{
			findJanePuzzle.ClickOnFace(this.name);
		}
	}
}
