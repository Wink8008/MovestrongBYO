//using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuTitle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	private List<GameObject> childItems = new List<GameObject>();
	private bool childrenShowing = true;

	public void AddChildItems(List<GameObject> children)
	{
		childItems.AddRange (children);

		ToggleChildItems ();			
	}

	public void ToggleChildItems()
	{
		for(int i = 0; i < childItems.Count; ++i)
		{
			childItems [i].SetActive (!childrenShowing);
		}
		
		childrenShowing = !childrenShowing;
		SceneManager.Instance.DropMenu.ResizeParent ();
	}

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
