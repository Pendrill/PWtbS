using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour {

	public GameObject textBox;
	public Text theText;

	//this is the block of text
	public TextAsset textFile;
	public string[] textLines;

	public int currentLine;
	public int endAtLine;

	//we could include a way for the player to stop moving when dialogue pops up

	// Use this for initialization
	void Start () {
		if (textFile != null) {
			textLines = (textFile.text.Split('\n'));
		}

		if (endAtLine == 0) {
			endAtLine = textLines.Length - 1;
		}
	}

	// Update is called once per frame
	void Update () {
		theText.text = textLines [currentLine];
		if(Input.GetKeyDown(KeyCode.Mouse0)){
			currentLine += 1;
		}
		if (currentLine > endAtLine) {
			textBox.SetActive (false);
			currentLine = 0;
		}
	}
}
