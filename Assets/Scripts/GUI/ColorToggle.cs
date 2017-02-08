//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public enum MS_Color
{
	Black = 0,
	Dark_Blue = 1,
	Dark_Green = 2,
	Forrest_Green = 3,
	Hunter_Green = 4,
	Lime_Green = 5,
	MS_Blue = 6,
	Red = 7,
	Tan = 8,
	White = 9,
	Yellow = 10,
	MedBall = 11,
	SilverVein = 12
}

[RequireComponent(typeof(UnityEngine.UI.Toggle))]
public class ColorToggle : MonoBehaviour 
{	
	[SerializeField]
	private bool mainColor = false;
	[SerializeField]
	private MS_Color myColor = MS_Color.Black;

	private ColorManager colorMngr = null;
	private UnityEngine.UI.Toggle toggle = null;

	void Awake()
	{
		colorMngr = FindObjectOfType<ColorManager> ();
		if (!colorMngr)
			Debug.LogError ("Could not find a GameObject in scene with a ColorManager Component.");	

		toggle = GetComponent<UnityEngine.UI.Toggle> ();
	}

	void Start()
	{
		ToggleChanged ();
	}

	public void ToggleChanged()
	{		
		if(toggle.isOn)
		{
			if (mainColor)
				colorMngr.SetPrimaryColor (myColor);
			else
			{
				colorMngr.SetSecondaryColor (myColor);
				SceneManager.Instance.DropMenu.UpdateAllItems ();
			}
		}
	}


}
