using UnityEngine;
using System.Collections;

public class FindJanePuzzle : MonoBehaviour {
	
	bool gameCompleted = false;
	bool gameActive = false;

	GameObject mainCamera;
	GameLogic gameLogic;

	public ParticleSystem particles;

	public bool GameCompleted {
		get {
			return gameCompleted;
		}
	}

	public bool GameActive {
		get {
			return gameActive;
		}
	}

	// Use this for initialization
	void Start () 
	{
		mainCamera = GameObject.Find("MainCamera");
		gameLogic = mainCamera.GetComponent<GameLogic>();
		StartGame();
	}

	public void StartGame()
	{
		gameActive = true;
		gameCompleted = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		/*
		if(!gameActive)
		{
			if(!gameCompleted)
			{

			}
		}*/
	}

	public void ClickOnFace(string faceName)
	{
		if(faceName == "Jane")
		{
			gameCompleted = true;
			gameActive = false;
			particles.Play();

			gameLogic.ActivateAlert("Congratulations, you found Jane!");
		}
	}
}
