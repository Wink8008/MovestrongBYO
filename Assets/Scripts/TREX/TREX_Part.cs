//using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TREX_Part : BaseSelectable
{
	//private static List<byte> uniqueNumbers = new List<byte>();

	new public TREX_PartID PartID { get	{ return (TREX_PartID)partType; } set { partType = (byte)value; }}

	protected override void Awake()
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

	public static bool IsAPart(TREX_PartID partType)
	{
		if (partType.Equals (TREX_PartID.Part_Ab_Board) ||
		   partType.Equals (TREX_PartID.Part_Dip) ||
		   partType.Equals (TREX_PartID.Part_MedBall_Dbl) ||
		   partType.Equals (TREX_PartID.Part_Step) ||
		   partType.Equals (TREX_PartID.Part_TopCap) ||
		   partType.Equals (TREX_PartID.Part_Y_Extender))
		{
			return true;
		} else
			return false;
	}
}
