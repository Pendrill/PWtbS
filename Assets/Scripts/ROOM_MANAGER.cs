using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ROOM_MANAGER : MonoBehaviour {
	public TranslatorManager theTranslatorManager;
	public TextBoxManager theTextBoxManager;
	public MoveCameraDialogueDO theMoveCameraDialogueDo;
	public TextAsset theText, chargerAndNotebook, notebook, charger;
	public int startLine, endLine, startLineNC, endLineNC,startLineC, endLineC,startLineN, endLineN;
	public static bool complete;
    public Vector3 specificOffset;
	public DropOff theDropOff;
	//public objectPickupManager theObjectPickupManager;
    // Use this for initialization
    void Start () {
		theTranslatorManager = FindObjectOfType<TranslatorManager> ();
		theTextBoxManager = FindObjectOfType<TextBoxManager> ();
		theMoveCameraDialogueDo = FindObjectOfType<MoveCameraDialogueDO> ();
		theDropOff = FindObjectOfType<DropOff> ();
		//theObjectPickupManager = FindObjectOfType<objectPickupManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Mouse0) && !theTextBoxManager.isTextBoxActive && theMoveCameraDialogueDo.transform.position == theMoveCameraDialogueDo.OriginalCameraPosition && !theTranslatorManager.panelIsActive) {

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			//if there is a hit
			if (Physics.Raycast (ray, out hit)) {
				//we only want to run this for the specific object that got hit (as this script will be attached to many objects)
				if (hit.collider.gameObject.name == this.gameObject.name) {
					if (objectPickupManager.notebook && objectPickupManager.charger) {
						theMoveCameraDialogueDo.moveTowardObject (hit.transform.gameObject,specificOffset);
						//and then we need to update the dialogue text, start, and end line
						theDropOff.reloadScript (hit.transform.gameObject.GetComponent<ROOM_MANAGER> ().chargerAndNotebook);
						theDropOff.currentLine = hit.transform.gameObject.GetComponent<ROOM_MANAGER> ().startLineNC;
						theDropOff.endAtLine = hit.transform.gameObject.GetComponent<ROOM_MANAGER> ().endLineNC;
						//Finally we have a coroutine that starts so as to wait that the camera has zoomed in
						complete =true;
						StartCoroutine (waitToDisplayDialogueBox ());
					} else if (objectPickupManager.notebook) {
						theMoveCameraDialogueDo.moveTowardObject (hit.transform.gameObject, specificOffset);
						//and then we need to update the dialogue text, start, and end line
						theDropOff.reloadScript (hit.transform.gameObject.GetComponent<ROOM_MANAGER> ().notebook);
						theDropOff.currentLine = hit.transform.gameObject.GetComponent<ROOM_MANAGER> ().startLineN;
						theDropOff.endAtLine = hit.transform.gameObject.GetComponent<ROOM_MANAGER> ().endLineN;
						complete =true;
						StartCoroutine (waitToDisplayDialogueBox ());
					} else if (objectPickupManager.charger) {
						theMoveCameraDialogueDo.moveTowardObject (hit.transform.gameObject, specificOffset);
						//and then we need to update the dialogue text, start, and end line
						theDropOff.reloadScript (hit.transform.gameObject.GetComponent<ROOM_MANAGER> ().charger);
						theDropOff.currentLine = hit.transform.gameObject.GetComponent<ROOM_MANAGER> ().startLineC;
						theDropOff.endAtLine = hit.transform.gameObject.GetComponent<ROOM_MANAGER> ().endLineC;
						complete =true;
						StartCoroutine (waitToDisplayDialogueBox ());
					} else {
						theMoveCameraDialogueDo.moveTowardObject (hit.transform.gameObject,specificOffset);
						//and then we need to update the dialogue text, start, and end line
						theDropOff.reloadScript (hit.transform.gameObject.GetComponent<ROOM_MANAGER> ().theText);
						theDropOff.currentLine = hit.transform.gameObject.GetComponent<ROOM_MANAGER> ().startLine;
						theDropOff.endAtLine = hit.transform.gameObject.GetComponent<ROOM_MANAGER> ().endLine;
						//Finally we have a coroutine that starts so as to wait that the camera has zoomed in
						StartCoroutine (waitToDisplayDialogueBox ());
					}
				}
			}
		}
	}
	private IEnumerator waitToDisplayDialogueBox(){
		yield return new WaitForSeconds (0.2f);
		theDropOff.enableTextBox ();
		//if (destroyWhenActivated) {
		//	disableObject = true;
		//}
	}
}
