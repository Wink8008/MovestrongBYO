using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TREX_Post : BaseSelectable 
{
	//private static List<byte> uniqueNumbers = new List<byte>();

	new public TREX_PartID PartID { get{ return (TREX_PartID)partType; } set { partType = (byte)value; }}

	protected HoleType mountingHoles = HoleType.None;
	public HoleType MountingHoles { get { return mountingHoles; } }

	protected float labelSizeModifier = 7.5f;

	protected override void Awake()
	{
		base.Awake ();
//		SetUniqueNumber ();
		//Rename ();
	}

	protected override void Start () 
	{
		base.Start ();
		if(ShouldSpawnTopCap())
			SpawnChild((byte)TREX_PartID.Part_TopCap);
		SpawnPartLabel (labelSizeModifier);
	}

	protected override void OnDestroy()
	{			
		base.OnDestroy ();
//		uniqueNumbers.Remove (myNumber);
//		uniqueNumbers.Sort ();
	}

	protected override void Rename ()
	{
		string newName = gameObject.name.Remove (0, 5); //remove TREX from the beginning

		int indexOf = newName.IndexOf ("POST");
		newName = newName.Insert (indexOf + 4, " " + myNumber.ToString () + " ");

		indexOf = newName.IndexOf ("(Clone)");
		newName = newName.Remove (indexOf, 7);

		indexOf = newName.IndexOf ("T-");
		string postType = newName.Substring (indexOf, 6);
		newName = newName.Remove (indexOf, 6);
		newName += "("+postType+")";

		gameObject.name = newName;
	}

	private bool ShouldSpawnTopCap()
	{
		// this method is to check if children parts contains a yExtender or dblMedBall which if added
		// to children parts via TransferOrDestroyChildren() will be added before Start() is called.
		foreach(GameObject child in childrenParts)
		{
			BaseSelectable childBS = child.GetComponent<BaseSelectable> ();
			if(childBS.PartID.Equals((byte)TREX_PartID.Part_Y_Extender) ||
				childBS.PartID.Equals((byte)TREX_PartID.Part_MedBall_Dbl))
			{
				return false;
			}
		}
		return true;
	}

	public override void TransferOrDestroyChildren(BaseSelectable newParent)
	{
		TREX_Post newPostParent = newParent as TREX_Post;
		List<byte> compatParts = newPostParent.GetCompatibleParts ();
		List<GameObject> transferParts = new List<GameObject> ();
		List<GameObject> destroyParts = new List<GameObject> ();
		for(int i = 0; i < childrenParts.Count; ++i)
		{
			BaseSelectable childBS = childrenParts [i].GetComponent<BaseSelectable> ();
			byte childPartID = childBS.PartID;

			if(childPartID.Equals((byte)TREX_PartID.Part_TopCap))
				destroyParts.Add (childrenParts [i]);
			else if (compatParts.Contains (childPartID))				
				transferParts.Add (childrenParts [i]);			
			else
				destroyParts.Add (childrenParts [i]);			
		}

		// destroy incompatible children
		foreach (GameObject part in destroyParts)
			Destroy (part);

		// transfer compatible children to new parent
		for(int i = 0; i < transferParts.Count; ++i)
		{			
			BaseSelectable transferPartBS = transferParts [i].GetComponent<BaseSelectable> ();

			Transform marker = newPostParent.GetMarker (transferPartBS.PartID);
			transferParts [i].transform.position = marker.position;
			transferParts [i].transform.rotation = marker.rotation;

			newPostParent.childrenParts.Add (transferParts [i]);
			transferPartBS.ParentPart = newPostParent.gameObject;
		}
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
			if (TREX_Part.IsAPart ((TREX_PartID)childBS.PartID))
				childBS.ShowHighlightMaterialsMeOnly ();
		}
	}

	public override void CreateConnectionData (HoleType _holeType, MountingPos _mountPos, GameObject connectThis)
	{
		TREX_Connector conn = connectThis.GetComponent<TREX_Connector> ();
		if (!conn)
			Debug.LogError (gameObject.name+" is trying to create a connection to "+connectThis.name+
				", but "+connectThis.name+" does not contain a TREX_Connector Component.");

		connections.Add(new TREX_Connection2Conn (_holeType, _mountPos, conn));
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

		if (spawnID.Equals ((byte)TREX_PartID.Part_Y_Extender) ||
			spawnID.Equals ((byte)TREX_PartID.Part_MedBall_Dbl))
			DestroyChild ((byte)TREX_PartID.Part_TopCap);	
	}

	public override void DestroyChild(byte killID)
	{
		base.DestroyChild (killID);

		if (killID.Equals ((byte)TREX_PartID.Part_Y_Extender) ||
		   killID.Equals ((byte)TREX_PartID.Part_MedBall_Dbl))
			SpawnChild ((byte)TREX_PartID.Part_TopCap);		
	}

	protected override string GetMarkerName (byte partType)
	{
		// this is for all parts that can be used by all posts
		string marker = "INVALID";
		switch((TREX_PartID)partType)
		{
		case TREX_PartID.Part_Y_Extender:
			marker = "Marker Y EXTENDER North";
			break;		
		case TREX_PartID.Part_TopCap:
			marker = "Marker TOP CAP";
			break;		
		}
		return marker;
	}

	protected override string GetMarkerName (byte partType, MountingPos relativePos)
	{
		string marker = "INVALID";
		switch((TREX_PartID)partType)
		{
		case TREX_PartID.Conn_PullUp_Std:
			marker = "Marker PULLUP ";
			break;		
		case TREX_PartID.Conn_PullUp_Ergo:
			marker = "Marker ERGO PULLUP ";
			break;		
		}

		marker += GetRelativePosString (relativePos);
		return marker;
	}

	public static TREX_PartID GetPostForPart(TREX_PartID partID)
	{
		TREX_PartID chosenPost = TREX_PartID.None;
		switch(partID)
		{
		case TREX_PartID.Part_Ab_Board:			
			chosenPost = TREX_PartID.Post_T6000;
			break;
		case TREX_PartID.Part_Dip:
			chosenPost = TREX_PartID.Post_T4000;
			break;
		case TREX_PartID.Part_Step:
			chosenPost = TREX_PartID.Post_T5000;
			break;
		case TREX_PartID.Post_T4600:
			chosenPost = TREX_PartID.Post_T4600;
			break;
		case TREX_PartID.Post_T6400:
			chosenPost = TREX_PartID.Post_T6400;
			break;
		case TREX_PartID.Post_T6500:
			chosenPost = TREX_PartID.Post_T6500;
			break;
		default:
			Debug.LogError ("No post for "+partID.ToString());
			break;
		}
		return chosenPost;
	}

//	public virtual List<TREX_PartID> GetActiveChildrenTypes()
//	{
//		List<TREX_PartID> childIDs = new List<TREX_PartID> ();
//
//		foreach(GameObject child in childrenParts)
//		{
//			BaseSelectable childBS = child.GetComponent<BaseSelectable> ();
//			childIDs.Add ((TREX_PartID)childBS.PartID);
//		}
//		return childIDs;
//	}

	public static bool IsAPost(TREX_PartID partType)
	{
		if (partType.Equals (TREX_PartID.Post_T2200) ||
		   partType.Equals (TREX_PartID.Post_T3000) ||
		   partType.Equals (TREX_PartID.Post_T3500) ||
		   partType.Equals (TREX_PartID.Post_T4000) ||
		   partType.Equals (TREX_PartID.Post_T4600) ||
		   partType.Equals (TREX_PartID.Post_T5000) ||
		   partType.Equals (TREX_PartID.Post_T6000) ||
			partType.Equals (TREX_PartID.Post_T6400) ||
			partType.Equals (TREX_PartID.Post_T6500))
		{
			return true;
		} else
			return false;
	}

	protected List<TREX_PartID> GetMenuItems_HighMount()
	{
		List<TREX_PartID> menuItems = new List<TREX_PartID> ();

		menuItems.Add (TREX_PartID.Part_Ab_Board);
		menuItems.Add (TREX_PartID.Part_Y_Extender);

		menuItems.Add (TREX_PartID.Post_T4600);
		menuItems.Add (TREX_PartID.Post_T6400);

//		// if parent is not a monkey bar bridge mount
//		BaseSelectable parentBS = parentPart.GetComponent<BaseSelectable> ();
//		if (!parentBS.PartID.Equals ((byte)TREX_PartID.Conn_MonkeyBarBridgeMount))
//		{
//			menuItems.Add (TREX_PartID.Post_T4600);
//			menuItems.Add (TREX_PartID.Post_T6400);
//		}

		return menuItems;
	}

	protected List<TREX_PartID> GetMenuItems_LowMount()
	{
		List<TREX_PartID> menuItems = new List<TREX_PartID> ();

		menuItems.Add (TREX_PartID.Part_Dip);
		menuItems.Add (TREX_PartID.Part_Step);
		menuItems.Add (TREX_PartID.Part_Y_Extender);
		menuItems.Add (TREX_PartID.Post_T6500);

//		// if parent is not a monkey bar bridge mount
//		BaseSelectable parentBS = parentPart.GetComponent<BaseSelectable> ();
//		if (!parentBS.PartID.Equals ((byte)TREX_PartID.Conn_MonkeyBarBridgeMount))
//		{			
//			menuItems.Add (TREX_PartID.Post_T6500);
//		}

		return menuItems;
	}

	new public abstract List<TREX_PartID> GetMenuItems ();
}
