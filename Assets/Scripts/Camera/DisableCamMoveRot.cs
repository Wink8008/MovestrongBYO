//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisableCamMoveRot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public void OnPointerEnter (UnityEngine.EventSystems.PointerEventData eventData)
	{		
		FrameManager.Instance.CamController.DisableMoveRot ();
	}

	public virtual void OnPointerExit (UnityEngine.EventSystems.PointerEventData eventData)
	{	
		FrameManager.Instance.CamController.EnableMoveRot ();
	}
}
