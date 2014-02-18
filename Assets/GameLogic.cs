using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour {

	public GameObject mainGame;

	//Puzzle GameObjects
	public GameObject turnPiecesPuzzle;
	public GameObject findJanePuzzle1;
	public GameObject findJanePuzzle2;
	public GameObject turnCubesPuzzle;
	public GameObject wordPuzzle;

	//Instructions
	public GameObject puzzleInstructions;
	public GameObject instructionLabel1;
	public GameObject instructionLabel2;

	//Alerts
	public GameObject alerts;
	public GameObject alertLabel;

	//Other
	private float speed = 2.5f;
	private Vector3 cameraPos;

	void Start () 
	{
		cameraPos = transform.position;
	}

	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.A))
		{
			ActivateMainGame();
		} else if(Input.GetKeyDown(KeyCode.S))
		{
			ActivateTurnPiecesPuzzle();
		} else if(Input.GetKeyDown(KeyCode.D))
		{
			ActivateFindJanePuzzle1();
		} else if(Input.GetKeyDown(KeyCode.F))
		{
			ActivateFindJanePuzzle2();
		} else if(Input.GetKeyDown(KeyCode.G))
		{
			ActivateTurnCubesPuzzle();	
		} else if(Input.GetKeyDown(KeyCode.H))
		{
			ActivateWordPuzzle("Kittens", "Knatens");	
		}

		transform.position = Vector3.Lerp(transform.position, cameraPos, speed*Time.deltaTime);
	}

	//Starts TurnPiecesPuzzle
	public void ActivateTurnPiecesPuzzle()
	{
		mainGame.SetActive(false);
		findJanePuzzle1.SetActive(false);
		findJanePuzzle2.SetActive(false);
		turnCubesPuzzle.SetActive(false);
		wordPuzzle.SetActive(false);

		turnPiecesPuzzle.SetActive(true);
		puzzleInstructions.SetActive(true);

		instructionLabel1.GetComponent<UILabel>().text = "Use Up/Down arrow keys to switch pieces";
		instructionLabel2.GetComponent<UILabel>().text = "Use Left/Right arrow keys or scroll to rotate selected piece";

		//this.transform.position =  new Vector3 (turnPiecesPuzzle.transform.position.x, this.transform.position.y, this.transform.position.z);
		cameraPos = new Vector3 (turnPiecesPuzzle.transform.position.x, this.transform.position.y, this.transform.position.z);

		turnPiecesPuzzle.GetComponent<TurnPiecesPuzzle>().StartGame();
	}

	//Starts FindJanePuzzle1
	public void ActivateFindJanePuzzle1()
	{
		mainGame.SetActive(false);
		turnPiecesPuzzle.SetActive(false);
		findJanePuzzle2.SetActive(false);
		turnCubesPuzzle.SetActive(false);
		wordPuzzle.SetActive(false);

		findJanePuzzle1.SetActive(true);
		puzzleInstructions.SetActive(true);

		instructionLabel1.GetComponent<UILabel>().text = "Find Jane in this picture";
		instructionLabel2.GetComponent<UILabel>().text = "Use the mouse pointer to click on Jane";

		cameraPos =  new Vector3 (findJanePuzzle1.transform.position.x, this.transform.position.y, this.transform.position.z);

		findJanePuzzle1.GetComponent<FindJanePuzzle>().StartGame();
	}

	//Starts FindJanePuzzle2
	public void ActivateFindJanePuzzle2()
	{
		mainGame.SetActive(false);
		turnPiecesPuzzle.SetActive(false);
		findJanePuzzle1.SetActive(false);
		turnCubesPuzzle.SetActive(false);
		wordPuzzle.SetActive(false);

		findJanePuzzle2.SetActive(true);
		puzzleInstructions.SetActive(true);

		instructionLabel1.GetComponent<UILabel>().text = "Find Jane in this picture";
		instructionLabel2.GetComponent<UILabel>().text = "Use the mouse pointer to click on Jane";

		cameraPos =  new Vector3 (findJanePuzzle2.transform.position.x, this.transform.position.y, this.transform.position.z);
		
		findJanePuzzle2.GetComponent<FindJanePuzzle>().StartGame();
	}

	//Starts TurnCubesPuzzle
	public void ActivateTurnCubesPuzzle()
	{
		mainGame.SetActive(false);
		turnPiecesPuzzle.SetActive(false);
		findJanePuzzle1.SetActive(false);
		findJanePuzzle2.SetActive(false);
		wordPuzzle.SetActive(false);

		turnCubesPuzzle.SetActive(true);
		puzzleInstructions.SetActive(true);
		
		instructionLabel1.GetComponent<UILabel>().text = "Use arrow keys to switch cubes, use Space to select/unselect";
		instructionLabel2.GetComponent<UILabel>().text = "Once selected, use arrow keys to turn cube";
		
		cameraPos =  new Vector3 (turnCubesPuzzle.transform.position.x, this.transform.position.y, this.transform.position.z);
		
		turnCubesPuzzle.GetComponent<TurnCubesPuzzle>().StartGame();
	}

	//Starts WordPuzzle
	public void ActivateWordPuzzle(string correctWord, string faultyWord)
	{
		mainGame.SetActive(false);
		turnPiecesPuzzle.SetActive(false);
		findJanePuzzle1.SetActive(false);
		findJanePuzzle2.SetActive(false);
		turnCubesPuzzle.SetActive(false);

		wordPuzzle.SetActive(true);
		puzzleInstructions.SetActive(true);
		
		instructionLabel1.GetComponent<UILabel>().text = "Jane got confused and said '" + faultyWord + "' instead of another word";
		instructionLabel2.GetComponent<UILabel>().text = "Click on the letters in the right order to guess the word she meant";
		
		cameraPos =  new Vector3 (wordPuzzle.transform.position.x, this.transform.position.y, this.transform.position.z);
		
		wordPuzzle.GetComponent<WordPuzzle>().StartGame(correctWord, faultyWord);
	}

	//Activates Main Game (Chat Bot)
	public void ActivateMainGame()
	{
		puzzleInstructions.SetActive(false);
		turnPiecesPuzzle.SetActive(false);
		findJanePuzzle1.SetActive(false);
		findJanePuzzle2.SetActive(false);
		turnCubesPuzzle.SetActive(false);
		wordPuzzle.SetActive(false);

		mainGame.SetActive(true);
		
		cameraPos =  new Vector3 (0, this.transform.position.y, this.transform.position.z);
		
	}

	//Activates AlertPanel and displays a message
	public void ActivateAlert(string message)
	{
		alerts.SetActive(true);
		alertLabel.GetComponent<UILabel>().text = message;
	}
}
