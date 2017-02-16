using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTextAtLine : MonoBehaviour {

	public TextAsset theText;

	public int startLine;
	public int endLine;

	public TextBoxManager theTextBoxManager;

	public bool destroyWhenActivated, isTranslator;

	public bool requireButtonPress;
	bool waitForPress;

	public bool interactable;

	public GameObject canTalk;
	public bool hasCanTalk;

	static string nameOfHit;

	public MoveCameraDialogue MoveCameraDialogue;

	public Vector3 canTalkDialogueBoxOffset;
	public TranslatorManager theTranslatorManager;

	// Use this for initialization
	void Start () {
		theTranslatorManager = FindObjectOfType<TranslatorManager> ();
		theTextBoxManager = FindObjectOfType<TextBoxManager> ();
		MoveCameraDialogue = FindObjectOfType<MoveCameraDialogue> ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (MoveCameraDialogue.transform);
		if (theTextBoxManager.isTextBoxActive || MoveCameraDialogue.transform.position != MoveCameraDialogue.OriginalCameraPosition) {
			canTalk.SetActive (false);
		} else if(interactable && !theTranslatorManager.panelIsActive){
			hoverOverObject ();
		}
		if (Input.GetKeyDown (KeyCode.Mouse0) && !theTextBoxManager.isTextBoxActive && MoveCameraDialogue.transform.position == MoveCameraDialogue.OriginalCameraPosition && !theTranslatorManager.panelIsActive) {
			//this is some test out shit

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit) ) {
				if (hit.collider.gameObject.name == this.gameObject.name && interactable) {
					//Debug.Log (hit.transform.name);
					//Debug.Log (theText);
					if (isTranslator) {
						theTranslatorManager.startTranslatingJournal ();
					}
					MoveCameraDialogue.moveTowardObject (hit.transform.gameObject);
					theTextBoxManager.reloadScript (hit.transform.gameObject.GetComponent<ActivateTextAtLine> ().theText);
					theTextBoxManager.currentLine = hit.transform.gameObject.GetComponent<ActivateTextAtLine> ().startLine;
					theTextBoxManager.endAtLine = hit.transform.gameObject.GetComponent<ActivateTextAtLine> ().endLine;
					StartCoroutine (waitToDisplayDialogueBox ());
					//theTextBoxManager.enableTextBox ();
				} /*else if (!interactable) {
					
					Debug.Log ("If this shows up, then this is bad");
					
				}*/
			} /*else {
				Debug.Log ("If this shows up, then this is bad");
			}*/


			if (destroyWhenActivated) {
				Destroy (gameObject);
			}
		}


	}

	void hoverOverObject(){
		Ray hover = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitHover;

		if (Physics.Raycast (hover, out hitHover) && hitHover.collider.gameObject.name == this.gameObject.name) {
			if (!hasCanTalk ) {
				canTalk = Instantiate (canTalk, hitHover.transform.position + canTalkDialogueBoxOffset, hitHover.transform.rotation) as GameObject;
				hasCanTalk = true;
			}
			//canTalk.transform.position = transform.position + new Vector3 (0.63f, 1.32f, 0f);
			canTalk.SetActive (true);
		} else {
			canTalk.SetActive (false);
		}
	}

	/*void OnTriggerEnter(Collider other){
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
	}*/

	private IEnumerator waitToDisplayDialogueBox(){
		yield return new WaitForSeconds (0.4f);
		theTextBoxManager.enableTextBox ();
	}
}
