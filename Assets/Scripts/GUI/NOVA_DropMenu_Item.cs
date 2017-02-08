//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GameObjRef))]
public class NOVA_DropMenu_Item : MenuItem 
{	
	private GameObjRef goRefComp = null;

	protected override void Awake ()
	{
		base.Awake ();
		goRefComp = GetComponent<GameObjRef> ();
	}

	public void TogglePart()
	{		
		((FrameManager_NOVA)FrameManager.Instance).TogglePart (goRefComp.GO_Ref, partID);
		// must call ShowActiveColor after TogglePart because ShowActiveColor will check if the part exists
		ShowActiveColor ();
	}

	public override void ShowActiveColor ()
	{
		BaseSelectable goRefBS = goRefComp.GO_Ref.GetComponent<BaseSelectable> ();
		ColorBlock cb = btnComp.colors;
		// This "goRefBS.PartID.Equals((byte)partID)" checks if the BaseSelectable PartID equals the partID of the menu item.
		// this will be the case for Post T4600.
		if (goRefBS.HasChildPart ((byte)partID) || goRefBS.PartID.Equals((byte)partID))
			cb.normalColor = ColorManager.SecondaryMat.color;
		else
			cb.normalColor = myNormalColor;
		btnComp.colors = cb;
	}
}
