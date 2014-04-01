using UnityEngine;
using System.Collections;

public class TurnPiecesPuzzle : MonoBehaviour 
{
	public GameObject[] parts;
	private int i = 0;
	public float scrollIncrement = 10;

	public bool gameCompleted = false;
	bool gameActive = false;

	public Color selectedColor = new Color(0.6f, 1f, 0.2f, 1f);
	public ParticleSystem particles;

	GameObject mainCamera;
	GameLogic gameLogic;

	// Use this for initialization
	void Start () 
	{
		mainCamera = GameObject.Find("MainCamera");
		gameLogic = mainCamera.GetComponent<GameLogic>();
	}

	public void StartGame()
	{
		//Randomize Rotations
		foreach(GameObject p in parts)
		{
			if(!p.Equals(parts[0]))
			{
				p.transform.transform.Rotate(Vector3.down * (Random.Range(-15, 15)*scrollIncrement));
			}
		}

		i = 0;
		gameActive = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(gameActive)
		{
			if(!gameCompleted)
			{
				parts[i].GetComponent<MeshRenderer>().material.color = selectedColor;

				if(Input.GetAxis("Mouse ScrollWheel") > 0 || Input.GetKeyDown ("left"))
				{
					parts[i].transform.Rotate(Vector3.down * scrollIncrement);

				} else if( Input.GetAxis("Mouse ScrollWheel") < 0 || Input.GetKeyDown ("right"))
				{
					parts[i].transform.Rotate(Vector3.up * scrollIncrement);

				} else if(Input.GetKeyDown ("up"))
				{
					parts[i].GetComponent<MeshRenderer>().material.color = Color.white;
					i++; if(i >= parts.Length){ i = parts.Length-1;}

				} else if(Input.GetKeyDown ("down"))
				{
					parts[i].GetComponent<MeshRenderer>().material.color = Color.white;
					i--; if(i < 0){i = 0;}
				}

				float angle = Quaternion.Angle(parts[i].transform.localRotation, Quaternion.identity);
				if(Mathf.Abs(angle) < 1)
				{
					parts[i].transform.localRotation = Quaternion.identity;
				}

				CheckGameComplete();
			} 
		}
	}

	private void CheckGameComplete()
	{
		int cnt = 0;

		foreach(GameObject p in parts)
		{
			if(p.transform.localRotation == Quaternion.identity)
			{
				cnt++;
			}
		}

		if(cnt == parts.Length)
		{
			gameCompleted = true;
			gameActive = false;
			parts[i].GetComponent<MeshRenderer>().material.color = Color.white;
			particles.Play();
			gameLogic.ActivateAlert("Congratulations, you completed the picture!");
		}
	}

}
