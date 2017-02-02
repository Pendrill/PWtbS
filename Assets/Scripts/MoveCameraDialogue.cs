using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraDialogue : MonoBehaviour {

	public bool moveToObject;
	public GameObject theObject;
	public Vector3 OriginalCameraPosition, offsetPosition;//originalCameraRotation;
	public float time;
	public TextBoxManager theTextBoxManager;



	// Use this for initialization
	void Start () {
		time = 0f;
		moveToObject = false;
		OriginalCameraPosition = transform.position;
		//originalCameraRotation = Camera.main.transform.eulerAngles;
		theTextBoxManager = FindObjectOfType<TextBoxManager> ();
		theObject = this.gameObject;
		offsetPosition = new Vector3 (0, 0, -5);
	}
	
	// Update is called once per frame
	void Update () {
		if (moveToObject) {
			time += Time.deltaTime*2;
			transform.position = Vector3.Lerp (OriginalCameraPosition, theObject.transform.position + offsetPosition, time);
			//transform.LookAt (theObject.transform);
			//Quaternion toRotation = Quaternion.FromToRotation(Vector3.up, transform.forward);
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.identity, time);
			//Camera.main.transform.eulerAngles = Vector3.Lerp (-Camera.main.transform.eulerAngles, originalCameraRotation, time);
			if (!theTextBoxManager.isTextBoxActive && transform.position == theObject.transform.position + offsetPosition) {
				moveToObject = false;
			}
		} else {
			time -= Time.deltaTime*2;
			transform.position = Vector3.Lerp (OriginalCameraPosition, theObject.transform.position + offsetPosition, time);
		}
		if (time > 1f) {
			time = 1f;
		} else if (time < 0f) {
			time = 0f;
		}
	}

	public void moveTowardObject(GameObject theDestination){
		theObject = theDestination;
		moveToObject = true;
	}
}
