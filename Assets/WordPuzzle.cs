using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class WordPuzzle : MonoBehaviour {


	string correctWord = "Kittens";
	string faultyWord = "Knatens";
	int wordIndex = 0;

	public GameObject words;
	//public GameObject faultyWordObject;
	//public GameObject correctWordObject;
	public GameObject janeMessageObject;
	Hashtable wordOptions = new Hashtable();
	List<string> listwords;

	public ParticleSystem particles;

	GameObject mainCamera;
	GameLogic gameLogic;

	public bool gameActive = false;
	private float timeSpent = 0;
	GameObject scorePanel;

	public GameObject timeLabel;

	// Use this for initialization
	void Start () 
	{
		mainCamera = GameObject.Find("MainCamera");
		gameLogic = mainCamera.GetComponent<GameLogic>();
		scorePanel = GameObject.Find("ScorePanel");

		initializeHashtable();
	}

	public void StartGame(string janeMessage, string correctWord, string faultyWord)
	{
		this.correctWord = correctWord;
		this.faultyWord = faultyWord;
		wordIndex = 0;

		timeSpent = 0;

		timeLabel.GetComponent<UILabel>().text = "0 s";

		//faultyWordObject.GetComponent<TextMesh>().text = faultyWord;
		//correctWordObject.GetComponent<TextMesh>().text = "";

		string janeMessageText = janeMessage + " What could she mean by " + faultyWord.ToUpper() + "?";
		janeMessageObject.GetComponent<UILabel>().text = janeMessageText;

		int i = 0; int k = 0;
		int j = 0; j = UnityEngine.Random.Range(0,5);

		foreach (Transform wordObject in words.transform)
		{
			wordObject.GetComponent<BoxCollider>().enabled = true;

			if(i == j)
			{
				wordObject.GetComponent<TextMesh>().text = correctWord;
			} else 
			{
				List<string> wordList = (List<string>) wordOptions[correctWord];
				wordObject.GetComponent<TextMesh>().text = wordList[k];
				k++;
			}
			i++;
		}

		/*
		foreach (Transform letter in word.transform)
		{
			if(i < correctWord.Length)
			{
				letter.GetComponent<TextMesh>().text = correctWord[i].ToString();
				letter.GetComponent<BoxCollider>().enabled = true;
				//correctWordObject.GetComponent<TextMesh>().text += "_";
				i++;
			} else 
			{
				letter.GetComponent<TextMesh>().text = "";
				letter.GetComponent<BoxCollider>().enabled = false;
			}
		}*/

		gameActive = true;
	}
	
	void Update () 
	{
		if(gameActive)
		{
			timeSpent += Time.deltaTime;
			timeLabel.GetComponent<UILabel>().text = Mathf.Round(timeSpent) + " s";
		}
	}

	private void CheckGameComplete(string word)
	{
		if(word == correctWord && gameActive)
		{
			gameActive = false;
			float points = Mathf.Round(1/timeSpent*20);
			particles.Play();
			gameLogic.ActivateAlert("Congratulations, you guessed the correct word and scored " + points + " Points.");
			scorePanel.GetComponent<Score>().addScore(points);

			foreach (Transform wordObject in words.transform)
			{
				wordObject.GetComponent<BoxCollider>().enabled = false;
			}

		} else 
		{
			gameActive = false;
			gameLogic.ActivateAlert("Sorry, you clicked the wrong word. The correct word is " + correctWord);

			foreach (Transform wordObject in words.transform)
			{
				wordObject.GetComponent<BoxCollider>().enabled = false;
			}
		}

		/*
		if(correctWordObject.GetComponent<TextMesh>().text == correctWord)
		{
			gameActive = false;
			float points = Mathf.Round(1/timeSpent*100);
			particles.Play();
			gameLogic.ActivateAlert("Congratulations, you guessed the word and scored " + points + " Points.");
			scorePanel.GetComponent<Score>().addScore(points);		
		}
		*/
	}

	public void selectWord(string word)
	{
		CheckGameComplete(word);
	}

	/*
	public bool AddLetter(string letter)
	{
		if(letter == correctWord[wordIndex].ToString())
		{
			correctWordObject.GetComponent<TextMesh>().text = correctWordObject.GetComponent<TextMesh>().text.Substring(0,wordIndex) + letter + correctWordObject.GetComponent<TextMesh>().text.Substring(wordIndex+1,correctWord.Length-(wordIndex+1));
			wordIndex++;

			CheckGameComplete();

			if(gameActive && correctWord[wordIndex].ToString() == " ")
			{
				correctWordObject.GetComponent<TextMesh>().text = correctWordObject.GetComponent<TextMesh>().text.Substring(0,wordIndex) + " " + correctWordObject.GetComponent<TextMesh>().text.Substring(wordIndex+1,correctWord.Length-(wordIndex+1));
				wordIndex++;
			}

			return true;
		} else 
		{
			return false;
		}
	}*/

	public void stopParticles()
	{
		particles.Stop();
		particles.Clear();
	}

	void initializeHashtable()
	{
		listwords = new List<string>();
		listwords.Add("element");
		listwords.Add("mental");
		listwords.Add("elemental"); 
		listwords.Add("electrical"); 
			
		wordOptions.Add("elementary", listwords);


		listwords = new List<string>();
		listwords.Add("celebration");
		listwords.Add("mental");
		listwords.Add("elemental"); 
		listwords.Add("electrical");  

		wordOptions.Add("celebrate", listwords);


		listwords = new List<string>();
		listwords.Add("birthhour");
		listwords.Add("birthyear");
		listwords.Add("birthplace"); 
		listwords.Add("birthday present");  
		
		wordOptions.Add("birthday", listwords);


		listwords = new List<string>();
		listwords.Add("celebrate my birthhour");
		listwords.Add("celebrate my birthyear");
		listwords.Add("celebrate my birthplace"); 
		listwords.Add("celebrate my birthday present");  
		
		wordOptions.Add("celebrate my birthday", listwords);


		listwords = new List<string>();
		listwords.Add("girl");
		listwords.Add("baby");
		listwords.Add("young"); 
		listwords.Add("youngster"); 
		
		wordOptions.Add("child", listwords);


		listwords = new List<string>();
		listwords.Add("extreme");
		listwords.Add("extremely");
		listwords.Add("expecting"); 
		listwords.Add("exhilarating");  
		
		wordOptions.Add("exhausting", listwords);


		listwords = new List<string>();
		listwords.Add("Wisconsin");
		listwords.Add("West");
		listwords.Add("Westfield"); 
		listwords.Add("Wayne");  
		
		wordOptions.Add("Washington D.C.", listwords);


		listwords = new List<string>();
		listwords.Add("expert");
		listwords.Add("expertise");
		listwords.Add("executing"); 
		listwords.Add("cultural"); 
		
		wordOptions.Add("executive", listwords);


		listwords = new List<string>();
		listwords.Add("secretive");
		listwords.Add("second creative");
		listwords.Add("second career"); 
		listwords.Add("secret"); 
		
		wordOptions.Add("secretary", listwords);


		listwords = new List<string>();
		listwords.Add("choice");
		listwords.Add("check");
		listwords.Add("chuck out"); 
		listwords.Add("change"); 
		
		wordOptions.Add("choose", listwords);


		listwords = new List<string>();
		listwords.Add("geology");
		listwords.Add("geoscience");
		listwords.Add("geo studies"); 
		listwords.Add("geothermal"); 
		
		wordOptions.Add("geography", listwords);


		listwords = new List<string>();
		listwords.Add("home school");
		listwords.Add("her school");
		listwords.Add("higher school"); 
		listwords.Add("school"); 
		
		wordOptions.Add("highschool", listwords);


		listwords = new List<string>();
		listwords.Add("dancer");
		listwords.Add("dentist");
		listwords.Add("decorator"); 
		listwords.Add("distributor");  
		
		wordOptions.Add("debutant", listwords);


		listwords = new List<string>();
		listwords.Add("Birthhour");
		listwords.Add("Birthyear");
		listwords.Add("Birthplace"); 
		listwords.Add("Birthday present"); 

		wordOptions.Add("history", listwords);


		listwords = new List<string>();
		listwords.Add("ambiguous");
		listwords.Add("ambivalent");
		listwords.Add("am a bit"); 
		listwords.Add("american"); 
		
		wordOptions.Add("ambitious", listwords);


		listwords = new List<string>();
		listwords.Add("creative");
		listwords.Add("caring");
		listwords.Add("creations"); 
		listwords.Add("central"); 
		
		wordOptions.Add("career", listwords);


		listwords = new List<string>();
		listwords.Add("creative");
		listwords.Add("caring");
		listwords.Add("creations"); 
		listwords.Add("central"); 
		
		wordOptions.Add("culture", listwords);


		listwords = new List<string>();
		listwords.Add("Birthhour");
		listwords.Add("Birthyear");
		listwords.Add("Birthplace"); 
		listwords.Add("Birthday present"); 
		
		wordOptions.Add("animals", listwords);


		listwords = new List<string>();
		listwords.Add("Birthhour");
		listwords.Add("Birthyear");
		listwords.Add("Birthplace"); 
		listwords.Add("Birthday present"); 
		
		wordOptions.Add("bachelor", listwords);


		listwords = new List<string>();
		listwords.Add("assigning");
		listwords.Add("assorting");
		listwords.Add("assembling"); 
		listwords.Add("assuming"); 
		
		wordOptions.Add("assistant", listwords);

		listwords = new List<string>();
		listwords.Add("co-workers");
		listwords.Add("friends");
		listwords.Add("college"); 
		listwords.Add("collection"); 
		
		wordOptions.Add("colleagues", listwords);


		listwords = new List<string>();
		listwords.Add("forging");
		listwords.Add("french");
		listwords.Add("familiar"); 
		listwords.Add("formular");  
		
		wordOptions.Add("foreign", listwords);


		listwords = new List<string>();
		listwords.Add("car");
		listwords.Add("camera");
		listwords.Add("camping"); 
		listwords.Add("calligraphy"); 
		
		wordOptions.Add("cat", listwords);


		listwords = new List<string>();
		listwords.Add("cars");
		listwords.Add("cameras");
		listwords.Add("camping"); 
		listwords.Add("calligraphy"); 
		
		wordOptions.Add("cats", listwords);


		listwords = new List<string>();
		listwords.Add("Birthhour");
		listwords.Add("Birthyear");
		listwords.Add("Birthplace"); 
		listwords.Add("Birthday present");  
		
		wordOptions.Add("Pondares", listwords);


		listwords = new List<string>();
		listwords.Add("family");
		listwords.Add("familiar");
		listwords.Add("fantastic"); 
		listwords.Add("funny"); 
		
		wordOptions.Add("famous", listwords);


		listwords = new List<string>();
		listwords.Add("Mac Otello");
		listwords.Add("Mab Othello");
		listwords.Add("Mabeth Othello"); 
		listwords.Add("Macbeth Othelo"); 
		
		wordOptions.Add("MacBeth Othello", listwords);


		listwords = new List<string>();
		listwords.Add("winter");
		listwords.Add("september");
		listwords.Add("spring"); 
		listwords.Add("autumn"); 
		
		wordOptions.Add("summer", listwords);


		listwords = new List<string>();
		listwords.Add("church");
		listwords.Add("college");
		listwords.Add("house"); 
		listwords.Add("boat"); 
		
		wordOptions.Add("cottage", listwords);


		listwords = new List<string>();
		listwords.Add("paris");
		listwords.Add("pub");
		listwords.Add("palace"); 
		listwords.Add("panama"); 
		
		wordOptions.Add("park", listwords);


		listwords = new List<string>();
		listwords.Add("edutainment");
		listwords.Add("endurance");
		listwords.Add("educating"); 
		listwords.Add("during"); 
		
		wordOptions.Add("education", listwords);


		listwords = new List<string>();
		listwords.Add("singing");
		listwords.Add("art");
		listwords.Add("records"); 
		listwords.Add("musicals"); 
		
		wordOptions.Add("music", listwords);


		listwords = new List<string>();
		listwords.Add("broadcasted");
		listwords.Add("transferred");
		listwords.Add("telecommunicated"); 
		listwords.Add("teleported"); 
		
		wordOptions.Add("televised", listwords);


		listwords = new List<string>();
		listwords.Add("interviews");
		listwords.Add("interrogations");
		listwords.Add("inspections"); 
		listwords.Add("investments"); 
		
		wordOptions.Add("investigations", listwords);


		listwords = new List<string>();
		listwords.Add("air");
		listwords.Add("attendees");
		listwords.Add("amount"); 
		listwords.Add("amazing"); 
		
		wordOptions.Add("atmosphere", listwords);


		listwords = new List<string>();
		listwords.Add("Kitchen");
		listwords.Add("Knitting");
		listwords.Add("Nate"); 
		listwords.Add("Kate"); 
		
		wordOptions.Add("Kittens", listwords);


		listwords = new List<string>();
		listwords.Add("Kitchen");
		listwords.Add("Knitting");
		listwords.Add("Nate"); 
		listwords.Add("Kate"); 
		
		wordOptions.Add("Kitten", listwords);

		listwords = new List<string>();
		listwords.Add("firefighters");
		listwords.Add("postmen");
		listwords.Add("soldiers"); 
		listwords.Add("security guards"); 
		
		wordOptions.Add("police", listwords);


		listwords = new List<string>();
		listwords.Add("serious");
		listwords.Add("servant");
		listwords.Add("sergeant"); 
		listwords.Add("surname"); 
		
		wordOptions.Add("surgeon", listwords);


		listwords = new List<string>();
		listwords.Add("imperial");
		listwords.Add("interesting");
		listwords.Add("essential"); 
		listwords.Add("intriguing"); 
		
		wordOptions.Add("impressive", listwords);


		listwords = new List<string>();
		listwords.Add("imperial");
		listwords.Add("interested");
		listwords.Add("essential"); 
		listwords.Add("intrigued"); 
		
		wordOptions.Add("impressed", listwords);
		

		listwords = new List<string>();
		listwords.Add("fantastic");
		listwords.Add("interesting");
		listwords.Add("intruiging"); 
		listwords.Add("fantastical"); 
		
		wordOptions.Add("fascinating", listwords);


		listwords = new List<string>();
		listwords.Add("effective");
		listwords.Add("efficient");
		listwords.Add("effect"); 
		listwords.Add("efficacy"); 
		
		wordOptions.Add("effort", listwords);


		listwords = new List<string>();
		listwords.Add("June");
		listwords.Add("Julia");
		listwords.Add("January"); 
		listwords.Add("Jane"); 
		
		wordOptions.Add("July", listwords);


		listwords = new List<string>();
		listwords.Add("long breaks");
		listwords.Add("looking around");
		listwords.Add("laughing"); 
		listwords.Add("less work"); 
		
		wordOptions.Add("lunch", listwords);


		listwords = new List<string>();
		listwords.Add("muh");
		listwords.Add("milk");
		listwords.Add("chocolate"); 
		listwords.Add("cheese"); 
		
		wordOptions.Add("cow", listwords);


		listwords = new List<string>();
		listwords.Add("diverse");
		listwords.Add("distinct");
		listwords.Add("distant"); 
		listwords.Add("doable"); 
		
		wordOptions.Add("different", listwords);


		listwords = new List<string>();
		listwords.Add("narrow");
		listwords.Add("naming");
		listwords.Add("narrative"); 
		listwords.Add("chaotic"); 
		
		wordOptions.Add("narcotic", listwords);

	}
}
