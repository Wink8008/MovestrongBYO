//using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class DropdownMenu : MenuBase 
{
	[SerializeField]
	protected UnityEngine.UI.Button titleItemPrefab = null;

	protected override void Awake ()
	{
		base.Awake ();

		if (!titleItemPrefab)
			Debug.LogError ("titleItemPrefab variable is null in "+gameObject.name);
	}

//	public void DeleteItemsForPart(BaseSelectable part)
//	{
//		List<GameObject> deleteItems = new List<GameObject> ();
//
//		foreach(GameObject item in allItems)
//		{
//			GameObjRef goRef = item.GetComponent<GameObjRef> ();
//			if(goRef.GO_Ref == part.gameObject)
//			{
//				deleteItems.Add (item);
//			}
//		}
//
//		foreach(GameObject killItem in deleteItems)
//		{
//			allItems.Remove (killItem);
//			Destroy (killItem);
//		}
//	}

//	public void ResizeParent()
//	{
//		VerticalLayoutGroup vlg = menuItemParent.GetComponent<VerticalLayoutGroup> ();
//		float spacing = vlg.spacing;
//
//		float yDim = 0f;
//		foreach(GameObject itemGO in allItems)
//		{
//			if (itemGO.activeSelf)
//			{
//				RectTransform itemRT = itemGO.GetComponent<RectTransform> ();
//				yDim += itemRT.sizeDelta.y + spacing;
//			}
//		}
//		yDim += spacing;
//
//		Vector2 sizeDelta = menuItemParent.sizeDelta;
//		sizeDelta.y = yDim;
//
//		menuItemParent.sizeDelta = sizeDelta;
//	}

	public virtual void ReorderMenuItems()
	{
		// reorders menu items based on TREX_Post UniqueID, from first to last.
		List<GameObject> tempList = new List<GameObject>();

		byte highestID = 1;
		while(allItems.Count > 0)
		{	
			List<GameObject> list = allItems.FindAll((GameObject obj) => 
				(obj.GetComponent<GameObjRef>().GO_Ref.GetComponent<BaseSelectable>().MyNumber == highestID));

			for(int i = 0; i < list.Count; ++i)
			{
				allItems.Remove (list [i]);
			}

			tempList.AddRange (list);

			++highestID;
		}

		allItems = tempList;
	}

	public void SetActive_ItemsForPart(BaseSelectable part, byte partID)
	{
		foreach(GameObject itemGO in allItems)
		{
			GameObjRef goRef = itemGO.GetComponent<GameObjRef> ();
			if (goRef.GO_Ref == part.gameObject)
			{
				MenuItem item = itemGO.GetComponent<MenuItem> ();
				if (item) // title items will not have a Menu_Item derived component
				{
					if (item.PartID.Equals (partID))
					{
						item.ShowActiveColor ();
					}
				}
			}
		}
	}

	public void UpdateItemsForPart(BaseSelectable part)
	{
		foreach(GameObject itemGO in allItems)
		{
			GameObjRef goRef = itemGO.GetComponent<GameObjRef> ();
			if (goRef.GO_Ref == part.gameObject)
			{
				MenuItem item = itemGO.GetComponent<MenuItem> ();
				if (item) // title items will not have a Menu_Item derived component
				{
					item.ShowActiveColor ();
				}
			}
		}
	}

	public void UpdateAllItems()
	{
		foreach(GameObject itemGO in allItems)
		{
			GameObjRef goRef = itemGO.GetComponent<GameObjRef> ();
			BaseSelectable miParentPart = goRef.GO_Ref.GetComponent<BaseSelectable> ();
			MenuItem menuItem = itemGO.GetComponent<MenuItem> ();

			if(menuItem) // title items will not have a MenuItem Component
			{
				for (int childID = 0; childID < miParentPart.ChildrenParts.Count; ++childID)
				{
					BaseSelectable childBS = miParentPart.ChildrenParts [childID].GetComponent<BaseSelectable> ();
					if (childBS.PartID == menuItem.PartID)
					{
						menuItem.ShowActiveColor ();
						childID = miParentPart.ChildrenParts.Count;
					}
				}
			}
		}
	}
}
