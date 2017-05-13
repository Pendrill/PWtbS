using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogueTest : MonoBehaviour {

    public Text theText;
    public string scrambled;
    public string notScrambled;
    public float typingSpeed;
    public int  currentLetter, currentLetterTranslation;
    public bool isTyping;

	// Use this for initialization
	void Start () {
        scrambled = "@#$%$#@#$%@*@%#$@&#%@#^";
        notScrambled = "hello there fellow arb";
        theText.text = "";
        isTyping = true;
        StartCoroutine(typeDatWord());
	}
	
	// Update is called once per frame
	void Update () {
       
		
	}
    public IEnumerator typeDatWord()
    {
        while(isTyping && currentLetter < scrambled.Length - 1)
        {
            theText.text += scrambled[currentLetter];
            
            currentLetter += 1;
            if(currentLetter == 5)
            {
                StartCoroutine(showTranslation());
            }
            yield return new WaitForSeconds(typingSpeed);
        }
        //theText.text = notScrambled;
        //isTyping = false; 

    }

    public IEnumerator showTranslation()
    {
        while(isTyping && currentLetterTranslation < notScrambled.Length - 1)
        {
            theText.text = theText.text.Remove(currentLetterTranslation, 1);
            theText.text = theText.text.Insert(currentLetterTranslation, notScrambled[currentLetterTranslation].ToString());
            //theText.text.Substring(currentLetterTranslation, currentLetterTranslation + 1) = notScrambled[currentLetterTranslation].ToString();
            currentLetterTranslation += 1;
            yield return new WaitForSeconds(typingSpeed);

        }
        theText.text = notScrambled;
        isTyping = false;

    }
}
