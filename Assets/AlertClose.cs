using UnityEngine;
using System.Collections;

public class AlertClose : MonoBehaviour {

	public GameObject alerts;

	GameObject mainCamera;
	GameLogic gameLogic;

	// Use this for initialization
	void Start () 
	{
		mainCamera = GameObject.Find("MainCamera");
		gameLogic = mainCamera.GetComponent<GameLogic>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnPress (bool isPressed)
	{
		if (isPressed)
		{
			alerts.SetActive(false);

			if(gameLogic.gameIsWaitingForActivation)
			{
				gameLogic.ActivateGame();
			} else 
			{
				gameLogic.ActivateMainGame();
			}
		}
	}
}
