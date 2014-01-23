using UnityEngine;
using System.Collections;

public class InventoryItem : MonoBehaviour {

	bool empty = true; 
	GameObject inventoryLargeCloseButton;
	GameObject inventoryLargeScanButton;
	GameObject label;
	GameObject background;
	GameObject document;
	UILogic uiLogic;
	GameObject panel;

	// Use this for initialization
	void Start () 
	{
		panel = GameObject.Find("Panel");
		inventoryLargeCloseButton = GameObject.Find("InventoryLargeCloseButton");
		inventoryLargeScanButton = GameObject.Find("InventoryLargeScanButton");
		uiLogic = panel.GetComponent<UILogic>();

		label = GameObject.Find(this.name + "/Label");
		background = GameObject.Find(this.name + "/Background");
		document = GameObject.Find(this.name + "/Sprite");

		if(document != null)
		{
			label.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnPress (bool isPressed)
	{
		if (isPressed)
		{
			//Change Sprite
			//sprite.spriteName = "PJanepassport2";

			uiLogic.selectItemInInventory(document.GetComponent<UISprite>());
			inventoryLargeScanButton.SetActive(true);
			inventoryLargeCloseButton.GetComponentInChildren<UILabel>().text = "Show Jane";
		}
	}
}
