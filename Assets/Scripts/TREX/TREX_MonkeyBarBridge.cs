//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class TREX_MonkeyBarBridge : TREX_Connector
{
	protected override void Awake()
	{
		base.Awake ();
		PartID = TREX_PartID.Conn_MonkeyBarBridge;
	}

	protected override void Start()
	{
		base.Start ();
		SpawnPartLabel (labelSizeModifier);
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

	public override void OnPointerClick (UnityEngine.EventSystems.PointerEventData eventData)
	{
		Vector3 camTargetPos = new Vector3 (transform.position.x, 0f, transform.position.z);
		camController.MoveTargetTo (camTargetPos);

		//SceneManager.Instance.ShowPopupMenu (this);
	}

	protected override string GetMarkerName (byte partType)
	{
		string marker = "INVALID";
		switch((TREX_PartID)partType)
		{
		case TREX_PartID.Conn_MonkeyBarBridgeMount:
			marker = "Marker MONKEY BAR BRIDGE MOUNT South";
			break;
		default:
			Debug.LogError ("TREX_PartID "+((TREX_PartID)partType).ToString()+" does not match.");
			break;
		}
		return marker;
	}
}
