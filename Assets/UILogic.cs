using UnityEngine;
using System.Collections;

public class UILogic : MonoBehaviour {
	
	private bool inventoryIsActive = false;
	private bool phoneIsActive = false;

	//Inventory Elements
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
	Vector3 phonePosition;
	GameObject phoneCloseButton;

	GameObject Input1btn;
	GameObject Input2btn;
	GameObject Input3btn;

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
		phoneCloseButton = GameObject.Find("PhoneCloseButton");

		Input1btn = GameObject.Find("Input1Button");
		Input2btn = GameObject.Find("Input2Button");
		Input3btn = GameObject.Find("Input3Button");

		phoneCloseButton.SetActive(false);
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

	public bool PhoneIsActive {
		get {
			return phoneIsActive;
		}
	}

	public void SetPhoneIsActive(bool active)
	{
		phoneIsActive = active;

		phoneCloseButton.SetActive(active);
		inventoryLargeBackgroundDark.SetActive(active);

		//Enable/Disable Phone and InventoryViewAllButton
		phone.GetComponent<BoxCollider>().enabled = !active;
		inventoryViewAllButton.GetComponent<BoxCollider>().enabled = !active;
		
		//Enable/Disable InputButtons
		Input1btn.GetComponent<BoxCollider>().enabled = !active;
		Input2btn.GetComponent<BoxCollider>().enabled = !active;
		Input3btn.GetComponent<BoxCollider>().enabled = !active;

		if(active)
		{
			phone.GetComponent<phoneHover>().enabled = false;
			phonePosition = phone.transform.position;
			phone.transform.position = new Vector3(0,0,0);
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

		//Enable/Disable InputButtons
		Input1btn.GetComponent<BoxCollider>().enabled = !active;
		Input2btn.GetComponent<BoxCollider>().enabled = !active;
		Input3btn.GetComponent<BoxCollider>().enabled = !active;

	}
}
