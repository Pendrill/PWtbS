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

	public string ContractTitle, Objective, Target;

	// Use this for initialization
	void Start () {
		//contractGotSelected = true;
		//theTranslatorManager = FindObjectOfType<TranslatorManager> ();
		theContractManager = FindObjectOfType<Contract_Manager> ();
		contractLocationOnBar = gameObject.transform.position;
		movedContract = new Vector3 (13, 1, -0.64f);
		selectedContract = new Vector3 (-2, 1, -6);
	}
	
	// Update is called once per frame
	void Update () {
		hoverOverContract ();
		if (Input.GetKeyDown (KeyCode.Mouse0) ){//&& !theContractManager.contractGotSelected ){//&& !theTranslatorManager.panelIsActive) {
			//we create a ray cast that is emmited forward from the position of the mouse
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			//if there is a hit
			if (Physics.Raycast (ray, out hit)) {
				//Debug.Log ("hit");
				aSelectionHappened = true;
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
