using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour {

	public GameObject mainGame;

	//Puzzle GameObjects
	public GameObject turnPiecesPuzzle;
	public GameObject turnPiecesPuzzle2;
	public GameObject findJanePuzzle1;
	public GameObject findJanePuzzle2;
	public GameObject turnCubesPuzzle;
	public GameObject turnCubesPuzzle2;
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
	public bool gameIsWaitingForActivation = false;
	string gameWaitingForActivation;

	void Start () 
	{
		cameraPos = transform.position;
	}

	// Update is called once per frame
	void Update () 
	{
		/*
		if(Input.GetKeyDown(KeyCode.A))
		{
			ActivateMainGame();
		} else if(Input.GetKeyDown(KeyCode.S))
		{
			ActivateTurnPiecesPuzzle();
		} else if(Input.GetKeyDown(KeyCode.D))
		{
			ActivateTurnPiecesPuzzle2();
		} else if(Input.GetKeyDown(KeyCode.F))
		{
			ActivateTurnCubesPuzzle();
		} else if(Input.GetKeyDown(KeyCode.G))
		{
			ActivateTurnCubesPuzzle2();	
		} else if(Input.GetKeyDown(KeyCode.H))
		{
			ActivateWordPuzzle("I love Knatens!", "Kittens", "Knatens");	
		}*/

		transform.position = Vector3.Lerp(transform.position, cameraPos, speed*Time.deltaTime);
	}
	
	public void ActivateGame()
	{
		if(gameWaitingForActivation == "ActivateTurnPiecesPuzzle")
		{
			ActivateTurnPiecesPuzzle();
		} else if(gameWaitingForActivation == "ActivateTurnPiecesPuzzle2")
		{
			ActivateTurnPiecesPuzzle2();
		} else if(gameWaitingForActivation == "ActivateTurnCubesPuzzle")
		{
			ActivateTurnCubesPuzzle();
		} else if(gameWaitingForActivation == "ActivateTurnCubesPuzzle2")
		{
			ActivateTurnCubesPuzzle2();
		}
	}

	//Starts TurnPiecesPuzzle
	public void ActivateTurnPiecesPuzzle()
	{
		if(!gameIsWaitingForActivation)
		{
			ActivateAlert("Jane has given you a new document but it has been torn into pieces." + "\n" + "You need to put it back together.");
			gameIsWaitingForActivation = true;
			gameWaitingForActivation = "ActivateTurnPiecesPuzzle";
		} else 
		{

			if(!turnPiecesPuzzle.GetComponent<TurnPiecesPuzzle>().gameCompleted)
			{

				mainGame.SetActive(false);
				findJanePuzzle1.SetActive(false);
				findJanePuzzle2.SetActive(false);
				turnCubesPuzzle.SetActive(false);
				turnCubesPuzzle2.SetActive(false);
				wordPuzzle.SetActive(false);
				turnPiecesPuzzle2.SetActive(false);

				turnPiecesPuzzle.SetActive(true);
				puzzleInstructions.SetActive(true);

				instructionLabel1.GetComponent<UILabel>().text = "Use Up/Down arrow keys to switch pieces";
				instructionLabel2.GetComponent<UILabel>().text = "Use Left/Right arrow keys or scroll to rotate selected piece";

				//this.transform.position =  new Vector3 (turnPiecesPuzzle.transform.position.x, this.transform.position.y, this.transform.position.z);
				cameraPos = new Vector3 (turnPiecesPuzzle.transform.position.x, this.transform.position.y, this.transform.position.z);

				turnPiecesPuzzle.GetComponent<TurnPiecesPuzzle>().StartGame();
			}
		}
	}

	//Starts TurnPiecesPuzzle2
	public void ActivateTurnPiecesPuzzle2()
	{
		if(!gameIsWaitingForActivation)
		{
			ActivateAlert("Jane has given you a new document but it has been torn into pieces." + "\n" + "You need to put it back together.");
			gameIsWaitingForActivation = true;
			gameWaitingForActivation = "ActivateTurnPiecesPuzzle2";
		} else 
		{

			if(!turnPiecesPuzzle2.GetComponent<TurnPiecesPuzzle>().gameCompleted)
			{
				
				mainGame.SetActive(false);
				findJanePuzzle1.SetActive(false);
				findJanePuzzle2.SetActive(false);
				turnCubesPuzzle.SetActive(false);
				turnCubesPuzzle2.SetActive(false);
				wordPuzzle.SetActive(false);
				turnPiecesPuzzle.SetActive(false);
				
				turnPiecesPuzzle2.SetActive(true);
				puzzleInstructions.SetActive(true);
				
				instructionLabel1.GetComponent<UILabel>().text = "Use Up/Down arrow keys to switch pieces";
				instructionLabel2.GetComponent<UILabel>().text = "Use Left/Right arrow keys or scroll to rotate selected piece";
				
				//this.transform.position =  new Vector3 (turnPiecesPuzzle.transform.position.x, this.transform.position.y, this.transform.position.z);
				cameraPos = new Vector3 (turnPiecesPuzzle2.transform.position.x, this.transform.position.y, this.transform.position.z);
				
				turnPiecesPuzzle2.GetComponent<TurnPiecesPuzzle>().StartGame();
			}
		}
	}

	//Starts FindJanePuzzle1
	//TODO: not up to date
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
	//TODO: not up to date	
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
		if(!gameIsWaitingForActivation)
		{
			ActivateAlert("Jane has given you a new document but it has been torn into pieces." + "\n" + "You need to put it back together.");
			gameIsWaitingForActivation = true;
			gameWaitingForActivation = "ActivateTurnCubesPuzzle";
		} else 
		{

			if(!turnCubesPuzzle.GetComponent<TurnCubesPuzzle>().gameCompleted)
			{
				mainGame.SetActive(false);
				turnPiecesPuzzle.SetActive(false);
				turnPiecesPuzzle2.SetActive(false);
				findJanePuzzle1.SetActive(false);
				findJanePuzzle2.SetActive(false);
				wordPuzzle.SetActive(false);
				turnCubesPuzzle2.SetActive(false);

				turnCubesPuzzle.SetActive(true);
				puzzleInstructions.SetActive(true);
				
				instructionLabel1.GetComponent<UILabel>().text = "Use arrow keys to switch cubes, use Space to select/unselect";
				instructionLabel2.GetComponent<UILabel>().text = "Once selected, use arrow keys to turn cube";
				
				cameraPos =  new Vector3 (turnCubesPuzzle.transform.position.x, this.transform.position.y, this.transform.position.z);
				
				turnCubesPuzzle.GetComponent<TurnCubesPuzzle>().StartGame();
			}
		}
	}

	//Starts TurnCubesPuzzle2
	public void ActivateTurnCubesPuzzle2()
	{
		if(!gameIsWaitingForActivation)
		{
			ActivateAlert("Jane has given you a new document but it has been torn into pieces." + "\n" + "You need to put it back together.");
			gameIsWaitingForActivation = true;
			gameWaitingForActivation = "ActivateTurnCubesPuzzle2";

		} else 
		{
			if(!turnCubesPuzzle2.GetComponent<TurnCubesPuzzle>().gameCompleted)
			{
				mainGame.SetActive(false);
				turnPiecesPuzzle.SetActive(false);
				turnPiecesPuzzle2.SetActive(false);
				findJanePuzzle1.SetActive(false);
				findJanePuzzle2.SetActive(false);
				wordPuzzle.SetActive(false);
				turnCubesPuzzle.SetActive(false);
				
				turnCubesPuzzle2.SetActive(true);
				puzzleInstructions.SetActive(true);
				
				instructionLabel1.GetComponent<UILabel>().text = "Use arrow keys to switch cubes, use Space to select/unselect";
				instructionLabel2.GetComponent<UILabel>().text = "Once selected, use arrow keys to turn cube";
				
				cameraPos =  new Vector3 (turnCubesPuzzle2.transform.position.x, this.transform.position.y, this.transform.position.z);
				
				turnCubesPuzzle2.GetComponent<TurnCubesPuzzle>().StartGame();
			}
		}
	}

	//Starts WordPuzzle
	public void ActivateWordPuzzle(string janeMessage, string correctWord, string faultyWord)
	{
		mainGame.SetActive(false);
		turnPiecesPuzzle.SetActive(false);
		turnPiecesPuzzle2.SetActive(false);
		findJanePuzzle1.SetActive(false);
		findJanePuzzle2.SetActive(false);
		turnCubesPuzzle.SetActive(false);
		turnCubesPuzzle2.SetActive(false);

		wordPuzzle.SetActive(true);
		puzzleInstructions.SetActive(true);
		
		instructionLabel1.GetComponent<UILabel>().text = "Jane got confused and said '" + faultyWord + "' instead of another word";
		instructionLabel2.GetComponent<UILabel>().text = "Click on the letters in the right order to guess the word she meant";
		
		cameraPos =  new Vector3 (wordPuzzle.transform.position.x, this.transform.position.y, this.transform.position.z);
		
		wordPuzzle.GetComponent<WordPuzzle>().StartGame(janeMessage, correctWord, faultyWord);
	}

	//Activates Main Game (Chat Bot)
	public void ActivateMainGame()
	{
		puzzleInstructions.SetActive(false);
		turnPiecesPuzzle.SetActive(false);
		turnPiecesPuzzle2.SetActive(false);
		findJanePuzzle1.SetActive(false);
		findJanePuzzle2.SetActive(false);
		turnCubesPuzzle.SetActive(false);
		turnCubesPuzzle2.SetActive(false);
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
