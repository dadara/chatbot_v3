using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour 
{

	float score = 0;
	bool gameDone = false;
	public GameObject scoreLabel;


	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		scoreLabel.GetComponent<UILabel>().text = score + " Points";
	}

	public void addScore(float scoreToAdd)
	{
		score = score + scoreToAdd;
	}

	public float getScore()
	{
		return score;
	}

	public void evaluateEndScore(int numberDocuments)
	{
		if(!gameDone)
		{
			score += numberDocuments * 2;
			gameDone = true;
		}
	}
}
