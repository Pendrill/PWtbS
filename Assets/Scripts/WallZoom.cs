using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallZoom : MonoBehaviour {

	public TextBoxManager theTextBoxManager;
	public TranslatorManager theTranslatorManager;
	public MoveCameraDialogue theMoveCameraDialogue;
	public bool isZoom, ZoomOut;


	// Use this for initialization
	void Start () {
		theTextBoxManager = FindObjectOfType<TextBoxManager> ();
		theTranslatorManager = FindObjectOfType<TranslatorManager> ();
		theMoveCameraDialogue = FindObjectOfType<MoveCameraDialogue> ();
		isZoom = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Mouse0) && !theTextBoxManager.isTextBoxActive && theMoveCameraDialogue.transform.position == theMoveCameraDialogue.OriginalCameraPosition && !theTranslatorManager.panelIsActive) {

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit) && hit.collider.gameObject.name == this.gameObject.name) {
				theMoveCameraDialogue.moveTowardNonObject (hit.point);
				//isZoom = true;
				StartCoroutine(waitToZoomOut());
			}
		}
		if (isZoom) {
			if (Input.GetKeyDown (KeyCode.Mouse0)) {
				theMoveCameraDialogue.wallZoom = true;
				theMoveCameraDialogue.moveToMouse = false;
				isZoom = false;
				ZoomOut = true;
			}

		}
		if (ZoomOut) {
			if (theMoveCameraDialogue.transform.position == theMoveCameraDialogue.OriginalCameraPosition) {
				theMoveCameraDialogue.wallZoom = false;
				ZoomOut = false;
			}
		}
	}

	private IEnumerator waitToZoomOut(){
		yield return new WaitForSeconds (0.4f);
		isZoom = true;
		//theTextBoxManager.enableTextBox ();
	}
}
