using UnityEngine;
using System.Collections;

public class UILogic : MonoBehaviour {

	private bool inventoryIsActive = false;

	//Inventory GameObjects that are to be hidden
	GameObject inventoryLargeBackground;
	GameObject inventoryLargeBackgroundDark;
	GameObject inventoryLargeCloseButton;
	GameObject inventoryLargeHeader;
	GameObject inventoryLargeLabel;
	GameObject inventoryLargeScrollBar;
	GameObject inventoryLargePanel;

	//Other UI Elements
	GameObject inventoryViewAllButton;
	GameObject phone;

	// Use this for initialization
	void Start () 
	{
		inventoryLargeBackground = GameObject.Find("InventoryLargeBackground");
		inventoryLargeBackgroundDark = GameObject.Find("InventoryLargeBackgroundDark");
		inventoryLargeCloseButton = GameObject.Find("InventoryLargeCloseButton");
		inventoryLargeHeader = GameObject.Find("InventoryLargeHeader");
		inventoryLargeLabel = GameObject.Find("InventoryLargeLabel");
		inventoryLargeScrollBar = GameObject.Find("InventoryLargeScrollBar");
		inventoryLargePanel = GameObject.Find("InventoryLargePanel");

		inventoryViewAllButton = GameObject.Find("InventoryViewAllButton");
		phone = GameObject.Find("Phone");

		inventoryLargeBackground.SetActive(false);
		inventoryLargeBackgroundDark.SetActive(false);
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

		//Enable/Disable Inventory UI Elements
		inventoryLargeBackground.SetActive(active);
		inventoryLargeBackgroundDark.SetActive(active);
		inventoryLargeCloseButton.SetActive(active);
		inventoryLargeHeader.SetActive(active);
		inventoryLargeLabel.SetActive(active);
		inventoryLargeScrollBar.SetActive(active);
		inventoryLargePanel.SetActive(active);

		//Enable/Disable Phone and InventoryViewAllButton
		phone.GetComponent<BoxCollider>().enabled = !active;
		inventoryViewAllButton.GetComponent<BoxCollider>().enabled = !active;

	}
}
