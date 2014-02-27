using UnityEngine;
using System.Collections;

public class DragDropSprite : MonoBehaviour 
{


	void OnDrop (GameObject go)
	{
		DragDropDocument ddo = go.GetComponent<DragDropDocument>();

		if (ddo != null)
		{
			string document = ddo.getDocumentName();
			Debug.Log("Show Jane " + document);	
			//TODO: Show Jane
			GameObject janecb;
			janecb = GameObject.Find("JaneChatbox");
			UILabel jane = janecb.GetComponent<UILabel>();
			GameObject panel = GameObject.Find ("Panel");
			Alice2 a2 = panel.GetComponent<Alice2>();
			jane.text = a2.getOutput(document);

			//Destroy(go);
		}
	}
}
