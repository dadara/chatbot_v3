using UnityEngine;
using System.Collections;

public class InventoryItem : MonoBehaviour {

	bool empty = true; 
	GameObject inventoryLargeCloseButton;
	GameObject inventoryLargeScanButton;
	public UISprite sprite;

	// Use this for initialization
	void Start () 
	{
		inventoryLargeCloseButton = GameObject.Find("InventoryLargeCloseButton");
		inventoryLargeScanButton = GameObject.Find("InventoryLargeScanButton");
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


			inventoryLargeScanButton.SetActive(true);
			inventoryLargeCloseButton.GetComponentInChildren<UILabel>().text = "Show Jane";
		}
	}
}
