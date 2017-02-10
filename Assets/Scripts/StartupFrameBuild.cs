//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.UI.Toggle))]
public class StartupFrameBuild : MonoBehaviour 
{
	[SerializeField]
	private FrameType buildFrame = FrameType.TREX_4Post;

	[SerializeField]
	private GameObject subMenu = null;

	private UnityEngine.UI.Toggle toggle = null;

	// Use this for initialization
	void Start () 
	{
		toggle = GetComponent<UnityEngine.UI.Toggle> ();
		if(toggle.isOn)
		{
			if (subMenu)
				subMenu.SetActive (true);
			
			SceneManager.Instance.BuildFrame (buildFrame);
		}
	}

	public void ToggleConfig()
	{
		if(toggle.isOn)
		{
			if (subMenu)
				subMenu.SetActive (true);
			
			SceneManager.Instance.BuildFrame (buildFrame);
		}
		else
		{
			if (subMenu)
				subMenu.SetActive (false);
		}
	}
}
