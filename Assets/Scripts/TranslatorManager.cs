using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TranslatorManager : MonoBehaviour {
	public List<string> newScrambledWord = new List<string>();
	public List<string> definitionOffered = new List<string> ();
	public Text wordScambled;
	public Text wordDefined;
	public GameObject translatorPanel;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void enableTranslatorPanel(){
		translatorPanel.SetActive (true);
	}
	public void disableTranslatorPanel(){
		translatorPanel.SetActive (true);
	}
	public void checkIfWordHasAlreadyBeenEncountered (string word){
		for (int i = 0; i < newScrambledWord.Count; i++) {
			if (!newScrambledWord [i].Trim ().Equals (word.Trim ())) {
				newScrambledWord.Add (word);
				//Have the new word encountered here thing
			}
		}
	}
}
