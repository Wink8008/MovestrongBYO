//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class SceneManager_NOVA : SceneManager 
{
	protected SubConfigType activeSubConfig = SubConfigType.None;
	public SubConfigType ActiveSubConfig{get{ return activeSubConfig;}}

	protected override void Awake()
	{
		base.Awake ();
	}

	protected override void Start()
	{
		base.Start ();
	}

//	public override void BuildFrame (FrameType frameType)
//	{
//		activeFrame = frameType;
//
//		switch(frameType)
//		{
//		case FrameType.NOVA_1:
//			((FrameManager_NOVA)FrameManager.Instance).BuildNOVA1();
//			break;
//		case FrameType.NOVA_4:
//			((FrameManager_NOVA)FrameManager.Instance).BuildNOVA4();
//			break;
//		case FrameType.NOVA_6:
//			((FrameManager_NOVA)FrameManager.Instance).BuildNOVA6_Single();
//			break;		
//		}
//
//		StartCoroutine(dropMenu.CreateStartItems ());
//		StartCoroutine(popupMenu.CreateStartItems ());
//	}

	public void BuildNovaFrame(FrameType frameType, SubConfigType subType)
	{
		if (activeFrame == frameType && activeSubConfig == subType)
			return;

		activeFrame = frameType;
		activeSubConfig = subType;

		switch(frameType)
		{
		case FrameType.NOVA_1:
			switch(subType)
			{
			case SubConfigType.None:
			case SubConfigType.Single:
				((FrameManager_NOVA)FrameManager.Instance).BuildNOVA1();
				break;
			default:
				Debug.LogWarning (frameType.ToString() + " " + subType.ToString() + " is not recognized.");
				break;
			}
			break;
		case FrameType.NOVA_4:
			switch(subType)
			{
			case SubConfigType.None:
			case SubConfigType.Single:
				((FrameManager_NOVA)FrameManager.Instance).BuildNOVA4();
				break;
			default:
				Debug.LogWarning (frameType.ToString() + " " + subType.ToString() + " is not recognized.");
				break;
			}
			break;
		case FrameType.NOVA_6:
			switch(subType)
			{
			case SubConfigType.None:
			case SubConfigType.Single:
				((FrameManager_NOVA)FrameManager.Instance).BuildNOVA6_Single();
				break;
			case SubConfigType.Extended:				
				((FrameManager_NOVA)FrameManager.Instance).BuildNOVA6_Extended();
				break;
			default:
				Debug.LogWarning (frameType.ToString() + " " + subType.ToString() + " is not recognized.");
				break;
			}
			break;
		default:
			Debug.LogWarning (frameType.ToString() + " " + subType.ToString() + " is not recognized.");
			break;
		}

		StartCoroutine(dropMenu.CreateStartItems ());
		popupMenu.HideMenu();
		StartCoroutine(popupMenu.CreateStartItems ());

	}
}
