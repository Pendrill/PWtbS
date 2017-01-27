using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTextAtLine : MonoBehaviour {

	public TextAsset theText;

	public int startLine;
	public int endLine;

	public TextBoxManager theTextBoxManager;

	public bool destroyWhenActivated;

	public bool requireButtonPress;
	bool waitForPress;

	public bool interactable;

	// Use this for initialization
	void Start () {
		theTextBoxManager = FindObjectOfType<TextBoxManager> ();	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Mouse0) && !theTextBoxManager.isTextBoxActive) {
			//this is some test out shit 
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				if (interactable) {
					Debug.Log (hit.transform.name);
					Debug.Log (theText);
					theTextBoxManager.reloadScript (hit.transform.gameObject.GetComponent<ActivateTextAtLine> ().theText);
					theTextBoxManager.currentLine = hit.transform.gameObject.GetComponent<ActivateTextAtLine> ().startLine;
					theTextBoxManager.endAtLine = hit.transform.gameObject.GetComponent<ActivateTextAtLine> ().endLine;
					theTextBoxManager.enableTextBox ();
				}
			}


			if (destroyWhenActivated) {
				Destroy (gameObject);
			}
		}


	}

	void OnTriggerEnter(Collider other){
		if (other.name == "Player1") {

			if (requireButtonPress) {
				waitForPress = true;
				return;
			}
			theTextBoxManager.reloadScript (theText);
			theTextBoxManager.currentLine = startLine;
			theTextBoxManager.endAtLine = endLine;
			theTextBoxManager.enableTextBox ();

			if (destroyWhenActivated) {
				Destroy (gameObject);
			}
		}
	}

	void OnTriggerExit(Collider other){
		if (other.name == "Player1") {
			waitForPress = false;
		}
	}
}
