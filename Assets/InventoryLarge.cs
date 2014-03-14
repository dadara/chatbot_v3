using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class InventoryLarge : MonoBehaviour {

	public GameObject[] inventoryItems;
	
	GameObject panel;
	Alice2 alice2;
	
	List<string> inventory;

	// Use this for initialization
	void Start () 
	{
		panel = GameObject.Find("Panel");
		alice2 = panel.GetComponent<Alice2>();

		//Disable all InventoryItems
		for(int i=0; i<inventoryItems.Length;i++)
		{
			inventoryItems[i].SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		inventory = alice2.inventory;

		for(int i=0; i<inventory.Count;i++)
		{
			if(inventory.ElementAt(i) != null)
			{
				inventoryItems[i].SetActive(true);
				foreach(Transform child in inventoryItems[i].transform)
				{
					if(child.name == "Sprite")
					{
						child.GetComponent<UISprite>().spriteName = inventory.ElementAt(i);
					}
				}
			}
		}
	}
}
