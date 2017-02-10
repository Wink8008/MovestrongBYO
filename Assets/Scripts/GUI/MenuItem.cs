//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public abstract class MenuItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	protected Color myNormalColor = Color.white;
	protected Button btnComp = null;
	protected byte partID = 0;
	public byte PartID {get{ return partID;} set{ partID = value;}}

	protected virtual void Awake()
	{
		btnComp = GetComponent<Button> ();
	}

	protected virtual void Start()
	{
		ColorBlock cb = btnComp.colors;
		cb.highlightedColor = cb.pressedColor = ColorManager.SecondaryMat.color;
		btnComp.colors = cb;
	}

	public abstract void ShowActiveColor ();

	public void OnPointerEnter (UnityEngine.EventSystems.PointerEventData eventData)
	{		
		if(SceneManager.Instance.Labels_Setting == LabelsSetting.MouseOverOn)
		{
			GameObjRef goRef = GetComponent<GameObjRef> ();
			BaseSelectable goRefBS = goRef.GO_Ref.GetComponent<BaseSelectable> ();
			goRefBS.PartLabel.SetActive (true);
		}
	}

	public void OnPointerExit (UnityEngine.EventSystems.PointerEventData eventData)
	{		
		if(SceneManager.Instance.Labels_Setting == LabelsSetting.MouseOverOn)
		{
			GameObjRef goRef = GetComponent<GameObjRef> ();
			BaseSelectable goRefBS = goRef.GO_Ref.GetComponent<BaseSelectable> ();
			goRefBS.PartLabel.SetActive (false);
		}
	}
}
