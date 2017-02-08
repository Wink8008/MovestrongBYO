using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOVA_Post : BaseSelectable 
{
	//private static List<byte> uniqueNumbers = new List<byte>();

	new public NOVA_PartID PartID { get{ return (NOVA_PartID)partType; } set { partType = (byte)value; }}

	protected float labelSizeModifier = 9.66f;

	protected override void Awake()
	{
		base.Awake ();
		PartID = NOVA_PartID.NFTS_1000;
		//SetUniqueNumber ();
//		Rename ();
	}

	protected override void Start () 
	{
		base.Start ();
		SpawnPartLabel (labelSizeModifier);

//		switch(spawnWithPart)
//		{
//		case NOVA_PartID.Part_ConnBracket_N6:
//			SpawnChild ((byte)NOVA_PartID.Part_ConnBracket_N6);
//			break;
//		case NOVA_PartID.Part_ConnBracket_N4_Left:
//			SpawnChild ((byte)NOVA_PartID.Part_ConnBracket_N4_Left);
//			break;
//		case NOVA_PartID.Part_ConnBracket_N4_Right:
//			SpawnChild ((byte)NOVA_PartID.Part_ConnBracket_N4_Right);
//			break;
//		default:
//			Debug.LogWarning (spawnWithPart.ToString () + " is not recognized. " + gameObject.name + " will not spawn a child.");
//			break;
//		}
	}

	protected override void OnDestroy()
	{			
		base.OnDestroy ();
//		uniqueNumbers.Remove (myNumber);
//		uniqueNumbers.Sort ();
	}

	protected override void Rename ()
	{
		gameObject.name = "POST " + myNumber.ToString ();
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

	protected override IEnumerator SetStartMaterial ()
	{
		MeshRenderer rendComp = GetComponent<MeshRenderer> ();

		while (!ColorManager.PrimaryMat)
			yield return null;

		rendComp.material = ColorManager.PrimaryMat;
	}

	public override void ShowStandardMaterials ()
	{
		ShowStandardMaterials_Children ();
		MeshRenderer rendComp = GetComponent<MeshRenderer> ();
		rendComp.material = ColorManager.PrimaryMat;
	}

	public override void ShowHighlightMaterials ()
	{
		ShowHighlightMaterials_Children ();
		MeshRenderer rendComp = GetComponent<MeshRenderer> ();
		rendComp.material = ColorManager.HighlightMat;
	}

	public override void OnPointerClick (UnityEngine.EventSystems.PointerEventData eventData)
	{
		Vector3 camTargetPos = new Vector3 (transform.position.x, 0f, transform.position.z);
		camController.MoveTargetTo (camTargetPos);

		SceneManager.Instance.ShowPopupMenu (this);
	}

	public override void ShowHighlightMaterials_Children()
	{
		for(int i = 0; i < childrenParts.Count; ++i)
		{
			BaseSelectable childBS = childrenParts [i].GetComponent<BaseSelectable> ();
			if (NOVA_Part.IsAPart ((NOVA_PartID)childBS.PartID))
				childBS.ShowHighlightMaterialsMeOnly ();			
		}
	}

//	public override void CreateConnectionData (HoleType _holeType, MountingPos _mountPos, GameObject connectThis)
//	{
//		TREX_Connector conn = connectThis.GetComponent<TREX_Connector> ();
//		if (!conn)
//			Debug.LogError (gameObject.name+" is trying to create a connection to "+connectThis.name+
//				", but "+connectThis.name+" does not contain a TREX_Connector Component.");
//
//		connections.Add(new TREX_Connection2Conn (_holeType, _mountPos, conn));
//	}

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

	public void SpawnChildAtLoc (byte spawnID, MountingPos pos)
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



	public override void DestroyChild(byte killID)
	{
		base.DestroyChild (killID);

//		if (killID.Equals ((byte)TREX_PartID.Part_Y_Extender) ||
//			killID.Equals ((byte)TREX_PartID.Part_MedBall_Dbl))
//			SpawnChild ((byte)TREX_PartID.Part_TopCap);		
	}

	protected override string GetMarkerName (byte partType)
	{
		// this is for all parts that can be used by all posts
		string marker = "INVALID";
		switch((NOVA_PartID)partType)
		{
		case NOVA_PartID.Part_ConnBracket_N6:
			marker = "Marker CONNECTOR BRACKET - HEX East";
			break;
		case NOVA_PartID.Part_ConnBracket_N4_Left:
			marker = "Marker CONNECTOR BRACKET N4 LEFT";
			break;
		case NOVA_PartID.Part_ConnBracket_N4_Right:
			marker = "Marker CONNECTOR BRACKET N4 RIGHT";
			break;
		case NOVA_PartID.Conn_StdPullUp:
			marker = "Marker PULL-UP BAR CROSSMEMBER";
			break;	
		case NOVA_PartID.Conn_BasicCrossmember:
			if(SceneManager.Instance.ActiveFrame == FrameType.NOVA_6)
				marker = "Marker BASIC CROSSMEMBER N6";
			else if(SceneManager.Instance.ActiveFrame == FrameType.NOVA_4)
				marker = "Marker BASIC CROSSMEMBER N4";
			break;	
		case NOVA_PartID.Conn_LogoCrossmember:
			if(SceneManager.Instance.ActiveFrame == FrameType.NOVA_6)
				marker = "Marker LOGO CROSSMEMBER N6";
			else if(SceneManager.Instance.ActiveFrame == FrameType.NOVA_4)
				marker = "Marker LOGO CROSSMEMBER N4";
			break;	
		case NOVA_PartID.Conn_ArchBridge:
			marker = "Marker NOVA ARCH BRIDGE";
			break;
		case NOVA_PartID.Conn_HorizontalBridge_Long:
			marker = "Marker HORIZONTAL LONG BRIDGE";
			break;
		case NOVA_PartID.Part_Dip:
			marker = "Marker DIP";
			break;
		case NOVA_PartID.Part_Step:
			marker = "Marker STEP";
			break;
		case NOVA_PartID.Part_GRT_Single:
			marker = "Marker SINGLE GRT";
			break;
		case NOVA_PartID.Part_GRT_Double:
			marker = "Marker DOUBLE GRT";
			break;
		case NOVA_PartID.Part_RopeSlide_Single:
			marker = "Marker SINGLE ROPE SLIDE";
			break;
		case NOVA_PartID.Part_RopeSlide_Double:
			marker = "Marker DOUBLE ROPE SLIDE";
			break;
		case NOVA_PartID.Part_RopeAnchor:
			marker = "Marker ROPE ANCHOR";
			break;
		case NOVA_PartID.Part_GLoops:
			marker = "Marker G LOOPS";
			break;
		}
		return marker;
	}

//	protected override string GetMarkerName (byte partType, MountingPos relativePos)
//	{
//		string marker = "INVALID";
//		switch((NOVA_PartID)partType)
//		{
//		case NOVA_PartID.Part_ConnBracket_N4:
//			marker = GetMarkerName (partType);
//			if(relativePos == MountingPos.East)
//				marker += " RIGHT";
//			else if(relativePos == MountingPos.West)
//				marker += " LEFT";
//			break;				
//		}
//
//		//marker += GetRelativePosString (relativePos);
//		return marker;
//	}

	new public List<NOVA_PartID> GetMenuItems ()
	{
		List<NOVA_PartID> menuItems = new List<NOVA_PartID> ();

		menuItems.Add (NOVA_PartID.Part_Dip);
		menuItems.Add (NOVA_PartID.Part_Step);
		menuItems.Add (NOVA_PartID.Part_GRT_Single);
		menuItems.Add (NOVA_PartID.Part_GRT_Double);
		menuItems.Add (NOVA_PartID.Part_RopeAnchor);
		menuItems.Add (NOVA_PartID.Part_RopeSlide_Single);
		menuItems.Add (NOVA_PartID.Part_RopeSlide_Double);
		//menuItems.Add (NOVA_PartID.Part_SlidingRopeAnchor);
		menuItems.Add (NOVA_PartID.Part_GLoops);

		return menuItems;
	}

	public override List<byte> GetCompatibleParts ()
	{
		List<byte> compatList = new List<byte> ();

		compatList.Add ((byte)NOVA_PartID.Part_Dip);
		compatList.Add ((byte)NOVA_PartID.Part_Step);
		compatList.Add ((byte)NOVA_PartID.Part_GRT_Single);
		compatList.Add ((byte)NOVA_PartID.Part_GRT_Double);
		compatList.Add ((byte)NOVA_PartID.Part_RopeAnchor);
		compatList.Add ((byte)NOVA_PartID.Part_RopeSlide_Single);
		compatList.Add ((byte)NOVA_PartID.Part_RopeSlide_Double);
		compatList.Add ((byte)NOVA_PartID.Part_GLoops);

		return compatList;
	}
}
