using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TREX_Post_T3500 : TREX_Post 
{
	protected override void Awake()
	{
		base.Awake ();
		PartID = TREX_PartID.Post_T3500;
		mountingHoles = HoleType.Low;
	}

	public override void ShowStandardMaterials ()
	{
		ShowStandardMaterials_Children ();
		MeshRenderer rendComp = GetComponent<MeshRenderer> ();
		rendComp.material = ColorManager.PrimaryMat;
	}

	public override void ShowHighlightMaterials ()
	{
		ShowHighlightMaterials_Children ();
		MeshRenderer rendComp = GetComponent<MeshRenderer> ();
		rendComp.material = ColorManager.HighlightMat;
	}

	public override List<TREX_PartID> GetMenuItems ()
	{	
		return GetMenuItems_LowMount ();
	}

	public override List<byte> GetCompatibleParts ()
	{
		List<byte> compatList = new List<byte> ();

		compatList.Add ((byte)TREX_PartID.Part_Y_Extender);

		return compatList;
	}

	protected override string GetMarkerName (byte partType)
	{		
		string marker = base.GetMarkerName (partType);
		if (marker == "INVALID")
		{
			switch ((TREX_PartID)partType)
			{
			case TREX_PartID.Conn_MonkeyBarBridgeMount:
				marker = "Marker MONKEY BAR BRIDGE MOUNT North";
				break;			
			default:
				Debug.LogError ("TREX_PartID " + ((TREX_PartID)partType).ToString () + " does not match.");
				break;
			}
		}
		return marker;
	}
}
