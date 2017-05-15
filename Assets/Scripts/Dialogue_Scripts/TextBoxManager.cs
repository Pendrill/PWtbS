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
    bool cancelTyping, isTyping_TB1, cancelTyping_TB1, isTyping_TB2, cancelTyping_TB2, isTyping_TB3, cancelTyping_TB3;
    public bool isTyping;
    public float typeSpeed;

    //checks how much time is left in relation to the coroutine for the zoom in and out of the camera
    public float time_left;

    //reference to the game manager script and the object it is attached to;
    public gameManager theGameManager;
    //string that keeps track of the updated line of dialogue that needs to be displayed on screen
    public string updatedLineOfText, updatedLineOfText_TB1, updatedLineOfText_TB2, updatedLineOfText_TB3;
    public TranslatorManager theTranslatorManager;

    public GameObject thoughtBubble_1, thoughtBubble_2, thoughtBubble_3;
    public Text thoughtBubbleText_1, thoughtBubbleText_2, thoughtBubbleText_3;
    public string thoughtBubbleString_1, thoughtBubbleString_2, thoughtBubbleString_3;
    public string[] individualWordTBT_1, individualWordTBT_2, individualWordTBT_3;
    public bool ThoughtBubbleRequiered = false, beingDisplayed = false;
    public string playerText;
    public bool noDialogue, isHuman, shiftDialogueBox;
    public bool[] isScrambled;
    public Vector3 OriginalPanelPosition, PanelDestination;
    public float time;
    public GameObject endDialogueIndicator;
    public string[] dialogue;
    public int dialogueSize;
    public bool displayDecisions, displayBox, answer1,answer2,answer3, doneActivating, done1, done2, done3;


    //we could include a way for the player to stop moving when dialogue pops up (DONE)

    // Use this for initialization
    void Start()
    {
        time = 0f;
        //get the game manager object
        theGameManager = FindObjectOfType<gameManager>();
        theTranslatorManager = FindObjectOfType<TranslatorManager>();

        //make sure that the istyping and canceltyping are both set to false, as nothing should be displaying on the screen when the scene first runs
        isTyping = false;
        cancelTyping = false;

        //make sure that a textfile with the dialogue has been inputed
        if (textFile != null)
        {
            //so that we can then split the dialogue from the textfile into the specific array
            //textLines = (textFile.text.Split('\n'));
        }

        //if no end at line has been specified then it should be the last line of the textfile.
        if (endAtLine == 0)
        {
            endAtLine = textLines.Length - 1;
        }

        //if the textbox bool is set to active then it should be enable at the beginning of the scene. Otherwise no.
        if (isTextBoxActive)
        {
            enableTextBox(isHuman);
        }
        else
        {
            disableTextBox();
        }

        OriginalPanelPosition = textBox.GetComponent<RectTransform>().anchoredPosition3D;
    }

    // Update is called once per frame
    void Update()
    {
        if (!doneActivating)
        {
            if (thoughtBubble_1 != null)
            {
                thoughtBubble_1.GetComponent<Button>().interactable = false;
                thoughtBubble_2.GetComponent<Button>().interactable = false;
                thoughtBubble_3.GetComponent<Button>().interactable = false;
            }
        }
        else
        {
            if (thoughtBubble_1 != null)
            {
                StartCoroutine(waitToClickOptions());
            }
        }
        //if the dialogue box is not acitve then there is no point in having the text box running.
        if (!isTextBoxActive)
        {
            if (thoughtBubble_3 != null)
            {
                StopCoroutine(DisplayResponces());
                DeActivateResponceBoxes();
            }
            return;
        }
        else
        {
            //Considering that the button to click through text and activate text, there needs to be a slight delay just so that both are not activated at the same time
            time_left -= Time.deltaTime;
        }

        if (shiftDialogueBox)
        {
            time += Time.deltaTime*6;
            textBox.GetComponent<RectTransform>().anchoredPosition3D = Vector3.Lerp(OriginalPanelPosition, PanelDestination, time);
        }else
        {
            time -= Time.deltaTime*6;
            textBox.GetComponent<RectTransform>().anchoredPosition3D = Vector3.Lerp(OriginalPanelPosition, PanelDestination, time);
        }
        if(textBox.GetComponent<RectTransform>().anchoredPosition3D == PanelDestination)
        {
            
            if (shiftDialogueBox && !displayBox)
            {
                DeActivateResponceBoxes();
                StartCoroutine(DisplayResponces());
            }
            //shiftDialogueBox = false;

        }else
        {
            DeActivateResponceBoxes();
        }
        //theText.text = textLines [currentLine];
        if (ThoughtBubbleRequiered && !beingDisplayed && !isHuman && displayBox)
        {
            //ActivateResponceBoxes();
            beingDisplayed = true;

            individualWordTBT_1 = thoughtBubbleString_1.Split(' ');
            individualWordTBT_2 = thoughtBubbleString_2.Split(' ');
            individualWordTBT_3 = thoughtBubbleString_3.Split(' ');
            for (int i = 0; i < individualWordTBT_1.Length; i++)
            {
                updatedLineOfText_TB1 += theGameManager.checkIfScramble(individualWordTBT_1[i], isScrambled, i) + " ";
            }
            for (int i = 0; i < individualWordTBT_2.Length; i++)
            {
                updatedLineOfText_TB2 += theGameManager.checkIfScramble(individualWordTBT_2[i]) + "  ";
            }
            for (int i = 0; i < individualWordTBT_3.Length; i++)
            {
                updatedLineOfText_TB3 += theGameManager.checkIfScramble(individualWordTBT_3[i]) + "  ";
            }
            if (answer1)
            {
                StartCoroutine(TextScroll_TB1(thoughtBubbleString_1));//updatedLineOfText_TB1));
            }
            if (answer2)
            {
                StartCoroutine(TextScroll_TB2(thoughtBubbleString_2));//updatedLineOfText_TB2));
            }
            if (answer3)
            {
                StartCoroutine(TextScroll_TB3(thoughtBubbleString_3));//updatedLineOfText_TB3));
            }
            //StartCoroutine(TextScroll_TB1(thoughtBubbleString_1));//updatedLineOfText_TB1));
            // thoughtBubble_1.GetComponent<buttonshapetest>().displayButtonText(updatedLineOfText_TB1, isScrambled);
           // StartCoroutine(TextScroll_TB2(thoughtBubbleString_2));//updatedLineOfText_TB2));
            //StartCoroutine(TextScroll_TB3(thoughtBubbleString_3));//updatedLineOfText_TB3));
            //StartCoroutine (TextScroll (updatedLineOfText));
        }
        //Checks if the player clicked the mouse
        if (Input.GetKeyDown(KeyCode.Mouse0) && !theTranslatorManager.panelIsActive && isTextBoxActive)
        {//if(Input.GetKeyDown(KeyCode.Mouse0) && !theTranslatorManager.panelIsActive){
         //checks that all the letters of the specific dialogue line have been displayed
            if (!isTyping && !displayDecisions)
            {
                currentLine += 1;
                endDialogueIndicator.SetActive(false);
                StartCoroutine(TextScroll(dialogue[currentLine]));
            }
            else
            {
                cancelTyping = true;
                cancelTyping_TB1 = true;
                cancelTyping_TB2 = true;
                cancelTyping_TB3 = true;
            }

            if (currentLine >= dialogueSize -1 && !displayDecisions)
            {
                displayDecisions = true;
                shiftDialogueBox = true;
                endDialogueIndicator.SetActive(false);
                //StartCoroutine(TextScroll(dialogue[currentLine]));
            }
            else if(!isTyping)
            {
                Debug.Log("This getting acessed");
              
            }
            if (time > 1f)
            {
                time = 1f;
            }
            else if (time < 0f)
            {
                time = 0f;
            }

            //if yes then we move on to the next line
            //currentLine += 1;
            //we check if we have passed the final line of dialogue
            //if (currentLine > endAtLine) {
            //if so we call the disable text box function and we reset the current line variable
            //disableTextBox ();
            //currentLine = 0;
            //} else {
            //if not, then we take the current line and split it as to have each word have its own index within the array
            //individualWord = textLines[currentLine].Split (' ');
            //individualWord = playerText.Split (' ');
            //we then use a for loop that will go through each word within the array
            //for (int i = 0; i < individualWord.Length; i++) {
            //we want to check wether or not we need to scramble the words that appear in the dialogue
            //we call on the check if scramble function in the game manager script and then add the returned word to the updatedLinneOfText
            //updatedLineOfText += theGameManager.checkIfScramble (individualWord [i]) + "  ";
            //}
            //Once that is done, we start the coroutine that will display the updated line of text one letter at a time.
            //StartCoroutine (TextScroll (updatedLineOfText));//textLines [currentLine]));
            //}
            //If the user clicks but the letters are still being displayed then we need to cancel the typing as to show the full line of dialogue immediately
            //} else if(((isTyping && !cancelTyping) ||(isTyping_TB1 && !cancelTyping_TB1)||(isTyping_TB2 && !cancelTyping_TB2)||(isTyping_TB3 && !cancelTyping_TB3)) && time_left < 0) {
            //time_left = 0.2f;
            /* if (((isTyping && !cancelTyping) || (isTyping_TB1 && !cancelTyping_TB1) || (isTyping_TB2 && !cancelTyping_TB2) || (isTyping_TB3 && !cancelTyping_TB3)) && time_left < 0)
             {
                 Debug.Log("is it being set to false?");
                 cancelTyping = true;
                 cancelTyping_TB1 = true;
                 cancelTyping_TB2 = true;
                 cancelTyping_TB3 = true;
             }*/
        }
        if(done1 && done2 && done3)
        {
            doneActivating = true;
        }

    }

    /// <summary>
    /// This is a coroutine function that will enable us to display the line of dialogue on line at a time.
    /// </summary>
    /// <returns>The scroll.</returns>
    /// <param name="lineOfText"> this is the specific line that needs to be displayed on the screen .</param>
    private IEnumerator TextScroll(string lineOfText)
    {
        Debug.Log(lineOfText);
        //we reset the int that keeps track of the number of letters
        int letter = 0;
        //we reset the text that will be displayed on the screen as dialogue
        theText.text = "";
        //when this coroutine is happening than, we are currently typing letters on the screen
        isTyping = true;
        //we reset the cancel typing bool to false
        cancelTyping = false;

        //we have a while loop that will display the line of text one letter at a time
        while (isTyping && !cancelTyping && letter < lineOfText.Length - 1)
        {
            Debug.Log("Does the textScroll get accessed?");
            //we add one letter to the text object
            theText.text += lineOfText[letter];
            //we move on to the next letter
            letter += 1;
            //we then return and wait a number of seconds before displaying the nest letter
            yield return new WaitForSeconds(typeSpeed);
        }
        //once all the letters have been displayed or if the user cancelled the typing, we diplay the whole line of dialogue
        theText.text = lineOfText;
        if (currentLine < dialogueSize - 1)
        {
            endDialogueIndicator.SetActive(true);
        }
        //endDialogueIndicator.SetActive(true);
        //we are no longer typing
        isTyping = false;
        //there is no longer a need to cancel the typing
        cancelTyping = false;
        //we reset the individual word array for the next line of dialogue
        individualWord = new string[1];
        //we do the same for the updated line of text
        updatedLineOfText = "";
    }

    private IEnumerator TextScroll_TB1(string lineOfText)
    {
        //we reset the int that keeps track of the number of letters
        Debug.Log("Is this getting accessed twice?");
        int letter = 0;
        //we reset the text that will be displayed on the screen as dialogue
        thoughtBubbleText_1.text = "";
        //when this coroutine is happening than, we are currently typing letters on the screen
        isTyping_TB1 = true;
        //we reset the cancel typing bool to false
        cancelTyping_TB1 = false;

        //we have a while loop that will display the line of text one letter at a time
        while (isTyping_TB1 && !cancelTyping_TB1 && letter < lineOfText.Length - 1)
        {
            //we add one letter to the text object
            thoughtBubbleText_1.text += lineOfText[letter];
            //we move on to the next letter
            letter += 1;
            //we then return and wait a number of seconds before displaying the nest letter
            yield return new WaitForSeconds(typeSpeed);
        }
        //once all the letters have been displayed or if the user cancelled the typing, we diplay the whole line of dialogue
        thoughtBubbleText_1.text = lineOfText;
        done1 = true;
        //we are no longer typing
        isTyping_TB1 = false;
        //there is no longer a need to cancel the typing
        cancelTyping_TB1 = false;
        //we reset the individual word array for the next line of dialogue
        individualWordTBT_1 = new string[1];
        //we do the same for the updated line of text
        updatedLineOfText_TB1 = "";
    }
    private IEnumerator TextScroll_TB2(string lineOfText)
    {
        //we reset the int that keeps track of the number of letters
        int letter = 0;
        //we reset the text that will be displayed on the screen as dialogue
        thoughtBubbleText_2.text = "";
        //when this coroutine is happening than, we are currently typing letters on the screen
        isTyping_TB2 = true;
        //we reset the cancel typing bool to false
        cancelTyping_TB2 = false;

        //we have a while loop that will display the line of text one letter at a time
        while (isTyping_TB2 && !cancelTyping_TB2 && letter < lineOfText.Length - 1)
        {
            //we add one letter to the text object
            thoughtBubbleText_2.text += lineOfText[letter];
            //we move on to the next letter
            letter += 1;
            //we then return and wait a number of seconds before displaying the nest letter
            yield return new WaitForSeconds(typeSpeed);
        }
        //once all the letters have been displayed or if the user cancelled the typing, we diplay the whole line of dialogue
        thoughtBubbleText_2.text = lineOfText;
        //we are no longer typing
        isTyping_TB2 = false;
        done2 = true;
        //there is no longer a need to cancel the typing
        cancelTyping_TB2 = false;
        //we reset the individual word array for the next line of dialogue
        individualWordTBT_2 = new string[1];
        //we do the same for the updated line of text
        updatedLineOfText_TB2 = "";
    }
    private IEnumerator TextScroll_TB3(string lineOfText)
    {
        //we reset the int that keeps track of the number of letters
        int letter = 0;
        //we reset the text that will be displayed on the screen as dialogue
        thoughtBubbleText_3.text = "";
        //when this coroutine is happening than, we are currently typing letters on the screen
        isTyping_TB3 = true;
        //we reset the cancel typing bool to false
        cancelTyping_TB3 = false;

        //we have a while loop that will display the line of text one letter at a time
        while (isTyping_TB3 && !cancelTyping_TB3 && letter < lineOfText.Length - 1)
        {
            //we add one letter to the text object
            thoughtBubbleText_3.text += lineOfText[letter];
            //we move on to the next letter
            letter += 1;
            //we then return and wait a number of seconds before displaying the nest letter
            yield return new WaitForSeconds(typeSpeed);
        }
        //once all the letters have been displayed or if the user cancelled the typing, we diplay the whole line of dialogue
        thoughtBubbleText_3.text = lineOfText;
        //we are no longer typing
        isTyping_TB3 = false;
        done3 = true;
        //there is no longer a need to cancel the typing
        cancelTyping_TB3 = false;
        //we reset the individual word array for the next line of dialogue
        individualWordTBT_3 = new string[1];
        //we do the same for the updated line of text
        updatedLineOfText_TB3 = "";
    }
    /// <summary>
    /// Enables the text box specific for the dialogue.
    /// </summary>
    public void enableTextBox(bool human)
    {
        noDialogue = true;
        //we set the text box to active
        textBox.SetActive(true);
        //the text box is thus currently active
        isTextBoxActive = true;
        //if (stopPlayerMovement) {
        //playerMovement.canMove = false;
        //}
        if (!human)
        {
            //shiftDialogueBox = true;
        } else
        {
            //shiftDialogueBox = false;
        }

        //we represt the same process as above to check if the words witin the line of dialogue need to be scrambled, and then updated the line of text accordingly 
        //individualWord = textLines[currentLine].Split (' ');
        individualWord = playerText.Split(' ');
        for (int i = 0; i < individualWord.Length; i++)
        {
            updatedLineOfText += theGameManager.checkIfScramble(individualWord[i]) + "  ";
        }
        //we then start the couroutine that will display the sentences of dialogue one letter at a time.


        //StartCoroutine(TextScroll(playerText));//updatedLineOfText));//textLines [currentLine]));
        
    }
    public void enableTextBox()
    {
        
        noDialogue = true;
        //we set the text box to active
        textBox.SetActive(true);
        //the text box is thus currently active
        isTextBoxActive = true;
        //if (stopPlayerMovement) {
        //playerMovement.canMove = false;
        //}
        //shiftDialogueBox = true;

        //we represt the same process as above to check if the words witin the line of dialogue need to be scrambled, and then updated the line of text accordingly 
        //individualWord = textLines[currentLine].Split (' ');
        individualWord = playerText.Split(' ');
        for (int i = 0; i < individualWord.Length; i++)
        {
            updatedLineOfText += theGameManager.checkIfScramble(individualWord[i]) + "  ";
        }
        //we then start the couroutine that will display the sentences of dialogue one letter at a time.
        //StartCoroutine(TextScroll(updatedLineOfText));//textLines [currentLine]));
    }

    /// <summary>
    /// Disables the text box specific to the dialogue.
    /// </summary>
    public void disableTextBox()
    {
        time = 0;
        doneActivating = false;
        done1 = false;
        done2 = false;
        done3 = false;
        //we set the text box to unactive
        textBox.SetActive(false);
        //thus the text box is no longer active
        isTextBoxActive = false;
        //playerMovement.canMove = true;\
        //we reset the time left
        time_left = 0.2f;
       // DeActivateResponceBoxes();
        if (ThoughtBubbleRequiered)
        {
            thoughtBubbleText_1.text = "";
            thoughtBubbleText_2.text = "";
            thoughtBubbleText_3.text = "";
        }
        if (thoughtBubble_1 != null || thoughtBubble_2 != null || thoughtBubble_3 != null)
        {
            thoughtBubble_1.SetActive(false);
            thoughtBubble_2.SetActive(false);
            thoughtBubble_3.SetActive(false);
            beingDisplayed = false;
        }
    }

    /// <summary>
    /// Reloads the script. Each interactable object will have its own personalized dialogue. This function will make sure to display that dialogue
    /// </summary>
    /// <param name="newText">New text.</param>
    public void reloadScript(TextAsset newText)
    {
        //makes sure there is a text file that can be parsed
        if (theText != null)
        {
            //reset the array that will contain each line of dialogue
            textLines = new string[1];
            //have the text file be split by line into the newly reset array.
            textLines = (newText.text.Split('\n'));
        }
    }

    public void reloadScript(string[] arbThought)
    {
        //playerText = arbThought;
        StopCoroutine(DisplayResponces());
        if (thoughtBubble_3 != null)
        {
            DeActivateResponceBoxes();
        }
        currentLine = 0;
        //thoughtBubbleText_1.text = "";
        //thoughtBubbleText_2.text = "";
        //thoughtBubbleText_3.text = "";
        shiftDialogueBox = false;
        beingDisplayed = false;
        displayDecisions = false;
        displayBox = false;
        answer1 = false;
        answer2 = false;
        answer3 = false;
        dialogue = arbThought;
        dialogueSize = arbThought.Length;
        Debug.Log(dialogue[0] + "blahblahblah");
        StartCoroutine(TextScroll(dialogue[0]));
        
        //enableTextBox();
    }
    public void reloadScriptNext(string arbThought)
    {
        playerText = arbThought;
        individualWord = playerText.Split(' ');
        for (int i = 0; i < individualWord.Length; i++)
        {
            updatedLineOfText += theGameManager.checkIfScramble(individualWord[i]) + "  ";
        }
        //we then start the couroutine that will display the sentences of dialogue one letter at a time.
        StartCoroutine(TextScroll(playerText));//updatedLineOfText));//textLines [currentLine]));
    }

    public void reloadThoughtBubble(GameObject hit)
    {
        PrototypeDialogueButton currentThoughtBubble = hit.GetComponent<PrototypeDialogueButton>();
        thoughtBubble_1 = currentThoughtBubble.thoughtBubble_1;
        thoughtBubble_2 = currentThoughtBubble.thoughtBubble_2;
        thoughtBubble_3 = currentThoughtBubble.thoughtBubble_3;
        thoughtBubbleText_1 = currentThoughtBubble.thoughtBubbleText_1;
        thoughtBubbleText_2 = currentThoughtBubble.thoughtBubbleText_2;
        thoughtBubbleText_3 = currentThoughtBubble.thoughtBubbleText_3;
        thoughtBubbleString_1 = currentThoughtBubble.thoughtBubbleString_1;
        thoughtBubbleString_2 = currentThoughtBubble.thoughtBubbleString_2;
        thoughtBubbleString_3 = currentThoughtBubble.thoughtBubbleString_3;
        ThoughtBubbleRequiered = true;
    }
    public void reloadThoughtBubble(GameObject hit, bool isHuman)
    {
        ThoughtBubble currentThoughtBubble = hit.GetComponent<ThoughtBubble>();
        thoughtBubble_1 = currentThoughtBubble.thoughtBubble_1;
        thoughtBubble_2 = currentThoughtBubble.thoughtBubble_2;
        thoughtBubble_3 = currentThoughtBubble.thoughtBubble_3;
        thoughtBubbleText_1 = currentThoughtBubble.thoughtBubbleText_1;
        thoughtBubbleText_2 = currentThoughtBubble.thoughtBubbleText_2;
        thoughtBubbleText_3 = currentThoughtBubble.thoughtBubbleText_3;
        thoughtBubbleString_1 = currentThoughtBubble.thoughtBubbleString_1;
        thoughtBubbleString_2 = currentThoughtBubble.thoughtBubbleString_2;
        thoughtBubbleString_3 = currentThoughtBubble.thoughtBubbleString_3;
        ThoughtBubbleRequiered = true;
    }
    public void reloadThoughtBubble(GameObject hit, bool isHuman, bool start)
    {
        StartTalkingToLoretto currentThoughtBubble = hit.GetComponent<StartTalkingToLoretto>();
        thoughtBubble_1 = currentThoughtBubble.thoughtBubble_1;
        thoughtBubble_2 = currentThoughtBubble.thoughtBubble_2;
        thoughtBubble_3 = currentThoughtBubble.thoughtBubble_3;
        thoughtBubbleText_1 = currentThoughtBubble.thoughtBubbleText_1;
        thoughtBubbleText_2 = currentThoughtBubble.thoughtBubbleText_2;
        thoughtBubbleText_3 = currentThoughtBubble.thoughtBubbleText_3;
        thoughtBubbleString_1 = currentThoughtBubble.thoughtBubbleString_1;
        thoughtBubbleString_2 = currentThoughtBubble.thoughtBubbleString_2;
        thoughtBubbleString_3 = currentThoughtBubble.thoughtBubbleString_3;
        ThoughtBubbleRequiered = true;
    }
    public void nextThoughtBubble(GameObject hit)
    {
        NewThoughtBubble currentThoughtBubble = hit.GetComponent<NewThoughtBubble>();
        thoughtBubble_1 = currentThoughtBubble.thoughtBubble_1;
        thoughtBubble_2 = currentThoughtBubble.thoughtBubble_2;
        thoughtBubble_3 = currentThoughtBubble.thoughtBubble_3;
        thoughtBubbleText_1 = currentThoughtBubble.thoughtBubbleText_1;
        thoughtBubbleText_2 = currentThoughtBubble.thoughtBubbleText_2;
        thoughtBubbleText_3 = currentThoughtBubble.thoughtBubbleText_3;
        thoughtBubbleString_1 = currentThoughtBubble.thoughtBubbleString_1;
        thoughtBubbleString_2 = currentThoughtBubble.thoughtBubbleString_2;
        thoughtBubbleString_3 = currentThoughtBubble.thoughtBubbleString_3;
        ThoughtBubbleRequiered = true;
        beingDisplayed = false;
    }
    public void setBox(GameObject thought)
    {

    }
    private IEnumerator DisplayResponces()
    {
        if (!isTextBoxActive)
        {
            DeActivateResponceBoxes();
            StopCoroutine(DisplayResponces());
        }
        endDialogueIndicator.SetActive(false);
        Debug.Log("go");
        thoughtBubble_1.SetActive (true);
        //StartCoroutine(TextScroll_TB1(thoughtBubbleString_1));//updatedLineOfText_TB1));
        answer1 = true;                                                                                    
        // thoughtBubble_1.GetComponent<buttonshapetest>().displayButtonText(updatedLineOfText_TB1, isScrambled);
       
        
        yield return new WaitForSeconds(0.2f);
        if (!isTextBoxActive)
        {
            DeActivateResponceBoxes();
            StopCoroutine(DisplayResponces());
        }
        thoughtBubble_2.SetActive (true);
        answer2 = true;
        //StartCoroutine(TextScroll_TB2(thoughtBubbleString_2));//updatedLineOfText_TB2));
        yield return new WaitForSeconds(0.2f);
        if (!isTextBoxActive)
        {
            DeActivateResponceBoxes();
            StopCoroutine(DisplayResponces());
        }
        thoughtBubble_3.SetActive(true);
        answer3 = true;
        //StartCoroutine(TextScroll_TB3(thoughtBubbleString_3));//updatedLineOfText_TB3));
        displayBox = true;
        //doneActivating = true;
    }
    public void DeActivateResponceBoxes()
    {
        thoughtBubble_1.SetActive(false);
        //yield return new WaitForSeconds(0.2f);
        thoughtBubble_2.SetActive(false);
        //yield return new WaitForSeconds(0.2f);
        thoughtBubble_3.SetActive(false);

    }
    public IEnumerator waitToClickOptions(){
        yield return new WaitForSeconds(0.1f);
        thoughtBubble_1.GetComponent<Button>().interactable = true;
        thoughtBubble_2.GetComponent<Button>().interactable = true;
        thoughtBubble_3.GetComponent<Button>().interactable = true;
    }   
}
