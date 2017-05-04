using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallZoom : MonoBehaviour {

	//get the text box manager, translator manager, and the move camera dialogue objects/scripts
	public TextBoxManager theTextBoxManager;
	public TranslatorManager theTranslatorManager;
	public MoveCameraDialogue theMoveCameraDialogue;
	//bools checking if we are zooming in or out.
	public bool isZoom, ZoomOut, isBar, behind;
	public Collider barCollider;
	public Vector3 cameraLocationContractSelect;
	public Contract_Manager theContractManager;
    public RotateMouseClick theRotateMouseClick;


    // Use this for initialization
    void Start () {
		//set the various game objects so that we have access to their scripts
		theTextBoxManager = FindObjectOfType<TextBoxManager> ();
		theTranslatorManager = FindObjectOfType<TranslatorManager> ();
		theMoveCameraDialogue = FindObjectOfType<MoveCameraDialogue> ();
		isZoom = false;
		barCollider = GetComponent<Collider>();
		cameraLocationContractSelect = new Vector3 (-2.62f, 5.59f, -20.35f);
		theContractManager = FindObjectOfType <Contract_Manager> ();
        theRotateMouseClick = FindObjectOfType<RotateMouseClick>();

    }
	
	// Update is called once per frame
	void Update () {
		//if we click and we are not in a conversation, the camera is in its original position, and the translator journal is currently not on screen
		if (Input.GetKeyDown (KeyCode.Mouse0) && !theTextBoxManager.isTextBoxActive && theMoveCameraDialogue.transform.position == theMoveCameraDialogue.OriginalCameraPosition && !theTranslatorManager.panelIsActive && !theContractManager.confirmed) {

			//we send out a raycast from the mouse position
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			//if it hits and it is for this specific object
			if (Physics.Raycast (ray, out hit) && hit.collider.gameObject.name == this.gameObject.name) {
				//we then move towards the point where the raycast hit
				if (isBar) {
					theMoveCameraDialogue.moveTowardNonObject (cameraLocationContractSelect, isBar);
					barCollider.enabled = false;
                    theRotateMouseClick.back = behind;
                } else {
					theMoveCameraDialogue.moveTowardNonObject (hit.point, isBar);
					//isZoom = true;
					// we wait so that the zoom in can happen
				}
				StartCoroutine (waitToZoomOut ());
			}
		}
		//if we are zoomed in
		if (isZoom) {
			if (isBar){
                theRotateMouseClick.euler = new Vector3 (0, 180,0);
                if (theContractManager.confirmed) {
					theMoveCameraDialogue.wallZoom = true;
					theMoveCameraDialogue.moveToMouse = false;
                    
					isZoom = false;
					ZoomOut = true;
					theContractManager.confirmed = false;
				}
			} else {
				if (Input.GetKeyDown (KeyCode.Mouse0)) {
					//then we need to zoom out
					theMoveCameraDialogue.wallZoom = true;
					theMoveCameraDialogue.moveToMouse = false;
					isZoom = false;
					ZoomOut = true;
				}
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
	public IEnumerator waitToZoomOut(){
		yield return new WaitForSeconds (0.4f);
		isZoom = true;
		//theTextBoxManager.enableTextBox ();
	}
}
