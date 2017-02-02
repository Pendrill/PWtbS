using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour {

	public float theScreenWidth, theScreenHeight, offsetScreenPostition, speed;
	public Vector3 euler;
	public TextBoxManager theTextBoxManager;
	public int RotateCameraMaxX, RotateCameraMaxY, RotateCameraMinY, RotateCameraMinX;
	// Use this for initialization
	void Start () {
		theTextBoxManager = FindObjectOfType<TextBoxManager> ();
		speed = 15;
		RotateCameraMaxY = 20;
		RotateCameraMinY = -20;
		RotateCameraMaxX = 10;
		RotateCameraMinX = -10;
		offsetScreenPostition = 20;
		theScreenWidth = Screen.width;
		theScreenHeight = Screen.height;

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.mousePosition.x > theScreenWidth - offsetScreenPostition) {
			Camera.main.transform.eulerAngles = euler;
			euler.y += speed * Time.deltaTime;
		} else if (Input.mousePosition.x < 0 + offsetScreenPostition){
			Camera.main.transform.eulerAngles = euler;
			euler.y -= speed * Time.deltaTime;
		}

		if (Input.mousePosition.y > theScreenHeight - offsetScreenPostition) {
			Camera.main.transform.eulerAngles = euler;
			euler.x -= speed * Time.deltaTime;
		} else if (Input.mousePosition.y < 0 + offsetScreenPostition) {
			Camera.main.transform.eulerAngles = euler;
			euler.x += speed * Time.deltaTime;
		}

		if (euler.x >= RotateCameraMaxX) {
			euler.x = Mathf.Clamp (euler.x, RotateCameraMinX, RotateCameraMaxX);
		} else if (euler.x <= RotateCameraMinX) {
			euler.x = Mathf.Clamp (euler.x, RotateCameraMinX, RotateCameraMaxX);
		}
		if (euler.y >= RotateCameraMaxY) {
			euler.y = Mathf.Clamp (euler.y, RotateCameraMinY, RotateCameraMaxY);
		} else if (euler.y <= RotateCameraMinY) {
			euler.y = Mathf.Clamp (euler.y, RotateCameraMinY, RotateCameraMaxY);
		}

		if (theTextBoxManager.isTextBoxActive) {
			euler = new Vector3 (0, 0, 0);
		}
		
	}
}
