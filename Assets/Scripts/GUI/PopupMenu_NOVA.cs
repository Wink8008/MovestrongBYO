using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupMenu_NOVA : PopupMenu 
{

	public override IEnumerator CreateStartItems ()
	{
		while (FrameManager.Instance.Parts.Count.Equals (0))
			yield return null;

		List<GameObject> parts = FrameManager.Instance.Parts;
		foreach (GameObject cur in parts)
		{
			BaseSelectable curBS = cur.GetComponent<BaseSelectable> ();
			CreateItemsFor (curBS);

//			if (TREX_Post.IsAPost ((TREX_PartID)curBS.PartID))
//				CreateItemsFor (curBS);			
//			else if(TREX_Connector.IsAConnector ((TREX_PartID)curBS.PartID))
//			{
//				CreateItemsFor (curBS);	
//				//SetActive_ItemsForPart (curBS, (TREX_PartID)curBS.PartID);
//			}
		}
	}

	public override void CreateItemsFor (BaseSelectable part)
	{
		List<NOVA_PartID> itemPartIDs = new List<NOVA_PartID>();
		if (part is NOVA_Post)
			itemPartIDs.AddRange ((part as NOVA_Post).GetMenuItems ());
		else if (part is NOVA_Connector)
			itemPartIDs.AddRange ((part as NOVA_Connector).GetMenuItems ());
		else
			return;

		if (itemPartIDs.Count <= 0)
			return;

		// create menu items
		for (int i = 0; i < itemPartIDs.Count; ++i)
		{					
			GameObject itemGO = Instantiate<GameObject> (menuItemPrefab.gameObject, menuItemParent);
			itemGO.name = "PM Item - " + part.name + " - " + itemPartIDs[i].ToString();
			Text sText = itemGO.GetComponentInChildren<Text> ();
			if (!sText)
				Debug.LogError ("No Text Component found in children of " + itemGO.name);

			switch (itemPartIDs [i])
			{
			case NOVA_PartID.Conn_StdPullUp:
				sText.text = "Pull Up - Standard";
				break;	
			case NOVA_PartID.Conn_ArchBridge:
				sText.text = "Arch Bridge";
				break;
			case NOVA_PartID.Conn_HorizontalBridge_Long:
				sText.text = "Long Bridge";
				break;
			case NOVA_PartID.Conn_HorizontalBridge_Short:
				sText.text = "Short Bridge";
				break;
			case NOVA_PartID.Part_ClimberBar:
				sText.text = "Climber Bars";
				break;
			case NOVA_PartID.Part_ErgoPullUp:
				sText.text = "Pull Up - Ergo";
				break;			
			case NOVA_PartID.Part_SlidingPullUp:
				sText.text = "Pull Up - Sliding";
				break;			
			case NOVA_PartID.Part_KickPlate:
				sText.text = "Kick Plate";
				break;
			case NOVA_PartID.Part_SquatStand:
				sText.text = "Squat Stand";
				break;
			case NOVA_PartID.Part_StallBar:
				sText.text = "Stall Bar";
				break;
			case NOVA_PartID.Part_StorageTrays:
				sText.text = "Storage Trays";
				break;	
			case NOVA_PartID.Part_RopeAnchor:
				sText.text = "Rope Anchor";
				break;
			case NOVA_PartID.Part_GRT_Single:
				sText.text = "Single GRT";
				break;
			case NOVA_PartID.Part_GRT_Double:
				sText.text = "Double GRT";
				break;
			case NOVA_PartID.Part_Dip:
				sText.text = "Dip";
				break;
			case NOVA_PartID.Part_Step:
				sText.text = "Step";
				break;
			case NOVA_PartID.Part_SlidingRopeAnchor:
				sText.text = "Sliding Rope Anchor";
				break;
			case NOVA_PartID.Part_RopeSlide_Single:
				sText.text = "Single Rope Slide";
				break;
			case NOVA_PartID.Part_RopeSlide_Double:
				sText.text = "Double Rope Slide";
				break;
			case NOVA_PartID.Part_GLoops:
				sText.text = "G-Loops";
				break;
			case NOVA_PartID.Part_SideRailPullUp:
				sText.text = "Pull Up - Side Rail";
				break;
			case NOVA_PartID.Part_GlobeGrips:
				sText.text = "Globe Grips";
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

			NOVA_PopMenu_Item pmItem = itemGO.GetComponent<NOVA_PopMenu_Item> ();
			if (!pmItem)
				Debug.LogError (itemGO.name + " does not contain a NOVA_PopMenu_Item Component.");
			pmItem.PartID = (byte)itemPartIDs [i];

			itemGO.SetActive (false);

			allItems.Add (itemGO);
		}
	}

}
