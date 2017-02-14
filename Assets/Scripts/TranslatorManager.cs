using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TranslatorManager : MonoBehaviour {
	public List<string> newScrambledWord = new List<string>();
	public List<string> definitionOffered = new List<string> ();
	public List<bool> wasDefinitionCorrect = new List<bool> ();
	public Text wordScrambled;
	public Text wordDefined;
	public GameObject translatorPanel;
	public int currentPage;
	public bool panelIsActive, doesExist, userIsTyping;
	public string userDefinition;
	// Use this for initialization
	void Start () {
		//newScrambledWord.Add ("test");
		if (panelIsActive) {
			enableTranslatorPanel ();
		} else {
			disableTranslatorPanel ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.I) && !panelIsActive) {
			enableTranslatorPanel ();
		} else if (Input.GetKeyDown (KeyCode.I) && panelIsActive && !userIsTyping) {
			disableTranslatorPanel ();
			currentPage = 0;
		}
		if (panelIsActive) {
			wordScrambled.text = newScrambledWord [currentPage];
			wordDefined.text = definitionOffered [currentPage];
			if (Input.GetKeyDown (KeyCode.Return) && !userIsTyping) {
				userIsTyping = true;
			} else if (Input.GetKeyDown (KeyCode.Return) && userIsTyping) {
				//lockItDown
				userIsTyping = false;
				userDefinition = "";
			}
			if (userIsTyping) {
				typeTranslation ();

			}
		}

	}

	public void enableTranslatorPanel(){
		panelIsActive = true;
		translatorPanel.SetActive (true);
	}
	public void disableTranslatorPanel(){
		panelIsActive = false;
		translatorPanel.SetActive (false);
	}
	public void checkIfWordHasAlreadyBeenEncountered (string word){
		if (newScrambledWord.Count.Equals(0)) {
			newScrambledWord.Add (word);
			definitionOffered.Add ("");
			wasDefinitionCorrect.Add (false);
		}
		for (int i = 0; i < newScrambledWord.Count; i++) {
			if (newScrambledWord [i].Trim ().Equals (word.Trim ())) {
				doesExist = true;
				break;
				//newScrambledWord.Add (word);
				//Have the new word encountered here thing
			}
		}
		if (!doesExist) {
			newScrambledWord.Add (word);
			definitionOffered.Add ("");
			wasDefinitionCorrect.Add (false);
		} else {
			doesExist = false;
		}
	}
	public void nextPage(){
		if (currentPage < newScrambledWord.Count - 1) {
			currentPage += 1;
			userIsTyping = false;
			userDefinition = "";
		} else {
			currentPage = 0;
			userIsTyping = false;
			userDefinition = "";
		}
	}
	public void previousPage(){
		if (currentPage > 0) {
			currentPage -= 1;
			userIsTyping = false;
			userDefinition = "";
		} else {
			currentPage = newScrambledWord.Count - 1;
			userIsTyping = false;
			userDefinition = "";
		}
	}

	public void typeTranslation(){

		if (Input.GetKeyDown (KeyCode.A)) {
			userDefinition += 'a';
			definitionOffered.RemoveAt (currentPage);
			definitionOffered.Insert (currentPage, userDefinition);
		} else if (Input.GetKeyDown (KeyCode.B)) {
			userDefinition += 'b';
			definitionOffered.RemoveAt (currentPage);
			definitionOffered.Insert (currentPage, userDefinition);
		} else if (Input.GetKeyDown (KeyCode.C)) {
			userDefinition += 'c';
			definitionOffered.RemoveAt (currentPage);
			definitionOffered.Insert (currentPage, userDefinition);
		} else if (Input.GetKeyDown (KeyCode.D)) {
			userDefinition += 'd';
			definitionOffered.RemoveAt (currentPage);
			definitionOffered.Insert (currentPage, userDefinition);
		} else if (Input.GetKeyDown (KeyCode.E)) {
			userDefinition += 'e';
			definitionOffered.RemoveAt (currentPage);
			definitionOffered.Insert (currentPage, userDefinition);
		} else if (Input.GetKeyDown (KeyCode.F)) {
			userDefinition += 'f';
			definitionOffered.RemoveAt (currentPage);
			definitionOffered.Insert (currentPage, userDefinition);
		} else if (Input.GetKeyDown (KeyCode.G)) {
			userDefinition += 'g';
			definitionOffered.RemoveAt (currentPage);
			definitionOffered.Insert (currentPage, userDefinition);
		} else if (Input.GetKeyDown (KeyCode.H)) {
			userDefinition += 'h';
			definitionOffered.RemoveAt (currentPage);
			definitionOffered.Insert (currentPage, userDefinition);
		} else if (Input.GetKeyDown (KeyCode.I)) {
			userDefinition += 'i';
			definitionOffered.RemoveAt (currentPage);
			definitionOffered.Insert (currentPage, userDefinition);
		} else if (Input.GetKeyDown (KeyCode.J)) {
			userDefinition += 'j';
			definitionOffered.RemoveAt (currentPage);
			definitionOffered.Insert (currentPage, userDefinition);
		} else if (Input.GetKeyDown (KeyCode.K)) {
			userDefinition += 'q';
			definitionOffered.RemoveAt (currentPage);
			definitionOffered.Insert (currentPage, userDefinition);
		} else if (Input.GetKeyDown (KeyCode.L)) {
			userDefinition += 'l';
			definitionOffered.RemoveAt (currentPage);
			definitionOffered.Insert (currentPage, userDefinition);
		} else if (Input.GetKeyDown (KeyCode.M)) {
			userDefinition += 'm';
			definitionOffered.RemoveAt (currentPage);
			definitionOffered.Insert (currentPage, userDefinition);
		} else if (Input.GetKeyDown (KeyCode.N)) {
			userDefinition += 'n';
			definitionOffered.RemoveAt (currentPage);
			definitionOffered.Insert (currentPage, userDefinition);
		} else if (Input.GetKeyDown (KeyCode.O)) {
			userDefinition += 'o';
			definitionOffered.RemoveAt (currentPage);
			definitionOffered.Insert (currentPage, userDefinition);
		} else if (Input.GetKeyDown (KeyCode.P)) {
			userDefinition += 'p';
			definitionOffered.RemoveAt (currentPage);
			definitionOffered.Insert (currentPage, userDefinition);
		} else if (Input.GetKeyDown (KeyCode.Q)) {
			userDefinition += 'q';
			definitionOffered.RemoveAt (currentPage);
			definitionOffered.Insert (currentPage, userDefinition);
		} else if (Input.GetKeyDown (KeyCode.R)) {
			userDefinition += 'r';
			definitionOffered.RemoveAt (currentPage);
			definitionOffered.Insert (currentPage, userDefinition);
		} else if (Input.GetKeyDown (KeyCode.S)) {
			userDefinition += 's';
			definitionOffered.RemoveAt (currentPage);
			definitionOffered.Insert (currentPage, userDefinition);
		} else if (Input.GetKeyDown (KeyCode.T)) {
			userDefinition += 't';
			definitionOffered.RemoveAt (currentPage);
			definitionOffered.Insert (currentPage, userDefinition);
		} else if (Input.GetKeyDown (KeyCode.U)) {
			userDefinition += 'u';
			definitionOffered.RemoveAt (currentPage);
			definitionOffered.Insert (currentPage, userDefinition);
		} else if (Input.GetKeyDown (KeyCode.V)) {
			userDefinition += 'v';
			definitionOffered.RemoveAt (currentPage);
			definitionOffered.Insert (currentPage, userDefinition);
		} else if (Input.GetKeyDown (KeyCode.W)) {
			userDefinition += 'w';
			definitionOffered.RemoveAt (currentPage);
			definitionOffered.Insert (currentPage, userDefinition);
		} else if (Input.GetKeyDown (KeyCode.X)) {
			userDefinition += 'x';
			definitionOffered.RemoveAt (currentPage);
			definitionOffered.Insert (currentPage, userDefinition);
		} else if (Input.GetKeyDown (KeyCode.Y)) {
			userDefinition += 'y';
			definitionOffered.RemoveAt (currentPage);
			definitionOffered.Insert (currentPage, userDefinition);
		} else if (Input.GetKeyDown (KeyCode.Z)) {
			userDefinition += 'z';
			definitionOffered.RemoveAt (currentPage);
			definitionOffered.Insert (currentPage, userDefinition);
		} else if (Input.GetKeyDown (KeyCode.Backspace)) {
			userDefinition = userDefinition.Substring (0, userDefinition.Length - 1);
			definitionOffered.RemoveAt (currentPage);
			definitionOffered.Insert (currentPage, userDefinition);
		}

	}
}
