using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraDialogue : MonoBehaviour {

	// a set of bools that checks various things regarding the camera movement
	public bool moveToObject, moveToMouse, wallZoom, objectZoom, zoomOut, bar;
	//reference to the specific object that needs to be focused on
	public GameObject theObject;
	//set of vector 3 for the original camera position, the offet once zoomed in for the camera;
	public Vector3 OriginalCameraPosition, offsetPosition, offsetPositionBack, mouseLocationZoom, mouseOffsetPosition, cameraRotationBar;//originalCameraRotation;
	//refrence to eleapsed time
	public float time;
	//reference to the text box manager
	public TextBoxManager theTextBoxManager;
	public objectPickupManager theObjectPickupManager;
    public objectExamineManager theObjectExamineManager;
	bool back;




	// Use this for initialization
	void Start () {
		//make sure the time is set to 0

		time = 0f;
		//moveToObject = false;
		//se the original camera position to it's current transform position
		OriginalCameraPosition = transform.position;
		//originalCameraRotation = Camera.main.transform.eulerAngles;
		//find the textbox manager
		theTextBoxManager = FindObjectOfType<TextBoxManager> ();
		theObjectPickupManager = FindObjectOfType<objectPickupManager> ();
        theObjectExamineManager = FindObjectOfType<objectExamineManager>();
		//set the object to the main camera
		//theObject = this.gameObject;
		//set the offset for the camera once it has zoomed in
		offsetPosition = new Vector3 (0, 0, -5);
		offsetPositionBack = new Vector3 (-0.5f, 1, 5);
		//dont think i use this anymore
		mouseOffsetPosition = new Vector3 (0, 0, -5);
		cameraRotationBar = new Vector3 (58.236f, -179.735f, 0.225f);

	}

    // Update is called once per frame
    void Update() {
        //if true then we need to zoom in
        if (moveToObject) {
            //change time so as to have the lerp work
            time += Time.deltaTime * 2;
            if (!back) {
                //lerp the camera position towards the object with the offset
                transform.position = Vector3.Lerp(OriginalCameraPosition, theObject.transform.position + offsetPosition, time);
                //transform.LookAt (theObject.transform);
                //Quaternion toRotation = Quaternion.FromToRotation(Vector3.up, transform.forward);
                //lerp the camera rotation so a to face the object
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, time);
                //Camera.main.transform.eulerAngles = Vector3.Lerp (-Camera.main.transform.eulerAngles, originalCameraRotation, time);
                if (!theTextBoxManager.isTextBoxActive && transform.position == theObject.transform.position + offsetPosition && !theObjectPickupManager.isActive && !theObjectExamineManager.isActive) {
                    //unzoom
                    moveToObject = false;
                    zoomOut = true;
                    //objectZoom = false;
                }
            } else {
                //lerp the camera position towards the object with the offset
                transform.position = Vector3.Lerp(OriginalCameraPosition, theObject.transform.position + offsetPositionBack, time);
                //transform.LookAt (theObject.transform);
                //Quaternion toRotation = Quaternion.FromToRotation(Vector3.up, transform.forward);
                //lerp the camera rotation so a to face the object
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity * Quaternion.Euler(0, 180, 0), time);
                //Camera.main.transform.eulerAngles = Vector3.Lerp (-Camera.main.transform.eulerAngles, originalCameraRotation, time);
                if (!theTextBoxManager.isTextBoxActive && transform.position == theObject.transform.position + offsetPositionBack && !theObjectPickupManager.isActive && !theObjectExamineManager.isActive) {
                    //unzoom
                    moveToObject = false;
                    zoomOut = true;
                    //objectZoom = false;
                }
            }
            //if dialogue is no longer happening and the camera is zoomed in
            //if (!theTextBoxManager.isTextBoxActive && transform.position == theObject.transform.position + offsetPosition) {
            //unzoom
            //moveToObject = false;
            //zoomOut = true;
            //objectZoom = false;
            //}
            //if you are moving towards a point on the wall
        } else if (moveToMouse) {
            //zoom in towards where the raycast of the mouse hit the wall + offset
            time += Time.deltaTime;
            transform.position = Vector3.Lerp(OriginalCameraPosition, mouseLocationZoom + mouseOffsetPosition, time);
            if (bar && transform.position == mouseLocationZoom + mouseOffsetPosition) {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(cameraRotationBar), time - 1);
            }
            //otherwise zoom out/ stay zoomed out
        } else {
            if (theObject != null)
            {

            
            time -= Time.deltaTime;
            //if there was a zoom in then we need to zoom out
            //based on whether it was a zoom towards the wall or and interactable object
            if (wallZoom) {
                transform.position = Vector3.Lerp(OriginalCameraPosition, mouseLocationZoom + mouseOffsetPosition, time);
                if (bar) {
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, -180, 0), time);
                }
                //wallZoom = false;
            } else if (objectZoom) {
                if (!back) {
                    transform.position = Vector3.Lerp(OriginalCameraPosition, theObject.transform.position + offsetPosition, time);
                } else {
                    transform.position = Vector3.Lerp(OriginalCameraPosition, theObject.transform.position + offsetPositionBack, time);
                }

            }
        }
    }
		//lock the time to either 0 or 1
		if (time > 1f) {
			time = 1f;
		} else if (time < 0f) {
			time = 0f;
		}
		//while zooming out, stop zooming out once the camera has reached its original position
		if (zoomOut) {
			if (transform.position == OriginalCameraPosition) {
				zoomOut = false;
				objectZoom = false;
				back = false;
			}
		}
	}

	/// <summary>
	/// Moves the toward object.
	/// </summary>
	/// <param name="theDestination">The destination.</param>
	public void moveTowardObject(GameObject theDestination, Vector3 objectOffset){
        offsetPosition = objectOffset;
		//sets the position that needs to be zoomed into to the position of the object
		theObject = theDestination;
        //we now can zoom in towards that object
        Debug.Log("This is reached");
        moveToObject = true;
		objectZoom = true;
		if (theDestination.GetComponent<ThoughtBubble> () != null) {
			back = theDestination.GetComponent<ThoughtBubble> ().behind;
		}
	}
	/// <summary>
	/// Moves the toward non object.
	/// </summary>
	/// <param name="MousePosition">Mouse position.</param>
	public void moveTowardNonObject(Vector3 MousePosition, bool isBar){
		//sets the position that needs to be zoomed into to the position of raycast hit on the wall with the mouse
		mouseLocationZoom = MousePosition;
		moveToMouse = true;
		bar = isBar;
	}


}
