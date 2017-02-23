using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour {

	//This is a reference to the actual panel gameobject for the dialogue
	public GameObject textBox;
	//refrence to the text object that will display dialogue 
	public Text theText;

	//reference to the textfile that will contain the dialogue spoken
	public TextAsset textFile;
	//reference to the array contianing each line of dialogue; and array containing each word of dialogue
	public string[] textLines, individualWord;

	//keep track of the currentline of dialogue we are at
	public int currentLine;
	//checks what the last line of dialogue that needs to be displayed.
	public int endAtLine;
	//checks whether the dialogue box is active or not
	public bool isTextBoxActive;

	//reference to the scrpt that enables the player to move left and right (OBSELETE NOW)
	//public MoveLeftRight playerMovement;
	//boolean that will disable the player's movement if certain conditions are met (OBSELETE NOW)
	//public bool stopPlayerMovement;

	//Checks if letters are still appearing on screen during dialogue (as opposed to already having them all displayed)
	//checks if the player has clicked and needs the whole line of text to be displayed.
	bool isTyping, cancelTyping;
	public float typeSpeed;

	//checks how much time is left in relation to the coroutine for the zoom in and out of the camera
	public float time_left;

	//reference to the game manager script and the object it is attached to;
	public gameManager theGameManager;
	//string that keeps track of the updated line of dialogue that needs to be displayed on screen
	public string updatedLineOfText;
	public TranslatorManager theTranslatorManager;

	//we could include a way for the player to stop moving when dialogue pops up (DONE)

	// Use this for initialization
	void Start () {

		//get the game manager object
		theGameManager = FindObjectOfType<gameManager> ();
		theTranslatorManager = FindObjectOfType<TranslatorManager> ();

		//make sure that the istyping and canceltyping are both set to false, as nothing should be displaying on the screen when the scene first runs
		isTyping = false;
		cancelTyping = false;

		//make sure that a textfile with the dialogue has been inputed
		if (textFile != null) {
			//so that we can then split the dialogue from the textfile into the specific array
			textLines = (textFile.text.Split('\n'));
		}

		//if no end at line has been specified then it should be the last line of the textfile.
		if (endAtLine == 0) {
			endAtLine = textLines.Length - 1;
		}

		//if the textbox bool is set to active then it should be enable at the beginning of the scene. Otherwise no.
		if (isTextBoxActive) {
			enableTextBox ();
		} else {
			disableTextBox ();
		}
	}

	// Update is called once per frame
	void Update () {

		//if the dialogue box is not acitve then there is no point in having the text box running.
		if (!isTextBoxActive) {
			return;
		} else {
			//Considering that the button to click through text and activate text, there needs to be a slight delay just so that both are not activated at the same time
			time_left -= Time.deltaTime;
		}

		//theText.text = textLines [currentLine];

		//Checks if the player clicked the mouse
		if(Input.GetKeyDown(KeyCode.Mouse0) && !theTranslatorManager.panelIsActive){
			//checks that all the letters of the specific dialogue line have been displayed
			if (!isTyping) {
				//if yes then we move on to the next line
				currentLine += 1;
				//we check if we have passed the final line of dialogue
				if (currentLine > endAtLine) {
					//if so we call the disable text box function and we reset the current line variable
					disableTextBox ();
					currentLine = 0;
				} else {
					//if not, then we take the current line and split it as to have each word have its own index within the array
					individualWord = textLines[currentLine].Split (' ');
					//we then use a for loop that will go through each word within the array
					for (int i = 0; i < individualWord.Length; i++) {
						//we want to check wether or not we need to scramble the words that appear in the dialogue
						//we call on the check if scramble function in the game manager script and then add the returned word to the updatedLinneOfText
						updatedLineOfText += theGameManager.checkIfScramble (individualWord [i]) + "  ";
					}
					//Once that is done, we start the coroutine that will display the updated line of text one letter at a time.
					StartCoroutine (TextScroll (updatedLineOfText));//textLines [currentLine]));
				}
			//If the user clicks but the letters are still being displayed then we need to cancel the typing as to show the full line of dialogue immediately
			} else if(isTyping && !cancelTyping && time_left < 0) {
				cancelTyping = true;
			}
		}

	}

	/// <summary>
	/// This is a coroutine function that will enable us to display the line of dialogue on line at a time.
	/// </summary>
	/// <returns>The scroll.</returns>
	/// <param name="lineOfText"> this is the specific line that needs to be displayed on the screen .</param>
	private IEnumerator TextScroll(string lineOfText){
		//we reset the int that keeps track of the number of letters
		int letter = 0;
		//we reset the text that will be displayed on the screen as dialogue
		theText.text = "";
		//when this coroutine is happening than, we are currently typing letters on the screen
		isTyping = true;
		//we reset the cancel typing bool to false
		cancelTyping = false;

		//we have a while loop that will display the line of text one letter at a time
		while (isTyping && !cancelTyping && letter < lineOfText.Length - 1) {
			//we add one letter to the text object
			theText.text += lineOfText [letter];
			//we move on to the next letter
			letter += 1;
			//we then return and wait a number of seconds before displaying the nest letter
			yield return new WaitForSeconds (typeSpeed);
		}
		//once all the letters have been displayed or if the user cancelled the typing, we diplay the whole line of dialogue
		theText.text = lineOfText;
		//we are no longer typing
		isTyping = false;
		//there is no longer a need to cancel the typing
		cancelTyping = false;
		//we reset the individual word array for the next line of dialogue
		individualWord = new string[1];
		//we do the same for the updated line of text
		updatedLineOfText = "";
	}

	/// <summary>
	/// Enables the text box specific for the dialogue.
	/// </summary>
	public void enableTextBox(){
		//we set the text box to active
		textBox.SetActive (true);
		//the text box is thus currently active
		isTextBoxActive = true;
		//if (stopPlayerMovement) {
		//playerMovement.canMove = false;
		//}

		//we represt the same process as above to check if the words witin the line of dialogue need to be scrambled, and then updated the line of text accordingly 
		individualWord = textLines[currentLine].Split (' ');
		for (int i = 0; i < individualWord.Length; i++) {
			updatedLineOfText += theGameManager.checkIfScramble (individualWord [i]) + "  ";
		}
		//we then start the couroutine that will display the sentences of dialogue one letter at a time.
		StartCoroutine (TextScroll (updatedLineOfText));//textLines [currentLine]));
	}

	/// <summary>
	/// Disables the text box specific to the dialogue.
	/// </summary>
	public void disableTextBox (){
		//we set the text box to unactive
		textBox.SetActive (false);
		//thus the text box is no longer active
		isTextBoxActive = false;
		//playerMovement.canMove = true;\
		//we reset the time left
		time_left = 0.2f;
	}

	/// <summary>
	/// Reloads the script. Each interactable object will have its own personalized dialogue. This function will make sure to display that dialogue
	/// </summary>
	/// <param name="newText">New text.</param>
	public void reloadScript(TextAsset newText){
		//makes sure there is a text file that can be parsed
		if (theText != null) {
			//reset the array that will contain each line of dialogue
			textLines = new string[1]; 
			//have the text file be split by line into the newly reset array.
			textLines = (newText.text.Split('\n'));
		}
	}
}
