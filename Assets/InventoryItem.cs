﻿using UnityEngine;
using System.Collections;

public class InventoryItem : MonoBehaviour {

	GameObject inventoryLargeCloseButton;
	GameObject inventoryLargeCancelButton;
	GameObject inventoryLargeViewImageButton;
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
		//inventoryLargeCancelButton = uiLogic.inventoryLargeCancelButton;
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
			inventoryLargeCancelButton = uiLogic.inventoryLargeCancelButton;
			inventoryLargeViewImageButton = uiLogic.inventoryLargeViewImageButton;

			if(document != null)
			{
				uiLogic.selectItemInInventory(document.GetComponent<UISprite>());
				inventoryLargeCancelButton.SetActive(true);
				inventoryLargeViewImageButton.SetActive(true);
				inventoryLargeCloseButton.GetComponentInChildren<UILabel>().text = "Show Jane";
			} else 
			{
				inventoryLargeViewImageButton.SetActive(false);
				inventoryLargeCancelButton.SetActive(false);
				inventoryLargeCloseButton.GetComponentInChildren<UILabel>().text = "Close";
			}
		}
	}
}
