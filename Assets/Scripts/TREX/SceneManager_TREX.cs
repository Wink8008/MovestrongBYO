//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class SceneManager_TREX : SceneManager 
{

	protected override void Awake()
	{
		base.Awake ();
	}

	protected override void Start()
	{
		base.Start ();
	}

	public override void BuildFrame (FrameType frameType)
	{
		activeFrame = frameType;
		
		switch(frameType)
		{
		case FrameType.TREX_4Post:
			((FrameManager_TREX)FrameManager.Instance).Build4Post();
			break;
		case FrameType.TREX_5Post:
			((FrameManager_TREX)FrameManager.Instance).Build5Post();
			break;
		case FrameType.TREX_7Post:
			((FrameManager_TREX)FrameManager.Instance).Build7Post ();
			break;
		case FrameType.TREX_10Post:
			((FrameManager_TREX)FrameManager.Instance).Build10Post ();
			break;
		case FrameType.TREX_12Post:
			((FrameManager_TREX)FrameManager.Instance).Build12Post ();
			break;
		}

		StartCoroutine(dropMenu.CreateStartItems ());
		StartCoroutine(popupMenu.CreateStartItems ());
	}
}
