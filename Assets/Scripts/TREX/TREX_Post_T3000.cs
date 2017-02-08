using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TREX_Post_T3000 : TREX_Post 
{

	protected override void Awake()
	{
		base.Awake ();
		PartID = TREX_PartID.Post_T3000;
		mountingHoles = HoleType.High;
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
		return GetMenuItems_HighMount ();
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
}
