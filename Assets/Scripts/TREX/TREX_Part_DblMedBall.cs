using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class TREX_Part_DblMedBall : TREX_Part
{

	protected override void Awake ()
	{
		base.Awake ();
		PartID = TREX_PartID.Part_MedBall_Dbl;
	}

	protected override IEnumerator SetStartMaterial ()
	{
		MeshRenderer rendComp = GetComponent<MeshRenderer> ();

		while (!ColorManager.PrimaryMat)
			yield return null;
		while (!ColorManager.SecondaryMat)
			yield return null;

		// materials[0] is the frame
		// materials[1] is the M
		Material[] mats = new Material[2]{ColorManager.SecondaryMat, ColorManager.PrimaryMat };
		rendComp.materials = mats;
	}

	public override void ShowHighlightMaterials ()
	{
		ShowHighlightMaterials_Parent ();
	}

	public override void ShowStandardMaterials ()
	{
		ShowStandardMaterials_Parent ();
	}

	public override void ShowStandardMaterialsMeOnly ()
	{
		MeshRenderer rendComp = GetComponent<MeshRenderer> ();

		// materials[0] is the frame
		// materials[1] is the M
		Material[] mats = new Material[2]{ColorManager.SecondaryMat, ColorManager.PrimaryMat };
		rendComp.materials = mats;
	}

	public override void ShowHighlightMaterialsMeOnly ()
	{
		MeshRenderer rendComp = GetComponent<MeshRenderer> ();

		// materials[0] is the frame
		// materials[1] is the M
		Material[] mats = new Material[2]{ColorManager.HighlightMat, ColorManager.HighlightMat };
		rendComp.materials = mats;
	}
}
