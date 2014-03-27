using UnityEngine;
using System.Collections;

public class UIButtonInput : MonoBehaviour {

	GameObject panel;
//	Alice alice;
	Alice2 alice2;
	UILogic uiLogic;
	Vector3 phonePosition;

	void Start(){
		panel = GameObject.Find("Panel");

//		alice = panel.GetComponent<Alice>();
		alice2 = panel.GetComponent<Alice2>();

		uiLogic = panel.GetComponent<UILogic>();
	}
	void OnPress (bool isPressed)
	{
		if (isPressed)
		{				
			if(this.name.Equals("Input1Button") || this.name == "Input2Button" || this.name == "Input3Button")
			{
//				alice.inputBot=this.GetComponentInChildren<UILabel>().text;
				alice2.inputBot=this.GetComponentInChildren<UILabel>().text;

//				Debug.Log ("Input: "+this.GetComponentInChildren<UILabel>().text);
			} else if(this.name == "InventoryViewAllButton")
			{
				uiLogic.SetInventoryIsActive(true);
				//this.gameObject.SetActive(false);
			} else if(this.name == "InventoryLargeCloseButton")
			{
				uiLogic.SetInventoryIsActive(false);
			} else if(this.name == "PhoneSprite")
			{
				if(uiLogic.PhoneIsActive)
				{
					uiLogic.SetPhoneIsActive(false);
				} else 
				{
					uiLogic.SetPhoneIsActive(true);
				}
			} else if(this.name == "PhoneCloseButton")
			{
				uiLogic.SetPhoneIsActive(false);
			} else if(this.name == "InventoryLargeScanButton")
			{
				//uiLogic.SetScanIsActive(true);
			} else if(this.name == "ScanPanelCloseButton")
			{
				//uiLogic.SetScanIsActive(false);
			}
		}
	}
	
}
