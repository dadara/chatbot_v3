using UnityEngine;
using System.Collections;

public class TurnCubesPuzzle : MonoBehaviour 
{

	public GameObject[] parts;
	private int i = 4;
	int cnt = 0;

	public bool gameCompleted = false;
	bool gameActive = false;
	bool cubeSelected = false;

	public Color selectedColor = new Color(0.6f, 1f, 0.2f, 1f);
	public ParticleSystem particles;

	GameObject mainCamera;
	GameLogic gameLogic;

	private float timeSpent = 0;
	GameObject scorePanel;

	public GameObject timeLabel;

	// Use this for initialization
	void Start () 
	{
		mainCamera = GameObject.Find("MainCamera");
		gameLogic = mainCamera.GetComponent<GameLogic>();
		scorePanel = GameObject.Find("ScorePanel");
	}

	public void StartGame()
	{
		i = 4;
		cnt = 0;
		gameActive = true;
		cubeSelected = false;
		timeSpent = 0;
		timeLabel.GetComponent<UILabel>().text = "0 s";

		//Color selected Cube
		foreach (Transform child in parts[i].transform)
		{
			child.GetComponent<MeshRenderer>().material.color = selectedColor;
		}
	}

	/*******
	**0*1*2*
	**3*4*5*
	**6*7*8*
	********/

	void Update () 
	{
		if(gameActive)
		{
			timeSpent += Time.deltaTime;
			timeLabel.GetComponent<UILabel>().text = Mathf.Round(timeSpent) + " s";

			if(!gameCompleted)
			{   
				if(!cubeSelected)
				{
					//Cube Selection
					if(Input.GetKeyDown ("up") && i!=0 && i!=1&& i!=2)
					{
						foreach (Transform child in parts[i].transform)
						{
							child.GetComponent<MeshRenderer>().material.color = Color.white;
						}
						i = i-3;
						foreach (Transform child in parts[i].transform)
						{
							child.GetComponent<MeshRenderer>().material.color = selectedColor;
						}

					} else if(Input.GetKeyDown ("down") && i!=6 && i!=7 && i!=8)
					{
						foreach (Transform child in parts[i].transform)
						{
							child.GetComponent<MeshRenderer>().material.color = Color.white;
						}
						i = i+3;
						foreach (Transform child in parts[i].transform)
						{
							child.GetComponent<MeshRenderer>().material.color = selectedColor;
						}
					} else if(Input.GetKeyDown ("left") && i!=0 && i!=3 && i!=6)
					{
						foreach (Transform child in parts[i].transform)
						{
							child.GetComponent<MeshRenderer>().material.color = Color.white;
						}
						i = i-1;
						foreach (Transform child in parts[i].transform)
						{
							child.GetComponent<MeshRenderer>().material.color = selectedColor;
						}
					} else if(Input.GetKeyDown ("right") && i!=2 && i!=5 && i!=8)
					{
						foreach (Transform child in parts[i].transform)
						{
							child.GetComponent<MeshRenderer>().material.color = Color.white;
						}
						i = i+1;
						foreach (Transform child in parts[i].transform)
						{
							child.GetComponent<MeshRenderer>().material.color = selectedColor;
						}
					} else if(Input.GetKeyDown("space"))
					{
						cubeSelected = true;
						parts[i].GetComponent<TurnCube>().SetSelected(true);
					}

				} else if(Input.GetKeyDown("space"))
				{
					cubeSelected = false;
					parts[i].GetComponent<TurnCube>().SetSelected(false);
				}

				CheckGameComplete();
			}
		}
	}

	//Check if the Game was succesfully completed
	private void CheckGameComplete()
	{
		int tempCnt = 0;
		foreach(GameObject p in parts)
		{
			if(p.GetComponent<TurnCube>().PuzzlePieceCorrectPosition)
			{
				tempCnt++;
			}
		}

		if(tempCnt > cnt)
		{
			cubeSelected = false;
			cnt = tempCnt;
		}
		
		if(cnt == parts.Length)
		{
			gameCompleted = true;
			gameActive = false;
			float points = Mathf.Round(1/timeSpent*200);

			foreach (Transform child in parts[i].transform)
			{
				child.GetComponent<MeshRenderer>().material.color = Color.white;
			}

			particles.Play();
			gameLogic.ActivateAlert("Congratulations, you completed the picture and scored " + points + " Points.");
			scorePanel.GetComponent<Score>().addScore(points);
		}
	}

	public void stopParticles()
	{
		particles.Stop();
		particles.Clear();
	}
}
