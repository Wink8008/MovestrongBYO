//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(UnityEngine.UI.Toggle))]
public class SubMenuToggle : MonoBehaviour 
{
	public SubConfigType subConfigType = SubConfigType.None;

	private UnityEngine.UI.Toggle myToggle = null;
	public UnityEngine.UI.Toggle MyToggle {get{return myToggle;}}

	void Awake()
	{
		myToggle = GetComponent<UnityEngine.UI.Toggle> ();
	}
}
