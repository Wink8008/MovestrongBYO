//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ZoomDisplay : MonoBehaviour 
{
	Text myTextComp = null;
	Slider slider = null;

	void Awake()
	{
		myTextComp = GetComponent<Text> ();

		slider = gameObject.transform.parent.gameObject.GetComponentInChildren<Slider> ();
		if (!slider)
			Debug.LogError (gameObject.name + "'s parent does not contain a Slider child component.");
	}

	public void UpdateDisplay()
	{
		myTextComp.text = slider.value.ToString() + " %";
	}
}
