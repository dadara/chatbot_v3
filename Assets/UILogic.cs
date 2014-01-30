using UnityEngine;
using System.Collections;

public class UILogic : MonoBehaviour {
	
	private bool inventoryIsActive = false;
	private bool phoneIsActive = false;
	private bool scanIsActive = false;

	//Inventory Elements
	GameObject inventoryLargeCloseButton;
	GameObject inventoryLargePanel;
	GameObject inventoryLargeScanButton;
	GameObject inventoryLargePanelItems;
	GameObject inventoryLargeButtonPanel;
	GameObject inventorySmallContainer;

	//Scan
	GameObject scanPanel;

	//Other UI Elements
	GameObject inventoryViewAllButton;
	GameObject phone;
	Vector3 phonePosition;

	GameObject Input1btn;
	GameObject Input2btn;
	GameObject Input3btn;

	public Color selectedColor = new Color(0.6f, 1f, 0.2f, 1f);
	UISprite lastSelectedSprite;

	// Use this for initialization
	void Start () 
	{
		inventoryLargeCloseButton = GameObject.Find("InventoryLargeCloseButton");
		inventoryLargePanel = GameObject.Find("InventoryLargePanel");
		inventoryLargeScanButton = GameObject.Find("InventoryLargeScanButton");
		inventoryLargePanelItems = GameObject.Find("InventoryLargePanelItems");
		inventoryLargeButtonPanel = GameObject.Find("InventoryLargeButtonPanel");
		inventorySmallContainer = GameObject.Find("InventorySmallContainer");

		scanPanel = GameObject.Find("ScanPanel");

		inventoryViewAllButton = GameObject.Find("InventoryViewAllButton");

		phone = GameObject.Find("Phone");

		Input1btn = GameObject.Find("Input1Button");
		Input2btn = GameObject.Find("Input2Button");
		Input3btn = GameObject.Find("Input3Button");

		inventoryLargePanel.SetActive(false);
		inventoryLargePanelItems.SetActive(false);
		inventoryLargeButtonPanel.SetActive(false);
		scanPanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public bool InventoryIsActive {
		get {
			return inventoryIsActive;
		}
	}

	public bool PhoneIsActive {
		get {
			return phoneIsActive;
		}
	}

	public bool ScanIsActive {
		get {
			return scanIsActive;
		}
	}

	public void SetPhoneIsActive(bool active)
	{
		phoneIsActive = active;

		//phoneCloseButton.SetActive(active);
		//inventoryLargeBackgroundDark.SetActive(active);

		//Enable/Disable Phone and InventoryViewAllButton
		//phone.GetComponent<BoxCollider>().enabled = !active;
		inventoryViewAllButton.GetComponent<BoxCollider>().enabled = !active;
		inventorySmallContainer.SetActive(!active);
		
		//Enable/Disable InputButtons
		Input1btn.GetComponent<BoxCollider>().enabled = !active;
		Input2btn.GetComponent<BoxCollider>().enabled = !active;
		Input3btn.GetComponent<BoxCollider>().enabled = !active;

		if(active)
		{
			phone.GetComponent<phoneHover>().enabled = false;
			phonePosition = phone.transform.position;
			phone.transform.position = new Vector3(phone.transform.position.x,phone.transform.position.y/3,0);
		} else 
		{
			phone.transform.position = phonePosition;
			phone.GetComponent<phoneHover>().enabled = true;
		}
	}

	public void SetInventoryIsActive(bool active)
	{
		inventoryIsActive = active;

		//Enable/Disable Inventory UI Elements
		inventoryLargePanel.SetActive(active);
		inventoryLargePanelItems.SetActive(active);
		inventoryLargeButtonPanel.SetActive(active);
		inventoryLargeScanButton.SetActive(!active);
		inventorySmallContainer.SetActive(!active);

		//Disable ScanButton
		if(!active)
		{
			inventoryLargeScanButton.SetActive(active);
			if(lastSelectedSprite != null)
			{
				lastSelectedSprite.color = Color.white;
			}
		}

		//Enable/Disable Phone and InventoryViewAllButton
		phone.GetComponent<BoxCollider>().enabled = !active;
		inventoryViewAllButton.GetComponent<BoxCollider>().enabled = !active;

		//Enable/Disable InputButtons
		Input1btn.GetComponent<BoxCollider>().enabled = !active;
		Input2btn.GetComponent<BoxCollider>().enabled = !active;
		Input3btn.GetComponent<BoxCollider>().enabled = !active;

	}

	//Changes Color of Selected Item in Inventory
	public void selectItemInInventory(UISprite sprite)
	{
		//Last Selected Item will be turned white
		if(lastSelectedSprite != null)
		{
			lastSelectedSprite.color = Color.white;
		}
		lastSelectedSprite = sprite;
		lastSelectedSprite.color = selectedColor;
	}

	public Color SelectedColor {
		get {
			return selectedColor;
		}
	}

	public void SetScanIsActive(bool active)
	{
		scanIsActive = active;

		if(scanIsActive)
		{
			scanPanel.SetActive(active);
		} else 
		{
			scanPanel.SetActive(active);
			inventoryLargeScanButton.SetActive(active);
			lastSelectedSprite.color = Color.white;
			inventoryLargeCloseButton.GetComponentInChildren<UILabel>().text = "Close";
		}
	}
}
