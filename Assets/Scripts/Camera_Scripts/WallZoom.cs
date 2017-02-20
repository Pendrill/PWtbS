using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallZoom : MonoBehaviour {

	//get the text box manager, translator manager, and the move camera dialogue objects/scripts
	public TextBoxManager theTextBoxManager;
	public TranslatorManager theTranslatorManager;
	public MoveCameraDialogue theMoveCameraDialogue;
	//bools checking if we are zooming in or out.
	public bool isZoom, ZoomOut;


	// Use this for initialization
	void Start () {
		//set the various game objects so that we have access to their scripts
		theTextBoxManager = FindObjectOfType<TextBoxManager> ();
		theTranslatorManager = FindObjectOfType<TranslatorManager> ();
		theMoveCameraDialogue = FindObjectOfType<MoveCameraDialogue> ();
		isZoom = false;
	}
	
	// Update is called once per frame
	void Update () {
		//if we click and we are not in a conversation, the camera is in its original position, and the translator journal is currently not on screen
		if (Input.GetKeyDown (KeyCode.Mouse0) && !theTextBoxManager.isTextBoxActive && theMoveCameraDialogue.transform.position == theMoveCameraDialogue.OriginalCameraPosition && !theTranslatorManager.panelIsActive) {

			//we send out a raycast from the mouse position
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			//if it hits and it is for this specific object
			if (Physics.Raycast (ray, out hit) && hit.collider.gameObject.name == this.gameObject.name) {
				//we then move towards the point where the raycast hit
				theMoveCameraDialogue.moveTowardNonObject (hit.point);
				//isZoom = true;
				// we wait so that the zoom in can happen
				StartCoroutine(waitToZoomOut());
			}
		}
		//if we are zoomed in
		if (isZoom) {
			if (Input.GetKeyDown (KeyCode.Mouse0)) {
				//then we need to zoom out
				theMoveCameraDialogue.wallZoom = true;
				theMoveCameraDialogue.moveToMouse = false;
				isZoom = false;
				ZoomOut = true;
			}

		}
		//as we zoom out
		if (ZoomOut) {
			//once we arrive back at the original camera position
			if (theMoveCameraDialogue.transform.position == theMoveCameraDialogue.OriginalCameraPosition) {
				//there is no longer a need for us to zoom out
				theMoveCameraDialogue.wallZoom = false;
				ZoomOut = false;
			}
		}
	}

	/// <summary>
	/// Waits to zoom out.
	/// </summary>
	/// <returns>The to zoom out.</returns>
	private IEnumerator waitToZoomOut(){
		yield return new WaitForSeconds (0.4f);
		isZoom = true;
		//theTextBoxManager.enableTextBox ();
	}
}
