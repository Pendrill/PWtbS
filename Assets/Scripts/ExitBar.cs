using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ExitBar : MonoBehaviour {

	public Contract_Manager theContractManager;
	public TextBoxManager theTextBoxManager;
	public MoveCameraDialogue theMoveCameraDialogue;
	public TranslatorManager theTranslatorManager;
	// Use this for initialization
	void Start () {
		theContractManager = FindObjectOfType<Contract_Manager>();
		theTextBoxManager = FindObjectOfType<TextBoxManager> ();
		theMoveCameraDialogue = FindObjectOfType<MoveCameraDialogue> ();
		theTranslatorManager = FindObjectOfType<TranslatorManager> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Mouse0) && !theTextBoxManager.isTextBoxActive && theMoveCameraDialogue.transform.position == theMoveCameraDialogue.OriginalCameraPosition && !theTranslatorManager.panelIsActive) {

			//we send out a raycast from the mouse position
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			//if it hits and it is for this specific object
			if (Physics.Raycast (ray, out hit) && hit.collider.gameObject.name == this.gameObject.name && QuestManager.currentContract != 0) {
				Debug.Log ("Clicked on the door");
				SceneManager.LoadScene ("MainMap");
			}
		}
				
	}
}
