using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TREX_Post_T6500 : TREX_Post 
{
	protected override void Awake()
	{
		base.Awake ();
		PartID = TREX_PartID.Post_T6500;
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

//	protected override string GetMarkerName (byte partType)
//	{
//		return base.GetMarkerName (partType);
//	}

	public override List<byte> GetActiveChildrenTypes()
	{
		// overriding this is probably not the best way to highligh active posts in the dropmenu, but it works
		List<byte> childIDs = base.GetActiveChildrenTypes ();

		childIDs.Add ((byte)this.PartID);

		return childIDs;
	}
}
