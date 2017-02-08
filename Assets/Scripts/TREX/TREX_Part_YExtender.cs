using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class TREX_Part_YExtender : TREX_Part
{

	protected override void Awake ()
	{
		base.Awake ();
		PartID = TREX_PartID.Part_Y_Extender;
	}

	protected override IEnumerator SetStartMaterial ()
	{
		MeshRenderer rendComp = GetComponent<MeshRenderer> ();

		while (!ColorManager.SecondaryMat)
			yield return null;

		rendComp.material = ColorManager.SecondaryMat;
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
		rendComp.material = ColorManager.SecondaryMat;
	}

	public override void ShowHighlightMaterialsMeOnly ()
	{
		MeshRenderer rendComp = GetComponent<MeshRenderer> ();
		rendComp.material = ColorManager.HighlightMat;
	}

	public override void OnPointerClick (UnityEngine.EventSystems.PointerEventData eventData)
	{
		Vector3 camTargetPos = new Vector3 (parentPart.transform.position.x, 0f, parentPart.transform.position.z);
		camController.MoveTargetTo (camTargetPos);
	}
}
