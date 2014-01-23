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
	bool fileSaved;


	// Use this for initialization
	void Start () {
		cnt=0;
		posKeywords = new List<string>();
		posKeywordsMOT = new List<string>();

		try{

			string path = "Assets"+Path.DirectorySeparatorChar+"AIMLbot"+Path.DirectorySeparatorChar+"posKeywords.txt";
			using (StreamReader sr = File.OpenText(path)) 
			{
				string s = "";
				while ((s = sr.ReadLine()) != null) 
				{
//					Debug.Log(s);
					posKeywords.Add(s);
				}
			}
			string pathMOT = "Assets"+Path.DirectorySeparatorChar+"AIMLbot"+Path.DirectorySeparatorChar+"posKeywordsMOT.txt";
			using (StreamReader sr = File.OpenText(pathMOT)) 
			{
				string s = "";
				while ((s = sr.ReadLine()) != null) 
				{
					Debug.Log(s);
					posKeywordsMOT.Add(s);
				}
			}
		}catch (Exception e)
		{
			Console.WriteLine("The file could not be read:");
			Console.WriteLine(e.Message);
		}


		myBot = new Bot();
		myUser = new User("consoleUser", myBot);

		myBot.loadSettings();
		myBot.isAcceptingUserInput = false;
		myBot.loadAIMLFromFiles();
		myBot.isAcceptingUserInput = true;
		
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

	}
	
	// Update is called once per frame
	void Update () {
	
//		1) bot answers to keywords

		if(inputBot.Length > 0 && !inputBot.Equals(cacheInputBot)){
			inputBot = RemoveSpecChar(inputBot);
			outputBot = getOutput(inputBot);
			Debug.Log ("inputBot: "+inputBot+" outputBot: "+outputBot);
			
			jane.text = "B: "+outputBot;
			chatHistory[cnt] = inputBot;
			cnt++;
			chatHistory[cnt] = outputBot;
			cnt++;
		

//			choose posKeywordsString for input buttons
			string button1;
			string button2;
			string button3;

			if(topicSet=="MARCHOFTIME"){
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
	//				Debug.Log("after Delete: "+posKeywords.ElementAt(0));
	//				Debug.Log("after Delete: "+posKeywords.ElementAt(1));
	//				Debug.Log("after Delete: "+posKeywords.ElementAt(2));
	//				Debug.Log("after Delete: "+posKeywords.ElementAt(3));
	//				Debug.Log("after Delete: "+posKeywords.ElementAt(4));
	//				Debug.Log("after Delete: "+posKeywords.ElementAt(5));
	//				Debug.Log("after Delete: "+posKeywords.ElementAt(6));
				}else{
					Input1btnLabel.text = "end";
					Input2btnLabel.text = "end";
					Input3btnLabel.text = "end";
				}
			}

		}

		cacheInputBot = inputBot;


		//checked finish condition Test how much time it needs to come to the end
		
		startTime += Time.deltaTime;
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
		if(!res.user.Topic.Equals(topicSet)){
			topicSet = res.user.Topic;
//		Debug.Log("NEWTopic set: "+topicSet);
		}else{
			//			Debug.Log("OLDTopic set: "+topicSet);
		}
		
		return(res.Output);
	}




}
