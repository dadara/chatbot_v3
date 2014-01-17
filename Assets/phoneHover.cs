using UnityEngine;
using System.Collections;

public class phoneHover : MonoBehaviour {

	public Transform tweenTarget;
	public Vector3 hover = Vector3.zero;
	public Vector3 pressed = new Vector3(2f, -2f);
	public float duration = 0.2f;
	
	Vector3 mPos;
	bool mInitDone = false;
	bool mStarted = false;
	bool mHighlighted = false;

	UILogic uiLogic;
	GameObject panel;
	
	void Start () 
	{
		mStarted = true;
		panel = GameObject.Find("Panel");
		uiLogic = panel.GetComponent<UILogic>();
	}
	
	void OnEnable () { if (mStarted && mHighlighted) OnHover(UICamera.IsHighlighted(gameObject)); }
	
	void OnDisable ()
	{
		if (tweenTarget != null)
		{
			TweenPosition tc = tweenTarget.GetComponent<TweenPosition>();
			
			if (tc != null)
			{
				tc.position = mPos;
				tc.enabled = false;
			}
		}
	}
	
	void Init ()
	{
		mInitDone = true;
		if (tweenTarget == null) tweenTarget = transform;
		mPos = tweenTarget.localPosition;
	}

	void OnHover (bool isOver)
	{
		if (enabled)
		{
			if (!mInitDone) Init();
			TweenPosition.Begin(tweenTarget.gameObject, duration, isOver ? mPos + hover : mPos).method = UITweener.Method.EaseInOut;
			mHighlighted = isOver;
		}
	}
}
