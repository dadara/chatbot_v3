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
	string topicSet;
	string[] chatHistory;
	float startTime;
	float topicChangeTime;
	bool fileSaved;

// Path were the possible Input Textfiles are saved
	string path = "Assets"+Path.DirectorySeparatorChar+"AIMLbot"+Path.DirectorySeparatorChar;
	

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
//		topics.Add("NOW");
//		topics.Add("MARCHOFTIME");
		topics.Add("CBS");
		topics.Add("DIPLOMAT");
		topics.Add("NEWYORKER");

//
//		SETTOPICMOT</pattern>
//			<template><think><set name="topic">MARCHOFTIME</set></think>balubalub</template>
//				</category>
//				<category><pattern>SETTOPICCBS</pattern>
//				<template><think><set name="topic">CBS</set></think>balubalub</template>
//				</category>
//				<category><pattern>SETTOPICDIP</pattern>
//				<template><think><set name="topic">DIPLOMAT</set></think>balubalub</template>
//				</category>
//				<category><pattern>SETTOPICNY</pattern>
//				<template><think><set name="topic">NEWYORKER</set></think>balubalub</template>
//				</category>
//				<category><pattern>SETTOPICNOW


//*************read in possible Keywords for the different topics (time modes of Jane)***********************************//
		path = "Assets"+Path.DirectorySeparatorChar+"AIMLbot"+Path.DirectorySeparatorChar+"posKeywords.txt";
		posKeywords = getKeywords(path, posKeywords);
		path = "Assets"+Path.DirectorySeparatorChar+"AIMLbot"+Path.DirectorySeparatorChar+"posKeywordsMOT.txt";
		posKeywordsMOT = getKeywords(path, posKeywordsMOT);
		path = "Assets"+Path.DirectorySeparatorChar+"AIMLbot"+Path.DirectorySeparatorChar+"posKeywordsCBS.txt";
		posKeywordsCBS = getKeywords(path, posKeywordsCBS);
		path = "Assets"+Path.DirectorySeparatorChar+"AIMLbot"+Path.DirectorySeparatorChar+"posKeywordsDipl.txt";
		posKeywordsDipl = getKeywords(path, posKeywordsDipl);
		path = "Assets"+Path.DirectorySeparatorChar+"AIMLbot"+Path.DirectorySeparatorChar+"posKeywordsNY.txt";
		posKeywordsNY = getKeywords(path, posKeywordsNY);

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
		Input2btnLabel.text = "address";
		Input3btnLabel.text = "call taxi";

		chatHistory = new string[1000];
		fileSaved = false;
		topicChangeTime = 0;
		topicSet = "";

	}
	
	// Update is called once per frame
	void Update () {
	

		startTime += Time.deltaTime;
		topicChangeTime += Time.deltaTime;

		
		int startTimeInt = (int) startTime;

		if(topicChangeTime>=30){
			int topicChooser = UnityEngine.Random.Range(0,topics.Count);
//			Debug.Log("TOPICCHANGE topicChooser: "+topicChooser+" "+topics.ElementAt(topicChooser));
			topicSet = topics.ElementAt(topicChooser);
			string test = getOutput("SETTOPIC"+topicSet);
			Debug.Log("input: SETTOPIC"+topicSet+", output: "+test);
			topicChangeTime = 0;

		}
		
		//		1) bot answers to keywords

		if(inputBot.Length > 0 && !inputBot.Equals(cacheInputBot)){
			inputBot = RemoveSpecChar(inputBot);
			outputBot = getOutput(inputBot);
//			Debug.Log ("inputBot: "+inputBot+" outputBot: "+outputBot);
			
			jane.text = "B: "+outputBot;
			chatHistory[cnt] = inputBot;
			cnt++;
			chatHistory[cnt] = outputBot;
			cnt++;
		
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
				}
			}else if(topicSet.Equals("CBS")){
				Debug.Log("CBS "+topicSet);
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
				
			}else if(topicSet.Equals("DIPLOMAT")){
				Debug.Log("DIPLOMAT "+topicSet);
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
				
			}else if(topicSet.Equals("NEWYORKER")){
				Debug.Log("NEWYORKER "+topicSet);
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
				
			}else{
				Debug.Log("NOW "+topicSet);
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
			}

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
//		Debug.Log("NEWTopic set: "+topicSet);
//		}else{
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
		}
	}
	
	
}
