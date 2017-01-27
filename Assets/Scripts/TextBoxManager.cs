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
	public bool isTextBoxActive;

	public MoveLeftRight playerMovement;
	public bool stopPlayerMovement;

	bool isTyping, cancelTyping;
	public float typeSpeed;

	//we could include a way for the player to stop moving when dialogue pops up

	// Use this for initialization
	void Start () {

		isTyping = false;
		cancelTyping = false;

		if (textFile != null) {
			textLines = (textFile.text.Split('\n'));
		}

		if (endAtLine == 0) {
			endAtLine = textLines.Length - 1;
		}

		if (isTextBoxActive) {
			enableTextBox ();
		} else {
			disableTextBox ();
		}
	}

	// Update is called once per frame
	void Update () {
		
		if (!isTextBoxActive) {
			return;
		}

		//theText.text = textLines [currentLine];

		if(Input.GetKeyDown(KeyCode.K)){//KeyCode.Mouse0)){
			if (!isTyping) {
				currentLine += 1;
				if (currentLine > endAtLine) {
					disableTextBox ();
					currentLine = 0;
				} else {
					StartCoroutine (TextScroll (textLines [currentLine]));
				}
			} else if(isTyping && !cancelTyping) {
				cancelTyping = true;
			}
		}

	}

	private IEnumerator TextScroll(string lineOfText){
		int letter = 0;
		theText.text = "";
		isTyping = true;
		cancelTyping = false;

		while (isTyping && !cancelTyping && letter < lineOfText.Length - 1) {
			theText.text += lineOfText [letter];
			letter += 1;
			yield return new WaitForSeconds (typeSpeed);
		}
		theText.text = lineOfText;
		isTyping = false;
		cancelTyping = false;
	}

	public void enableTextBox(){
		textBox.SetActive (true);
		isTextBoxActive = true;
		//if (stopPlayerMovement) {
		playerMovement.canMove = false;
		//}
		StartCoroutine (TextScroll (textLines [currentLine]));
	}

	public void disableTextBox (){
		textBox.SetActive (false);
		isTextBoxActive = false;
		playerMovement.canMove = true;
	}

	public void reloadScript(TextAsset newText){
		if (theText != null) {
			textLines = new string[1]; 
			textLines = (newText.text.Split('\n'));
		}
	}
}
