using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOVA_Part_SingleGRT : NOVA_Part
{
	protected override void Awake()
	{
		base.Awake ();
		PartID = NOVA_PartID.Part_GRT_Single;
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
		compatList.Add ((byte)NOVA_PartID.Part_ConnBracket_N6);
		compatList.Add ((byte)NOVA_PartID.Part_Dip);
		compatList.Add ((byte)NOVA_PartID.Part_GLoops);

		return compatList;
	}

}
