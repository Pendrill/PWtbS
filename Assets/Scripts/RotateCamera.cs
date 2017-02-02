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
