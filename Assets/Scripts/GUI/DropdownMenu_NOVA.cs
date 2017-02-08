using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownMenu_NOVA : DropdownMenu
{

	public override IEnumerator CreateStartItems ()
	{	
		while (FrameManager.Instance.Parts.Count.Equals (0))
			yield return null;

		List<GameObject> parts = FrameManager.Instance.Parts;
		foreach (GameObject cur in parts)
		{
			BaseSelectable curBS = cur.GetComponent<BaseSelectable> ();
			if (curBS is NOVA_Post)
				CreateItemsFor (curBS);			
			else if(NOVA_Connector.IsAConnector ((NOVA_PartID)curBS.PartID))
			{
				CreateItemsFor (curBS);	
				SetActive_ItemsForPart (curBS, curBS.PartID);
			}
		}

		ResizeParent ();
		ReorderMenuItems ();
	}

	public override void CreateItemsFor (BaseSelectable part)
	{
		List<NOVA_PartID> itemPartIDs = new List<NOVA_PartID>();
		if (part is NOVA_Post)
			itemPartIDs.AddRange((part as NOVA_Post).GetMenuItems ());
		else if (part is NOVA_Connector)
			itemPartIDs.AddRange((part as NOVA_Connector).GetMenuItems ());
		else
			Debug.LogError (part.gameObject.name + " is does not contain a NOVA_Post or NOVA_Connector derived component.");
		
		if (itemPartIDs.Count <= 0)
			return;

		// create title item
		GameObject itemGO = Instantiate<GameObject> (titleItemPrefab.gameObject, menuItemParent);
		MenuTitle menuTitle = itemGO.GetComponent<MenuTitle> ();
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

		List<GameObject> tempItems = new List<GameObject> ();
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

			goRef = itemGO.GetComponent<GameObjRef> ();
			if (!goRef)
				Debug.LogError (itemGO.name + " does not contain a GameObjRef Component.");
			goRef.GO_Ref = part.gameObject;

			NOVA_DropMenu_Item dmItem = itemGO.GetComponent<NOVA_DropMenu_Item> ();
			if (!dmItem)
				Debug.LogError (itemGO.name + " does not contain a NOVA_DropMenu_Item Component.");
			dmItem.PartID = (byte)itemPartIDs [i];

			tempItems.Add (itemGO);
		}

		menuTitle.AddChildItems (tempItems);
		allItems.AddRange (tempItems);
	}

	public override void ReorderMenuItems ()
	{
		Button[] allMenuItems = menuItemParent.GetComponentsInChildren<Button> (true);
		List<GameObject> postList = new List<GameObject> ();
		List<GameObject> bridgeList = new List<GameObject> ();
		List<GameObject> cmList = new List<GameObject> ();

		for(int i = 0; i < allMenuItems.Length; ++i)
		{
			GameObject partRef = allMenuItems [i].GetComponent<GameObjRef> ().GO_Ref;
			NOVA_Post postComp = partRef.GetComponent<NOVA_Post> ();
			NOVA_Connector connComp = partRef.GetComponent<NOVA_Connector> ();
			if (postComp)
				postList.Add (allMenuItems [i].gameObject);
			else if (connComp)
			{
				if (connComp is NOVA_Conn_ArchBridge || connComp is NOVA_Conn_HorizBridge_Long)
					bridgeList.Add(allMenuItems [i].gameObject);
				else if (connComp is NOVA_Conn_LogoCrossmember || connComp is NOVA_Conn_BasicCrossmember || connComp is NOVA_Conn_Pullup)
					cmList.Add (allMenuItems [i].gameObject);				
			}				

			allMenuItems [i].gameObject.transform.SetParent (null, false);
		}

		for(int i = 0; i < postList.Count; ++i)
		{
			postList [i].transform.SetParent (menuItemParent, false);
		}
		for(int i = 0; i < bridgeList.Count; ++i)
		{
			bridgeList [i].transform.SetParent (menuItemParent, false);
		}
		byte cmID = 1;
		while(cmList.Count > 0)
		{
			for(int i = 0; i < cmList.Count; ++i)
			{
				BaseSelectable bs = cmList [i].GetComponent<GameObjRef> ().GO_Ref.GetComponent<BaseSelectable>();
				if(bs.MyNumber == cmID)
				{
					cmList [i].transform.SetParent (menuItemParent, false);
					cmList.RemoveAt (i--);
				}
			}
			++cmID;
		}
	}

}
