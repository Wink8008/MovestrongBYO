//using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class PopupMenu : MenuBase, IPointerExitHandler
{
	protected List<MaskableGraphic> displayComponents = new List<MaskableGraphic>();

	protected Vector2 curSize = Vector2.zero;

	private void PopulateDisplayComps()
	{
		GameObject parentGO = transform.parent.gameObject;
		displayComponents.Add (parentGO.GetComponent<RawImage> ());
		displayComponents.AddRange (parentGO.GetComponentsInChildren<Image> ());
		displayComponents.Add (parentGO.GetComponentInChildren<Text> ());
	}

	public void DisplayMenu()
	{
		for(int i = 0; i < displayComponents.Count; ++i)
		{
			displayComponents [i].enabled = true;
		}
	}

	public void HideMenu()
	{
		if (displayComponents.Count == 0)
			PopulateDisplayComps ();
		
		for(int i = 0; i < displayComponents.Count; ++i)
		{
			displayComponents [i].enabled = false;
		}

		for(int i = 0; i < allItems.Count; ++i)
		{
			allItems [i].SetActive (false);
		}
	}

	public void UpdateMenuTitle(BaseSelectable partBS)
	{
		Text title = transform.parent.GetComponentInChildren<Text> ();
		title.text = partBS.name;
	}

	public void PositionOnScreen()
	{
		Vector3 adjustedPos = Input.mousePosition;
		adjustedPos.y += curSize.y * 0.5f;
		transform.parent.position = adjustedPos;
	}

	public void ShowItemsFor(BaseSelectable partBS)
	{
		for(int i = 0; i < allItems.Count; ++i)
		{
			BaseSelectable menuItemRefBS = allItems [i].GetComponent<GameObjRef> ().GO_Ref.GetComponent<BaseSelectable>();

			if (partBS == menuItemRefBS)
			{
				allItems [i].SetActive (true);

				MenuItem menuItem = allItems [i].GetComponent<MenuItem> ();
				menuItem.ShowActiveColor ();
			}
			else
				allItems [i].SetActive (false);
		}
	}

	public void OnPointerExit (UnityEngine.EventSystems.PointerEventData eventData)
	{		
		SceneManager.Instance.HidePopupMenu ();
	}

	public override void ResizeParent ()
	{
		VerticalLayoutGroup vlg = menuItemParent.GetComponent<VerticalLayoutGroup> ();
		float spacing = vlg.spacing;

		float yDim = 0f;
		foreach(GameObject itemGO in allItems)
		{
			if (itemGO.activeSelf)
			{
				RectTransform itemRT = itemGO.GetComponent<RectTransform> ();
				yDim += itemRT.sizeDelta.y + spacing;
			}
		}
		yDim += spacing;

		curSize = menuItemParent.sizeDelta;
		curSize.y = yDim;

		menuItemParent.sizeDelta = curSize;

		// also resize me
		curSize.y += spacing;
		((RectTransform)transform).sizeDelta = curSize;
	}

}
