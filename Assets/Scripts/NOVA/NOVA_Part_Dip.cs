using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOVA_Part_Dip : NOVA_Part
{
	protected override void Awake()
	{
		base.Awake ();
		PartID = NOVA_PartID.Part_Dip;
	}

	protected override void Start()
	{
		base.Start ();
	}

	protected override void Rename ()
	{
		string newName = gameObject.name.Remove(0, 10); // remove the NTFS-XXXX
		newName = newName.Remove(newName.Length-10, 10); // remove "BAR(Clone)"
		newName += myNumber.ToString();
		gameObject.name = newName;
	}

	protected override IEnumerator SetStartMaterial ()
	{
		MeshRenderer rendComp = GetComponent<MeshRenderer> ();

		while (!ColorManager.PrimaryMat)
			yield return null;
		while (!ColorManager.SecondaryMat)
			yield return null;

		// materials[0] is primary
		// materials[1] is secondary
		// materials[2] is silver and never changes
		Material[] mats = new Material[3]{ColorManager.PrimaryMat, ColorManager.SecondaryMat, rendComp.materials[2] };
		rendComp.materials = mats;
	}

	public override void ShowHighlightMaterials ()
	{
		ShowHighlightMaterials_Parent ();
	}

	public override void ShowHighlightMaterialsMeOnly ()
	{
		MeshRenderer rendComp = GetComponent<MeshRenderer> ();
		Material[] mats = new Material[3]{ColorManager.HighlightMat, ColorManager.HighlightMat, rendComp.materials[2] };
		rendComp.materials = mats;
	}

	public override void ShowStandardMaterials ()
	{
		ShowStandardMaterials_Parent ();
	}

	public override void ShowStandardMaterialsMeOnly ()
	{
		MeshRenderer rendComp = GetComponent<MeshRenderer> ();
		// materials[0] is primary
		// materials[1] is secondary
		// materials[2] is silver and never changes
		Material[] mats = new Material[3]{ColorManager.PrimaryMat, ColorManager.SecondaryMat, rendComp.materials[2] };
		rendComp.materials = mats;
	}

//	public override void OnPointerClick (UnityEngine.EventSystems.PointerEventData eventData)
//	{
//		Vector3 camTargetPos = new Vector3 (parentPart.transform.position.x, 0f, parentPart.transform.position.z);
//		camController.MoveTargetTo (camTargetPos);
//
//		SceneManager.Instance.ShowPopupMenu (parentPart.GetComponent<BaseSelectable>());
//	}

	//	protected override string GetMarkerName (byte partType)
	//	{
	//		string marker = "INVALID";
	//		switch((NOVA_PartID)partType)
	//		{
	//		case NOVA_PartID.NFTS_1000:
	//			marker = "Marker NFTS-1000";
	//			break;
	//		
	//		default:
	//			Debug.LogError ("NOVA_PartID "+((NOVA_PartID)partType).ToString()+" does not match.");
	//			break;
	//		}
	//		return marker;
	//	}

	//	public override List<TREX_PartID> GetMenuItems ()
	//	{	
	//		List<TREX_PartID> menuItems = new List<TREX_PartID> ();
	//
	//		menuItems.Add (TREX_PartID.Conn_PullUp_Std);
	//		menuItems.Add (TREX_PartID.Conn_PullUp_Ergo);
	//
	//		return menuItems;
	//	}

	public override List<byte> GetCompatibleParts ()
	{
		List<byte> compatList = new List<byte> ();

		compatList.Add ((byte)NOVA_PartID.Conn_ArchBridge);
		compatList.Add ((byte)NOVA_PartID.Part_GLoops);
		compatList.Add ((byte)NOVA_PartID.Part_GRT_Double);
		compatList.Add ((byte)NOVA_PartID.Part_GRT_Single);
		compatList.Add ((byte)NOVA_PartID.Part_RopeAnchor);
		compatList.Add ((byte)NOVA_PartID.Part_ConnBracket_N6);

		return compatList;
	}
}
