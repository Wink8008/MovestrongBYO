//using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOVA_Conn_LogoCrossmember : NOVA_Connector 
{

	protected override void Awake()
	{
		base.Awake ();
		PartID = NOVA_PartID.Conn_LogoCrossmember;
		labelSizeModifier = 9.66f;
	}

	protected override void Start()
	{
		base.Start ();
		SpawnPartLabel (labelSizeModifier);
	}

	protected override void Rename ()
	{
		gameObject.name = "LOGO CM " + myNumber.ToString ();
	}

	public override void ShowHighlightMaterials ()
	{
		ShowHighlightMaterials_Children ();
		MeshRenderer rendComp = GetComponent<MeshRenderer> ();
		rendComp.material = ColorManager.HighlightMat;
	}

	public override void ShowHighlightMaterials_Children()
	{
		for(int i = 0; i < childrenParts.Count; ++i)
		{
			BaseSelectable partBS = childrenParts [i].GetComponent<BaseSelectable> ();
			if (partBS is NOVA_Part)
				partBS.ShowHighlightMaterialsMeOnly ();
		}
	}

	//	public override void ShowHighlightMaterialsMeOnly ()
	//	{
	//		MeshRenderer rendComp = GetComponent<MeshRenderer> ();
	//		rendComp.material = ColorManager.HighlightMat;
	//	}

	public override void ShowStandardMaterials ()
	{
		ShowStandardMaterials_Children ();
		MeshRenderer rendComp = GetComponent<MeshRenderer> ();
		rendComp.material = ColorManager.SecondaryMat;
	}

	public override void ShowStandardMaterials_Children ()
	{
		for(int i = 0; i < childrenParts.Count; ++i)
		{
			BaseSelectable partBS = childrenParts [i].GetComponent<BaseSelectable> ();
			if (partBS is NOVA_Part)
				partBS.ShowStandardMaterialsMeOnly ();
		}
	}

	//	public override void ShowStandardMaterialsMeOnly ()
	//	{
	//		MeshRenderer rendComp = GetComponent<MeshRenderer> ();
	//		rendComp.material = ColorManager.SecondaryMat;
	//	}

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
//			if (SceneManager.Instance.ActiveFrame == FrameType.NOVA_6)
//				marker = "Marker NFTS-1000 N6";
//			else if (SceneManager.Instance.ActiveFrame == FrameType.NOVA_4)
//				marker = "Marker NFTS-1000 N4";	
			marker = "Marker NFTS-1000";
			break;
		case NOVA_PartID.Part_SlidingPullUp:
			marker = "Marker SLIDING PULL-UP";
			break;
		case NOVA_PartID.Part_ErgoPullUp:
			marker = "Marker ERGO PULL-UP";
			break;
		case NOVA_PartID.Part_KickPlate:
			marker = "Marker KICK PLATE";
			break;
		case NOVA_PartID.Part_StallBar:
			marker = "Marker STALL BAR";
			break;
		case NOVA_PartID.Part_StorageTrays:
			marker = "Marker STORAGE TRAY";
			break;
		case NOVA_PartID.Part_ClimberBar:
			marker = "Marker CLIMBER BAR";
			break;
		case NOVA_PartID.Part_SquatStand:
			if(SceneManager.Instance.ActiveFrame == FrameType.NOVA_6)
				marker = "Marker NOVA-6 SQUAT STAND";
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
		compatList.Add ((byte)NOVA_PartID.Part_SlidingPullUp);
		compatList.Add ((byte)NOVA_PartID.Part_ErgoPullUp);
		compatList.Add ((byte)NOVA_PartID.Part_ClimberBar);
		compatList.Add ((byte)NOVA_PartID.Part_KickPlate);
		compatList.Add ((byte)NOVA_PartID.Part_StallBar);
		compatList.Add ((byte)NOVA_PartID.Part_StorageTrays);
		compatList.Add ((byte)NOVA_PartID.Part_SquatStand);

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
