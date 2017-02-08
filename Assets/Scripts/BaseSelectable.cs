using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseSelectable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
	protected FrameManager frameMngr = null;
	protected List<GameObject> childrenParts = new List<GameObject>();
	public List<GameObject> ChildrenParts {get{ return childrenParts;} set{ childrenParts = value;}}
	protected GameObject parentPart = null;
	public GameObject ParentPart {get{ return parentPart;} set{ parentPart = value;}}
	protected CameraController camController = null;
	protected List<TREX_Connection> connections = new List<TREX_Connection>();
	public List<TREX_Connection> Connections {get{ return connections;} }
	protected byte partType = 0;
	public byte PartID {get{ return partType;}set{ partType = value;}}
	protected byte myNumber = 0;
	public byte MyNumber{get{ return myNumber;}}
	protected GameObject partLabel = null;
	public GameObject PartLabel{get{ return partLabel;}}

	protected virtual void Awake()
	{
		frameMngr = FrameManager.Instance;
		if (frameMngr)
			camController = frameMngr.CamController;
		else
			Debug.LogError ("FrameManager is not initialized yet.");

		StartCoroutine (SetStartMaterial ());
	}

	protected virtual void Start () 
	{
		frameMngr.RegisterPart (gameObject);
	}

	protected virtual void OnDestroy()
	{
		frameMngr.DeRegisterPart (gameObject);
		childrenParts = null;
		parentPart = null;

		if (partLabel)
		{
			SceneManager.Instance.SceneLabels.Remove (partLabel.GetComponent<PartLabel>());
			Destroy (partLabel);
		}
	}

	protected abstract void Rename ();

	protected void SpawnPartLabel(float sizeModifier)
	{
		// call this AFTER calling Rename on startup

		// get the world position marker
		string markerName = "Marker CANVAS LABEL";
		List<Transform> allMarkers = new List<Transform>(GetComponentsInChildren<Transform> ());
		Transform marker = allMarkers.Find ((Transform obj) => (obj.gameObject.name.Equals (markerName)));
		if (!marker)
			Debug.LogError (gameObject.name+" does not have a child Transform named "+markerName);		

		// spawn the label
		GameObject partLabelTemplate = SceneManager.Instance.PartLabel;
		if(partLabelTemplate)
		{
			Vector3 screenPos = Camera.main.WorldToScreenPoint (marker.position);
			partLabel = Instantiate<GameObject> (partLabelTemplate, screenPos, Quaternion.identity);
			PartLabel partLabelComp = partLabel.GetComponent<PartLabel> ();
			partLabelComp.WorldPos = marker.position;
			partLabel.transform.SetParent (SceneManager.Instance.sceneLabelsParent.transform, true);
			UnityEngine.UI.Text txt = partLabel.GetComponentInChildren<UnityEngine.UI.Text> ();
			txt.text = gameObject.name;
			partLabel.name = "PartLabel - " + gameObject.name;
			RectTransform recTrans = partLabel.GetComponent<RectTransform> ();						
			Vector2 newSize = recTrans.sizeDelta;
			newSize.x = sizeModifier * (float)txt.text.Length;
			recTrans.sizeDelta = newSize;
			SceneManager.Instance.SceneLabels.Add (partLabelComp);

			if(SceneManager.Instance.Labels_Setting == LabelsSetting.AllOn)
				partLabel.SetActive (true);			
			else
				partLabel.SetActive (false);
			
		}
	}

	protected abstract IEnumerator SetStartMaterial ();
	public abstract void ShowStandardMaterials ();

	public virtual void ShowStandardMaterials_Children ()
	{
		for(int i = 0; i < childrenParts.Count; ++i)
		{
			childrenParts [i].GetComponent<BaseSelectable> ().ShowStandardMaterialsMeOnly ();
		}
	}

	public void ShowStandardMaterials_Parent()
	{
		if(parentPart)
		{
			parentPart.GetComponent<BaseSelectable> ().ShowStandardMaterials ();
		}
	}

	public virtual void ShowStandardMaterialsMeOnly(){}

	public abstract void ShowHighlightMaterials ();

	public virtual void ShowHighlightMaterials_Children()
	{
		for(int i = 0; i < childrenParts.Count; ++i)
		{
			childrenParts [i].GetComponent<BaseSelectable> ().ShowHighlightMaterialsMeOnly ();
		}
	}

	public void ShowHighlightMaterials_Parent()
	{
		if (parentPart)
			parentPart.GetComponent<BaseSelectable>().ShowHighlightMaterials ();
	}

	public virtual void ShowHighlightMaterialsMeOnly(){}

	public virtual void OnPointerEnter (UnityEngine.EventSystems.PointerEventData eventData)
	{		
		ShowHighlightMaterials ();

		ShowLabel ();
	}

	public virtual void OnPointerExit (UnityEngine.EventSystems.PointerEventData eventData)
	{		
		ShowStandardMaterials ();

		HideLabel ();
	}

	public void ShowLabel()
	{
		if (partLabel)
		{
			if (SceneManager.Instance.Labels_Setting == LabelsSetting.MouseOverOn)
			{
				partLabel.SetActive (true);
			}
		}
	}

	public void HideLabel()
	{
		if (partLabel)
		{
			if (SceneManager.Instance.Labels_Setting == LabelsSetting.MouseOverOn)
			{
				partLabel.SetActive (false);
			}
		}
	}

	public virtual void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData){}

	public virtual void CreateConnectionData (HoleType _holeType, MountingPos _mountPos, GameObject connectThis){}

	/// <summary>
	/// Does this instance have a child part with partID?
	/// </summary>
	/// <returns><c>true</c> if this instance has a child part with the specified partID; otherwise, <c>false</c>.</returns>
	/// <param name="partID">Part to search for.</param>
	public bool HasChildPart(byte partID)
	{
		for(int i = 0; i < childrenParts.Count; ++i)
		{
			BaseSelectable bs = childrenParts [i].GetComponent<BaseSelectable> ();
			if (!bs)
				Debug.LogError (childrenParts[i].name+" does not contain a BaseSelectable component.");

			if (bs.PartID == partID)
				return true;			
		}
		return false;
	}

	/// <summary>
	/// Does this instance have a child part with partID? Yes: Returns index of child part. No: returns -1.
	/// </summary>
	/// <returns><c>the index of the child part</c> if a child part is found that matches partID; otherwise, <c>-1</c>.</returns>
	/// <param name="partID">Part to search for.</param>
	public int IndexOfChildPart(byte partID)
	{
		for(int i = 0; i < childrenParts.Count; ++i)
		{
			BaseSelectable bs = childrenParts [i].GetComponent<BaseSelectable> ();
			if (!bs)
				Debug.LogError (childrenParts[i].name+" does not contain a BaseSelectable component.");

			if (bs.PartID == partID)
				return i;			
		}
		return -1;
	}

	public virtual void DestroyChild(byte killID)
	{
		int index = IndexOfChildPart (killID);
		if (index < 0)
			Debug.LogError (gameObject.name+" does not have a child part of type "+((TREX_PartID)killID).ToString());

		Destroy (childrenParts [index]);
		childrenParts.RemoveAt (index);
	}

	public virtual void SpawnChild(byte spawnID)
	{
		Debug.LogWarning (gameObject.name+" does not override SpawnChild. No child spawned.");
	}

	protected virtual string GetMarkerName (byte partType)
	{
		Debug.LogError ("Returning INVALID Marker.  This method must be overriden in a derived class.");
		return "INVALID";
	}

	protected virtual string GetMarkerName (byte partType, MountingPos relativePos)
	{
		Debug.LogError ("Returning INVALID Marker.  This method must be overriden in a derived class.");
		return "INVALID";
	}

	public Transform GetMarker (byte partType)
	{
		string markerName = GetMarkerName (partType);

		return GetMarkerByName (markerName);
	}

	public Transform GetMarker(byte partType, MountingPos relativePos)
	{
		string markerName = GetMarkerName (partType, relativePos);

		return GetMarkerByName (markerName);
	}

	public Transform GetMarkerByName(string markerName)
	{
		List<Transform> allMarkers = new List<Transform>(GetComponentsInChildren<Transform> ());
		Transform marker = allMarkers.Find ((Transform obj) => (obj.gameObject.name.Equals (markerName)));
		if (!marker)
			Debug.LogError (gameObject.name+" does not have a child Transform named "+markerName);
		return marker;
	}

	//protected abstract void SetUniqueNumber ();

	public abstract void Initialize (byte idNumber);

	public virtual List<byte> GetMenuItems ()
	{
		return new List<byte>();
	}

	public virtual List<byte> GetCompatibleParts ()
	{
		return new List<byte>();
	}

	public virtual void TransferOrDestroyChildren(BaseSelectable newParent)
	{
		
	}

	protected string GetRelativePosString(MountingPos relativePos)
	{
		string posString = "INVALID";
		switch(relativePos)
		{
		case MountingPos.North:
			posString = "North";
			break;
		case MountingPos.South:
			posString = "South";
			break;
		case MountingPos.East:
			posString = "East";
			break;
		case MountingPos.West:
			posString = "West";
			break;
		}
		return posString;
	}

	public virtual List<byte> GetActiveChildrenTypes()
	{
		List<byte> childIDs = new List<byte> ();

		foreach(GameObject child in childrenParts)
		{
			BaseSelectable childBS = child.GetComponent<BaseSelectable> ();
			childIDs.Add (childBS.PartID);
		}
		return childIDs;
	}
}
