using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOVA_Part_KickPlate : NOVA_Part
{
	protected override void Awake()
	{
		base.Awake ();
		PartID = NOVA_PartID.Part_KickPlate;
	}

	protected override void Start()
	{
		base.Start ();
	}

	protected override void Rename ()
	{		
		string newName = gameObject.name.Remove(gameObject.name.Length-7, 7); // remove "(Clone)"
		newName += " " + myNumber.ToString();
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
		// materials[2] is med ball color
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
		// materials[0] is primary
		// materials[1] is secondary
		// materials[2] is med ball color
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
		// materials[2] is med ball color
		Material[] mats = new Material[3]{ColorManager.PrimaryMat, ColorManager.SecondaryMat, rendComp.materials[2] };
		rendComp.materials = mats;
	}

//	public override void OnPointerClick (UnityEngine.EventSystems.PointerEventData eventData)
//	{
//		Vector3 camTargetPos = new Vector3 (parentPart.transform.position.x, 0f, parentPart.transform.position.z);
//		camController.MoveTargetTo (camTargetPos);
//
//		SceneManager.Instance.ShowPopupMenu (this);
//	}

	public override List<byte> GetCompatibleParts ()
	{
		List<byte> compatList = new List<byte> ();

		compatList.Add ((byte)NOVA_PartID.Part_ConnBracket_N6);
//		compatList.Add ((byte)NOVA_PartID.Part_ClimberBar);
//		compatList.Add ((byte)NOVA_PartID.Part_ErgoPullUp);
//		compatList.Add ((byte)NOVA_PartID.Part_SlidingPullUp);

		return compatList;
	}

}
