using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOVA_Part : BaseSelectable
{
	//private static List<byte> uniqueNumbers = new List<byte>();

	new public NOVA_PartID PartID { get	{ return (NOVA_PartID)partType; } set { partType = (byte)value; }}

	protected override void Awake()
	{
		base.Awake ();
		//SetUniqueNumber ();
		Rename ();
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

	public override void ShowStandardMaterials ()
	{
		ShowStandardMaterials_Parent ();
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
		string newName = gameObject.name.Remove (0, 9); //remove NFTS-XXXX from the beginning

		string badStr = "(Clone)";
		if (newName.Contains (badStr)) 
		{
			newName = newName.Replace (badStr, " "+myNumber.ToString());
		}
		gameObject.name = newName;
	}

	public static bool IsAPart(NOVA_PartID partType)
	{
		if (partType.Equals (NOVA_PartID.Part_AccessoryLoop) ||
			partType.Equals (NOVA_PartID.Part_GRT_Single) ||
			partType.Equals (NOVA_PartID.Part_GRT_Double) ||
			partType.Equals (NOVA_PartID.Part_Dip) ||
			partType.Equals (NOVA_PartID.Part_Step) ||
			partType.Equals (NOVA_PartID.Part_RopeAnchor) ||
			partType.Equals (NOVA_PartID.Part_RopeSlide_Single) ||
			partType.Equals (NOVA_PartID.Part_RopeSlide_Double) ||
			partType.Equals (NOVA_PartID.Part_KickPlate) ||
			partType.Equals (NOVA_PartID.Part_StorageTrays) ||
			partType.Equals (NOVA_PartID.Part_StallBar) ||
			partType.Equals (NOVA_PartID.Part_SquatStand) ||
			partType.Equals (NOVA_PartID.Part_GLoops) ||
			partType.Equals (NOVA_PartID.Part_SlidingPullUp) ||
			partType.Equals (NOVA_PartID.Part_ErgoPullUp) ||
			partType.Equals (NOVA_PartID.Part_ClimberBar) ||
			partType.Equals (NOVA_PartID.Part_ConnBracket_N6) ||
			partType.Equals (NOVA_PartID.Part_SideRailPullUp) ||
			partType.Equals (NOVA_PartID.Part_GlobeGrips) ||
			partType.Equals (NOVA_PartID.Part_ConnBracket_N4_Left) ||
			partType.Equals (NOVA_PartID.Part_ConnBracket_N4_Right))
		{
			return true;
		} else
			return false;
	}

	public override void OnPointerEnter (UnityEngine.EventSystems.PointerEventData eventData)
	{
		base.OnPointerEnter (eventData);

		if(SceneManager.Instance.Labels_Setting == LabelsSetting.MouseOverOn)
			parentPart.GetComponent<BaseSelectable> ().ShowLabel ();
	}

	public override void OnPointerExit (UnityEngine.EventSystems.PointerEventData eventData)
	{
		base.OnPointerExit (eventData);

		if(SceneManager.Instance.Labels_Setting == LabelsSetting.MouseOverOn)
			parentPart.GetComponent<BaseSelectable> ().HideLabel ();
	}

	public override void OnPointerClick (UnityEngine.EventSystems.PointerEventData eventData)
	{
		Vector3 camTargetPos = new Vector3 (parentPart.transform.position.x, 0f, parentPart.transform.position.z);
		camController.MoveTargetTo (camTargetPos);

		SceneManager.Instance.ShowPopupMenu (parentPart.GetComponent<BaseSelectable>());
	}
}
