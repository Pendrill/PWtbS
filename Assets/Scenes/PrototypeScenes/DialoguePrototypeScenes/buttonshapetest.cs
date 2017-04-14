using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonshapetest : MonoBehaviour {
    public Text[] buttonText = new Text[8];
    public Button[] wordButton = new Button[8];
    //public string newWord1, newWord2, newWord3, newWord4, newWord5, newWord6, newWord7, newWord8;
    public string[] individualWord;
    public GameObject panel1, panel2, panel3;
    public string nextText1, updatedLine;
    public bool[] isScrambles = new bool[8];
    public GameObject nextButton_1;
    public bool isBeingDisplayed, removeOldText;
    public gameManager theGameManager;

    // Use this for initialization
    void Start () {
        // Button1.text = newWord1;
        theGameManager = FindObjectOfType<gameManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!isBeingDisplayed)
        {
            //isBeingDisplayed = true;
            displayButtonText(nextText1, isScrambles);
        }
	}
    public void displayButtonText(string line, bool[] isScrambled)
    {
        //individualWord = new string[1];
        individualWord = line.Split(' ');
        for (int i = 0; i < individualWord.Length; i++)
        {
            updatedLine += theGameManager.checkIfScramble(individualWord[i], isScrambled, i) + " ";
        }
        // individualWord = new string[1];
        updatedLine = updatedLine.Substring(0, updatedLine.Length - 1);
        individualWord = updatedLine.Split(' ');
        updatedLine = "";
        for (int i = 0; i < wordButton.Length; i++)
        {
            if(isScrambled[i])
            {
                wordButton[i].GetComponent<ButtonInfo>().isScrambled = true;
            }else
            {
                wordButton[i].GetComponent<ButtonInfo>().isScrambled = false;
                wordButton[i].GetComponent<ButtonInfo>().nextButton_1 = nextButton_1;
            }
        }
        for (int i = 0; i < individualWord.Length; i++)
        {
            
            if (!removeOldText)
            {
                resetText(i);
            }
            buttonText[i].text = individualWord[i];
            
        }
        removeOldText = false;
    }
    public void resetText(int index)
    {
        for(int i = index; i < buttonText.Length; i++)
        {
            buttonText[i].text = "";
        }
        removeOldText = true;
    }
}
