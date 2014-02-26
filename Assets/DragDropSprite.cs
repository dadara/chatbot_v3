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

			//Destroy(go);
		}
	}
}
