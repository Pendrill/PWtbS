using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour {

	//sets the info of various variables including the size of the screen, the offset for the screen position, and the speed
	public float theScreenWidth, theScreenHeight, offsetScreenPostition, speed;
	//set a vector 3 for rotation purposes
	public Vector3 euler;
	//get the textboxmanager object/scripts
	public TextBoxManager theTextBoxManager;
	//get the translatormanager object/scripts
	public TranslatorManager theTranslatorManager;
	//sets the info for various ints regarding the maximum and minimum amount of ratation for the camera.
	public int RotateCameraMaxX, RotateCameraMaxY, RotateCameraMinY, RotateCameraMinX;
	public MoveCameraDialogue theMoveCameraDialogue;

	// Use this for initialization
	void Start () {
		//get the textboxmanager and the translator manager
		theTextBoxManager = FindObjectOfType<TextBoxManager> ();
		theTranslatorManager = FindObjectOfType<TranslatorManager> ();
		theMoveCameraDialogue = FindObjectOfType<MoveCameraDialogue> ();
		//we set the speed for how fast the rotation of the camera will happen when the mouse reaches the edges of the screen
		speed = 25;
		//maximum and minimum degrees fro which the degrees the camera can rotate on both the x and y axis
		RotateCameraMaxY = 20;
		RotateCameraMinY = -20;
		RotateCameraMaxX = 10;
		RotateCameraMinX = -10;
		//we set the offset to where the mouse needs to reach so that the camera starts to rotate
		offsetScreenPostition = 20;
		//gets/sets the screen width and height
		theScreenWidth = Screen.width;
		theScreenHeight = Screen.height;

	}
	
	// Update is called once per frame
	void Update () {
		//if the translator journal panel is no active 
		if (!theTranslatorManager.panelIsActive && !theMoveCameraDialogue.moveToMouse) {
			//then if the mouse reaches the edges of the screen
			//rotate on the x and y axises accordingly 
			if (Input.mousePosition.x > theScreenWidth - offsetScreenPostition) {
				Camera.main.transform.eulerAngles = euler;
				euler.y += speed * Time.deltaTime;
			} else if (Input.mousePosition.x < 0 + offsetScreenPostition) {
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

			//have the ratototion stop if the angles of the camera reach the max rotation
			if (euler.x >= RotateCameraMaxX) {
				euler.x = Mathf.Clamp (euler.x, RotateCameraMinX, RotateCameraMaxX);
			} else if (euler.x <= RotateCameraMinX) {
				euler.x = Mathf.Clamp (euler.x, RotateCameraMinX, RotateCameraMaxX);
			}
			//.y is for left and right whereas .x is for up and down
			/*if (euler.y >= RotateCameraMaxY) {
				euler.y = Mathf.Clamp (euler.y, RotateCameraMinY, RotateCameraMaxY);
			} else if (euler.y <= RotateCameraMinY) {
				euler.y = Mathf.Clamp (euler.y, RotateCameraMinY, RotateCameraMaxY);
			}*/

			//reset euler when text box is active.
			if (theTextBoxManager.isTextBoxActive ) {
				euler = new Vector3 (0, 0, 0);
			}
		}
		
	}
}
