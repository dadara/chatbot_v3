using UnityEngine;
using System.Collections;

public class SceneFade : MonoBehaviour {

	public GameObject scene;
	public GameObject clouds;
	GameObject panel;
	Alice2 alice2;

	// Use this for initialization
	void Start () 
	{
		panel = GameObject.Find("Panel");
		alice2 = panel.GetComponent<Alice2>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(alice2.topicSet == "")
		{
			this.GetComponent<UISprite>().spriteName = "promenade";
			if(clouds.GetComponent<ParticleSystem>().isPlaying)
			{
				clouds.GetComponent<ParticleSystem>().Stop();
				clouds.GetComponent<ParticleSystem>().Clear();
			}
		} else if(alice2.topicSet == "MARCHOFTIME")
		{
			this.GetComponent<UISprite>().spriteName = "hbo_banner";
			if(!clouds.GetComponent<ParticleSystem>().isPlaying)
			{
				clouds.GetComponent<ParticleSystem>().Play();
			}
		} else if(alice2.topicSet == "CBS")
		{
			this.GetComponent<UISprite>().spriteName = "cbs";
			if(!clouds.GetComponent<ParticleSystem>().isPlaying)
			{
				clouds.GetComponent<ParticleSystem>().Play();
			}
		} else if(alice2.topicSet == "DIPLOMAT")
		{
			this.GetComponent<UISprite>().spriteName = "Trieste";
			if(!clouds.GetComponent<ParticleSystem>().isPlaying)
			{
				clouds.GetComponent<ParticleSystem>().Play();
			}
		} else if(alice2.topicSet == "NEWYORKER")
		{
			this.GetComponent<UISprite>().spriteName = "new yorker";
			if(!clouds.GetComponent<ParticleSystem>().isPlaying)
			{
				clouds.GetComponent<ParticleSystem>().Play();
			}
		} else if(alice2.topicSet == "NOW")
		{
			this.GetComponent<UISprite>().spriteName = "promenade";
			if(clouds.GetComponent<ParticleSystem>().isPlaying)
			{
				clouds.GetComponent<ParticleSystem>().Stop();
				clouds.GetComponent<ParticleSystem>().Clear();
			}
		}


		//TweenColor.Begin(scene, 0.5f, new Color(1f, 1f, 1f, 0f));
	}
}
