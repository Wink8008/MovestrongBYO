//using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FrameManager : MonoBehaviour 
{
	[SerializeField]
	protected GameObject[] templates;
	public GameObject[] Templates {get { return templates; } }
	protected List<GameObject> templatesList = null;
//	protected List<GameObject> TemplatesList 
//	{
//		get
//		{ 
//			if (templatesList == null)
//				templatesList = new List<GameObject> (templates);
//			
//			return templatesList;
//		}
//		set
//		{
//			templatesList = value;
//		}
//	}
	[SerializeField]
	protected GameObject[] parts;
	public List<GameObject> Parts {get{ return new List<GameObject> (parts);}}
	protected CameraController camController = null;
	public CameraController CamController{get{ return camController;}}

	public static FrameManager Instance = null;

	void Awake()
	{
		if (Instance)
			Destroy (this.gameObject);
		else
			Instance = this;
		
		camController = Camera.main.GetComponentInParent<CameraController> ();
		if (!camController)
			Debug.LogError (Camera.main.name+" does not have a parent GameObject with a CameraController Component.");	

		parts = new GameObject[0];

		templatesList = new List<GameObject> (templates);
	}

	public void RegisterPart(GameObject part)
	{
		List<GameObject> tempList = new List<GameObject> (parts);
		tempList.Add (part);
		parts = tempList.ToArray ();
	}

	public void DeRegisterPart(GameObject part)
	{
		List<GameObject> tempList = new List<GameObject> (parts);
		tempList.Remove (part);
		parts = tempList.ToArray ();
	}

	public void UpdateColors()
	{
		for(int i = 0; i < parts.Length; ++i)
		{
			BaseSelectable bs = parts [i].GetComponent<BaseSelectable> ();
			bs.ShowStandardMaterials ();

			SceneManager.Instance.DropMenu.UpdateItemsForPart (bs);
		}
	}

	protected void DestroyAllParts()
	{		
		for(int i = parts.Length - 1; i >= 0; --i)
		{
			DestroyImmediate (parts [i]);
		}

		SceneManager.Instance.DropMenu.DestroyAllItems ();
		SceneManager.Instance.PopupMenu.DestroyAllItems ();
	}

	public abstract void TogglePart (GameObject selectedPart, byte spawnID);

	protected void SetParentChild(GameObject parent, GameObject child)
	{
		BaseSelectable parentBS = parent.GetComponent<BaseSelectable> ();
		if (!parentBS)
			Debug.LogError (parent.name+" does not contain a BaseSelectable derived component.");
		
		BaseSelectable childBS = child.GetComponent<BaseSelectable> ();
		if (!childBS)
			Debug.LogError (child.name+" does not contain a BaseSelectable derived component.");

		parentBS.ChildrenParts.Add (child);
		childBS.ParentPart = parent;
	}

	/// <summary>
	/// Spawns a part with position x and z of 0.
	/// </summary>
	/// <param name="templateName">Template name.</param>
	protected GameObject SpawnPart(string templateName)
	{
		GameObject template = templatesList.Find ((GameObject obj) => (obj.name.Equals (templateName)));
		if (!template)
			Debug.LogError (gameObject.name+" does not contain a template named "+templateName);

		GameObject part = Instantiate<GameObject> (template);
		Vector3 pos = part.transform.position;
		pos.x = 0f;
		pos.z = 0f;
		part.transform.position = pos;

		return part;
	}

	protected GameObject SpawnPart(string templateName, string markerName, GameObject parent)
	{
		List<Transform> allMarkers = new List<Transform>(parent.GetComponentsInChildren<Transform> ());
		Transform marker = allMarkers.Find ((Transform obj) => (obj.gameObject.name.Equals (markerName)));
		if (!marker)
			Debug.LogError (parent.name+" does not have a child Transform named "+markerName);

		GameObject template = templatesList.Find ((GameObject obj) => (obj.name.Equals (templateName)));
		if (!template)
			Debug.LogError (gameObject.name+" does not contain a template named "+templateName);

		GameObject part = Instantiate<GameObject> (template, marker.position, marker.rotation);
		return part;
	}

	/// <summary>
	/// Spawns a part with position x and z of 0.
	/// </summary>
	/// <param name="templateName">Template name.</param>
	protected GameObject SpawnPart(byte partType)
	{
		GameObject template = GetTemplate (partType);

		GameObject part = Instantiate<GameObject> (template);
		Vector3 pos = part.transform.position;
		pos.x = 0f;
		pos.z = 0f;
		part.transform.position = pos;

		return part;
	}

	protected GameObject SpawnPart(byte partType, GameObject parent)
	{
		GameObject template = GetTemplate (partType);

		BaseSelectable parentBS = parent.GetComponent<BaseSelectable> ();
		Transform marker = parentBS.GetMarker (partType);

		GameObject part = Instantiate<GameObject> (template, marker.position, marker.rotation);

		return part;
	}

	protected GameObject SpawnPart(byte partType, string markerName, GameObject parent)
	{
		GameObject template = GetTemplate (partType);

		BaseSelectable parentBS = parent.GetComponent<BaseSelectable> ();
		Transform marker = parentBS.GetMarkerByName (markerName);

		GameObject part = Instantiate<GameObject> (template, marker.position, marker.rotation);

		return part;
	}

	protected virtual GameObject SpawnPart(byte partType, GameObject parent, MountingPos relativePos)
	{
		GameObject template = GetTemplate (partType);

		BaseSelectable parentBS = parent.GetComponent<BaseSelectable> ();
		Transform marker = parentBS.GetMarker (partType, relativePos);

		GameObject part = Instantiate<GameObject> (template, marker.position, marker.rotation);

		return part;
	}

	protected abstract string GetTemplateName (byte partType);

	public GameObject GetTemplate(byte partType)
	{
		string templateName = GetTemplateName (partType);
		GameObject template = templatesList.Find ((GameObject obj) => (obj.name.Equals (templateName)));
		return template;
	}

	public GameObject GetTemplate(string templateName)
	{		
		GameObject template = templatesList.Find ((GameObject obj) => (obj.name.Equals (templateName)));
		return template;
	}
}
