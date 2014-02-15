using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour {

	public GameObject mainGame;

	public GameObject turnPiecesPuzzle;
	public GameObject findJanePuzzle1;
	public GameObject findJanePuzzle2;
	public GameObject turnCubesPuzzle;

	public GameObject puzzleInstructions;
	public GameObject instructionLabel1;
	public GameObject instructionLabel2;

	public GameObject alerts;
	public GameObject alertLabel;

	private float speed = 2.5f;
	private Vector3 cameraPos;

	// Use this for initialization
	void Start () 
	{
		cameraPos = transform.position;
	}

	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.A))
		{
			ActivateTurnPiecesPuzzle();
		} else if(Input.GetKeyDown(KeyCode.S))
		{
			ActivateFindJanePuzzle1();
		} else if(Input.GetKeyDown(KeyCode.D))
		{
			ActivateFindJanePuzzle2();
		} else if(Input.GetKeyDown(KeyCode.F))
		{
			ActivateTurnCubesPuzzle();	
		} else if(Input.GetKeyDown(KeyCode.G))
		{
			ActivateMainGame();	
		}

		transform.position = Vector3.Lerp(transform.position, cameraPos, speed*Time.deltaTime);
	}

	public void ActivateTurnPiecesPuzzle()
	{
		mainGame.SetActive(false);
		findJanePuzzle1.SetActive(false);
		findJanePuzzle2.SetActive(false);
		turnCubesPuzzle.SetActive(false);

		turnPiecesPuzzle.SetActive(true);
		puzzleInstructions.SetActive(true);

		instructionLabel1.GetComponent<UILabel>().text = "Use Up/Down arrow keys to switch pieces";
		instructionLabel2.GetComponent<UILabel>().text = "Use Left/Right arrow keys or scroll to rotate selected piece";

		//this.transform.position =  new Vector3 (turnPiecesPuzzle.transform.position.x, this.transform.position.y, this.transform.position.z);
		cameraPos = new Vector3 (turnPiecesPuzzle.transform.position.x, this.transform.position.y, this.transform.position.z);

		turnPiecesPuzzle.GetComponent<TurnPiecesPuzzle>().StartGame();
	}

	public void ActivateFindJanePuzzle1()
	{
		mainGame.SetActive(false);
		turnPiecesPuzzle.SetActive(false);
		findJanePuzzle2.SetActive(false);
		turnCubesPuzzle.SetActive(false);

		findJanePuzzle1.SetActive(true);
		puzzleInstructions.SetActive(true);

		instructionLabel1.GetComponent<UILabel>().text = "Find Jane in this picture";
		instructionLabel2.GetComponent<UILabel>().text = "Use the mouse pointer to click on Jane";

		cameraPos =  new Vector3 (findJanePuzzle1.transform.position.x, this.transform.position.y, this.transform.position.z);

		findJanePuzzle1.GetComponent<FindJanePuzzle>().StartGame();
	}

	public void ActivateFindJanePuzzle2()
	{
		mainGame.SetActive(false);
		turnPiecesPuzzle.SetActive(false);
		findJanePuzzle1.SetActive(false);
		turnCubesPuzzle.SetActive(false);

		findJanePuzzle2.SetActive(true);
		puzzleInstructions.SetActive(true);

		instructionLabel1.GetComponent<UILabel>().text = "Find Jane in this picture";
		instructionLabel2.GetComponent<UILabel>().text = "Use the mouse pointer to click on Jane";

		cameraPos =  new Vector3 (findJanePuzzle2.transform.position.x, this.transform.position.y, this.transform.position.z);
		
		findJanePuzzle2.GetComponent<FindJanePuzzle>().StartGame();
	}

	public void ActivateTurnCubesPuzzle()
	{
		mainGame.SetActive(false);
		turnPiecesPuzzle.SetActive(false);
		findJanePuzzle1.SetActive(false);
		findJanePuzzle2.SetActive(false);

		turnCubesPuzzle.SetActive(true);
		puzzleInstructions.SetActive(true);
		
		instructionLabel1.GetComponent<UILabel>().text = "Use arrow keys to switch cubes, use Space to select/unselect";
		instructionLabel2.GetComponent<UILabel>().text = "Once selected, use arrow keys to turn cube";
		
		cameraPos =  new Vector3 (turnCubesPuzzle.transform.position.x, this.transform.position.y, this.transform.position.z);
		
		turnCubesPuzzle.GetComponent<TurnCubesPuzzle>().StartGame();
	}

	public void ActivateMainGame()
	{
		puzzleInstructions.SetActive(false);
		turnPiecesPuzzle.SetActive(false);
		findJanePuzzle1.SetActive(false);
		findJanePuzzle2.SetActive(false);
		turnCubesPuzzle.SetActive(false);

		mainGame.SetActive(true);
		
		cameraPos =  new Vector3 (0, this.transform.position.y, this.transform.position.z);
		
	}

	public void ActivateAlert(string message)
	{
		alerts.SetActive(true);
		alertLabel.GetComponent<UILabel>().text = message;
	}
}
