using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMouseClick : MonoBehaviour {

	public float dragSpeed = 45f;
	public Vector3 dragOrigin;
	Vector3 euler;
	public int RotateCameraMaxX, RotateCameraMinX;
	public bool horizontal, vertical;
	// Use this for initialization
	void Start () {
		RotateCameraMaxX = 10;
		RotateCameraMinX = -10;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			dragOrigin = Input.mousePosition;
			return;
		}
		if (!Input.GetMouseButton (0)) {
			horizontal = false;
			vertical = false;
			return;
		}
		Vector3 direction = Input.mousePosition - dragOrigin;
		if (direction.x < -10 && !horizontal) {
			vertical = true;
			Camera.main.transform.eulerAngles = euler;
			euler.y += dragSpeed * Time.deltaTime;
		} else if (direction.x > 10 && !horizontal) {
			vertical = true;
			Camera.main.transform.eulerAngles = euler;
			euler.y -= dragSpeed * Time.deltaTime;
		}
		else if (direction.y > 20 && !vertical) {
			horizontal = true;
			Camera.main.transform.eulerAngles = euler;
			euler.x -= dragSpeed * Time.deltaTime;
		} else if (direction.y < -20 && !vertical) {
			horizontal = true;
			Camera.main.transform.eulerAngles = euler;
			euler.x += dragSpeed * Time.deltaTime;
		} 

		if (euler.x >= RotateCameraMaxX) {
			euler.x = Mathf.Clamp (euler.x, RotateCameraMinX, RotateCameraMaxX);
		} else if (euler.x <= RotateCameraMinX) {
			euler.x = Mathf.Clamp (euler.x, RotateCameraMinX, RotateCameraMaxX);
		}

	}
}
