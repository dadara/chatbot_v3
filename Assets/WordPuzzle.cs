using UnityEngine;
using System.Collections;

public class WordPuzzle : MonoBehaviour {


	string correctWord = "Kittens";
	string faultyWord = "Knatens";
	int wordIndex = 0;

	public GameObject word;
	public GameObject faultyWordObject;
	public GameObject correctWordObject;

	public ParticleSystem particles;

	GameObject mainCamera;
	GameLogic gameLogic;

	// Use this for initialization
	void Start () 
	{
		mainCamera = GameObject.Find("MainCamera");
		gameLogic = mainCamera.GetComponent<GameLogic>();
	}

	public void StartGame(string correctWord, string faultyWord)
	{
		this.correctWord = correctWord;
		this.faultyWord = faultyWord;
		wordIndex = 0;

		int i = 0;

		faultyWordObject.GetComponent<TextMesh>().text = faultyWord;
		correctWordObject.GetComponent<TextMesh>().text = "";

		foreach (Transform letter in word.transform)
		{
			if(i < correctWord.Length)
			{
				letter.GetComponent<TextMesh>().text = correctWord[i].ToString();
				letter.GetComponent<BoxCollider>().enabled = true;
				correctWordObject.GetComponent<TextMesh>().text += "_";
				i++;
			} else 
			{
				letter.GetComponent<TextMesh>().text = "";
				letter.GetComponent<BoxCollider>().enabled = false;
			}
		}
	}
	
	void Update () 
	{
	}

	private void CheckGameComplete()
	{
		if(correctWordObject.GetComponent<TextMesh>().text == correctWord)
		{
			particles.Play();
			gameLogic.ActivateAlert("Congratulations, you correctly guessed the word!");
		}
	}

	public bool AddLetter(string letter)
	{
		if(letter == correctWord[wordIndex].ToString())
		{
			correctWordObject.GetComponent<TextMesh>().text = correctWordObject.GetComponent<TextMesh>().text.Substring(0,wordIndex) + letter + correctWordObject.GetComponent<TextMesh>().text.Substring(wordIndex+1,correctWord.Length-(wordIndex+1));
			wordIndex++;

			CheckGameComplete();

			return true;
		} else 
		{
			return false;
		}
	}
}
