using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TREX_Post_T2200 : TREX_Post
{

	protected override void Awake()
	{
		base.Awake ();
		PartID = TREX_PartID.Post_T2200;
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
		List<TREX_PartID> menuItems = new List<TREX_PartID> ();

		menuItems.Add (TREX_PartID.Part_MedBall_Dbl);

		return menuItems;
	}

	public override List<byte> GetCompatibleParts ()
	{
		List<byte> compatList = new List<byte> ();

		compatList.Add ((byte)TREX_PartID.Part_MedBall_Dbl);

		return compatList;
	}

	protected override string GetMarkerName (byte partType)
	{
		string marker = base.GetMarkerName (partType);
		if (marker == "INVALID")
		{
			switch ((TREX_PartID)partType)
			{
			case TREX_PartID.Part_MedBall_Dbl:
				marker = "Marker DBL MED BALL TARGET";
				break;			
			default:
				Debug.LogError ("TREX_PartID " + ((TREX_PartID)partType).ToString () + " does not match.");
				break;
			}
		}
		return marker;
	}

}
