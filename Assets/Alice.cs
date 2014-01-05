using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIMLbot;

//namespace Chatbot
public class Alice : MonoBehaviour {

//	GameObject inputLabel;
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


	// Use this for initialization
	void Start () {

		janecb = GameObject.Find("JaneChatbox");
		jane = janecb.GetComponent<UILabel>();

		Input1btn = GameObject.Find("Input1Button");
		Input2btn = GameObject.Find("Input2Button");
		Input3btn = GameObject.Find("Input3Button");

		Input1btnLabel = Input1btn.GetComponentInChildren<UILabel>();
		Input2btnLabel = Input2btn.GetComponentInChildren<UILabel>();
		Input3btnLabel = Input3btn.GetComponentInChildren<UILabel>();

//		Input1btnLabel.text = "balalal";

		myBot = new Bot();
		myUser = new User("consoleUser", myBot);


		int cnt=0;
		string path = myBot.loadSettings(cnt);
		Debug.Log("Alice.cs path: "+path);

		myBot.loadSettings();
		myBot.isAcceptingUserInput = false;
		myBot.loadAIMLFromFiles();
		myBot.isAcceptingUserInput = true;

	}
	
	// Update is called once per frame
	void Update () {

		if(inputBot.Length > 0){
			inputBot = RemoveSpecChar(inputBot);
			outputBot = getOutput(inputBot);
//			if(inputBot.Contains("Are you sure that you are still working Arent you retired")){
//				Debug.Log("inputBot: "+inputBot);
//				outputBot = getOutput("ARE YOU SURE THAT YOU ARE STILL WORKING ARENT YOU RETIRED");
//			}
			jane.text = "B: "+outputBot;
		}

		if(outputBot.Length > 0){
			outputBot = RemoveSpecChar(outputBot);
			Debug.Log ("outputBot; "+outputBot);

			posUserInput = getOutput(outputBot);
//			Debug.Log ("posUserInput; "+posUserInput);
//			Debug.Log(posUserInput.IndexOf("#")+" "+ posUserInput.LastIndexOf("#"));
			int index = posUserInput.IndexOf("#");
			if(index>0){
				Input1btnLabel.text = posUserInput.Substring(0,	index);
				posUserInput = posUserInput.Remove(0, index+1);
				Debug.Log("1: "+posUserInput);
				index = posUserInput.IndexOf("#");
				Input2btnLabel.text = posUserInput.Substring(0,	index);
				posUserInput = posUserInput.Remove(0, index+1);
				Input3btnLabel.text = posUserInput;
			}
		}

//		answerBot = answerBot.Replace("#","\n");
//		if(answerBot.Contains("{")){
//			Debug.Log("answer: "+answerBot);
//		}



	}

	/// <summary>
	/// This method takes an input string, then finds a response using the the AIMLbot library and returns it
	/// </summary>
	/// <param name="input">Input Text</param>
	/// <returns>Response</returns>
	public string getOutput(string input)
	{
		Request r = new Request(input, myUser, myBot);
		Result res = myBot.Chat(r);
		return(res.Output);
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

	private string getKeyInput(){
					if(Input.GetKeyDown(KeyCode.A)){
						return "a";
					}else if(Input.GetKeyDown(KeyCode.B)){
						return "b";
					}else if(Input.GetKeyDown(KeyCode.C)){
						return "c";
					}else if(Input.GetKeyDown(KeyCode.D)){
						return "d";
					}else if(Input.GetKeyDown(KeyCode.E)){
						return "e";
					}else if(Input.GetKeyDown(KeyCode.F)){
						return "f";
					}else if(Input.GetKeyDown(KeyCode.G)){
						return "g";
					}else if(Input.GetKeyDown(KeyCode.H)){
						return "h";
					}else if(Input.GetKeyDown(KeyCode.I)){
						return "i";
					}else if(Input.GetKeyDown(KeyCode.J)){
						return "j";
					}else if(Input.GetKeyDown(KeyCode.K)){
						return "k";
					}else if(Input.GetKeyDown(KeyCode.L)){
						return "l";
					}else if(Input.GetKeyDown(KeyCode.M)){
						return "m";
					}else if(Input.GetKeyDown(KeyCode.N)){
						return "n";
					}else if(Input.GetKeyDown(KeyCode.O)){
						return "o";
					}else if(Input.GetKeyDown(KeyCode.P)){
						return "p";
					}else if(Input.GetKeyDown(KeyCode.Q)){
						return "q";
					}else if(Input.GetKeyDown(KeyCode.R)){
						return "r";
					}else if(Input.GetKeyDown(KeyCode.S)){
						return "s";
					}else if(Input.GetKeyDown(KeyCode.T)){
						return "t";
					}else if(Input.GetKeyDown(KeyCode.U)){
						return "u";
					}else if(Input.GetKeyDown(KeyCode.V)){
						return "v";
					}else if(Input.GetKeyDown(KeyCode.W)){
						return "w";
					}else if(Input.GetKeyDown(KeyCode.X)){
						return "x";
					}else if(Input.GetKeyDown(KeyCode.Y)){
						return "y";
					}else if(Input.GetKeyDown(KeyCode.Z)){
						return "z";
					}else if(Input.GetKeyDown(KeyCode.Space)){
						return " ";
					}else if(Input.GetKeyDown(KeyCode.Semicolon)){
						return ";";
					}else if(Input.GetKeyDown(KeyCode.Comma)){
						return ",";
					}else if(Input.GetKeyDown(KeyCode.Question)){
						return "?";
					}else if(Input.GetKeyDown(KeyCode.Exclaim)){
						return "!";
					}else if(Input.GetKeyDown(KeyCode.Period)){
						return ".";
					}else if(Input.GetKeyDown(KeyCode.Delete)){
						return "";
					}else if(Input.GetKeyDown(KeyCode.Backspace)){
						return "";
					}
					
					return "no";
	}


}