using UnityEngine;
using System.Collections;

public class getTextInput : MonoBehaviour {
	

	GameObject inputLabel;
	GameObject janecb;
	UILabel uilab;
	UILabel jane;
	
	// Use this for initialization
	void Start () {
		inputLabel = GameObject.Find("InputLabel");
		uilab = inputLabel.GetComponent<UILabel>();
		janecb = GameObject.Find("JaneChatbox");
		jane = janecb.GetComponent<UILabel>();

	}
	
	// Update is called once per frame
	void Update () {
		if(uilab.text!=null){
			
			if(Input.GetKey(KeyCode.Return)){
				Debug.Log("keycode is RETURN: ");
				jane.text = "Hallo";
				uilab.text = "";
			}
		    Debug.Log("I typed: " + uilab.text);
		}
//		uiinp.
	}
	
	
}
