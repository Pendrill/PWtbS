﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class gameManager : MonoBehaviour {

	public TextAsset textFile, keyWordsTXT, keyWordsScrambleTXT;
	public string[] keyWords, keyWordsScramble;
	public string scrambledWord, removedPunctuation, testString;

	public bool happenedOnce;

	HashSet<char> validChars =	new HashSet<char>() {'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','q','r','s','t','u','v','w','x','y','z',
		'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'};
	System.Text.StringBuilder sb = new System.Text.StringBuilder ();

	char test;

	// Use this for initialization
	void Start () {
		if (keyWordsTXT != null) {
			keyWords = keyWordsTXT.text.Split ('\n');
		}
		if (keyWordsScrambleTXT != null) {
			keyWordsScramble = keyWordsScrambleTXT.text.Split ('\n');
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public string checkIfScramble(string word){
		//Debug.Log (word + " checkifScramble");
		removePunctuation (word);
		for (int i = 0; i < keyWords.Length; i++) {
			//Debug.Log (keyWords[i] + "   " + removedPunctuation);
			if (removedPunctuation.Trim().Equals(keyWords [i].Trim()) ) {
				//Debug.Log ("What about this one?");
				removedPunctuation = "";
				return keyWordsScramble[i];
			}
		}
		removedPunctuation = "";
		return word;
	}

	public void removePunctuation(string word){
		//Debug.Log (word + " removePunct");
		for (int i = 0; i < word.Length; i++) {
			if(validChars.Contains(word[i])){
				sb.Append(word[i]);
			}
		}
		removedPunctuation = sb.ToString();
		sb = new System.Text.StringBuilder ();
	}
}
