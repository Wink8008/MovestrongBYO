using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOVA_Part_SideRailPullup : NOVA_Part
{
	protected override void Awake()
	{
		base.Awake ();
		PartID = NOVA_PartID.Part_SideRailPullUp;
	}

	protected override void Start()
	{
		base.Start ();
	}

	protected override void Rename ()
	{
		gameObject.name = "SIDE RAIL PULLUP " + myNumber.ToString ();
	}

	protected override IEnumerator SetStartMaterial ()
	{
		MeshRenderer rendComp = GetComponent<MeshRenderer> ();

		while (!ColorManager.PrimaryMat)
			yield return null;

		rendComp.material = ColorManager.PrimaryMat;
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
		rendComp.material = ColorManager.PrimaryMat;
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

		compatList.Add ((byte)NOVA_PartID.Conn_HorizontalBridge_Long);
		compatList.Add ((byte)NOVA_PartID.Conn_HorizontalBridge_Short);
		compatList.Add ((byte)NOVA_PartID.Part_GlobeGrips);

		return compatList;
	}

}
