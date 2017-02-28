using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class gameManager : MonoBehaviour {

	//set up the various text assets we will be including te key words and their scrambled versions
	public TextAsset textFile, keyWordsTXT, keyWordsScrambleTXT;
	//we set up their respective arrays
	public string[] keyWords, keyWordsScramble;
	//we create a number of strings to keep track of the scrambled word and the word without punctuation
	public string scrambledWord, removedPunctuation;// testString;
	// we create a list that will keep track of wether or not the 
	public static List<bool> isWordTranslated = new List<bool> ();

	//public bool happenedOnce;

	//create a hashset of valid chars that contains the valid characters a word can contain.
	HashSet<char> validChars =	new HashSet<char>() {'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','q','r','s','t','u','v','w','x','y','z',
		'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'};
	System.Text.StringBuilder sb = new System.Text.StringBuilder ();

	//char test;

	//get the translatormanager object and script
	public TranslatorManager theTranslatorManager;

	public static bool first = true;

	// Use this for initialization
	void Start () {
		//get the translator object
		theTranslatorManager = FindObjectOfType<TranslatorManager> ();
		//check that there is a text object fro the key words and there scrambled equivalent and then split into the array
		if (keyWordsTXT != null) {
			keyWords = keyWordsTXT.text.Split ('\n');
		}
		if (keyWordsScrambleTXT != null) {
			keyWordsScramble = keyWordsScrambleTXT.text.Split ('\n');
		}
		//use a for loop to set the list keeping track of if the words got translated to false
		if (first) {
			for (int i = 0; i < keyWordsScramble.Length; i++) {
				isWordTranslated.Add (false);
			}
		}
		first = false;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// Checks if scramble. checks that the word offered has a scrambled equivalent
	/// </summary>
	/// <returns>The if scramble.</returns>
	/// <param name="word">Word.</param>
	public string checkIfScramble(string word){
		//Debug.Log (word + " checkifScramble");
		//we first remove any punctuation that might have come with the word
		removePunctuation (word);
		//we use a for loop ot go through the key words
		for (int i = 0; i < keyWords.Length; i++) {
			//Debug.Log (keyWords[i] + "   " + removedPunctuation);
			//we check if the word is equal to any of the words within the keywords list
			if (removedPunctuation.Trim().Equals(keyWords [i].Trim()) ) {
				//Debug.Log ("What about this one?");
				//have the check for if it has been translated within this if statement
				//if yes then we check if it has already been translated
				if (isWordTranslated [i]) {
					//if yes then we return the word
					removedPunctuation = "";
					return word;
				}
				//Otherwise we check if word has tentative definition
				if(theTranslatorManager.getTentativeDefinition(keyWordsScramble[i])){
					//if yes then we return the tentative definition offered by the user
					return TranslatorManager.definitionOffered [theTranslatorManager.translationIndex];
				}
				//Otherwise we first check if the specific word has been encountered
				removedPunctuation = "";
				theTranslatorManager.checkIfWordHasAlreadyBeenEncountered (keyWordsScramble [i]);
				//and then return the scrambled version of the word
				return keyWordsScramble[i];
			}
		}
		//if it is not in the key words then we simply return the word
		removedPunctuation = "";
		return word;
	}

	/// <summary>
	/// Removes the punctuation that might have come with the word.
	/// </summary>
	/// <param name="word">Word.</param>
	public void removePunctuation(string word){
		//Debug.Log (word + " removePunct");
		//we go through each character that is contained within the word
		for (int i = 0; i < word.Length; i++) {
			//we check if the character is valid
			if(validChars.Contains(word[i])){
				//and then append the string builder
				sb.Append(word[i]);
			}
		}
		//we update the string
		removedPunctuation = sb.ToString();
		//and then reset the string builder
		sb = new System.Text.StringBuilder ();
	}

	/// <summary>
	/// Checks if the translation offered by the user is correct.
	/// </summary>
	/// <returns><c>true</c>, if translation was checked, <c>false</c> otherwise.</returns>
	/// <param name="wordScrambled">Word scrambled.</param>
	/// <param name="userTranslation">User translation.</param>
	public bool checkTranslation(string wordScrambled, string userTranslation){
		//resets the index
		int indexOfScrambled = 0;
		//goes through the keywords
		for (int i = 0; i < keyWordsScramble.Length; i++) {
			//checks to find the specific index of the scrambled word
			if (wordScrambled.Trim ().Equals (keyWordsScramble [i].Trim ())) {
				indexOfScrambled = i;
				//what if i include a break here?
			}
		}
		//if the translation offered is equal to the key word at the found index
		if (userTranslation.Trim ().Equals (keyWords [indexOfScrambled].Trim())) {
			//then we set the word as being correctly translated
			isWordTranslated [indexOfScrambled] = true;
			//and we return true
			return true;
		}
		//if it wasn't corretly translated then we return false
		return false;

	}
}
