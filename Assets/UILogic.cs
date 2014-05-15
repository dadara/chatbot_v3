using UnityEngine;
using System.Collections;

public class UILogic : MonoBehaviour {
	
	private bool inventoryIsActive = false;
	private bool phoneIsActive = false;
	private bool scanIsActive = false;

	//Inventory Elements
	public GameObject inventoryLargeCloseButton;
	GameObject inventoryLargePanel;
	public GameObject inventoryLargeCancelButton;
	GameObject inventoryLargePanelItems;
	GameObject inventoryLargeButtonPanel;
	GameObject inventorySmallContainer;

	//Scan/Image Panel
	GameObject scanPanel;
	GameObject imageView;
	public GameObject inventoryLargeViewImageButton;

	//Phone
	GameObject inventoryViewAllButton;
	GameObject phone;
	GameObject phoneSprite;
	GameObject phoneInfoPanel;
	GameObject phoneInfo1;
	Vector3 phonePosition;
	GameObject phoneInfoScrollbar;

	//Input Buttons
	GameObject Input1btn;
	GameObject Input2btn;
	GameObject Input3btn;

	//Iventory Selection
	public Color selectedColor = new Color(0.6f, 1f, 0.2f, 1f);
	UISprite lastSelectedSprite;

	// Use this for initialization
	void Start () 
	{
		inventoryLargeCloseButton = GameObject.Find("InventoryLargeCloseButton");
		inventoryLargePanel = GameObject.Find("InventoryLargePanel");
		inventoryLargeCancelButton = GameObject.Find("InventoryLargeCancelButton");
		inventoryLargePanelItems = GameObject.Find("InventoryLargePanelItems");
		inventoryLargeButtonPanel = GameObject.Find("InventoryLargeButtonPanel");
		inventorySmallContainer = GameObject.Find("InventorySmallContainer");

		inventoryLargeViewImageButton = GameObject.Find("InventoryLargeViewImageButton");
		scanPanel = GameObject.Find("ScanPanel");
		imageView = GameObject.Find("ImageView");

		inventoryViewAllButton = GameObject.Find("InventoryViewAllButton");

		phone = GameObject.Find("Phone");
		phoneSprite = GameObject.Find("PhoneSprite");
		phoneInfoPanel = GameObject.Find("PhoneInfoPanel");
		phoneInfo1 = GameObject.Find("PhoneInfo1");
		phoneInfoScrollbar = GameObject.Find("PhoneInfoScrollbar");

		Input1btn = GameObject.Find("Input1Button");
		Input2btn = GameObject.Find("Input2Button");
		Input3btn = GameObject.Find("Input3Button");

		inventoryLargePanel.SetActive(false);
		inventoryLargePanelItems.SetActive(false);
		inventoryLargeButtonPanel.SetActive(false);
		inventoryLargeViewImageButton.SetActive(false);
		inventoryLargeCancelButton.SetActive(false);
		phoneInfoPanel.SetActive(false);
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


		//Enable/Disable Phone and InventoryViewAllButton
		inventoryViewAllButton.GetComponent<BoxCollider>().enabled = !active;
		inventorySmallContainer.SetActive(!active);
		phoneInfoPanel.SetActive(active);
		phoneInfo1.SetActive(!active);
		phoneInfoScrollbar.GetComponent<UIScrollBar>().scrollValue = 0;

		//Enable/Disable InputButtons
		//Input1btn.GetComponent<BoxCollider>().enabled = !active;
		//Input2btn.GetComponent<BoxCollider>().enabled = !active;
		//Input3btn.GetComponent<BoxCollider>().enabled = !active;

		if(active)
		{
			phoneSprite.GetComponent<phoneHover>().enabled = false;
			phonePosition = phone.transform.position;
			phone.transform.position = new Vector3(phone.transform.position.x,phone.transform.position.y/3,phone.transform.position.z);
		} else 
		{
			phone.transform.position = phonePosition;
			phoneSprite.GetComponent<phoneHover>().enabled = true;
		}
	}

	public void SetInventoryIsActive(bool active, bool show)
	{
		inventoryIsActive = active;

		//If disable
		if(!active)
		{
			//Disable ScanButton
			//inventoryLargeScanButton.SetActive(active);

			//Color item white
			if(lastSelectedSprite != null)
			{
				lastSelectedSprite.color = Color.white;
			}

			//Show Jane Item
			if(inventoryLargeCloseButton.GetComponentInChildren<UILabel>().text == "Show Jane" && show)	
			{
				showJaneLastSelectedInventoryItem();
			}
		} else 
		{

		}

		//Enable/Disable Inventory UI Elements
		inventoryLargePanel.SetActive(active);
		inventoryLargePanelItems.SetActive(active);
		inventoryLargeButtonPanel.SetActive(active);
		inventoryLargeCancelButton.SetActive(!active);
		inventorySmallContainer.SetActive(!active);

		if(active)
		{
			inventoryLargeCloseButton.GetComponentInChildren<UILabel>().text = "Close";
		}

		//Enable/Disable Phone and InventoryViewAllButton
		phoneSprite.GetComponent<BoxCollider>().enabled = !active;
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

	public void showJaneLastSelectedInventoryItem()
	{
		Debug.Log("Show Jane " + lastSelectedSprite.spriteName);
		GameObject janecb;
		janecb = GameObject.Find("JaneChatbox");
		UILabel jane = janecb.GetComponent<UILabel>();
		GameObject panel = GameObject.Find ("Panel");
		Alice2 a2 = panel.GetComponent<Alice2>();
		jane.text = a2.getOutput(lastSelectedSprite.spriteName);

		//TODO: Show Jane
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
			imageView.GetComponent<UISlicedSprite>().spriteName = lastSelectedSprite.spriteName;
		} else 
		{
			scanPanel.SetActive(active);
			//inventoryLargeViewImageButton.SetActive(active);
			//inventoryLargeCancelButton.SetActive(active);
			//lastSelectedSprite.color = Color.white;
			//inventoryLargeCloseButton.GetComponentInChildren<UILabel>().text = "Close";
		}
	}
}
