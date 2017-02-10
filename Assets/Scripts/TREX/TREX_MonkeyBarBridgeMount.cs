//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class TREX_MonkeyBarBridgeMount : TREX_Connector
{

	protected override void Awake()
	{
		base.Awake ();
		PartID = TREX_PartID.Conn_MonkeyBarBridgeMount;
	}

	protected override void Rename ()
	{
		string newName = gameObject.name.Remove (0, 5); //remove TREX from the beginning

		newName = newName.Replace("MONKEY", "M");

		int indexOf = newName.IndexOf ("T-"); // starting index of the part number (ie: T-7000)
		newName = newName.Remove(indexOf, 6);

		string badStr = "(Clone)";
		if (newName.Contains (badStr)) 
		{
			newName = newName.Replace (badStr, " "+myNumber.ToString());
		}
		gameObject.name = newName;
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

	public override void OnPointerClick (UnityEngine.EventSystems.PointerEventData eventData)
	{
		Vector3 camTargetPos = new Vector3 (parentPart.transform.position.x, 0f, parentPart.transform.position.z);
		camController.MoveTargetTo (camTargetPos);
	}

	protected override string GetMarkerName (byte partType)
	{
		string marker = "INVALID";
		switch((TREX_PartID)partType)
		{
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
		case TREX_PartID.Conn_MonkeyBarBridge:
			marker = "Marker MONKEY BAR BRIDGE South";
			break;
		default:
			Debug.LogError ("TREX_PartID "+((TREX_PartID)partType).ToString()+" does not match.");
			break;
		}
		return marker;
	}
}
