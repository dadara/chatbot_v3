using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using AIMLbot;

public class Alice2 : MonoBehaviour {

	StreamReader sr2;

	List<string> posKeywords;
	List<string> posKeywordsMOT; 
	List<string> posKeywordsCBS; 
	List<string> posKeywordsDipl; 
	List<string> posKeywordsNY;
	List<string> posKeyOrig;
	List<string> posKeyMOTorig; 
	List<string> posKeyCBSorig; 
	List<string> posKeyDorig; 
	List<string> posKeyNYorig;

	List<string> topics;
	int cnt;

	GameObject janecb;
	
	GameObject Input1btn;
	GameObject Input2btn;
	GameObject Input3btn;
	
	//	UILabel uilab;
	UILabel jane;

	UILabel Input1btnLabel;
	UILabel Input2btnLabel;
	UILabel Input3btnLabel;
	
	public string inputBot;
	public string outputBot;
	
	string posUserInput;
	
	private Bot myBot;
	private User myUser;
	
	string cacheInputBot;
	public string topicSet;
	string[] chatHistory;
	float startTime;
	float topicChangeTime;
	bool fileSaved;

	UILabel chatHistoryLabel;
	string chatHistoryString;
	GameObject chatHistoryPanel;


// Path were the possible Input Textfiles are saved
	string path = "Assets"+Path.DirectorySeparatorChar+"AIMLbot"+Path.DirectorySeparatorChar;

//	List which contains the documents received from Jane (for inventary) 
	public List<string> inventary;
//	last document got from Jane
	string cacheDocument;


	// Use this for initialization
	void Start () {
		cnt=0;
		posKeywords = new List<string>();
		posKeywordsMOT = new List<string>();
		posKeywordsCBS = new List<string>(); 
		posKeywordsDipl = new List<string>(); 
		posKeywordsNY = new List<string>();

//		List to choose randomly a topic

		topics = new List<string>();
		topics.Add("NOW");
		topics.Add("MARCHOFTIME");
		topics.Add("CBS");
		topics.Add("DIPLOMAT");
		topics.Add("NEWYORKER");


//*************read in possible Keywords for the different topics (time modes of Jane)***********************************//
		path = "Assets"+Path.DirectorySeparatorChar+"AIMLbot"+Path.DirectorySeparatorChar+"posKeywordsNew.txt";
//		path = Resources.Load("aiml"+Path.DirectorySeparatorChar+"posKeywordsNew.txt");
		posKeywords = getKeywords(path, posKeywords);
		path = "Assets"+Path.DirectorySeparatorChar+"AIMLbot"+Path.DirectorySeparatorChar+"posKeywordsMOT.txt";
		posKeywordsMOT = getKeywords(path, posKeywordsMOT);
		path = "Assets"+Path.DirectorySeparatorChar+"AIMLbot"+Path.DirectorySeparatorChar+"posKeywordsCBS.txt";
		posKeywordsCBS = getKeywords(path, posKeywordsCBS);
		path = "Assets"+Path.DirectorySeparatorChar+"AIMLbot"+Path.DirectorySeparatorChar+"posKeywordsD.txt";
		posKeywordsDipl = getKeywords(path, posKeywordsDipl);
		path = "Assets"+Path.DirectorySeparatorChar+"AIMLbot"+Path.DirectorySeparatorChar+"posKeywordsNY.txt";
		posKeywordsNY = getKeywords(path, posKeywordsNY);

		posKeyOrig = new List<string>(posKeywords);
		posKeyMOTorig = new List<string>(posKeywordsMOT); 
		posKeyCBSorig = new List<string>(posKeywordsCBS); 
		posKeyDorig = new List<string>(posKeywordsDipl); 
		posKeyNYorig = new List<string>(posKeywordsNY);

//******************initialize chatbot*****************************************************************************************//
		myBot = new Bot();
		myUser = new User("consoleUser", myBot);

		myBot.loadSettings();
		myBot.isAcceptingUserInput = false;
		myBot.loadAIMLFromFiles();
		myBot.isAcceptingUserInput = true;

//******************User Interface Elements for chat (input buttons, textfield for Jane's answer*******************************//
//		variable to check if input has changed (=Input buutton was pressed, Text saved in UIButtonInput in this string)
		cacheInputBot = "";

		janecb = GameObject.Find("JaneChatbox");
		jane = janecb.GetComponent<UILabel>();
		
		Input1btn = GameObject.Find("Input1Button");
		Input2btn = GameObject.Find("Input2Button");
		Input3btn = GameObject.Find("Input3Button");
		
		Input1btnLabel = Input1btn.GetComponentInChildren<UILabel>();
		Input2btnLabel = Input2btn.GetComponentInChildren<UILabel>();
		Input3btnLabel = Input3btn.GetComponentInChildren<UILabel>();
		
		Input1btnLabel.text = "home";
		Input2btnLabel.text = "call taxi";
		Input3btnLabel.text = "here alone";

		chatHistory = new string[1000];
		fileSaved = false;
		topicChangeTime = 0;
		topicSet = "";

		chatHistoryLabel = GameObject.Find ("ChatHistoryLabel").GetComponent<UILabel>();
		chatHistoryString="";
		chatHistoryPanel = GameObject.Find("ChatHistoryPanel");

		inventary = new List<string>();
		cacheDocument = "";

	}
	
	// Update is called once per frame
	void Update () {
	
		Debug.Log("posKEyOrig at 0: "+posKeyOrig.ElementAt(0));
		if(inventary.Count>0){
			for(int i=0; i<inventary.Count;i++){
				Debug.Log("Inventary: "+inventary.ElementAt(i));
			}
		}


		startTime += Time.deltaTime;
		topicChangeTime += Time.deltaTime;

		
		int startTimeInt = (int) startTime;

		if(topicChangeTime>=30){
			ChangeTopic();

		}
		
		//		1) bot answers to keywords

		if(inputBot.Length > 0 && !inputBot.Equals(cacheInputBot)){
			inputBot = RemoveSpecChar(inputBot);
			inputBot = inputBot.ToUpper ();
			outputBot = getOutput(inputBot);
//			Debug.Log ("inputBot: "+inputBot+" outputBot: "+outputBot);
			
			jane.text = "B: "+outputBot;
			chatHistory[cnt] = inputBot;
			cnt++;
			chatHistory[cnt] = outputBot;
			cnt++;

			chatHistoryString = inputBot + Environment.NewLine + outputBot +Environment.NewLine + chatHistoryString;
			chatHistoryPanel.GetComponent<UIDraggablePanel>().ResetPosition();
			chatHistoryLabel.text = chatHistoryString;
		
			//checked finish condition Test how much time it needs to come to the end
			//			choose posKeywordsString for input buttons

			setInputButtons();



		}
		
		cacheInputBot = inputBot;








		//		if(startTime>600){
		int numberChatHistoryFile=0;
		String line="";
		
		if(Input1btnLabel.text.Equals("end") && !fileSaved){
			try
			{
				using (StreamReader sr = new StreamReader("E:\\Dokumente\\TU\\Diplomarbeit\\chatHistory\\number.txt"))
				{
					line = sr.ReadToEnd();
					numberChatHistoryFile = Convert.ToInt32(line);
					numberChatHistoryFile++;
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("The file could not be read:");
				Console.WriteLine(e.Message);
			}


			System.IO.File.WriteAllText(@"E:\Dokumente\TU\Diplomarbeit\chatHistory\number.txt", numberChatHistoryFile.ToString());
			
			chatHistory[cnt] = "PlayTimeInSeconds: "+startTime;
			numberChatHistoryFile--;

			System.IO.File.WriteAllLines(@"E:\Dokumente\TU\Diplomarbeit\chatHistory\chatHistory"+numberChatHistoryFile.ToString()+".txt", chatHistory);
			//			Input1btnLabel.text = "END";
			//			Input2btnLabel.text = "END";
			//			Input3btnLabel.text = "END";
			fileSaved = true;

		}


	}

	public string RemoveSpecChar(string orig){
		StringBuilder sb = new StringBuilder();
		foreach (char c in orig)
		{
			if (Char.IsLetterOrDigit(c) || c == ' ' )
			{ sb.Append(c); }
		}
		return sb.ToString();
	}

	public string getOutput(string input)
	{
		Request r = new Request(input, myUser, myBot);
		Result res = myBot.Chat(r);
//		if(!res.user.Topic.Equals(topicSet)){
//			topicSet = res.user.Topic;
		if(res.user.Document!=null && !res.user.Document.Equals(cacheDocument)){
			Debug.Log("Document: "+res.user.Document);
			inventary.Add(res.user.Document);
			cacheDocument = res.user.Document;
		}
//		else{
//						Debug.Log("OLDTopic set: "+topicSet);
//		}
		
		return(res.Output);
	}

	public List<string> getKeywords(string path, List<string> kwList){

		try{
			using (StreamReader sr = File.OpenText(path)) 
			{
				string s = "";
				while ((s = sr.ReadLine()) != null) 
				{
//					Debug.Log(s);
					kwList.Add(s);
				}
			}
		}catch (Exception e)
		{
//			Console.WriteLine("The file could not be read:");
			Console.WriteLine(e.Message);
		}

		return kwList;

	}

	public void setInputButtons(){
		//checked finish condition Test how much time it needs to come to the end
		//			choose posKeywordsString for input buttons
		string button1;
		string button2;
		string button3;
		
		if(topicSet.Equals("MARCHOFTIME")){
			if(posKeywordsMOT.Count >=3){
				button1 = posKeywordsMOT.ElementAt(0);
				button2 = posKeywordsMOT.ElementAt(1);
				button3 = posKeywordsMOT.ElementAt(2);
				Input1btnLabel.text = button1;
				Input2btnLabel.text = button2;
				Input3btnLabel.text = button3;
				posKeywordsMOT.Remove(button1);
				posKeywordsMOT.Remove(button2);
				posKeywordsMOT.Remove(button3);
			}else{
				posKeywordsMOT = new List<string>(posKeyMOTorig);
				button1 = posKeywordsMOT.ElementAt(0);
				button2 = posKeywordsMOT.ElementAt(1);
				button3 = posKeywordsMOT.ElementAt(2);
				Input1btnLabel.text = button1;
				Input2btnLabel.text = button2;
				Input3btnLabel.text = button3;
				posKeywordsMOT.Remove(button1);
				posKeywordsMOT.Remove(button2);
				posKeywordsMOT.Remove(button3);
			}
		}else if(topicSet.Equals("CBS")){
			if(posKeywordsCBS.Count >=3){
				button1 = posKeywordsCBS.ElementAt(0);
				button2 = posKeywordsCBS.ElementAt(1);
				button3 = posKeywordsCBS.ElementAt(2);
				Input1btnLabel.text = button1;
				Input2btnLabel.text = button2;
				Input3btnLabel.text = button3;
				posKeywordsCBS.Remove(button1);
				posKeywordsCBS.Remove(button2);
				posKeywordsCBS.Remove(button3);
			}
			else{
				posKeywordsCBS = new List<string>(posKeyCBSorig);
				button1 = posKeywordsCBS.ElementAt(0);
				button2 = posKeywordsCBS.ElementAt(1);
				button3 = posKeywordsCBS.ElementAt(2);
				Input1btnLabel.text = button1;
				Input2btnLabel.text = button2;
				Input3btnLabel.text = button3;
				posKeywordsCBS.Remove(button1);
				posKeywordsCBS.Remove(button2);
				posKeywordsCBS.Remove(button3);
			}
			
		}else if(topicSet.Equals("DIPLOMAT")){
			if(posKeywordsDipl.Count >=3){
				button1 = posKeywordsDipl.ElementAt(0);
				button2 = posKeywordsDipl.ElementAt(1);
				button3 = posKeywordsDipl.ElementAt(2);
				Input1btnLabel.text = button1;
				Input2btnLabel.text = button2;
				Input3btnLabel.text = button3;
				posKeywordsDipl.Remove(button1);
				posKeywordsDipl.Remove(button2);
				posKeywordsDipl.Remove(button3);
			}
			else{
				posKeywordsDipl = new List<string>(posKeyDorig);
				button1 = posKeywordsDipl.ElementAt(0);
				button2 = posKeywordsDipl.ElementAt(1);
				button3 = posKeywordsDipl.ElementAt(2);
				Input1btnLabel.text = button1;
				Input2btnLabel.text = button2;
				Input3btnLabel.text = button3;
				posKeywordsDipl.Remove(button1);
				posKeywordsDipl.Remove(button2);
				posKeywordsDipl.Remove(button3);
			}
			
		}else if(topicSet.Equals("NEWYORKER")){
			if(posKeywordsNY.Count >=3){
				button1 = posKeywordsNY.ElementAt(0);
				button2 = posKeywordsNY.ElementAt(1);
				button3 = posKeywordsNY.ElementAt(2);
				Input1btnLabel.text = button1;
				Input2btnLabel.text = button2;
				Input3btnLabel.text = button3;
				posKeywordsNY.Remove(button1);
				posKeywordsNY.Remove(button2);
				posKeywordsNY.Remove(button3);
			}
			else{
				posKeywordsNY = new List<string>(posKeyNYorig);
				button1 = posKeywordsNY.ElementAt(0);
				button2 = posKeywordsNY.ElementAt(1);
				button3 = posKeywordsNY.ElementAt(2);
				Input1btnLabel.text = button1;
				Input2btnLabel.text = button2;
				Input3btnLabel.text = button3;
				posKeywordsNY.Remove(button1);
				posKeywordsNY.Remove(button2);
				posKeywordsNY.Remove(button3);
			}
			
		}else{
			
			if(posKeywords.Count >=3){
				button1 = posKeywords.ElementAt(0);
				button2 = posKeywords.ElementAt(1);
				button3 = posKeywords.ElementAt(2);
				Input1btnLabel.text = button1;
				Input2btnLabel.text = button2;
				Input3btnLabel.text = button3;
				posKeywords.Remove(button1);
				posKeywords.Remove(button2);
				posKeywords.Remove(button3);
			}
			else{
				posKeywords = new List<string>(posKeyOrig);
				button1 = posKeywords.ElementAt(0);
				button2 = posKeywords.ElementAt(1);
				button3 = posKeywords.ElementAt(2);
				Input1btnLabel.text = button1;
				Input2btnLabel.text = button2;
				Input3btnLabel.text = button3;
				posKeywords.Remove(button1);
				posKeywords.Remove(button2);
				posKeywords.Remove(button3);
			}
		}
	}

	public void ChangeTopic(){
		int topicChooser = UnityEngine.Random.Range(0,topics.Count);
		Debug.Log("TOPICCHANGE topicChooser: "+topicChooser+" "+topics.ElementAt(topicChooser));
		topicSet = topics.ElementAt(topicChooser);
		string test = getOutput("SETTOPIC"+topicSet);
		topicChangeTime = 0;
		setInputButtons();
	}
	
	
}
