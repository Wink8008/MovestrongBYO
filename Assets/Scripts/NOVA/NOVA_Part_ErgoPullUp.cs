using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOVA_Part_ErgoPullUp : NOVA_Part
{
	protected override void Awake()
	{
		base.Awake ();
		PartID = NOVA_PartID.Part_ErgoPullUp;
	}

	protected override void Start()
	{
		base.Start ();
	}

	protected override void Rename ()
	{
		gameObject.name = "ERGO PULL-UP " + myNumber.ToString (); 
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

	public override void ShowHighlightMaterialsMeOnly ()
	{
		MeshRenderer rendComp = GetComponent<MeshRenderer> ();
		rendComp.material = ColorManager.HighlightMat;
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

//	public override void OnPointerClick (UnityEngine.EventSystems.PointerEventData eventData)
//	{
//		Vector3 camTargetPos = new Vector3 (parentPart.transform.position.x, 0f, parentPart.transform.position.z);
//		camController.MoveTargetTo (camTargetPos);
//
//		SceneManager.Instance.ShowPopupMenu (this);
//	}

	public override List<byte> GetCompatibleParts ()
	{
		List<byte> compatList = new List<byte> ();

		//compatList.Add ((byte)NOVA_PartID.Part_GLoops);
		compatList.Add ((byte)NOVA_PartID.Part_ConnBracket_N6);
//		compatList.Add ((byte)NOVA_PartID.Part_SquatStand);
//		compatList.Add ((byte)NOVA_PartID.Part_StallBar);
//		compatList.Add ((byte)NOVA_PartID.Part_StorageTrays);
//		compatList.Add ((byte)NOVA_PartID.Part_KickPlate);

		return compatList;
	}

}
