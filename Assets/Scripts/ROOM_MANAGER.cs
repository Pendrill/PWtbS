using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ROOM_MANAGER : MonoBehaviour {
	public TranslatorManager theTranslatorManager;
	public TextBoxManager theTextBoxManager;
	public MoveCameraDialogue MoveCameraDialogue;
	public TextAsset theText, chargerAndNotebook, notebook, charger;
	public int startLine, endLine, startLineNC, endLineNC,startLineC, endLineC,startLineN, endLineN;
	public static bool complete;
    public Vector3 specificOffset;
    // Use this for initialization
    void Start () {
		theTranslatorManager = FindObjectOfType<TranslatorManager> ();
		theTextBoxManager = FindObjectOfType<TextBoxManager> ();
		MoveCameraDialogue = FindObjectOfType<MoveCameraDialogue> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Mouse0) && !theTextBoxManager.isTextBoxActive && MoveCameraDialogue.transform.position == MoveCameraDialogue.OriginalCameraPosition && !theTranslatorManager.panelIsActive) {

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			//if there is a hit
			if (Physics.Raycast (ray, out hit)) {
				//we only want to run this for the specific object that got hit (as this script will be attached to many objects)
				if (hit.collider.gameObject.name == this.gameObject.name) {
					if (ActivateTextAtLine.notebook && ActivateTextAtLine.charger) {
						MoveCameraDialogue.moveTowardObject (hit.transform.gameObject,specificOffset);
						//and then we need to update the dialogue text, start, and end line
						theTextBoxManager.reloadScript (hit.transform.gameObject.GetComponent<ROOM_MANAGER> ().chargerAndNotebook);
						theTextBoxManager.currentLine = hit.transform.gameObject.GetComponent<ROOM_MANAGER> ().startLineNC;
						theTextBoxManager.endAtLine = hit.transform.gameObject.GetComponent<ROOM_MANAGER> ().endLineNC;
						//Finally we have a coroutine that starts so as to wait that the camera has zoomed in
						complete =true;
						StartCoroutine (waitToDisplayDialogueBox ());
					} else if (ActivateTextAtLine.notebook) {
						MoveCameraDialogue.moveTowardObject (hit.transform.gameObject, specificOffset);
						//and then we need to update the dialogue text, start, and end line
						theTextBoxManager.reloadScript (hit.transform.gameObject.GetComponent<ROOM_MANAGER> ().notebook);
						theTextBoxManager.currentLine = hit.transform.gameObject.GetComponent<ROOM_MANAGER> ().startLineN;
						theTextBoxManager.endAtLine = hit.transform.gameObject.GetComponent<ROOM_MANAGER> ().endLineN;
						complete =true;
						StartCoroutine (waitToDisplayDialogueBox ());
					} else if (ActivateTextAtLine.charger) {
						MoveCameraDialogue.moveTowardObject (hit.transform.gameObject, specificOffset);
						//and then we need to update the dialogue text, start, and end line
						theTextBoxManager.reloadScript (hit.transform.gameObject.GetComponent<ROOM_MANAGER> ().charger);
						theTextBoxManager.currentLine = hit.transform.gameObject.GetComponent<ROOM_MANAGER> ().startLineC;
						theTextBoxManager.endAtLine = hit.transform.gameObject.GetComponent<ROOM_MANAGER> ().endLineC;
						complete =true;
						StartCoroutine (waitToDisplayDialogueBox ());
					} else {
						MoveCameraDialogue.moveTowardObject (hit.transform.gameObject,specificOffset);
						//and then we need to update the dialogue text, start, and end line
						theTextBoxManager.reloadScript (hit.transform.gameObject.GetComponent<ROOM_MANAGER> ().theText);
						theTextBoxManager.currentLine = hit.transform.gameObject.GetComponent<ROOM_MANAGER> ().startLine;
						theTextBoxManager.endAtLine = hit.transform.gameObject.GetComponent<ROOM_MANAGER> ().endLine;
						//Finally we have a coroutine that starts so as to wait that the camera has zoomed in
						StartCoroutine (waitToDisplayDialogueBox ());
					}
				}
			}
		}
	}
	private IEnumerator waitToDisplayDialogueBox(){
		yield return new WaitForSeconds (0.4f);
		theTextBoxManager.enableTextBox ();
		//if (destroyWhenActivated) {
		//	disableObject = true;
		//}
	}
}
