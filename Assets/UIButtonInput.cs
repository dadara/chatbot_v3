using UnityEngine;
using System.Collections;

public class UIButtonInput : MonoBehaviour {

	GameObject panel;
	Alice alice;
	UILogic uiLogic;
	Vector3 phonePosition;
	
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
				uiLogic.SetInventoryIsActive(true);
				//this.gameObject.SetActive(false);
			} else if(this.name == "InventoryLargeCloseButton")
			{
				uiLogic.SetInventoryIsActive(false);
			} else if(this.name == "Phone")
			{
				uiLogic.SetPhoneIsActive(true);
			} else if(this.name == "PhoneCloseButton")
			{
				uiLogic.SetPhoneIsActive(false);
			}
		}
	}
	
}
