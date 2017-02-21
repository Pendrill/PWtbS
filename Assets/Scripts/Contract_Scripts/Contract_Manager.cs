using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Contract_Manager : MonoBehaviour {

	//public Vector3 moveContract, focusContract, returnContract;
	//public float time;
	//public Contract_Selection theContractSelection;
	// Use this for initialization
	public SpriteRenderer panelColor;
	float time;
	Color currentAlpha, destinationAlpha;
	public bool contractGotSelected, switchDecision;
	public string ContractTitle, Objective, Target;
	public Text ContractTXT, ObjectiveTXT, TargetTXT;
	public Button button;
	public Vector3 ContractOP, ObjectiveOP, TargetOP, ContractD, ObjectiveD, TargetD, ButtonOP, ButtonD;
	void Start () {
		//theContractSelection = FindObjectOfType<Contract_Selection> ();
		currentAlpha = panelColor.color;
		destinationAlpha = new Color (1, 1, 1, 0.63f);
		ContractOP = ContractTXT.rectTransform.anchoredPosition3D;
		ObjectiveOP = ObjectiveTXT.rectTransform.anchoredPosition3D;
		TargetOP = TargetTXT.rectTransform.anchoredPosition3D;
		ButtonOP = button.GetComponent<RectTransform> ().anchoredPosition3D;
	}
	
	// Update is called once per frame
	void Update () {
		if (contractGotSelected) {
			time += Time.deltaTime * 2;
			panelColor.color = Color.Lerp (currentAlpha, destinationAlpha, time);
			ContractTXT.text = "Contract: " + ContractTitle;
			ObjectiveTXT.text = "Objective: " + Objective;
			TargetTXT.text = "Target: " + Target;
			ContractTXT.rectTransform.anchoredPosition3D = Vector3.Lerp (ContractOP, ContractD, time);
			ObjectiveTXT.rectTransform.anchoredPosition3D = Vector3.Lerp (ObjectiveOP, ObjectiveD, time);
			TargetTXT.rectTransform.anchoredPosition3D = Vector3.Lerp (TargetOP, TargetD, time);
			button.GetComponent<RectTransform> ().anchoredPosition3D = Vector3.Lerp (ButtonOP, ButtonD, time);

		} else {
			time -= Time.deltaTime * 2;
			panelColor.color = Color.Lerp (currentAlpha, destinationAlpha, time);
			ContractTXT.rectTransform.anchoredPosition3D = Vector3.Lerp (ContractOP, ContractD, time);
			ObjectiveTXT.rectTransform.anchoredPosition3D = Vector3.Lerp (ObjectiveOP, ObjectiveD, time);
			TargetTXT.rectTransform.anchoredPosition3D = Vector3.Lerp (TargetOP, TargetD, time);
			button.GetComponent<RectTransform> ().anchoredPosition3D = Vector3.Lerp (ButtonOP, ButtonD, time);
		}
		if (time > 1f) {
			time = 1f;
		} else if (time < 0f) {
			time = 0f;
		}
	}

	public void setUpSelectedContract (GameObject contract){
		ContractTitle = contract.GetComponent<Contract_Selection> ().ContractTitle;
		Objective = contract.GetComponent<Contract_Selection> ().Objective;
		Target = contract.GetComponent<Contract_Selection> ().Target;

	}
	public void moveUnselectedContractsAwayFromBar(GameObject contract){

	}
	public void ChooseOtherContract(){
		contractGotSelected = false;
		switchDecision = true;

	}
}
