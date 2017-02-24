using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TranslatorManager : MonoBehaviour {

	//create a set of three lists to keep track of any new encountered scrambled words, the user definition, and whether the word was correctly translated
	public List<string> newScrambledWord = new List<string>();
	public List<string> definitionOffered = new List<string> ();
	public List<bool> wasDefinitionCorrect = new List<bool> ();

	//we set a text object for the scrambled word, page number, and definition offered
	public Text wordScrambled, pageNumber;
	public Text wordDefined;

	//we have a game object for the translator journal panel
	public GameObject translatorPanel;

	//we have a set of ints to keep track of the total number of pages and the current number of pages
	public int currentPage, totalPage, translationIndex;

	//we have a set of bools to keep track of if the translator panel is active, if the user is typing in a translation, and if the specific word has already been encountered
	public bool panelIsActive, doesExist, userIsTyping;

	//we have  a string that keeps track of the definition offered by the user for a specific word
	public string userDefinition;

	//we have the gameManager object and script
	public gameManager theGameManager;

	public GameObject typingBar;
	public bool typer;
	public Vector3 typerPos, typerOriginalPos;

    //public GameObject journalButton;
    //protected Button journalBtn;

	// Use this for initialization
	void Start () {
		//we get the game manager from the scene
		theGameManager = FindObjectOfType<gameManager> ();
		//newScrambledWord.Add ("test");
		//if the panel needs to be active at the beginning of the scene then we activate it. Otherwise we deactivate it.
		if (panelIsActive) {
			enableTranslatorPanel ();
		} else {
			disableTranslatorPanel ();
		}
		typerPos = typingBar.GetComponent<RectTransform> ().anchoredPosition3D;
		typerOriginalPos = typingBar.GetComponent<RectTransform> ().anchoredPosition3D;
    
	}

    public void journalBtnClicked()
    {
        if (!panelIsActive)
        {
            enableTranslatorPanel();
        }else if(panelIsActive && !userIsTyping)
        {
            disableTranslatorPanel();
            typingBar.GetComponent<RectTransform>().anchoredPosition3D = typerOriginalPos;
            typingBar.SetActive(false);
            //we reset the current page to 0
            currentPage = 0;
        }
    
    }
	
	// Update is called once per frame
	void Update () {
		//if the user presses I and the panel is not active then we activate the panel
		if (Input.GetMouseButtonDown(2) && !panelIsActive) {
			enableTranslatorPanel ();
		//if the player presses I and the panel is active and the user is not typing in a definition, then we disable the translator panel
		} else if (Input.GetMouseButtonDown(2) && panelIsActive && !userIsTyping) {
			disableTranslatorPanel ();
			typingBar.GetComponent<RectTransform> ().anchoredPosition3D = typerOriginalPos;
			typingBar.SetActive (false);
			//we reset the current page to 0
			currentPage = 0;
		}
		//if the panel is active and the user has encountered at least one unknown word
		if (panelIsActive && newScrambledWord.Count > 0) {
			//we display the current page in relation to the total page
			pageNumber.text = (currentPage + 1) + "/" + totalPage;
			//we display the scrambled word based on the page number
			wordScrambled.text = newScrambledWord [currentPage];
			//we also display the possible definition offered based on the page number
			wordDefined.text = definitionOffered [currentPage];
			//if the user presses enter and they are not currently typing, and the word they are on was not already correctly translated
			if (Input.GetKeyDown (KeyCode.Return) && !userIsTyping && !wasDefinitionCorrect[currentPage]) {
				//then they should now be able to type
				userIsTyping = true;
			//if they press enter and they were typing 
			} else if ((Input.GetKeyDown (KeyCode.Return) || Input.GetMouseButtonDown(2)) && userIsTyping) {
				//then we no longer let the user type
				userIsTyping = false;
				//we reset the user definition
				userDefinition = "";
				typingBar.GetComponent<RectTransform> ().anchoredPosition3D = typerOriginalPos;
				typingBar.SetActive (false);
			}
			//if the user is typing then 
			if (userIsTyping) {
				//we go the the type translation function
				typeTranslation ();

			}
			//if the definition offered is correct 
			if (wasDefinitionCorrect [currentPage]) {
				//then we change the color of the word to green
				wordDefined.color = Color.green;
			//otherwise it remains white
			} else {
				wordDefined.color = Color.white;
			}

		}
		if (Input.GetAxis ("Mouse ScrollWheel") > 0f) {
			nextPage ();
		}
		if (Input.GetAxis ("Mouse ScrollWheel") < 0f) {
			previousPage ();
		}

	}

	/// <summary>
	/// Enables the translator panel.
	/// </summary>
	public void enableTranslatorPanel(){
		//we set the total page the the number of current scrambled words found
		totalPage = newScrambledWord.Count;
		// we set the bool to true as the panel is here
		panelIsActive = true;
		//and then turn the panel on
		translatorPanel.SetActive (true);
	}
	/// <summary>
	/// Disables the translator panel.
	/// </summary>
	public void disableTranslatorPanel(){
		//we turn the panel off
		panelIsActive = false;
		translatorPanel.SetActive (false);
	}
	/// <summary>
	/// Checks if word has already been encountered.
	/// </summary>
	/// <param name="word">Word.</param>
	public void checkIfWordHasAlreadyBeenEncountered (string word){
		//if no scrambled word have been encounterde yet
		if (newScrambledWord.Count.Equals(0)) {
			//then we add this new word directly
			newScrambledWord.Add (word);
			definitionOffered.Add ("");
			wasDefinitionCorrect.Add (false);
		}
		//thus if there already at least ine word in the list, we use a for loop to go through the list
		for (int i = 0; i < newScrambledWord.Count; i++) {
			//if the word already is in the list
			if (newScrambledWord [i].Trim ().Equals (word.Trim ())) {
				//then we won't want to add it again
				doesExist = true;
				break;
				//newScrambledWord.Add (word);
				//Have the new word encountered here thing
			}
		}
		//if it wasn't in the list
		if (!doesExist) {
			//then we add the word
			newScrambledWord.Add (word);
			definitionOffered.Add ("");
			wasDefinitionCorrect.Add (false);
		} else {
			doesExist = false;
		}
	}

	/// <summary>
	/// We move to the next page in the translator journal.
	/// </summary>
	public void nextPage(){
		typingBar.GetComponent<RectTransform> ().anchoredPosition3D = typerOriginalPos;
		typingBar.SetActive (false);
		//we make sure the the current page value does not go over the total number of pages
		if (currentPage < newScrambledWord.Count - 1) {
			//we increase the number by one
			currentPage += 1;
			//the user is no longer typing as they have just switched pages
			userIsTyping = false;
			//we reset the the user definition
			userDefinition = "";
		} else {
			//if the current page goes over, we go back to the first page
			currentPage = 0;
			userIsTyping = false;
			userDefinition = "";
		}
	}
	public void previousPage(){
		typingBar.GetComponent<RectTransform> ().anchoredPosition3D = typerOriginalPos;
		typingBar.SetActive (false);
		//we make sure the the current page value does not go under 0
		if (currentPage > 0) {
			//we reduce the page value by one
			currentPage -= 1;
			userIsTyping = false;
			userDefinition = "";
		//if it goes to negative numbers then we jump to the last page.
		} else {
			currentPage = newScrambledWord.Count - 1;
			userIsTyping = false;
			userDefinition = "";
		}
	}

	/// <summary>
	/// Enables the user to type the traslation for the given word
	/// </summary>
	public void typeTranslation(){
		//makes sure that the translation given does not go over 10 characters
		if (userDefinition.Length < 10) {
			//if the user types in a letter
			if (Input.GetKeyDown (KeyCode.A)) {
				//then we add that letter to the user definition string
				userDefinition += 'a';
				//we remove the definition offered in the list
				definitionOffered.RemoveAt (currentPage);
				//and add the one with the extra letter in its place
				definitionOffered.Insert (currentPage, userDefinition);
				moveTyperForward ();
			} else if (Input.GetKeyDown (KeyCode.B)) {
				userDefinition += 'b';
				definitionOffered.RemoveAt (currentPage);
				definitionOffered.Insert (currentPage, userDefinition);
				moveTyperForward ();
			} else if (Input.GetKeyDown (KeyCode.C)) {
				userDefinition += 'c';
				definitionOffered.RemoveAt (currentPage);
				definitionOffered.Insert (currentPage, userDefinition);
				moveTyperForward ();
			} else if (Input.GetKeyDown (KeyCode.D)) {
				userDefinition += 'd';
				definitionOffered.RemoveAt (currentPage);
				definitionOffered.Insert (currentPage, userDefinition);
				moveTyperForward ();
			} else if (Input.GetKeyDown (KeyCode.E)) {
				userDefinition += 'e';
				definitionOffered.RemoveAt (currentPage);
				definitionOffered.Insert (currentPage, userDefinition);
				moveTyperForward ();
			} else if (Input.GetKeyDown (KeyCode.F)) {
				userDefinition += 'f';
				definitionOffered.RemoveAt (currentPage);
				definitionOffered.Insert (currentPage, userDefinition);
				moveTyperForward ();
			} else if (Input.GetKeyDown (KeyCode.G)) {
				userDefinition += 'g';
				definitionOffered.RemoveAt (currentPage);
				definitionOffered.Insert (currentPage, userDefinition);
				moveTyperForward ();
			} else if (Input.GetKeyDown (KeyCode.H)) {
				userDefinition += 'h';
				definitionOffered.RemoveAt (currentPage);
				definitionOffered.Insert (currentPage, userDefinition);
				moveTyperForward ();
			} else if (Input.GetKeyDown (KeyCode.I)) {
				userDefinition += 'i';
				definitionOffered.RemoveAt (currentPage);
				definitionOffered.Insert (currentPage, userDefinition);
				moveTyperForward ();
			} else if (Input.GetKeyDown (KeyCode.J)) {
				userDefinition += 'j';
				definitionOffered.RemoveAt (currentPage);
				definitionOffered.Insert (currentPage, userDefinition);
				moveTyperForward ();
			} else if (Input.GetKeyDown (KeyCode.K)) {
				userDefinition += 'k';
				definitionOffered.RemoveAt (currentPage);
				definitionOffered.Insert (currentPage, userDefinition);
				moveTyperForward ();
			} else if (Input.GetKeyDown (KeyCode.L)) {
				userDefinition += 'l';
				definitionOffered.RemoveAt (currentPage);
				definitionOffered.Insert (currentPage, userDefinition);
				moveTyperForward ();
			} else if (Input.GetKeyDown (KeyCode.M)) {
				userDefinition += 'm';
				definitionOffered.RemoveAt (currentPage);
				definitionOffered.Insert (currentPage, userDefinition);
				moveTyperForward ();
			} else if (Input.GetKeyDown (KeyCode.N)) {
				userDefinition += 'n';
				definitionOffered.RemoveAt (currentPage);
				definitionOffered.Insert (currentPage, userDefinition);
				moveTyperForward ();
			} else if (Input.GetKeyDown (KeyCode.O)) {
				userDefinition += 'o';
				definitionOffered.RemoveAt (currentPage);
				definitionOffered.Insert (currentPage, userDefinition);
				moveTyperForward ();
			} else if (Input.GetKeyDown (KeyCode.P)) {
				userDefinition += 'p';
				definitionOffered.RemoveAt (currentPage);
				definitionOffered.Insert (currentPage, userDefinition);
				moveTyperForward ();
			} else if (Input.GetKeyDown (KeyCode.Q)) {
				userDefinition += 'q';
				definitionOffered.RemoveAt (currentPage);
				definitionOffered.Insert (currentPage, userDefinition);
				moveTyperForward ();
			} else if (Input.GetKeyDown (KeyCode.R)) {
				userDefinition += 'r';
				definitionOffered.RemoveAt (currentPage);
				definitionOffered.Insert (currentPage, userDefinition);
				moveTyperForward ();
			} else if (Input.GetKeyDown (KeyCode.S)) {
				userDefinition += 's';
				definitionOffered.RemoveAt (currentPage);
				definitionOffered.Insert (currentPage, userDefinition);
				moveTyperForward ();
			} else if (Input.GetKeyDown (KeyCode.T)) {
				userDefinition += 't';
				definitionOffered.RemoveAt (currentPage);
				definitionOffered.Insert (currentPage, userDefinition);
				moveTyperForward ();
			} else if (Input.GetKeyDown (KeyCode.U)) {
				userDefinition += 'u';
				definitionOffered.RemoveAt (currentPage);
				definitionOffered.Insert (currentPage, userDefinition);
				moveTyperForward ();
			} else if (Input.GetKeyDown (KeyCode.V)) {
				userDefinition += 'v';
				definitionOffered.RemoveAt (currentPage);
				definitionOffered.Insert (currentPage, userDefinition);
				moveTyperForward ();
			} else if (Input.GetKeyDown (KeyCode.W)) {
				userDefinition += 'w';
				definitionOffered.RemoveAt (currentPage);
				definitionOffered.Insert (currentPage, userDefinition);
				moveTyperForward ();
			} else if (Input.GetKeyDown (KeyCode.X)) {
				userDefinition += 'x';
				definitionOffered.RemoveAt (currentPage);
				definitionOffered.Insert (currentPage, userDefinition);
				moveTyperForward ();
			} else if (Input.GetKeyDown (KeyCode.Y)) {
				userDefinition += 'y';
				definitionOffered.RemoveAt (currentPage);
				definitionOffered.Insert (currentPage, userDefinition);
				moveTyperForward ();
			} else if (Input.GetKeyDown (KeyCode.Z)) {
				userDefinition += 'z';
				definitionOffered.RemoveAt (currentPage);
				definitionOffered.Insert (currentPage, userDefinition);
				moveTyperForward ();
			}

		}
		//if the user presses backspace and the user definition's length is not negative
		if (Input.GetKeyDown (KeyCode.Backspace) && userDefinition.Length > 0) {
			//then we remove the last character from the string
			userDefinition = userDefinition.Substring (0, userDefinition.Length - 1);
			//we remove the definition offered at that index 
			definitionOffered.RemoveAt (currentPage);
			//and replace it with the word with one less letter
			definitionOffered.Insert (currentPage, userDefinition);
			moveTyperBackward ();
		}


	}

	/// <summary>
	/// Starts the translating process for the journal. Basically checks if the translated words offered by the user were correct
	/// </summary>
	public void startTranslatingJournal(){
		//goes throught the list of translations offered by the user
		for (int i = 0; i < definitionOffered.Count; i++) {
			//if the translation was correct
			if(theGameManager.checkTranslation(newScrambledWord[i], definitionOffered[i])){
				//then we set it as so in the list
				wasDefinitionCorrect [i] = true;
			} 

		}
	}

	/// <summary>
	/// Gets the tentative definition.
	/// </summary>
	/// <returns><c>true</c>, if tentative definition was gotten, <c>false</c> otherwise.</returns>
	/// <param name="word">Word.</param>
	public bool getTentativeDefinition(string word){
		//goes through the scrambled words discovered
		for (int i = 0; i < newScrambledWord.Count; i++) {
			//if the scrambled word encountered has been discovered
			if(word.Trim().Equals(newScrambledWord[i].Trim())){
				//and the user has offered a deifinition for that specific word
				if (definitionOffered [i].Length > 0) {
					//then return true
					//i don't think this is used anymore 
					translationIndex = i;
					return true;
				//if no definition was offered, then return false
				} else {

					return false;
				}
				//translationIndex = i;
				//return true;
			}
		}
		//and if the word has not been ecountered then return false
		return false;
	}
	public void userWantsToType(){
		if(!userIsTyping && !wasDefinitionCorrect[currentPage]){
			userIsTyping = true;
			typingBar.SetActive (true);
			definitionOffered.RemoveAt (currentPage);
			definitionOffered.Insert (currentPage, "");
			StartCoroutine (typingBarFlash ());
		}
	}
	public IEnumerator typingBarFlash(){
		yield return new WaitForSeconds (0.4f);
		type ();

	}
	public void type(){
		if (userIsTyping) {
			if (typer) {
				typingBar.SetActive (false);
				typer = false;
			} else {
				typingBar.SetActive (true);
				typer = true;
			}
			StartCoroutine (typingBarFlash ());
		}
	}
	public void moveTyperForward(){
		typingBar.GetComponent<RectTransform> ().anchoredPosition3D += new Vector3 (10, 0, 0);
	}
	public void moveTyperBackward(){
		typingBar.GetComponent<RectTransform> ().anchoredPosition3D -= new Vector3 (10, 0, 0);
	}
}
