﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class objectPickupManager : MonoBehaviour {


	//keep track of the currentline of dialogue we are at
	public int currentLine;
	//checks what the last line of dialogue that needs to be displayed.
	public int endAtLine;
	//checks whether the dialogue box is active or not
	public bool isTextBoxActive, isTyping, cancelTyping;
	float time_left;
	public string[] textLines;
	public TranslatorManager theTranslatorManager;
	public Text theText;
	public float typeSpeed;
	public Button pickUp, leave;
	public bool pickUpChoice, isActive;
	public GameObject textBox, clickedObject;
	public TextAsset placeHolder;
	public static bool notebook, charger;
	public static bool notebookInv, chargerInv;
	public Image[] InventorySlot;
	public static bool[] slotOpen = new bool[4];
	//public GameObject notebookObj, chargerObj;
    public static GameObject notebookObjSt, chargerObjSt;
	public bool isDropOff;
    public  GameObject[] startPanel;
    public static bool start;
    Scene scene;
    public GameObject canvas;
    public static bool outOfScene;
	public GameObject o1, o2, o3, o4;
    public PlayerStatistics savedPlayerData = new PlayerStatistics();
    public static int[] inventoryReference = new int[4];
    public static int[] droppedInEndRef = new int[4];
    public GameObject[] ReferenceObjects;
    public string[] InvString = new string[4];

    // Use this for initialization
	//private void Awake(){
		//DontDestroyOnLoad (o1);
		//DontDestroyOnLoad (o2);
		//DontDestroyOnLoad (o3);
		//DontDestroyOnLoad (o4);
	//}
   /* private void Awake()
    {
        scene = SceneManager.GetActiveScene();
        DontDestroyOnLoad(transform.gameObject);
        DontDestroyOnLoad(canvas);
        if (start)
        {
            
            for (int i = 0; i < 4; i++)
            {
                //startPanel[i];
               DontDestroyOnLoad(startPanel);
            }
        }
        //DontDestroyOnLoad(InventorySlot);
    }*/
    
	void Start () {
        theTranslatorManager = FindObjectOfType<TranslatorManager>();
       /* if (scene.name == "Classroom" && !outOfScene )
        {
            if (!start)
            {
                Debug.Log("Boom");
                start = true;
                notebookObjSt = notebookObj;
                chargerObjSt = chargerObj;
                InventorySlot = new Image[startPanel.Length];
                slotOpen = new bool[startPanel.Length];
                for (int i = 0; i < startPanel.Length; i++)
                {
                    objectPickupManager.InventorySlot[i] = startPanel[i];
                }
            }*/
		//InventorySlot = GlobalControl.Instance.inventory;
		//chargerObj = GlobalControl.Instance.chargerObj;
		//notebookObj = GlobalControl.Instance.notebookObj;
		//slotOpen = GlobalControl.Instance.slotOpen;
        //theTranslatorManager = FindObjectOfType<TranslatorManager>();
            //make sure that a textfile with the dialogue has been inputed
            if (placeHolder != null)
            {
                //so that we can then split the dialogue from the textfile into the specific array
                textLines = (placeHolder.text.Split('\n'));
            }

            //if no end at line has been specified then it should be the last line of the textfile.
            if (endAtLine == 0)
            {
                endAtLine = textLines.Length - 1;
            }
      /*  }
        else if(scene.name != "Classroom")
        {
            Debug.Log("We are out of the scene");
            outOfScene = true;
        }*/
	}
	
	// Update is called once per frame
	void Update () {
       // if (scene.name == "Classroom")
       // {
       for(int i = 0; i < inventoryReference.Length; i++)
        {
            for(int j = 0; j< ReferenceObjects.Length; j++)
            {
                if(inventoryReference[i] == 0)
                {
                    InventorySlot[i].GetComponent<Image>().sprite = null;
                }else if(inventoryReference[i] == j)
                {
                    InventorySlot[i].GetComponent<Image>().sprite = ReferenceObjects[j].GetComponent<ObjectInfo>().objectSprite.sprite;
                    InventorySlot[i].GetComponent<InventoryButton>().currentObjectInSlot = ReferenceObjects[j];
                    ReferenceObjects[j].GetComponent<ObjectInfo>().inInv = true;
                    InventorySlot[i].GetComponent<InventoryButton>().OriginalLocation = SceneManager.GetActiveScene().name;
                }
            }
        }
            alphaCheck();
        /*if (SceneManager.GetActiveScene().name.Trim().Equals("Classroom".Trim()))
        {
            notebookObj = GameObject.FindGameObjectWithTag("notebook");
            chargerObj = GameObject.FindGameObjectWithTag("charger");

        }*/
           if (!SceneManager.GetActiveScene().name.Trim().Equals("Location Selection".Trim()) && Input.GetKeyDown(KeyCode.Mouse0) && !theTranslatorManager.panelIsActive && !pickUpChoice && isActive )
            {
                //checks that all the letters of the specific dialogue line have been displayed
                if (!isTyping)
                {
                    //if yes then we move on to the next line
                    currentLine += 1;
                    //we check if we have passed the final line of dialogue
                    if (currentLine >= endAtLine && !isDropOff)
                    {
                        StartCoroutine(TextScroll(textLines[currentLine]));
                        //if so we call the disable text box function and we reset the current line variable
                        pickUpChoice = true;
                        pickUp.gameObject.SetActive(true);
                        leave.gameObject.SetActive(true);

                    }
                    else
                    {
                        StartCoroutine(TextScroll(textLines[currentLine]));//textLines [currentLine]));
                    }
                    //If the user clicks but the letters are still being displayed then we need to cancel the typing as to show the full line of dialogue immediately
                }
                else if (isTyping && !cancelTyping && time_left < 0)
                {
                    cancelTyping = true;
                }
            }
            if (currentLine > endAtLine && isDropOff)
            {

                disableTextBox();
                currentLine = 0;
            }
       // }
	}

	public void enableTextBox(){
		//we set the text box to active
		textBox.SetActive (true);
		isActive = true;
		//the text box is thus currently active
		isTextBoxActive = true;
		//if (stopPlayerMovement) {
		//playerMovement.canMove = false;
		//}

		//we represt the same process as above to check if the words witin the line of dialogue need to be scrambled, and then updated the line of text accordingly 
		//individualWord = textLines[currentLine].Split (' ');
		//for (int i = 0; i < individualWord.Length; i++) {
		//	updatedLineOfText += theGameManager.checkIfScramble (individualWord [i]) + "  ";
		//}
		//we then start the couroutine that will display the sentences of dialogue one letter at a time.
		StartCoroutine (TextScroll(textLines[currentLine]));//textLines [currentLine]));
	}

	public void reloadScript(TextAsset newText, GameObject theObject){
		clickedObject = theObject;
		isDropOff = false;
		//makes sure there is a text file that can be parsed
		if (theText != null) {
			//reset the array that will contain each line of dialogue
			textLines = new string[1]; 
			//have the text file be split by line into the newly reset array.
			textLines = (newText.text.Split('\n'));
		}
	}
	public void reloadScript(TextAsset newText){
		isDropOff = true;
		if (theText != null) {
			//reset the array that will contain each line of dialogue
			textLines = new string[1]; 
			//have the text file be split by line into the newly reset array.
			textLines = (newText.text.Split('\n'));
		}
	}
	
	public void disableTextBox(){
		currentLine = 0;
		pickUpChoice = false;
		pickUp.gameObject.SetActive(false);
		leave.gameObject.SetActive (false);
		textBox.SetActive (false);
		isActive = false;
        isTextBoxActive = false;
	}

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
		//individualWord = new string[1];
		//we do the same for the updated line of text
		//updatedLineOfText = "";
	}
	public void pickedUp(){
       /* Debug.Log("picked up got accessed");
		if (clickedObject.name.Equals ("notebook")) {
			notebook = true;

		} else if (clickedObject.name.Equals ("charger")) {
            Debug.Log("Charger got picked up");
			charger = true;
		}*/
        clickedObject.GetComponent<ObjectInfo>().objectSprite.enabled = false;
		//clickedObject.GetComponent<PickUpObject> ().chargerSprite.enabled = false;
		//clickedObject.SetActive (false);
        clickedObject.GetComponent<ObjectInfo>().droppedInEnd = false;
		disableTextBox ();
        checkInventory();
    }
	public void left(){
        clickedObject.GetComponent<ObjectInfo>().objectSprite.enabled = false;
        clickedObject.GetComponent<PickUpObject>().leave = true;
        //clickedObject.GetComponent<PickUpObject> ().notebookSprite.enabled = false;
		//clickedObject.GetComponent<PickUpObject> ().chargerSprite.enabled = false;
		//clickedObject.SetActive (false);
		disableTextBox ();
	}

	public void checkInventory(){
       /* if (notebook)
        {
            notebookObj.SetActive(false);
        }else if (charger)
        {
            chargerObj.SetActive(false);
        }*/
		//if (notebook) {
        for (int i = 0; i < droppedInEndRef.Length; i++ )
        {
            if(droppedInEndRef[i] == clickedObject.GetComponent<ObjectInfo>().reference) {
                Debug.Log("remove from ref");
                droppedInEndRef[i] = 0;
            }
        }
			for (int i = 0; i < slotOpen.Length; i++) {
				if (!slotOpen[i]) {
					Debug.Log ("got up to change sprite");
					slotOpen [i] = true;
					//InventorySlot [i].GetComponent<Image> ().sprite = notebookObj.GetComponent<PickUpObject> ().notebookSprite.sprite;
                    //InventorySlot[i].GetComponent<InventoryButton>().currentObjectInSlot = notebookObj;
                    //InventorySlot[i].GetComponent<InventoryButton>().OriginalLocation = SceneManager.GetActiveScene().name;
                    clickedObject.GetComponent<ObjectInfo>().inInv = true;
                    clickedObject.GetComponent<ObjectInfo>().InvIndex = i;
                    inventoryReference[i] = clickedObject.GetComponent<ObjectInfo>().reference;
                    //notebookInv = true;
					break;
				}
                
			}

		/*}else if (charger) {
			for (int i = 0; i < slotOpen.Length; i++) {
				if (!slotOpen[i]) {
					Debug.Log ("got up to change sprite");
					slotOpen [i] = true;
					//InventorySlot [i].GetComponent<Image> ().sprite = chargerObj.GetComponent<PickUpObject> ().chargerSprite.sprite;
                    //InventorySlot[i].GetComponent<InventoryButton>().currentObjectInSlot = chargerObj;
                    //InventorySlot[i].GetComponent<InventoryButton>().OriginalLocation = SceneManager.GetActiveScene().name;
                    chargerObj.GetComponent<ObjectInfo>().inInv = true;
                    chargerObj.GetComponent<ObjectInfo>().InvIndex = i;
                    inventoryReference[i] = 2;
                    //chargerInv = true;
					break;
				}
			}

		}
        notebook = false;
        charger = false;*/
	}
    public void checkInventory(GameObject currentObject)
    {
        Debug.Log("is this getting accessed - objectpickupmanager");
        clickedObject = currentObject;
        /* if (notebook)
         {
             notebookObj.SetActive(false);
         }else if (charger)
         {
             chargerObj.SetActive(false);
         }*/
        //if (notebook) {
        for (int i = 0; i < droppedInEndRef.Length; i++)
        {
            if (droppedInEndRef[i] == clickedObject.GetComponent<ObjectInfo>().reference)
            {
                Debug.Log("remove from ref");
                droppedInEndRef[i] = 0;
            }
        }
        for (int i = 0; i < slotOpen.Length; i++)
        {
            if (!slotOpen[i])
            {
                Debug.Log("got up to change sprite");
                slotOpen[i] = true;
                //InventorySlot [i].GetComponent<Image> ().sprite = notebookObj.GetComponent<PickUpObject> ().notebookSprite.sprite;
                //InventorySlot[i].GetComponent<InventoryButton>().currentObjectInSlot = notebookObj;
                //InventorySlot[i].GetComponent<InventoryButton>().OriginalLocation = SceneManager.GetActiveScene().name;
                clickedObject.GetComponent<ObjectInfo>().inInv = true;
                clickedObject.GetComponent<ObjectInfo>().InvIndex = i;
                inventoryReference[i] = clickedObject.GetComponent<ObjectInfo>().reference;
                //notebookInv = true;
                break;
            }

        }

        /*}else if (charger) {
			for (int i = 0; i < slotOpen.Length; i++) {
				if (!slotOpen[i]) {
					Debug.Log ("got up to change sprite");
					slotOpen [i] = true;
					//InventorySlot [i].GetComponent<Image> ().sprite = chargerObj.GetComponent<PickUpObject> ().chargerSprite.sprite;
                    //InventorySlot[i].GetComponent<InventoryButton>().currentObjectInSlot = chargerObj;
                    //InventorySlot[i].GetComponent<InventoryButton>().OriginalLocation = SceneManager.GetActiveScene().name;
                    chargerObj.GetComponent<ObjectInfo>().inInv = true;
                    chargerObj.GetComponent<ObjectInfo>().InvIndex = i;
                    inventoryReference[i] = 2;
                    //chargerInv = true;
					break;
				}
			}

		}
        notebook = false;
        charger = false;*/
    }
    public void alphaCheck()
    {
        for (int i = 0; i < slotOpen.Length; i++)
        {
            Color color = InventorySlot[i].GetComponent<Image>().color;
            if (slotOpen[i])
            {
                InventorySlot[i].GetComponent<Button>().interactable = true;
                color.a = 255;
            }
            else
            {
                InventorySlot[i].GetComponent<Button>().interactable = false;
                color.a = 0;
            }
            InventorySlot[i].GetComponent<Image>().color = color;
        }
    }

	public void SaveInventory(){

		GlobalControl.Instance.o1 = o1;
		GlobalControl.Instance.o2 = o2;
		GlobalControl.Instance.o3 = o3;
		GlobalControl.Instance.o4 = o4;
		GlobalControl.Instance.inventory = InventorySlot;
		GlobalControl.Instance.slotOpen = slotOpen;
		//GlobalControl.Instance.notebookObj = notebookObj;
		//GlobalControl.Instance.chargerObj = chargerObj;
	}

	
}
