using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class TREX_Part_Step : TREX_Part
{

	protected override void Awake ()
	{
		base.Awake ();
		PartID = TREX_PartID.Part_Step;
	}

	protected override IEnumerator SetStartMaterial ()
	{
		MeshRenderer rendComp = GetComponent<MeshRenderer> ();

		while (!ColorManager.PrimaryMat)
			yield return null;
		while (!ColorManager.SecondaryMat)
			yield return null;

		// temp material, change color
		Material secondary = rendComp.materials[1];
		secondary.color = ColorManager.SecondaryMat.color;

		// materials[0] is the frame
		// materials[1] is the plate
		Material[] mats = new Material[2]{ColorManager.PrimaryMat, secondary };
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

		// temp material, change color
		Material secondary = rendComp.materials[1];
		secondary.color = ColorManager.SecondaryMat.color;

		// materials[0] is the frame
		// materials[1] is the plate
		Material[] mats = new Material[2]{ColorManager.PrimaryMat, secondary };
		rendComp.materials = mats;
	}

	public override void ShowHighlightMaterialsMeOnly ()
	{
		MeshRenderer rendComp = GetComponent<MeshRenderer> ();

		// temp material, change color
		Material secondary = rendComp.materials[1];
		secondary.color = ColorManager.HighlightMat.color;

		// materials[0] is the frame
		// materials[1] is the plate
		Material[] mats = new Material[2]{ColorManager.HighlightMat, secondary };
		rendComp.materials = mats;
	}

	public override void OnPointerClick (UnityEngine.EventSystems.PointerEventData eventData)
	{
		Vector3 camTargetPos = new Vector3 (parentPart.transform.position.x, 0f, parentPart.transform.position.z);
		camController.MoveTargetTo (camTargetPos);
	}
}
