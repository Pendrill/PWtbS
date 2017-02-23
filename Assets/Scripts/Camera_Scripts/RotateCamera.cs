using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

	public float time, time2;
	public Image panelRight, panelLeft, panelUp, panelDown;
	Color currentAlpha, destinationAlpha;
	bool left, right, up, down;

	// Use this for initialization
	void Start () {
		//get the textboxmanager and the translator manager
		theTextBoxManager = FindObjectOfType<TextBoxManager> ();
		theTranslatorManager = FindObjectOfType<TranslatorManager> ();
		theMoveCameraDialogue = FindObjectOfType<MoveCameraDialogue> ();
		//we set the speed for how fast the rotation of the camera will happen when the mouse reaches the edges of the screen
		speed = 55;
		//maximum and minimum degrees fro which the degrees the camera can rotate on both the x and y axis
		RotateCameraMaxY = 20;
		RotateCameraMinY = -20;
		RotateCameraMaxX = 10;
		RotateCameraMinX = -10;
		//we set the offset to where the mouse needs to reach so that the camera starts to rotate
		offsetScreenPostition = 100;
		//gets/sets the screen width and height
		theScreenWidth = Screen.width;
		theScreenHeight = Screen.height;

		currentAlpha = panelLeft.color;
		destinationAlpha = new Color (1, 1, 1, 0.23f);

	}
	
	// Update is called once per frame
	void Update () {
		//if the translator journal panel is no active 
		if (!theTranslatorManager.panelIsActive && !theMoveCameraDialogue.moveToMouse && !theTextBoxManager.isTextBoxActive) {
			//then if the mouse reaches the edges of the screen
			//rotate on the x and y axises accordingly 
			if (Input.mousePosition.x > theScreenWidth - offsetScreenPostition) {
				right = true;
				left = false;
				time += Time.deltaTime * 2;
				panelRight.color = Color.Lerp (currentAlpha, destinationAlpha, time);
				Camera.main.transform.eulerAngles = euler;
				euler.y += speed * Time.deltaTime;
				panelLeft.color = new Color (1, 1, 1, 0);
			} else if (Input.mousePosition.x < 0 + offsetScreenPostition) {
				right = false;
				left = true;
				time += Time.deltaTime * 2;
				panelLeft.color = Color.Lerp (currentAlpha, destinationAlpha, time);
				Camera.main.transform.eulerAngles = euler;
				euler.y -= speed * Time.deltaTime;
				panelRight.color = new Color (1, 1, 1, 0);
			} else {
				time -= Time.deltaTime * 2;
				if (right) {
					panelRight.color = Color.Lerp (currentAlpha, destinationAlpha, time);
					panelLeft.color = new Color (1, 1, 1, 0);
				} else if (left) {
					panelLeft.color = Color.Lerp (currentAlpha, destinationAlpha, time);
					panelRight.color = new Color (1, 1, 1, 0);
				}
			}

			if (Input.mousePosition.y > theScreenHeight - offsetScreenPostition) {
				up = true;
				down = false;
				time2 += Time.deltaTime * 2;
				panelUp.color = Color.Lerp (currentAlpha, destinationAlpha, time2);
				panelDown.color = new Color (1, 1, 1, 0);
				Camera.main.transform.eulerAngles = euler;
				euler.x -= speed * Time.deltaTime;
			} else if (Input.mousePosition.y < 0 + offsetScreenPostition) {
				up = false;
				down = true;
				time2 += Time.deltaTime * 2;
				panelDown.color = Color.Lerp (currentAlpha, destinationAlpha, time2);
				panelUp.color = new Color (1, 1, 1, 0);
				Camera.main.transform.eulerAngles = euler;
				euler.x += speed * Time.deltaTime;
			} else {
				time2 -= Time.deltaTime * 2;
				if (up) {
					panelUp.color = Color.Lerp (currentAlpha, destinationAlpha, time2);
				} else if (down) {
					panelDown.color = Color.Lerp (currentAlpha, destinationAlpha, time2);
				}
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

		if (time > 1f) {
			time = 1f;
		} else if (time < 0f) {
			time = 0f;
		}
		if (time2 > 1f) {
			time2 = 1f;
		} else if (time2 < 0f) {
			time2 = 0f;
		}
		
	}
}
