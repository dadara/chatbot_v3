using UnityEngine;
using System.Collections;

public class UIButtonInput : MonoBehaviour {

	GameObject panel;
//	Alice alice;
	Alice2 alice2;
	UILogic uiLogic;
	Vector3 phonePosition;
	bool closeGame = false;

	GameObject labelBtnKeyword;

	GameObject mainCamera;
	GameLogic gameLogic;

	void Start(){
		panel = GameObject.Find("Panel");

//		alice = panel.GetComponent<Alice>();
		alice2 = panel.GetComponent<Alice2>();

		uiLogic = panel.GetComponent<UILogic>();

		mainCamera = GameObject.Find("MainCamera");
		gameLogic = mainCamera.GetComponent<GameLogic>();
	}
	void OnPress (bool isPressed)
	{
		if (isPressed)
		{				
			if(this.name.Equals("Input1Button") || this.name == "Input2Button" || this.name == "Input3Button")
			{
				//alice.inputBot=this.GetComponentInChildren<UILabel>().text;
				//alice2.inputBot=this.GetComponentInChildren<UILabel>().text;

				if(labelBtnKeyword == null)
				{
					foreach (Transform child in this.transform)
					{
						if(child.name == "LabelBtnKeyword")
						{
							labelBtnKeyword = child.gameObject;
						}
					}
				}

				alice2.inputBot=labelBtnKeyword.GetComponent<UILabel>().text;

			} else if(this.name == "InventoryViewAllButton")
			{
				uiLogic.SetInventoryIsActive(true, false);
				//this.gameObject.SetActive(false);
			} else if(this.name == "InventoryLargeCloseButton")
			{
				uiLogic.SetInventoryIsActive(false, true);
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
			} else if(this.name == "InventoryLargeCancelButton")
			{
				uiLogic.SetInventoryIsActive(false, false);
			} else if(this.name == "ScanPanelCloseButton")
			{
				uiLogic.SetScanIsActive(false);
			} else if(this.name == "InventoryLargeViewImageButton")
			{
				uiLogic.SetScanIsActive(true);
			} else if(this.name == "ContinueBtn")
			{
				if(!closeGame)
				{
					GameObject.Find("EndscreenLabel").GetComponent<UILabel>().text = "About 'Do I Know You?'";
					GameObject.Find("EndScreenImage").SetActive(false);

					GameObject.Find("EndscreenText").GetComponent<UILabel>().lineWidth = 730;
					GameObject.Find("EndscreenText").transform.position = new Vector3(0,0,0);
					GameObject.Find("EndscreenText").GetComponent<UILabel>().text = "Dementia is a growing issue of our society and governments are faced with the challenges of providing treatment and care for a growing number of people with memory loss. This game was designed to raise awareness of dementia."
						+ "\n" + "\n" + "Thanks for playing."
						+ "\n" + "\n" + "created by:"
							+ "\n" + "Sebastian Czekierski-Werner    czekierski@gmail.com"
							+ "\n" + "Daniela Ramsauer    e0207433@student.tuwien.ac.at";
					this.GetComponentInChildren<UILabel>().text = "Quit";
					closeGame = true;
				} else 
				{
					Application.Quit();
				}
			} else if(this.name == "SkipButton")
			{
				gameLogic.ActivateMainGame();
			}
		}
	}
	
}
