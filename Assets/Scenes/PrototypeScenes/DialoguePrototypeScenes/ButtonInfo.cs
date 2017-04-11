﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInfo : MonoBehaviour {
    public bool isScrambled, isTyping;
    public Text buttonText;
    public TranslatorManager theTranslatorManager;
    public GameObject parentPanel;
    public GameObject nextButton_1;
    public GameObject panel1, panel2, panel3;
    public string nextText1, nextText2, nextText3;
    public TextBoxManager thep;

    // Use this for initialization
    void Start () {
        theTranslatorManager = FindObjectOfType<TranslatorManager>();
        theTranslatorManager = theTranslatorManager.GetComponent<TranslatorManager>();
        thep = FindObjectOfType<TextBoxManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isScrambled)
        {
            buttonText.color = Color.red;
        }else
        {
            buttonText.color = Color.white;
        }
        if (isTyping)
        {
            theTranslatorManager.typeTranslation();
            buttonText.text = theTranslatorManager.userDefinition;
            if (Input.GetKeyDown(KeyCode.Return))
            {
                isTyping = false;
            }
        }
        if(nextButton_1 != null)
        {
            nextText1 = nextButton_1.GetComponent<NextThoughtBubble>().thoughtBubbleString_1;
            nextText2 = nextButton_1.GetComponent<NextThoughtBubble>().thoughtBubbleString_2;
            nextText3 = nextButton_1.GetComponent<NextThoughtBubble>().thoughtBubbleString_3;
            
        }
	}

    public void translateOrMoveOn()
    {
        if (isScrambled)
        {
            isTyping = true;
        }
        else
        {
            if (nextButton_1.GetComponent<NextThoughtBubble>().isFinalThought)
            {
                //disable the text box
                //panel1.GetComponent<buttonshapetest>().resetText();
                thep.GetComponent<TextBoxManager>().disableTextBox();
                panel1.SetActive(false);
                panel2.SetActive(false);
                panel3.SetActive(false);
            }
           /* for(int i = 0; i < 8; i++)
            {
                panel1.GetComponent<buttonshapetest>().wordButton[i].GetComponent<ButtonInfo>().isScrambled = false;
                panel2.GetComponent<buttonshapetest>().wordButton[i].GetComponent<ButtonInfo>().isScrambled = false;
                panel3.GetComponent<buttonshapetest>().wordButton[i].GetComponent<ButtonInfo>().isScrambled = false;
            }*/
            panel1.GetComponent<buttonshapetest>().isScrambles = new bool[8];
            panel2.GetComponent<buttonshapetest>().isScrambles = new bool[8];
            panel3.GetComponent<buttonshapetest>().isScrambles = new bool[8];
            panel1.GetComponent<buttonshapetest>().nextButton_1 = nextButton_1.GetComponent<NextThoughtBubble>().nextThoughtBubble;
            panel2.GetComponent<buttonshapetest>().nextButton_1 = nextButton_1.GetComponent<NextThoughtBubble>().otherBubble_1;
            panel3.GetComponent<buttonshapetest>().nextButton_1 = nextButton_1.GetComponent<NextThoughtBubble>().otherBubble_2;
            panel1.GetComponent<buttonshapetest>().nextText1 = nextText1;
            panel2.GetComponent<buttonshapetest>().nextText1 = nextText2;
            panel3.GetComponent<buttonshapetest>().nextText1 = nextText3;

        }
    }
}
