//using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameManager_NOVA : FrameManager
{

	public void BuildNOVA1()
	{

	}

	public void BuildNOVA4()
	{

	}

	public NOVA_Post BuildNOVA6_Single()
	{
		if(parts.Length > 0)
			DestroyAllParts ();

		// spawn post1
		GameObject post1 = SpawnPart((byte)NOVA_PartID.NFTS_1000);
		// spawn pullup1
		GameObject pullup1 = SpawnPart ((byte)NOVA_PartID.Conn_StdPullUp, post1);
		// spawn post2
		GameObject post2 = SpawnPart((byte)NOVA_PartID.NFTS_1000, pullup1);
		// spawn pullup2
		GameObject pullup2 = SpawnPart ((byte)NOVA_PartID.Conn_StdPullUp, post2);
		// spawn archBridge
		GameObject archBridge = SpawnPart ((byte)NOVA_PartID.Conn_ArchBridge, post1);
		// spawn post3
		GameObject post3 = SpawnPart((byte)NOVA_PartID.NFTS_1000, pullup2);
		// spawn pullup3
		GameObject pullup3 = SpawnPart ((byte)NOVA_PartID.Conn_StdPullUp, post3);
		// spawn post4
		GameObject post4 = SpawnPart((byte)NOVA_PartID.NFTS_1000, pullup3);
		// spawn pullup4
		GameObject pullup4 = SpawnPart ((byte)NOVA_PartID.Conn_StdPullUp, post4);
		// spawn post5
		GameObject post5 = SpawnPart((byte)NOVA_PartID.NFTS_1000, pullup4);
		// spawn logoCM
		GameObject logoCM = SpawnPart ((byte)NOVA_PartID.Conn_LogoCrossmember, post5);
		// spawn post6
		GameObject post6 = SpawnPart((byte)NOVA_PartID.NFTS_1000, logoCM);
		// spawn pullup6
		GameObject pullup6 = SpawnPart ((byte)NOVA_PartID.Conn_StdPullUp, post6);

		NOVA_Post postComp = post1.GetComponent<NOVA_Post> ();
		postComp.Initialize ((byte)1);
		postComp.SpawnChild ((byte)NOVA_PartID.Part_ConnBracket_N6);
		postComp = post2.GetComponent<NOVA_Post> ();
		postComp.Initialize ((byte)2);
		postComp.SpawnChild ((byte)NOVA_PartID.Part_ConnBracket_N6);
		postComp = post3.GetComponent<NOVA_Post> ();
		postComp.Initialize ((byte)3);
		postComp.SpawnChild ((byte)NOVA_PartID.Part_ConnBracket_N6);
		postComp = post4.GetComponent<NOVA_Post> ();
		postComp.Initialize ((byte)4);
		postComp.SpawnChild ((byte)NOVA_PartID.Part_ConnBracket_N6);
		postComp = post5.GetComponent<NOVA_Post> ();
		postComp.Initialize ((byte)5);
		postComp.SpawnChild ((byte)NOVA_PartID.Part_ConnBracket_N6);
		postComp = post6.GetComponent<NOVA_Post> ();
		postComp.Initialize ((byte)6);
		postComp.SpawnChild ((byte)NOVA_PartID.Part_ConnBracket_N6);

		NOVA_Connector connComp = pullup1.GetComponent<NOVA_Connector> ();
		connComp.Initialize ((byte)1);
		connComp = pullup2.GetComponent<NOVA_Connector> ();
		connComp.Initialize ((byte)2);
		connComp.CanHaveClimberBar = true;
		connComp = pullup3.GetComponent<NOVA_Connector> ();
		connComp.Initialize ((byte)3);
		connComp = pullup4.GetComponent<NOVA_Connector> ();
		connComp.Initialize ((byte)4);
		connComp = logoCM.GetComponent<NOVA_Connector> ();
		connComp.Initialize ((byte)5);
		connComp.CanHaveClimberBar = true;
		connComp = pullup6.GetComponent<NOVA_Connector> ();
		connComp.Initialize ((byte)6);
		connComp = archBridge.GetComponent<NOVA_Connector> ();
		connComp.Initialize ((byte)1);

		// post1 is parent of pullup1
		SetParentChild (post1, pullup1);
		// pullup1 is parent of post2
		SetParentChild (pullup1, post2);

		// post1 is parent to archBridge
		SetParentChild (post1, archBridge);
		// post2 is parent to pullup2
		SetParentChild (post2, pullup2);

		// pullup2 is parent to post3
		SetParentChild (pullup2, post3);
		// post3 is parent to pullup3
		SetParentChild (post3, pullup3);
		// pullup3 is parent to post4
		SetParentChild (pullup3, post4);
		// post4 is parent to pullup4
		SetParentChild (post4, pullup4);
		// pullup4 is parent to post5
		SetParentChild (pullup4, post5);
		// post5 is parent to logoCM
		SetParentChild (post5, logoCM);
		// logoCM is parent to post6
		SetParentChild (logoCM, post6);
		// post6 is parent to pullup6
		SetParentChild (post6, pullup6);

		return post5.GetComponent<NOVA_Post> ();
	}

	public void BuildNOVA6_Extended()
	{
		NOVA_Post bridgePost = BuildNOVA6_Single ();

		GameObject extBridge = SpawnPart((byte)NOVA_PartID.Conn_HorizontalBridge_Long, "Marker HORIZONTAL LONG BRIDGE Extended", bridgePost.gameObject);

		for(int i = 0; i < bridgePost.ChildrenParts.Count; ++i)
		{
			NOVA_Connector conn = bridgePost.ChildrenParts [i].GetComponent<NOVA_Connector> ();
			if(conn)
			{
				conn.CanHaveClimberBar = false;
				i = bridgePost.ChildrenParts.Count;
			}
		}

		SetParentChild (bridgePost.gameObject, extBridge);

		GameObject post1 = SpawnPart ((byte)NOVA_PartID.NFTS_1000, extBridge);
		post1.GetComponent<NOVA_Post> ().SpawnChild ((byte)NOVA_PartID.Part_ConnBracket_N4_Right);

		GameObject pullup1 = SpawnPart ((byte)NOVA_PartID.Conn_BasicCrossmember, "Marker BASIC CROSSMEMBER N4", post1);

		SetParentChild (post1, pullup1);

		GameObject post2 = SpawnPart ((byte)NOVA_PartID.NFTS_1000, "Marker NFTS-1000 N4", pullup1);
		post2.GetComponent<NOVA_Post> ().SpawnChild ((byte)NOVA_PartID.Part_ConnBracket_N4_Left);
	}

	public override void TogglePart (GameObject selectedPart, byte spawnID)
	{
		NOVA_Post sPost = selectedPart.GetComponent<NOVA_Post> ();
		NOVA_Connector sConn = selectedPart.GetComponent<NOVA_Connector> ();
		if (sPost)
		{	
			if(NOVA_Part.IsAPart((NOVA_PartID)spawnID))
			{
				// trying to spawn a new part
				List<byte> compatPartIDs = sPost.GetCompatibleParts ();
				if (compatPartIDs.Contains (spawnID))
				{
					if (sPost.HasChildPart (spawnID))
					{
						// post has spawn part already. destroy child.
						sPost.DestroyChild (spawnID);
					} else
					{						
						List<GameObject> deleteChildren = new List<GameObject>();
						for(int childID = 0; childID < sPost.ChildrenParts.Count; ++childID)
						{
							BaseSelectable childBS = sPost.ChildrenParts [childID].GetComponent<BaseSelectable> ();
							List<byte> childCompatWith = childBS.GetCompatibleParts ();
							if (!childCompatWith.Contains (spawnID))
								deleteChildren.Add (sPost.ChildrenParts [childID]);
						}

						for(int i = 0; i < deleteChildren.Count; ++i)
						{
							int index = sPost.ChildrenParts.FindIndex ((GameObject obj) => (obj == deleteChildren [i]));
							Destroy(sPost.ChildrenParts[index]);
							sPost.ChildrenParts.RemoveAt (index);
						}

						sPost.SpawnChild (spawnID);	
						SceneManager.Instance.DropMenu.UpdateItemsForPart (sPost);
					}
				}
			}
		}
		else if(sConn)
		{
			if(NOVA_Part.IsAPart((NOVA_PartID)spawnID))
			{
				// trying to spawn a new part
				List<byte> compatPartIDs = sConn.GetCompatibleParts ();
				if (compatPartIDs.Contains (spawnID))
				{
					if (sConn.HasChildPart (spawnID))
					{
						// post has spawn part already. destroy child.
						sConn.DestroyChild (spawnID);
					} 
					else
					{
						// find any existing parts that conflict with new part and delete them.
						List<GameObject> deleteChildren = new List<GameObject>();
						for(int childID = 0; childID < sConn.ChildrenParts.Count; ++childID)
						{
							NOVA_Part childPart = sConn.ChildrenParts [childID].GetComponent<NOVA_Part> ();
							if (childPart)
							{
								// we dont want to delete child posts
								List<byte> childCompatWith = childPart.GetCompatibleParts ();
								if (!childCompatWith.Contains (spawnID))
									deleteChildren.Add (childPart.gameObject);
							}
						}

						for(int deleteID = 0; deleteID < deleteChildren.Count; ++deleteID)
						{
							sConn.ChildrenParts.Remove (deleteChildren [deleteID]);
							Destroy (deleteChildren [deleteID]);
						}

						// post does not have spawn part. spawn child.
						sConn.SpawnChild (spawnID);

						SceneManager.Instance.DropMenu.UpdateItemsForPart (sConn);
					}
				}
				else
				{
					// current connector is not compatible with new spawn part.
					//Debug.Log(sConn.gameObject.name + " is not compatible with " + ((NOVA_PartID)spawnID).ToString());

					GameObject newConnGO = SpawnPart ((byte)NOVA_PartID.Conn_BasicCrossmember, sConn.ParentPart);
					SetParentChild (sConn.ParentPart, newConnGO);
					sConn.TransferOrDestroyChildren (newConnGO.GetComponent<NOVA_Connector> ());

					BaseSelectable selectedParentBS = sConn.ParentPart.GetComponent<BaseSelectable> ();
					selectedParentBS.ChildrenParts.Remove (selectedPart);
					Destroy (selectedPart);

					NOVA_Connector sNewConn = newConnGO.GetComponent<NOVA_Connector> ();
					if (sConn.CanHaveClimberBar)
						sNewConn.CanHaveClimberBar = true;
					sNewConn.Initialize (sConn.MyNumber);
					sNewConn.SpawnChild (spawnID);

					DropdownMenu_NOVA novaDropMenu = SceneManager.Instance.DropMenu as DropdownMenu_NOVA;
					novaDropMenu.DeleteItemsForPart (sConn);
					novaDropMenu.CreateItemsFor (sNewConn);
					novaDropMenu.ReorderMenuItems ();
					novaDropMenu.ResizeParent ();

					PopupMenu_NOVA novaPopMenu = SceneManager.Instance.PopupMenu as PopupMenu_NOVA;
					novaPopMenu.DeleteItemsForPart (sConn);
					novaPopMenu.CreateItemsFor (sNewConn);

					List<byte> activeChildrenTypes = sNewConn.GetActiveChildrenTypes ();
					foreach (byte partID in activeChildrenTypes)
						novaDropMenu.SetActive_ItemsForPart (sNewConn, partID);
				}
			}
			else if(NOVA_Connector.IsAConnector((NOVA_PartID)spawnID))
			{
				GameObject newConnGO = SpawnPart (spawnID, sConn.ParentPart);
				SetParentChild (sConn.ParentPart, newConnGO);
				sConn.TransferOrDestroyChildren (newConnGO.GetComponent<NOVA_Connector> ());

				BaseSelectable selectedParentBS = sConn.ParentPart.GetComponent<BaseSelectable> ();
				selectedParentBS.ChildrenParts.Remove (selectedPart);
				Destroy (selectedPart);

				NOVA_Connector sNewConn = newConnGO.GetComponent<NOVA_Connector> ();
				sNewConn.Initialize (sConn.MyNumber);
				if (sConn.CanHaveClimberBar)
					sNewConn.CanHaveClimberBar = true;
				
				DropdownMenu_NOVA novaDropMenu = SceneManager.Instance.DropMenu as DropdownMenu_NOVA;
				novaDropMenu.DeleteItemsForPart (sConn);
				novaDropMenu.CreateItemsFor (sNewConn);
				novaDropMenu.SetActive_ItemsForPart (sNewConn, (byte)sNewConn.PartID);
				novaDropMenu.ReorderMenuItems ();
				novaDropMenu.ResizeParent ();

				PopupMenu_NOVA novaPopMenu = SceneManager.Instance.PopupMenu as PopupMenu_NOVA;
				novaPopMenu.DeleteItemsForPart (sConn);
				novaPopMenu.CreateItemsFor (sNewConn);
			}
			else
				Debug.LogError (((NOVA_PartID)spawnID).ToString()+" does not register as a NOVA_Part or NOVA_Connector.");
		}
	}

	protected override string GetTemplateName(byte partType)
	{
		string templateName = "INVALID";
		switch((NOVA_PartID)partType)
		{
		case NOVA_PartID.NFTS_1000:
			templateName = "NFTS-1000";
			break;
		case NOVA_PartID.Conn_BasicCrossmember:
			templateName = "NFTS-2000 BASIC CROSSMEMBER";
			break;
		case NOVA_PartID.Conn_LogoCrossmember:
			templateName = "NFTS-2200 LOGO CROSSMEMBER";
			break;
		case NOVA_PartID.Part_ConnBracket_N6:
			templateName = "NFTS-1050 CONNECTOR BRACKET - HEX";
			break;
		case NOVA_PartID.Conn_StdPullUp:
			templateName = "NFTS-2100 PULL-UP BAR CROSSMEMBER";
			break;
		case NOVA_PartID.Conn_ArchBridge:
			templateName = "NOVA ARCH BRIDGE";
			break;
		case NOVA_PartID.Conn_HorizontalBridge_Long:
			templateName = "HORIZONTAL LONG BRIDGE";
			break;
		case NOVA_PartID.Part_AccessoryLoop:
			templateName = "NFTS-3400 ACCESSORY LOOP";
			break;
		case NOVA_PartID.Part_Dip:
			templateName = "NFTS-7000 DIP BAR";
			break;
		case NOVA_PartID.Part_Step:
			templateName = "NFTS-7100 STEP";
			break;
		case NOVA_PartID.Part_GRT_Single:
			templateName = "NFTS-9400 SINGLE GRT";
			break;
		case NOVA_PartID.Part_GRT_Double:
			templateName = "NFTS-9100 DOUBLE GRT";
			break;
		case NOVA_PartID.Part_RopeSlide_Single:
			templateName = "NFTS-8400 ROPE SLIDE SINGLE";
			break;
		case NOVA_PartID.Part_RopeSlide_Double:
			templateName = "NFTS-8500 ROPE SLIDE DOUBLE";
			break;
		case NOVA_PartID.Part_RopeAnchor:
			templateName = "NFTS-9200 ROPE ANCHOR";
			break;
		case NOVA_PartID.Part_GLoops:
			templateName = "NFTS-3450 G-LOOPS";
			break;
		case NOVA_PartID.Part_SlidingPullUp:
			templateName = "NFTS-8000 SLIDING PULL-UP";
			break;		
		case NOVA_PartID.Part_ErgoPullUp:
			templateName = "ERGO PULL-UP BAR";
			break;	
		case NOVA_PartID.Part_KickPlate:
			templateName = "KICK PLATE";
			break;
		case NOVA_PartID.Part_StallBar:
			templateName = "STALL BAR";
			break;
		case NOVA_PartID.Part_StorageTrays:
			templateName = "STORAGE TRAY";
			break;
		case NOVA_PartID.Part_SideRailPullUp:
			templateName = "SIDE RAIL PULLUP";
			break;
		case NOVA_PartID.Part_GlobeGrips:
			templateName = "GLOBE GRIPS";
			break;
		case NOVA_PartID.Part_ClimberBar:
			templateName = "NFTS-3100 CLIMBER BAR";
			break;
		case NOVA_PartID.Part_SquatStand:
			if(SceneManager.Instance.ActiveFrame == FrameType.NOVA_6)
				templateName = "NOVA-6 SQUAT STAND";
			break;
		case NOVA_PartID.Part_ConnBracket_N4_Right:
			templateName = "CONNECTOR BRACKET N4 RIGHT";
			break;
		case NOVA_PartID.Part_ConnBracket_N4_Left:
			templateName = "CONNECTOR BRACKET N4 LEFT";
			break;
		default:
			Debug.LogError (((NOVA_PartID)partType).ToString()+" is not recognized.");
			break;
		}
		return templateName;
	}
}
