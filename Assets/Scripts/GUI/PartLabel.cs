//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartLabel : MonoBehaviour 
{
	private Vector3 worldPos = Vector3.zero;
	public Vector3 WorldPos{get{ return worldPos;}set{ worldPos = value;}}

	private RawImage rawImageComp = null;
	private Text textComp = null;

	private float visibleThreshold = 0f;

	void Start()
	{
		rawImageComp = GetComponent<RawImage> ();
		textComp = GetComponentInChildren<Text> ();
	}

	// Update is called once per frame
	void Update () 
	{
		Vector3 screenPos = Camera.main.WorldToScreenPoint (worldPos);
		transform.position = screenPos;

		// this helps to not render in strange places on screen, labels that are behind the camera.
		float dot = Vector3.Dot ((worldPos - Camera.main.transform.position).normalized, Camera.main.transform.forward);
		if (dot <= visibleThreshold && rawImageComp.enabled)
		{			
			rawImageComp.enabled = false;
			textComp.enabled = false;
		} else if (dot > visibleThreshold && !rawImageComp.enabled)
		{
			rawImageComp.enabled = true;
			textComp.enabled = true;
		}
	}
}
