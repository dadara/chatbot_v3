﻿using UnityEngine;
using System.Collections;

public class UILogic : MonoBehaviour {

	private bool inventoryIsActive = false;

	GameObject inventoryLargeBackground;
	GameObject inventoryLargeCloseButton;
	GameObject inventoryLargeHeader;
	GameObject inventoryLargeLabel;
	GameObject inventoryLargeScrollBar;
	GameObject inventoryLargePanel;

	// Use this for initialization
	void Start () 
	{
		inventoryLargeBackground = GameObject.Find("InventoryLargeBackground");
		inventoryLargeCloseButton = GameObject.Find("InventoryLargeCloseButton");
		inventoryLargeHeader = GameObject.Find("InventoryLargeHeader");
		inventoryLargeLabel = GameObject.Find("InventoryLargeLabel");
		inventoryLargeScrollBar = GameObject.Find("InventoryLargeScrollBar");
		inventoryLargePanel = GameObject.Find("InventoryLargePanel");

		inventoryLargeBackground.SetActive(false);
		inventoryLargeCloseButton.SetActive(false);
		inventoryLargeHeader.SetActive(false);
		inventoryLargeLabel.SetActive(false);
		inventoryLargeScrollBar.SetActive(false);
		inventoryLargePanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public bool InventoryIsActive {
		get {
			return inventoryIsActive;
		}
	}

	public void setInventoryIsActive(bool active)
	{
		inventoryIsActive = active;

		inventoryLargeBackground.SetActive(active);
		inventoryLargeCloseButton.SetActive(active);
		inventoryLargeHeader.SetActive(active);
		inventoryLargeLabel.SetActive(active);
		inventoryLargeScrollBar.SetActive(active);
		inventoryLargePanel.SetActive(active);
	}
}
