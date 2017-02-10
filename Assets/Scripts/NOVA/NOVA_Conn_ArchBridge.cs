using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOVA_Conn_ArchBridge : NOVA_Connector 
{

	protected override void Awake()
	{
		base.Awake ();
		PartID = NOVA_PartID.Conn_ArchBridge;
		labelSizeModifier = 8.8f;
	}

	protected override void Start()
	{
		base.Start ();
		SpawnPartLabel (labelSizeModifier);

		for (int i = 0; i < 4; ++i)
			SpawnChild ((byte)NOVA_PartID.Part_AccessoryLoop);
	}

	protected override void Rename ()
	{
		gameObject.name = "ARCH BRIDGE " + myNumber.ToString ();
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
		Material[] mats = new Material[2]{ColorManager.PrimaryMat, ColorManager.SecondaryMat };
		rendComp.materials = mats;
	}

	public override void ShowHighlightMaterials ()
	{
		ShowHighlightMaterials_Children ();
		MeshRenderer rendComp = GetComponent<MeshRenderer> ();
		// materials[0] is primary color
		// materials[1] is secondary color
		Material[] mats = new Material[2]{ColorManager.HighlightMat, ColorManager.HighlightMat };
		rendComp.materials = mats;
	}

	public override void ShowStandardMaterials ()
	{
		ShowStandardMaterials_Children ();
		MeshRenderer rendComp = GetComponent<MeshRenderer> ();
		// materials[0] is primary color
		// materials[1] is secondary color
		Material[] mats = new Material[2]{ColorManager.PrimaryMat, ColorManager.SecondaryMat };
		rendComp.materials = mats;
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
		case NOVA_PartID.Part_AccessoryLoop:
			int count = GetCountChildrenOfType (NOVA_PartID.Part_AccessoryLoop);
			marker = "Marker ACCESSORY LOOP " + (count + 1).ToString();
			break;
		default:
			Debug.LogError ("NOVA_PartID "+((NOVA_PartID)partType).ToString()+" does not match.");
			break;
		}
		return marker;
	}

	private int GetCountChildrenOfType(NOVA_PartID partType)
	{
		int partCount = 0;
		for(int i = 0; i < childrenParts.Count; ++i)
		{
			BaseSelectable childBS = childrenParts [i].GetComponent<BaseSelectable> ();
			if ((NOVA_PartID)childBS.PartID == partType)
				++partCount;
		}
		return partCount;
	}

	public override List<NOVA_PartID> GetMenuItems ()
	{	
		List<NOVA_PartID> menuItems = new List<NOVA_PartID> ();

		menuItems.Add (NOVA_PartID.Conn_ArchBridge);
		menuItems.Add (NOVA_PartID.Conn_HorizontalBridge_Long);

		return menuItems;
	}

	public override List<byte> GetCompatibleParts ()
	{
		List<byte> compatList = new List<byte> ();

		compatList.Add ((byte)NOVA_PartID.Part_AccessoryLoop);
		compatList.Add ((byte)NOVA_PartID.Part_GRT_Single);
		compatList.Add ((byte)NOVA_PartID.Part_GRT_Double);
		compatList.Add ((byte)NOVA_PartID.Part_Dip);
		compatList.Add ((byte)NOVA_PartID.Part_Step);
		compatList.Add ((byte)NOVA_PartID.Part_RopeAnchor);
		compatList.Add ((byte)NOVA_PartID.Part_RopeSlide_Single);
		compatList.Add ((byte)NOVA_PartID.Part_RopeSlide_Double);
		compatList.Add ((byte)NOVA_PartID.Part_GLoops);

		return compatList;
	}

	public override void SpawnChild (byte spawnID)
	{
		if((NOVA_PartID)spawnID != NOVA_PartID.Part_AccessoryLoop)
		{
			if (HasChildPart (spawnID))
				return;
		}

		GameObject template = frameMngr.GetTemplate (spawnID);
		Transform marker = GetMarker (spawnID);
		GameObject childPart = Instantiate<GameObject> (template, marker.position, marker.rotation);

		BaseSelectable childBS = childPart.GetComponent<BaseSelectable> ();
		if (!childBS)
			Debug.LogError (childPart.name+" does not contain a BaseSelectable derived component.");

		childrenParts.Add (childPart);
		childBS.ParentPart = gameObject;

//		if (spawnID.Equals ((byte)TREX_PartID.Part_Y_Extender) ||
//			spawnID.Equals ((byte)TREX_PartID.Part_MedBall_Dbl))
//			DestroyChild ((byte)TREX_PartID.Part_TopCap);
	}
}
