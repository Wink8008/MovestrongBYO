using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TREX_Post_T4600 : TREX_Post
{
	protected override void Awake()
	{
		base.Awake ();
		PartID = TREX_PartID.Post_T4600;
		mountingHoles = HoleType.High;
	}

	protected override IEnumerator SetStartMaterial ()
	{		
		MeshRenderer rendComp = GetComponent<MeshRenderer> ();

		while (!ColorManager.PrimaryMat)
			yield return null;
		while (!ColorManager.SecondaryMat)
			yield return null;

		// materials[0] is the post
		// materials[1] is the plate
		Material[] mats = new Material[2]{ColorManager.PrimaryMat, ColorManager.SecondaryMat };
		rendComp.materials = mats;
	}

	public override void ShowStandardMaterials ()
	{
		ShowStandardMaterials_Children ();
		MeshRenderer rendComp = GetComponent<MeshRenderer> ();
		// materials[0] is the post
		// materials[1] is the plate
		Material[] mats = new Material[2]{ColorManager.PrimaryMat, ColorManager.SecondaryMat };
		rendComp.materials = mats;
	}

	public override void ShowHighlightMaterials ()
	{
		ShowHighlightMaterials_Children ();
		MeshRenderer rendComp = GetComponent<MeshRenderer> ();
		// materials[0] is the post
		// materials[1] is the plate
		Material[] mats = new Material[2]{ColorManager.HighlightMat, ColorManager.HighlightMat };
		rendComp.materials = mats;
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

	public override List<byte> GetActiveChildrenTypes()
	{
		// overriding this is probably not the best way to highligh active posts in the dropmenu, but it works
		List<byte> childIDs = base.GetActiveChildrenTypes ();

		childIDs.Add ((byte)this.PartID);

		return childIDs;
	}
}
