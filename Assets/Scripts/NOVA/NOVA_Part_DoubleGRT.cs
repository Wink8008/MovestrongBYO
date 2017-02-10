using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOVA_Part_DoubleGRT : NOVA_Part
{
	protected override void Awake()
	{
		base.Awake ();
		PartID = NOVA_PartID.Part_GRT_Double;
	}

	protected override void Start()
	{
		base.Start ();
	}

	protected override void Rename ()
	{
		string newName = gameObject.name.Remove(0, 10); // remove the NTFS-XXXX
		newName = newName.Remove(newName.Length-7, 7); // remove "(Clone)"
		newName += " " + myNumber.ToString();
		gameObject.name = newName;
	}

	protected override IEnumerator SetStartMaterial ()
	{
		MeshRenderer rendComp = GetComponent<MeshRenderer> ();

		while (!ColorManager.SecondaryMat)
			yield return null;

		// materials[0] is secondary
		// materials[1] is silver and never changes
		// materials[2] is black and never changes
		Material[] mats = new Material[3]{ColorManager.SecondaryMat, rendComp.materials[1], rendComp.materials[2] };
		rendComp.materials = mats;
	}

	public override void ShowHighlightMaterials ()
	{
		ShowHighlightMaterials_Parent ();
	}

	public override void ShowHighlightMaterialsMeOnly ()
	{
		MeshRenderer rendComp = GetComponent<MeshRenderer> ();
		// materials[0] is secondary
		// materials[1] is silver and never changes
		// materials[2] is black and never changes
		Material[] mats = new Material[3]{ColorManager.HighlightMat, rendComp.materials[1], rendComp.materials[2] };
		rendComp.materials = mats;
	}

	public override void ShowStandardMaterials ()
	{
		ShowStandardMaterials_Parent ();
	}

	public override void ShowStandardMaterialsMeOnly ()
	{
		MeshRenderer rendComp = GetComponent<MeshRenderer> ();
		// materials[0] is secondary
		// materials[1] is silver and never changes
		// materials[2] is black and never changes
		Material[] mats = new Material[3]{ColorManager.SecondaryMat, rendComp.materials[1], rendComp.materials[2] };
		rendComp.materials = mats;
	}

//	public override void OnPointerClick (UnityEngine.EventSystems.PointerEventData eventData)
//	{
//		Vector3 camTargetPos = new Vector3 (parentPart.transform.position.x, 0f, parentPart.transform.position.z);
//		camController.MoveTargetTo (camTargetPos);
//
//		SceneManager.Instance.ShowPopupMenu (parentPart.GetComponent<BaseSelectable>());
//	}

	public override List<byte> GetCompatibleParts ()
	{
		List<byte> compatList = new List<byte> ();

		compatList.Add ((byte)NOVA_PartID.Conn_ArchBridge);
		compatList.Add ((byte)NOVA_PartID.Part_ConnBracket_N6);
		compatList.Add ((byte)NOVA_PartID.Part_Dip);
		compatList.Add ((byte)NOVA_PartID.Part_GLoops);

		return compatList;
	}

}
