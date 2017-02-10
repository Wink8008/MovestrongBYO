//using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOVA_Conn_Pullup : NOVA_Connector 
{

	protected override void Awake()
	{
		base.Awake ();
		PartID = NOVA_PartID.Conn_StdPullUp;
	}

	protected override void Start()
	{
		base.Start ();
		SpawnPartLabel (labelSizeModifier);
	}

	protected override void Rename ()
	{
		gameObject.name = "PULL-UP CM " + myNumber.ToString ();
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

	protected override string GetMarkerName (byte partType)
	{
		string marker = "INVALID";
		switch((NOVA_PartID)partType)
		{
		case NOVA_PartID.NFTS_1000:
			marker = "Marker NFTS-1000";
			break;
		default:
			Debug.LogError ("NOVA_PartID "+((NOVA_PartID)partType).ToString()+" does not match.");
			break;
		}
		return marker;
	}

	public override List<NOVA_PartID> GetMenuItems ()
	{	
		List<NOVA_PartID> menuItems = new List<NOVA_PartID> ();

		menuItems.Add (NOVA_PartID.Conn_StdPullUp);
		menuItems.Add (NOVA_PartID.Part_ErgoPullUp);
		menuItems.Add (NOVA_PartID.Part_SlidingPullUp);
		menuItems.Add (NOVA_PartID.Part_KickPlate);
		menuItems.Add (NOVA_PartID.Part_SquatStand);
		menuItems.Add (NOVA_PartID.Part_StallBar);
		menuItems.Add (NOVA_PartID.Part_StorageTrays);

		if(canHaveClimberBar)
			menuItems.Add (NOVA_PartID.Part_ClimberBar);

		return menuItems;
	}

	public override List<byte> GetCompatibleParts ()
	{
		List<byte> compatList = new List<byte> ();

		compatList.Add ((byte)NOVA_PartID.NFTS_1000);

		//compatList.Add ((byte)NOVA_PartID.Part_ClimberBar);
		//compatList.Add ((byte)NOVA_PartID.Part_KickPlate);
		//compatList.Add ((byte)NOVA_PartID.Part_StallBar);
		//compatList.Add ((byte)NOVA_PartID.Part_StorageTrays);
		//compatList.Add ((byte)NOVA_PartID.Part_SquatRack);

		compatList.Add ((byte)NOVA_PartID.Part_ConnBracket_N6);
		compatList.Add ((byte)NOVA_PartID.Part_Dip);
		compatList.Add ((byte)NOVA_PartID.Part_GLoops);
		compatList.Add ((byte)NOVA_PartID.Part_GRT_Double);
		compatList.Add ((byte)NOVA_PartID.Part_GRT_Single);
		compatList.Add ((byte)NOVA_PartID.Part_RopeAnchor);
		compatList.Add ((byte)NOVA_PartID.Part_RopeSlide_Double);
		compatList.Add ((byte)NOVA_PartID.Part_RopeSlide_Single);
		compatList.Add ((byte)NOVA_PartID.Part_SlidingRopeAnchor);
		compatList.Add ((byte)NOVA_PartID.Part_Step);

		return compatList;
	}
}
