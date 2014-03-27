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

	Vector3 pos;
	Vector3 posDoc2;
	string spritename;
	int cnt;

	// Use this for initialization
	void Start () 
	{
		panel = GameObject.Find("Panel");
		alice2 = panel.GetComponent<Alice2>();

		document1.SetActive(false);
		document2.SetActive(false);

		posDoc2 = new Vector3();
		posDoc2 = document2.transform.position;
		pos = new Vector3(0.3f,0.5f,0);
		document2.transform.position = pos;
//		spritename = document2.transform.GetChild(2).GetComponent<UISprite>().spriteName;
		spritename = "PJanepassport";
		Debug.Log("spritename: "+spritename);
		cnt=0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		inventory = alice2.inventory;


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
			Debug.Log ("1: "+cnt+" document2.sprite.name : "+document2.transform.GetChild(2).GetComponent<UISprite>().spriteName );
			Debug.Log ("1: "+cnt+" document2.sprite.name : "+document2.transform.position);

			Vector3 posAct = new Vector3();
		
			posAct = document2.transform.position;
			float speed = 0.03f;


//			document2.transform.position = Vector3.MoveTowards(posAct, posDoc2, speed);


			if(posAct==posDoc2){
//				Debug.Log ("posDoc2: "+posDoc2+" posAct : "+posAct );
				pos = new Vector3(0.3f,0.5f,0);

				if(!spritename.Equals(document2.transform.GetChild(2).GetComponent<UISprite>().spriteName)){
					spritename = document2.transform.GetChild(2).GetComponent<UISprite>().spriteName;
					document2.transform.position = pos;

					Debug.Log (cnt+" document2.sprite.name : "+document2.transform.GetChild(2).GetComponent<UISprite>().spriteName );
				}

			}
				





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
