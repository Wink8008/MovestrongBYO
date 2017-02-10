//using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SceneManager : MonoBehaviour 
{	
	[SerializeField]
	protected DropdownMenu dropMenu = null;
	public DropdownMenu DropMenu {get{ return dropMenu;}}
	[SerializeField]
	protected PopupMenu popupMenu = null;
	public PopupMenu PopupMenu {get{ return popupMenu;}}
	[SerializeField]
	protected GameObject popupMenuParent = null;
	[SerializeField]
	protected GameObject partLabel = null;
	public GameObject PartLabel{get{ return partLabel;}}

	protected Canvas sceneCanvas = null;
	public Canvas SceneCanvas{get{ return sceneCanvas;}}

	public GameObject sceneLabelsParent = null; 

	private List<PartLabel> sceneLabels = new List<PartLabel> ();
	public List<PartLabel> SceneLabels {get{ return sceneLabels;}set{ sceneLabels = value;}}

	protected LabelsSetting labels_Setting = LabelsSetting.AllOn;
	public LabelsSetting Labels_Setting {get{ return labels_Setting;}set{ labels_Setting = value;}}

	public static SceneManager Instance = null;

	protected FrameType activeFrame = FrameType.None;
	public FrameType ActiveFrame{get{ return activeFrame;}}

	protected virtual void Awake()
	{
		if (Instance)
			Destroy (this.gameObject);
		else
			Instance = this;		

		if (!dropMenu)
			Debug.LogError ("dropMenu variable is null in "+gameObject.name);
		if (!popupMenu)
			Debug.LogError ("popupMenu variable is null in "+gameObject.name);
		if (!popupMenuParent)
			Debug.LogError ("popupMenuParent variable is null in " + gameObject.name);		
		if (!partLabel)
			Debug.LogError ("partLabel variable is null in "+gameObject.name);	
		if (!sceneLabelsParent)
			Debug.LogError ("sceneLabelsParent variable is null in "+gameObject.name);
		
		sceneCanvas = FindObjectOfType<Canvas> ();
		if(!sceneCanvas)
			Debug.LogError ("Error in "+gameObject.name+". No Canvas component found in scene.");
	}

	protected virtual void Start()
	{
		
	}

	public void SetLabels_Off()
	{
		labels_Setting = LabelsSetting.AllOff;

		for(int x = 0; x < sceneLabels.Count; ++x)
		{
			sceneLabels [x].gameObject.SetActive (false);
		}
	}

	public void SetLabels_On()
	{
		labels_Setting = LabelsSetting.AllOn;

		for(int x = 0; x < sceneLabels.Count; ++x)
		{
			sceneLabels [x].gameObject.SetActive (true);
		}
	}

	public void SetLabels_MouseOverOn()
	{
		labels_Setting = LabelsSetting.MouseOverOn;

		for(int x = 0; x < sceneLabels.Count; ++x)
		{
			sceneLabels [x].gameObject.SetActive (false);
		}
	}

	public virtual void BuildFrame (FrameType frameType)
	{
		throw new System.NotImplementedException ();
	}

	public void ShowPopupMenu(BaseSelectable forPartBS)
	{
		popupMenu.UpdateMenuTitle (forPartBS);
		popupMenu.DisplayMenu();
		popupMenu.ShowItemsFor (forPartBS);
		popupMenu.ResizeParent ();
		popupMenu.PositionOnScreen ();
	}

	public void HidePopupMenu ()
	{
		popupMenu.HideMenu ();
	}

	public void UpdateMenuItemHighlights()
	{
		dropMenu.UpdateItemHighlightColor ();
		popupMenu.UpdateItemHighlightColor ();
	}

}
