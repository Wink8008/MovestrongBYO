//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
     
	[SerializeField, Range(0.0f, 1.0f)]
	private float _lerpRate = 0f;
	public float minAngle = 345f;
	public float maxAngle = 80f;

	private float xRotation;
	private float yRotation;

	private bool moving2Target = false;
	private Vector3 newTargetPos;

	[SerializeField, Range(0.0f, 1.0f)]
	private float moveSpeed = 0.5f;
	[SerializeField, Range(0.001f, 0.1f)]
	private float closeEnough = 0.01f;

	private bool allowMoveRot = true;

	private Camera myCamera = null;

	[SerializeField]
	private UnityEngine.UI.Slider zoomSlider = null;

	private float zoomMultiplier = 0f;

	void Awake()
	{
		myCamera = GetComponentInChildren<Camera> ();
		if (!myCamera)
			Debug.LogError (gameObject.name + " does not have a child gameobject with a Camera component.");
		if(zoomSlider)
		{
			zoomMultiplier = myCamera.transform.localPosition.z / zoomSlider.value;

			Vector3 camPos = myCamera.transform.localPosition;
			camPos.z = zoomSlider.value * zoomMultiplier; 
			myCamera.transform.localPosition = camPos;
		}
		else
			Debug.LogError ("zoomSlider variable is null in " + gameObject.name + "'s CameraController component.");
	}

	void Update ()
	{
		if (allowMoveRot)
		{
			if (moving2Target)
			{			
				if (Vector3.Distance (transform.position, newTargetPos) < closeEnough)
				{
					moving2Target = false;
					transform.position = newTargetPos;
				} else
					transform.position = Vector3.Lerp (transform.position, newTargetPos, moveSpeed);
			} else if (Input.GetMouseButton (0))
			{
				xRotation += Input.GetAxis ("Mouse X");
				yRotation += Input.GetAxis ("Mouse Y");

				xRotation = Mathf.Lerp (xRotation, 0, _lerpRate);
				yRotation = Mathf.Lerp (yRotation, 0, _lerpRate);

				float newAngleX = transform.eulerAngles.x - yRotation;
				float newAngleY = transform.eulerAngles.y + xRotation;
				//Debug.Log ("newAngleX= "+newAngleX.ToString());

				if (newAngleX > maxAngle && newAngleX < minAngle)
				{			
					float maxAngleDif = Mathf.Abs (newAngleX - maxAngle);
					float minAngleDif = Mathf.Abs (minAngle - newAngleX);
					if (maxAngleDif < minAngleDif)
						newAngleX = maxAngle;
					else
						newAngleX = minAngle;
				}

				transform.eulerAngles = new Vector3 (newAngleX, newAngleY, 0f);	
			}
		}
	}

	public void MoveTargetTo(Vector3 pos)
	{
		moving2Target = true;
		newTargetPos = pos;
	}

	public void DisableMoveRot()
	{
		allowMoveRot = false;
	}

	public void EnableMoveRot()
	{
		allowMoveRot = true;
	}

	public void UpdateZoom()
	{
		Vector3 camPos = myCamera.transform.localPosition;
		camPos.z = zoomSlider.value * zoomMultiplier; 
		myCamera.transform.localPosition = camPos;
	}
}
