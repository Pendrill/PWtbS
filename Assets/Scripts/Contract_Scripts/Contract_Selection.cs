using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contract_Selection : MonoBehaviour {

	public TranslatorManager theTranslatorManager;
	public Contract_Manager theContractManager;
	public bool contractGotSelected, aSelectionHappened;
	public Vector3 contractLocationOnBar, movedContract, selectedContract;
	public float time;
	public GameObject outline;
	public MoveCameraDialogue theMoveCameraDialogue;
	public int Set, Number;

	public string ContractTitle, Objective, Target;

	// Use this for initialization
	void Start () {
		//contractGotSelected = true;
		//theTranslatorManager = FindObjectOfType<TranslatorManager> ();
		theContractManager = FindObjectOfType<Contract_Manager> ();
		theMoveCameraDialogue = FindObjectOfType<MoveCameraDialogue> ();
		contractLocationOnBar = gameObject.transform.position;
		movedContract = new Vector3 (-5.54f, 3.97f, -26.41f);
		selectedContract = new Vector3 (-2.11f, 4.57f, -26.06f);
	}
	
	// Update is called once per frame
	void Update () {
		hoverOverContract ();
		if (Input.GetKeyDown (KeyCode.Mouse0) && theMoveCameraDialogue.moveToMouse ){//&& !theContractManager.contractGotSelected ){//&& !theTranslatorManager.panelIsActive) {
			//we create a ray cast that is emmited forward from the position of the mouse
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			//if there is a hit
			if (Physics.Raycast (ray, out hit)) {
				//Debug.Log ("hit");
				aSelectionHappened = true;
				Debug.Log (hit.collider.gameObject.name);
				//we only want to run this for the specific object that got hit (as this script will be attached to many objects)
				if (hit.collider.gameObject.name == this.gameObject.name) {
					theContractManager.contractGotSelected = true;
					theContractManager.setUpSelectedContract (hit.collider.gameObject);
					contractGotSelected = true;
					//aSelectionHappened = true;
				} else {
					contractGotSelected = false;

				}
			}

		}
		if (!contractGotSelected && aSelectionHappened) {
			time += Time.deltaTime * 2;
			transform.position = Vector3.Lerp (contractLocationOnBar, movedContract, time);
			//theContractManager.moveUnselectedContractsAwayFromBar (this.gameObject);
		} else if (contractGotSelected && aSelectionHappened) {
			time += Time.deltaTime * 2;
			transform.position = Vector3.Lerp (contractLocationOnBar, selectedContract, time);
		} 
		if (theContractManager.switchDecision) {
			aSelectionHappened = false;
			time -= Time.deltaTime * 2;
			if (contractGotSelected) {
				transform.position = Vector3.Lerp (contractLocationOnBar, selectedContract, time);
				if (transform.position == contractLocationOnBar) {
					contractGotSelected = false;
					theContractManager.switchDecision = false;
				}
			} else {
				transform.position = Vector3.Lerp (contractLocationOnBar, movedContract, time);
			}

		}
		if (time > 1f) {
			time = 1f;
		} else if (time < 0f) {
			time = 0f;
		}

	}

	public void hoverOverContract(){
		if (contractGotSelected) {
			outline.SetActive (false);
			return;
		}
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) {
			//we only want to run this for the specific object that got hit (as this script will be attached to many objects)
			if (hit.collider.gameObject.name == this.gameObject.name) {
				outline.SetActive (true);
			} else {
				outline.SetActive (false);
			}
		} else {
			outline.SetActive (false);
		}
	}
}
