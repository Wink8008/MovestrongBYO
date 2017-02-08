using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TREX_TopCap : BaseSelectable 
{	
	//private static List<byte> uniqueNumbers = new List<byte>();

	new public TREX_PartID PartID { get	{ return (TREX_PartID)partType; } set { partType = (byte)value; }}

	protected override void Awake()
	{
		base.Awake ();
		//SetUniqueNumber ();

		//Rename ();
		PartID = TREX_PartID.Part_TopCap;
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

	public override void ShowStandardMaterials ()
	{
		ShowStandardMaterials_Parent ();
	}

	public override void ShowStandardMaterialsMeOnly ()
	{
		MeshRenderer rendComp = GetComponent<MeshRenderer> ();
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

	public override void OnPointerClick (UnityEngine.EventSystems.PointerEventData eventData)
	{
		Vector3 camTargetPos = new Vector3 (parentPart.transform.position.x, 0f, parentPart.transform.position.z);
		camController.MoveTargetTo (camTargetPos);
	}
}
