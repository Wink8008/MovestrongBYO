//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class ConfigManager_NOVA : ConfigManager 
{
	[SerializeField]
	private GameObject subMenu = null;

	// Use this for initialization
	public override void Start () 
	{
		base.Start ();

		if (toggle.isOn)
		{
			if (subMenu)
			{
				subMenu.SetActive (true);
				SubConfigType activeSubType = GetActiveSubToggle ();
				((SceneManager_NOVA)SceneManager.Instance).BuildNovaFrame (buildFrame, activeSubType);
			}
		}
	}
	
	public override void ToggleConfig()
	{
		if(toggle.isOn)
		{
			if (subMenu)
			{
				subMenu.SetActive (true);
				SubConfigType activeSubType = GetActiveSubToggle ();
				((SceneManager_NOVA)SceneManager.Instance).BuildNovaFrame (buildFrame, activeSubType);
			}
		}
		else
		{
			if (subMenu)
				subMenu.SetActive (false);
		}
	}

	private SubConfigType GetActiveSubToggle()
	{
		if(subMenu)
		{
			SubMenuToggle[] subMenuToggles = subMenu.GetComponentsInChildren<SubMenuToggle> (true);
			for(int i = 0; i < subMenuToggles.Length; ++i)
			{
				if (subMenuToggles [i].MyToggle.isOn)
					return subMenuToggles [i].subConfigType;
			}
		}
		else
			Debug.LogWarning (gameObject.name + ".ConfigManager_NOVA is calling GetActiveSubToggle() but subMenu is null.");

		return SubConfigType.None;
	}
}
