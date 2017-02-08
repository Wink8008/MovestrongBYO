using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TREX_Connector : BaseSelectable
{
	//private static List<byte> uniqueNumbers = new List<byte>();

	new public TREX_PartID PartID { get	{ return (TREX_PartID)partType; } set { partType = (byte)value; }}

	protected float labelSizeModifier = 8.5f;

	protected override void Awake () 
	{
		base.Awake ();
//		SetUniqueNumber ();
		//Rename ();
	}

	protected override void OnDestroy()
	{
		base.OnDestroy ();
//		uniqueNumbers.Remove (myNumber);
//		uniqueNumbers.Sort ();
	}

//	protected override void SetUniqueNumber ()
//	{
//		byte availNum = 1;
//		while (uniqueNumbers.Contains(availNum))
//			availNum++;
//		myNumber = availNum;
//		uniqueNumbers.Add (myNumber);
//		uniqueNumbers.Sort ();
//	}

	public override void Initialize (byte idNumber)
	{
		myNumber = idNumber;
		Rename ();
	}

	protected override void Rename ()
	{
		string newName = gameObject.name.Remove (0, 5); //remove TREX from the beginning

		int indexOf = newName.IndexOf ("T-"); // starting index of the part number (ie: T-7000)
		newName = newName.Remove(indexOf, 6);

		string badStr = "(Clone)";
		if (newName.Contains (badStr)) 
		{
			newName = newName.Replace (badStr, " "+myNumber.ToString());
		}
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
		ShowHighlightMaterials_Children ();
		MeshRenderer rendComp = GetComponent<MeshRenderer> ();
		rendComp.material = ColorManager.HighlightMat;
	}

	public override void ShowStandardMaterials ()
	{
		ShowStandardMaterials_Children ();
		MeshRenderer rendComp = GetComponent<MeshRenderer> ();
		rendComp.material = ColorManager.SecondaryMat;
	}

	public override void CreateConnectionData (HoleType _holeType, MountingPos _mountPos, GameObject connectThis)
	{
		TREX_Post post = connectThis.GetComponent<TREX_Post> ();
		if (!post)
			Debug.LogError (gameObject.name+" is trying to create a connection to "+connectThis.name+
				", but "+connectThis.name+" does not contain a TREX_Post Component.");

		connections.Add(new TREX_Connection2Post (_holeType, _mountPos, post));
	}

	public static bool IsAConnector(TREX_PartID partType)
	{
		if (partType.Equals (TREX_PartID.Conn_MonkeyBarBridge) ||
		   partType.Equals (TREX_PartID.Conn_MonkeyBarBridgeMount) ||
		   partType.Equals (TREX_PartID.Conn_PullUp_Ergo) ||
		   partType.Equals (TREX_PartID.Conn_PullUp_Std))
		{
			return true;
		} else
			return false;
	}

	public override void TransferOrDestroyChildren(BaseSelectable newParent)
	{
		TREX_Connector newConnParent = newParent as TREX_Connector;

		for(int i = 0; i < childrenParts.Count; ++i)
		{
			BaseSelectable transferPartBS = childrenParts [i].GetComponent<BaseSelectable> ();

			Transform marker = newConnParent.GetMarker (transferPartBS.PartID);
			childrenParts [i].transform.position = marker.position;
			childrenParts [i].transform.rotation = marker.rotation;

			newConnParent.childrenParts.Add (childrenParts [i]);
			transferPartBS.ParentPart = newConnParent.gameObject;		
		}
	}

	new public virtual List<TREX_PartID> GetMenuItems()
	{
		return new List<TREX_PartID> ();
	}
}
