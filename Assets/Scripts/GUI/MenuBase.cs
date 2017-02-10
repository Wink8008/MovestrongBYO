using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class MenuBase : MonoBehaviour 
{
	[SerializeField]
	protected Button menuItemPrefab = null;
	[SerializeField]
	protected List<GameObject> allItems = null;
	[SerializeField]
	protected RectTransform menuItemParent = null;

	protected virtual void Awake()
	{
		if (!menuItemPrefab)
			Debug.LogError ("menuItemPrefab variable is null in "+gameObject.name);
		if(!menuItemParent)
			Debug.LogError ("menuItemParent variable is null in "+gameObject.name);

		allItems = new List<GameObject> ();
	}

//	protected virtual void Start()
//	{
//		DeleteAllItems ();
//	}

	public abstract IEnumerator CreateStartItems ();

//	public void DeleteAllItems()
//	{
//		Button[] existing = menuItemParent.GetComponentsInChildren<Button> (true);
//		for(int i = 0; i < existing.Length; ++i)
//		{
//			Destroy (existing [i]);
//		}
//
//		allItems.Clear ();
//	}

	public abstract void CreateItemsFor (BaseSelectable part);


	public void DestroyAllItems()
	{
		for(int i = 0; i < allItems.Count; ++i)
		{
			Destroy (allItems [i]);
		}
		allItems.Clear ();
	}

	public virtual void ResizeParent()
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

		Vector2 sizeDelta = menuItemParent.sizeDelta;
		sizeDelta.y = yDim;

		menuItemParent.sizeDelta = sizeDelta;
	}

	public void DeleteItemsForPart(BaseSelectable part)
	{
		List<GameObject> deleteItems = new List<GameObject> ();

		foreach(GameObject item in allItems)
		{
			GameObjRef goRef = item.GetComponent<GameObjRef> ();
			if(goRef.GO_Ref == part.gameObject)
			{
				deleteItems.Add (item);
			}
		}

		foreach(GameObject killItem in deleteItems)
		{
			allItems.Remove (killItem);
			Destroy (killItem);
		}
	}

	public void UpdateItemHighlightColor()
	{
		for(int i = 0; i < allItems.Count; ++i)
		{
			Button itemBtnComp = allItems [i].GetComponent<Button> ();
			ColorBlock cb = itemBtnComp.colors;
			cb.highlightedColor = ColorManager.SecondaryMat.color;
			itemBtnComp.colors = cb;
		}
	}
}
