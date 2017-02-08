using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownMenu_TREX : DropdownMenu 
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
				SetActive_ItemsForPart (curBS, curBS.PartID);
			}
		}

		ResizeParent ();
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

		// create title item
		GameObject itemGO = Instantiate<GameObject> (titleItemPrefab.gameObject, menuItemParent);
		itemGO.name = "DM Title - " + part.name;
		Text sText = itemGO.GetComponentInChildren<Text> ();
		if (!sText)
			Debug.LogError ("No Text Component found in children of " + itemGO.name);
		sText.text = part.name;
		GameObjRef goRef = itemGO.GetComponent<GameObjRef> ();
		if (!goRef)
			Debug.LogError (itemGO.name + " does not contain a GameObjRef Component.");
		goRef.GO_Ref = part.gameObject;
		allItems.Add (itemGO);

		// create children menu items
		for (int i = 0; i < itemPartIDs.Count; ++i)
		{					
			itemGO = Instantiate<GameObject> (menuItemPrefab.gameObject, menuItemParent);
			itemGO.name = "DM Item - " + part.name + " - " + itemPartIDs [i].ToString ();
			sText = itemGO.GetComponentInChildren<Text> ();
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

			goRef = itemGO.GetComponent<GameObjRef> ();
			if (!goRef)
				Debug.LogError (itemGO.name + " does not contain a GameObjRef Component.");
			goRef.GO_Ref = part.gameObject;

			TREX_DropMenu_Item dmItem = itemGO.GetComponent<TREX_DropMenu_Item> ();
			if (!dmItem)
				Debug.LogError (itemGO.name + " does not contain a TREX_DropMenu_Item Component.");
			dmItem.PartID = (byte)itemPartIDs [i];

			allItems.Add (itemGO);
		}
	}
}
