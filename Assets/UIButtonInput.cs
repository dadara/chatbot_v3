using UnityEngine;
using System.Collections;

public class UIButtonInput : MonoBehaviour {

	GameObject panel;
	Alice alice;
	UILogic uiLogic;
	
	void Start(){
		panel = GameObject.Find("Panel");
		alice = panel.GetComponent<Alice>();
		uiLogic = panel.GetComponent<UILogic>();
	}
	void OnPress (bool isPressed)
	{
		if (isPressed)
		{				
			if(this.name.Equals("Input1Button") || this.name == "Input2Button" || this.name == "Input3Button")
			{
				alice.inputBot=this.GetComponentInChildren<UILabel>().text;
			} else if(this.name == "InventoryViewAllButton")
			{
				uiLogic.setInventoryIsActive(true);
				//this.gameObject.SetActive(false);
			} else if(this.name == "InventoryLargeCloseButton")
			{
				uiLogic.setInventoryIsActive(false);
			}
		}
	}
	
}
