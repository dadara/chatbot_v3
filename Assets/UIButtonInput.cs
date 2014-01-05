using UnityEngine;
using System.Collections;

public class UIButtonInput : MonoBehaviour {

	GameObject panel;
	Alice alice;
	
	void Start(){
		panel = GameObject.Find("Panel");
		alice = panel.GetComponent<Alice>();
	}
	void OnPress (bool isPressed)
	{
		if (isPressed){
			alice.inputBot=this.GetComponentInChildren<UILabel>().text;
		}
	}
	
}
