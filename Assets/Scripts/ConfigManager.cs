//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.UI.Toggle))]
public abstract class ConfigManager : MonoBehaviour 
{
	[SerializeField]
	protected FrameType buildFrame = FrameType.TREX_4Post;

	protected UnityEngine.UI.Toggle toggle = null;

	// Use this for initialization
	public virtual void Start () 
	{
		toggle = GetComponent<UnityEngine.UI.Toggle> ();
	}

	public abstract void ToggleConfig ();
}
