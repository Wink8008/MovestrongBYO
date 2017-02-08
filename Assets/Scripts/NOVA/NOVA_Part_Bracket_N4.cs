using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOVA_Part_Bracket_N4 : NOVA_Part
{
	protected override void Awake()
	{
		// Set PartID before calling Awake as base.Awake will rename the part.
		if (gameObject.name.Contains ("LEFT"))
			PartID = NOVA_PartID.Part_ConnBracket_N4_Left;
		else if(gameObject.name.Contains("RIGHT"))
			PartID = NOVA_PartID.Part_ConnBracket_N4_Right;

		base.Awake ();

	}

	protected override void Start()
	{
		base.Start ();
	}

	protected override void Rename ()
	{		
		gameObject.name = "sdfd";
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

	public override List<byte> GetCompatibleParts ()
	{
		List<byte> compatList = new List<byte> ();

		compatList.Add ((byte)NOVA_PartID.Conn_ArchBridge);
		compatList.Add ((byte)NOVA_PartID.Part_GRT_Single);
		compatList.Add ((byte)NOVA_PartID.Part_GRT_Double);
		compatList.Add ((byte)NOVA_PartID.Part_Dip);
		compatList.Add ((byte)NOVA_PartID.Part_Step);
		compatList.Add ((byte)NOVA_PartID.Part_RopeAnchor);
		compatList.Add ((byte)NOVA_PartID.Part_SlidingRopeAnchor);
		compatList.Add ((byte)NOVA_PartID.Part_RopeSlide_Single);
		compatList.Add ((byte)NOVA_PartID.Part_RopeSlide_Double);
		compatList.Add ((byte)NOVA_PartID.Part_GLoops);

		return compatList;
	}

}
