using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class InventorySmall : MonoBehaviour {

	public GameObject document1;
	public GameObject document2;

	GameObject panel;
	Alice2 alice2;

	List<string> inventory;

	// Use this for initialization
	void Start () 
	{
		panel = GameObject.Find("Panel");
		alice2 = panel.GetComponent<Alice2>();

		document1.SetActive(false);
		document2.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () 
	{
		inventory = alice2.inventary;


		if(inventory.Count == 1)
		{
			//Change First Item
			document1.SetActive(true);
			foreach(Transform child in document1.transform)
			{
				if(child.name == "Sprite")
				{
					child.GetComponent<UISprite>().spriteName = inventory.ElementAt(0);
				}
			}
		}

		if(inventory.Count > 1)
		{
			//Change First Item
			document1.SetActive(true);
			foreach(Transform child in document1.transform)
			{
				if(child.name == "Sprite")
				{
					child.GetComponent<UISprite>().spriteName = inventory.ElementAt(inventory.Count-2);
				}
			}
		}

		if(inventory.Count >= 2)
		{
			//Change Second Item
			document2.SetActive(true);
			foreach(Transform child in document2.transform)
			{
				if(child.name == "Sprite")
				{
					child.GetComponent<UISprite>().spriteName = inventory.ElementAt(inventory.Count-1);	
				}
			}
		}
	}
}
