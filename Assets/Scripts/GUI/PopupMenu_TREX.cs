﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupMenu_TREX : PopupMenu 
{
	public override IEnumerator CreateStartItems ()
	{
		while (FrameManager.Instance.Parts.Count.Equals (0))
			yield return null;

		List<GameObject> parts = FrameManager.Instance.Parts;
		foreach (GameObject cur in parts)
		{
			BaseSelectable curBS = cur.GetComponent<BaseSelectable> ();
			if (TREX_Post.IsAPost ((TREX_PartID)curBS.PartID))
				CreateItemsFor (curBS);			
			else if(TREX_Connector.IsAConnector ((TREX_PartID)curBS.PartID))
			{
				CreateItemsFor (curBS);	
				//SetActive_ItemsForPart (curBS, (TREX_PartID)curBS.PartID);
			}
		}
	}

	public override void CreateItemsFor (BaseSelectable part)
	{
		List<TREX_PartID> itemPartIDs = new List<TREX_PartID>();
		if (part is TREX_Post)
			itemPartIDs.AddRange((part as TREX_Post).GetMenuItems ());
		else if (part is TREX_Connector)
			itemPartIDs.AddRange((part as TREX_Connector).GetMenuItems ());
		else
			Debug.LogError (part.gameObject.name + " is does not contain a TREX_Post or TREX_Connector derived component.");

		if (itemPartIDs.Count <= 0)
			return;

		// create menu items
		for (int i = 0; i < itemPartIDs.Count; ++i)
		{					
			GameObject itemGO = Instantiate<GameObject> (menuItemPrefab.gameObject, menuItemParent);
			itemGO.name = "PM Item - " + part.name;
			Text sText = itemGO.GetComponentInChildren<Text> ();
			if (!sText)
				Debug.LogError ("No Text Component found in children of " + itemGO.name);

			switch (itemPartIDs [i])
			{
			case TREX_PartID.Part_Ab_Board:
				sText.text = "Ab Board";
				break;
			case TREX_PartID.Part_Dip:
				sText.text = "Dip";
				break;
			case TREX_PartID.Part_MedBall_Dbl:
				sText.text = "Dbl Med Ball Target";
				break;
			case TREX_PartID.Part_Step:
				sText.text = "Step";
				break;
			case TREX_PartID.Part_Y_Extender:
				sText.text = "Y-Extender";
				break;
			case TREX_PartID.Post_T4600:
				sText.text = "Kick Plate Post";
				break;
			case TREX_PartID.Post_T6400:
			case TREX_PartID.Post_T6500:
				sText.text = "Loop Post";
				break;
			case TREX_PartID.Conn_PullUp_Std:
				sText.text = "Pull Up - Standard";
				break;
			case TREX_PartID.Conn_PullUp_Ergo:
				sText.text = "Pull Up - Ergo";
				break;
			default:
				Debug.LogError (itemPartIDs[i].ToString()+" is unrecognized.");
				sText.text = "Unrecognized";
				break;
			}

			GameObjRef goRef = itemGO.GetComponent<GameObjRef> ();
			if (!goRef)
				Debug.LogError (itemGO.name + " does not contain a GameObjRef Component.");
			goRef.GO_Ref = part.gameObject;

			TREX_PopMenu_Item pmItem = itemGO.GetComponent<TREX_PopMenu_Item> ();
			if (!pmItem)
				Debug.LogError (itemGO.name + " does not contain a TREX_PopMenu_Item Component.");
			pmItem.PartID = (byte)itemPartIDs [i];

			itemGO.SetActive (false);

			allItems.Add (itemGO);
		}
	}

}
