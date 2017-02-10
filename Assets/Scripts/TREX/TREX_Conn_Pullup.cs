//using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TREX_Conn_Pullup : TREX_Connector
{

	protected override void Awake()
	{
		base.Awake ();
		PartID = TREX_PartID.Conn_PullUp_Std;
	}

	protected override void Start()
	{
		base.Start ();
		SpawnPartLabel (labelSizeModifier);
	}

	public override void ShowHighlightMaterials ()
	{
		ShowHighlightMaterialsMeOnly ();
	}

	public override void ShowHighlightMaterialsMeOnly ()
	{
		MeshRenderer rendComp = GetComponent<MeshRenderer> ();
		rendComp.material = ColorManager.HighlightMat;
	}

	public override void ShowStandardMaterials ()
	{
		ShowStandardMaterialsMeOnly ();
	}

	public override void ShowStandardMaterialsMeOnly ()
	{
		MeshRenderer rendComp = GetComponent<MeshRenderer> ();
		rendComp.material = ColorManager.SecondaryMat;
	}

	public override void OnPointerClick (UnityEngine.EventSystems.PointerEventData eventData)
	{
		Vector3 camTargetPos = new Vector3 (parentPart.transform.position.x, 0f, parentPart.transform.position.z);
		camController.MoveTargetTo (camTargetPos);

		SceneManager.Instance.ShowPopupMenu (this);
	}

//	protected override string GetMarkerName (byte partType, MountingPos relativePos)
//	{
//		string marker = GetMarkerName (partType);
//
//		switch(relativePos)
//		{
//		case MountingPos.North:
//			marker += "North";
//			break;
//		case MountingPos.South:
//			marker += "South";
//			break;
//		case MountingPos.East:
//			marker += "East";
//			break;
//		case MountingPos.West:
//			marker += "West";
//			break;
//		}
//		return marker;
//	}

	protected override string GetMarkerName (byte partType)
	{
		string marker = "INVALID";
		switch((TREX_PartID)partType)
		{
		case TREX_PartID.Post_T2200:
			marker = "Marker POST T-2200";
			break;
		case TREX_PartID.Post_T3000:
			marker = "Marker POST T-3000";
			break;
		case TREX_PartID.Post_T3500:
			marker = "Marker POST T-3500";
			break;
		case TREX_PartID.Post_T4000:
			marker = "Marker POST T-4000";
			break;
		case TREX_PartID.Post_T4600:
			marker = "Marker POST T-4600";
			break;
		case TREX_PartID.Post_T5000:
			marker = "Marker POST T-5000";
			break;
		case TREX_PartID.Post_T6000:
			marker = "Marker POST T-6000";
			break;	
		case TREX_PartID.Post_T6400:
			marker = "Marker POST T-6400";
			break;
		case TREX_PartID.Post_T6500:
			marker = "Marker POST T-6500";
			break;
		default:
			Debug.LogError ("TREX_PartID "+((TREX_PartID)partType).ToString()+" does not match.");
			break;
		}
		return marker;
	}

	public override List<TREX_PartID> GetMenuItems ()
	{	
		List<TREX_PartID> menuItems = new List<TREX_PartID> ();

		menuItems.Add (TREX_PartID.Conn_PullUp_Std);
		menuItems.Add (TREX_PartID.Conn_PullUp_Ergo);

		return menuItems;
	}

	public override List<byte> GetCompatibleParts ()
	{
		List<byte> compatList = new List<byte> ();

		compatList.Add ((byte)TREX_PartID.Conn_PullUp_Ergo);

		return compatList;
	}
}
