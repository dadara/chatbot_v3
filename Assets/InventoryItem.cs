using UnityEngine;
using System.Collections;

public class InventoryItem : MonoBehaviour {

	GameObject inventoryLargeCloseButton;
	//GameObject inventoryLargeScanButton;
	GameObject label;
	GameObject background;
	GameObject document;
	UILogic uiLogic;
	GameObject panel;

	// Use this for initialization
	void Start () 
	{
		panel = GameObject.Find("Panel");
		//inventoryLargeCloseButton = uiLogic.inventoryLargeCloseButton;
		//inventoryLargeScanButton = uiLogic.inventoryLargeScanButton;
		uiLogic = panel.GetComponent<UILogic>();

		background = GameObject.Find(this.name + "/Background");
		document = GameObject.Find(this.name + "/Sprite");

	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	void OnPress (bool isPressed)
	{
		if (isPressed)
		{
			inventoryLargeCloseButton = uiLogic.inventoryLargeCloseButton;
			//inventoryLargeScanButton = uiLogic.inventoryLargeScanButton;


			if(document != null)
			{
				uiLogic.selectItemInInventory(document.GetComponent<UISprite>());
				//inventoryLargeScanButton.SetActive(true);
				inventoryLargeCloseButton.GetComponentInChildren<UILabel>().text = "Show Jane";
			} else 
			{
				//inventoryLargeScanButton.SetActive(false);
				inventoryLargeCloseButton.GetComponentInChildren<UILabel>().text = "Close";
			}
		}
	}
}
