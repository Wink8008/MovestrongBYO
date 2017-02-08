//using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour 
{
	[SerializeField]
	private Material[] materials = new Material[0];

	private static Material primaryMat = null;
	public static Material PrimaryMat { get { return primaryMat; } set { primaryMat = value;}}
	private static Material secondaryMat = null;
	public static Material SecondaryMat {get { return secondaryMat;} set{ secondaryMat = value;}}
	private static Material highlightMat = null;
	public static Material HighlightMat { get { return highlightMat; }}

	private FrameManager frameMngr = null;

	private UnityEngine.UI.Text mainColorTxt = null;
	private UnityEngine.UI.Text accentColorTxt = null;

	void Awake()
	{
		if(materials.Length == 0)
			Debug.LogError ("materials array is empty in "+gameObject.name+"'s ColorManager Component.");
		else
		{
			for(int i = 0; i < materials.Length; ++i)
			{
				if (materials [i].name.Equals ("Black"))
					primaryMat = materials [i];
				else if (materials [i].name.Equals ("Movestrong Blue"))
					secondaryMat = materials [i];
				else if (materials [i].name.Equals ("Highlight Yellow"))
					highlightMat = materials [i];
			}
		}

		frameMngr = FrameManager.Instance;
		if(!frameMngr)
			Debug.LogError ("FrameManager.Instance is not initialized yet.");

		GameObject mainColorTxtGO = GameObject.FindGameObjectWithTag ("Main Color Text");
		if (!mainColorTxtGO)
			Debug.LogError ("No GameObject found in scene with Tag = 'Main Color Text'");
		else
			mainColorTxt = mainColorTxtGO.GetComponent<UnityEngine.UI.Text> ();
		
		GameObject accentColorTxtGO = GameObject.FindGameObjectWithTag ("Accent Color Text");
		if (!accentColorTxtGO)
			Debug.LogError ("No GameObject found in scene with Tag = 'Accent Color Text'");
		else
			accentColorTxt = accentColorTxtGO.GetComponent<UnityEngine.UI.Text> ();
	}

	public void SetPrimaryColor(MS_Color newColor)
	{
		primaryMat = FindMaterial (newColor);
		frameMngr.UpdateColors ();
		mainColorTxt.text = GetTextName4Color (newColor);
	}

	public void SetSecondaryColor(MS_Color newColor)
	{
		secondaryMat = FindMaterial (newColor);
		frameMngr.UpdateColors ();
		SceneManager.Instance.UpdateMenuItemHighlights ();
		accentColorTxt.text = GetTextName4Color (newColor);
	}

	private Material FindMaterial(MS_Color findMat)
	{
		List<Material> allMats = new List<Material> (materials);
		Material foundMat = null;
		switch(findMat)
		{
		case MS_Color.Black:
			foundMat = allMats.Find ((Material obj) => (obj.name.Equals ("Black")));
			break;
		case MS_Color.Dark_Blue:
			foundMat = allMats.Find ((Material obj) => (obj.name.Equals ("Dark Blue")));
			break;
		case MS_Color.Dark_Green:
			foundMat = allMats.Find ((Material obj) => (obj.name.Equals ("Dark Green")));
			break;
		case MS_Color.Forrest_Green:
			foundMat = allMats.Find ((Material obj) => (obj.name.Equals ("Forrest Green")));
			break;
		case MS_Color.Hunter_Green:
			foundMat = allMats.Find ((Material obj) => (obj.name.Equals ("Hunter Green")));
			break;
		case MS_Color.Lime_Green:
			foundMat = allMats.Find ((Material obj) => (obj.name.Equals ("Lime Green")));
			break;
		case MS_Color.MS_Blue:
			foundMat = allMats.Find ((Material obj) => (obj.name.Equals ("Movestrong Blue")));
			break;
		case MS_Color.Red:
			foundMat = allMats.Find ((Material obj) => (obj.name.Equals ("Red")));
			break;
		case MS_Color.Tan:
			foundMat = allMats.Find ((Material obj) => (obj.name.Equals ("Tan")));
			break;
		case MS_Color.White:
			foundMat = allMats.Find ((Material obj) => (obj.name.Equals ("White")));
			break;
		case MS_Color.Yellow:
			foundMat = allMats.Find ((Material obj) => (obj.name.Equals ("Yellow")));
			break;	
		case MS_Color.SilverVein:
			foundMat = allMats.Find ((Material obj) => (obj.name.Equals ("SilverVein")));
			break;
		default:
			Debug.LogError (findMat.ToString()+" is not a recognized material.");
			break;
		}

		if (!primaryMat)
			Debug.LogError (gameObject.name + " is missing " + findMat.ToString () + " in Materials array.");
		
		return foundMat;		
	}

	private string GetTextName4Color(MS_Color colorType)
	{
		string newName = null;
		switch(colorType)
		{
		case MS_Color.Black:
			newName = "Black";
			break;
		case MS_Color.Dark_Blue:
			newName = "Dark Blue";
			break;
		case MS_Color.Dark_Green:
			newName = "Dark Green";
			break;
		case MS_Color.Forrest_Green:
			newName = "Forrest Green";
			break;
		case MS_Color.Hunter_Green:
			newName = "Hunter Green";
			break;
		case MS_Color.Lime_Green:
			newName = "Lime Green";
			break;
		case MS_Color.MS_Blue:
			newName = "MS Blue";
			break;
		case MS_Color.Red:
			newName = "Red";
			break;
		case MS_Color.Tan:
			newName = "Tan";
			break;
		case MS_Color.White:
			newName = "White";
			break;
		case MS_Color.Yellow:
			newName = "Yellow";
			break;	
		case MS_Color.SilverVein:
			newName = "Silver Vein";
			break;	
		default:
			Debug.LogError (colorType.ToString()+" is not a recognized material.");
			newName = "NULL";
			break;
		}

		return newName;
	}

}
