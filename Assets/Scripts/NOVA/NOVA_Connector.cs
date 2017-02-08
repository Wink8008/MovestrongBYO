using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOVA_Connector : BaseSelectable
{
	//private static List<byte> uniqueNumbers = new List<byte>();

	new public NOVA_PartID PartID { get	{ return (NOVA_PartID)partType; } set { partType = (byte)value; }}

	protected float labelSizeModifier = 8.66f;

	protected bool canHaveClimberBar = false;
	public bool CanHaveClimberBar{get{ return canHaveClimberBar;} set{ canHaveClimberBar = value;}}

	protected override void Awake () 
	{
		base.Awake ();
		//SetUniqueNumber ();
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

	protected override void Rename(){}

//	protected override void Rename ()
//	{
//		string newName = gameObject.name.Remove (0, 5); //remove TREX from the beginning
//
//		int indexOf = newName.IndexOf ("T-"); // starting index of the part number (ie: T-7000)
//		newName = newName.Remove(indexOf, 6);
//
//		string badStr = "(Clone)";
//		if (newName.Contains (badStr)) 
//		{
//			newName = newName.Replace (badStr, " "+myNumber.ToString());
//		}
//		gameObject.name = newName;
//	}

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
//		TREX_Post post = connectThis.GetComponent<TREX_Post> ();
//		if (!post)
//			Debug.LogError (gameObject.name+" is trying to create a connection to "+connectThis.name+
//				", but "+connectThis.name+" does not contain a TREX_Post Component.");
//
//		connections.Add(new TREX_Connection2Post (_holeType, _mountPos, post));
	}

	public override void TransferOrDestroyChildren(BaseSelectable newParent)
	{
		NOVA_Connector newConnParent = newParent as NOVA_Connector;
		List<byte> compatTypes = newConnParent.GetCompatibleParts ();

		for(int i = 0; i < childrenParts.Count; ++i)
		{			
			BaseSelectable transferPartBS = childrenParts [i].GetComponent<BaseSelectable> ();

			if (compatTypes.Contains (transferPartBS.PartID))
			{
				// is compatible. transfer
				Transform marker = newConnParent.GetMarker (transferPartBS.PartID);
				childrenParts [i].transform.position = marker.position;
				childrenParts [i].transform.rotation = marker.rotation;

				newConnParent.childrenParts.Add (childrenParts [i]);
				transferPartBS.ParentPart = newConnParent.gameObject;	
			}
			else
			{
				// not compatible. destroy
				Destroy(childrenParts[i]);
			}
		}
	}

	public override void SpawnChild (byte spawnID)
	{
		if (HasChildPart (spawnID))
			return;

		GameObject template = frameMngr.GetTemplate (spawnID);
		Transform marker = GetMarker (spawnID);
		GameObject childPart = Instantiate<GameObject> (template, marker.position, marker.rotation);

		BaseSelectable childBS = childPart.GetComponent<BaseSelectable> ();
		if (!childBS)
			Debug.LogError (childPart.name+" does not contain a BaseSelectable derived component.");

		childrenParts.Add (childPart);
		childBS.ParentPart = gameObject;	
	}

	public static bool IsAConnector(NOVA_PartID partType)
	{
		if (partType.Equals (NOVA_PartID.Conn_StdPullUp) ||
			partType.Equals (NOVA_PartID.Conn_BasicCrossmember) ||
			partType.Equals (NOVA_PartID.Conn_LogoCrossmember) ||
			partType.Equals (NOVA_PartID.Conn_ArchBridge) ||
			partType.Equals (NOVA_PartID.Conn_HorizontalBridge_Long) ||
			partType.Equals (NOVA_PartID.Conn_HorizontalBridge_Short))
		{
			return true;
		} else
			return false;
	}

	new public virtual List<NOVA_PartID> GetMenuItems ()
	{
		return new List<NOVA_PartID> ();
	}

}
