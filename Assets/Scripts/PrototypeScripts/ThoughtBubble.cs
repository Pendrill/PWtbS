using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThoughtBubble : MonoBehaviour {
	//the text game object that will display the dialogue/text
	public TextAsset theText;

	//ints that keep track of where we start and end the dialogue (in regards to the text line)
	public int startLine;
	public int endLine;

	//get the script and game object that contain the textboxmanager info and functions
	public TextBoxManager theTextBoxManager;

	//bools that check whether or not we need to destroy the object once we talked to it (currently never being used)
	//and whether or not the object that is being interacted with is the translator character.
	public bool destroyWhenActivated, disableObject, isTranslator;

	//bool that checks if a button press is requiered to intereact with the specific character/object
	public bool requireButtonPress;

	//OBSOLETE
	//bool waitForPress;

	//checks if the object that has been clicked on is interactable
	public bool interactable;

	//canTalk is a game object that represents a mini dialogue box that appears over the specific character indicating that you can talk to them
	public GameObject canTalk;
	//bool to check if specific object has the dialogue box object mentionned above attached to it
	public bool hasCanTalk;

	//Never ended up using this
	//static string nameOfHit;

	//reference to the script and object that controls the camera when dialogue is activated
	public MoveCameraDialogue MoveCameraDialogue;

	// Vector 3 that keeps track of where the mini dialogue box mentionned above needs to be located in relation to its associated character
	public Vector3 canTalkDialogueBoxOffset;

	//reference to the translator manager object and script
	public TranslatorManager theTranslatorManager;

	public static bool charger, notebook;

	public Image chargerSprite;
	public Image notebookSprite;

	public bool behind;
	public RotateCamera theRotateCamera;
	public Vector3 specificOffset;
	public RotateMouseClick theRotateMouseClick;

	public GameObject thoughtBubble_1, thoughtBubble_2, thoughtBubble_3;
	public Text thoughtBubbleText_1, thoughtBubbleText_2, thoughtBubbleText_3;
	public string thoughtBubbleString_1, thoughtBubbleString_2, thoughtBubbleString_3;
	public bool ThoughtBubbleRequired = true;
	public GameObject currentHit, originalThoughtBubble_1, originalThoughtBubble_2, originalThoughtBubble_3;
	public string arbThought;
	public bool isHuman;
	public Vector3 thoughtBubble1Pos, thoughtBubble2Pos, thoughtBubble3Pos;
	public GameObject TB1, TB2, TB3;
    public GameObject panel1, panel2, panel3;
    public buttonshapetest BSTpanel1, BSTpanel2, BSTpanel3;
    //public NewThoughtBubble theNewThoughtBubble;
    // Use this for initialization
    void Start () {
        if (isHuman)
        {
            BSTpanel1 = panel1.GetComponent<buttonshapetest>();
            BSTpanel2 = panel2.GetComponent<buttonshapetest>();
            BSTpanel3 = panel3.GetComponent<buttonshapetest>();
        }
        //we find the specific objects in the scene as to be able to access their functions
        //thoughtBubble_1.GetComponent<NewThoughtBubble>().nextThoughtBubble = TB1;
        //thoughtBubble_2.GetComponent<NewThoughtBubble>().nextThoughtBubble = TB2;
        //thoughtBubble_3.GetComponent<NewThoughtBubble>().nextThoughtBubble = TB3;
        theTranslatorManager = FindObjectOfType<TranslatorManager> ();
		theTextBoxManager = FindObjectOfType<TextBoxManager> ();
		MoveCameraDialogue = FindObjectOfType<MoveCameraDialogue> ();
		//theRotateCamera = FindObjectOfType<RotateCamera>();
		theRotateMouseClick = FindObjectOfType<RotateMouseClick> ();
		//theNewThoughtBubble = FindObjectOfType<NewThoughtBubble> ();

        //originalThoughtBubble_1 = thoughtBubble_1.GetComponent<NewThoughtBubble>().nextThoughtBubble;
        //originalThoughtBubble_2 = thoughtBubble_2.GetComponent<NewThoughtBubble>().nextThoughtBubble;
        //originalThoughtBubble_3 = thoughtBubble_3.GetComponent<NewThoughtBubble>().nextThoughtBubble;
		//thoughtBubble_1.transform.position = thoughtBubble1Pos;
		//thoughtBubble_2.transform.position = thoughtBubble2Pos;
		//thoughtBubble_3.transform.position = thoughtBubble3Pos;
	}

	// Update is called once per frame
	void Update () {
        if (!theTextBoxManager.isTextBoxActive)
        {
            //thoughtBubble_1.GetComponent<NewThoughtBubble>().nextThoughtBubble = originalThoughtBubble_1;
            //thoughtBubble_2.GetComponent<NewThoughtBubble>().nextThoughtBubble = originalThoughtBubble_2;
            //thoughtBubble_3.GetComponent<NewThoughtBubble>().nextThoughtBubble = originalThoughtBubble_3;
        }
		//we set it so that the characters/objects are always facing the player
		transform.LookAt (MoveCameraDialogue.transform);

		//if the dialogue textbox is active and the camera is not in its original position
		if (theTextBoxManager.isTextBoxActive || MoveCameraDialogue.transform.position != MoveCameraDialogue.OriginalCameraPosition || theTranslatorManager.panelIsActive) {
			//then the mini dialogue box should not be displayed
			//canTalk.SetActive (false);
			//On the other hand, if the object is interactable then we enable the hoverOverObject function
		} else if(interactable){
			//hoverOverObject ();
		}

		//If the player clicks on the mouse, and we are currently not in dialogue, the camera is not moving, and we are not currently translating words, that means the player is trying to interact with an object in the world
		if (Input.GetKeyDown (KeyCode.Mouse0) && !theTextBoxManager.isTextBoxActive && MoveCameraDialogue.transform.position == MoveCameraDialogue.OriginalCameraPosition && !theTranslatorManager.panelIsActive) {
			//this is some test out shit

			//we create a ray cast that is emmited forward from the position of the mouse
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			//if there is a hit
			if (Physics.Raycast (ray, out hit) ) {
				//we only want to run this for the specific object that got hit (as this script will be attached to many objects)
				if (hit.collider.gameObject.name == this.gameObject.name && interactable) {
					currentHit = hit.collider.gameObject;
					//Debug.Log (hit.transform.name);
					//Debug.Log (theText);
					//we check if the object is the translator character
					if (isTranslator) {
						//if so then we need to check the translations offered by the player
						theTranslatorManager.startTranslatingJournal ();
					}
                    /*if(hit.collider.gameObject.name.Equals("notebook")){
						ActivateTextAtLine.notebook = true;
                        notebookSprite.enabled = true;
					}else if(hit.collider.gameObject.name.Equals("charger")){
						ActivateTextAtLine.charger = true;                       
                        chargerSprite.enabled = true;
					}*/
                    //otherwise we need to zoom in the camera towards the object that got hit by the raycast
                    theTextBoxManager.GetComponent<TextBoxManager>().isHuman = isHuman;
					MoveCameraDialogue.moveTowardObject (currentHit, specificOffset);
					//and then we need to update the dialogue text, start, and end line
					theTextBoxManager.reloadScript (currentHit.GetComponent<ThoughtBubble> ().arbThought);
					theTextBoxManager.currentLine = currentHit.GetComponent<ThoughtBubble> ().startLine;
					theTextBoxManager.endAtLine = currentHit.GetComponent<ThoughtBubble> ().endLine;
					theTextBoxManager.reloadThoughtBubble (currentHit, isHuman);
					if (isHuman) {
						//theTextBoxManager.textBox.GetComponent<RectTransform> ().localPosition = new Vector3 (0, -220, 0);
                        BSTpanel1.nextButton_1 = TB1;
                        BSTpanel2.nextButton_1 = TB2;
                        BSTpanel3.nextButton_1 = TB3;

                        BSTpanel1.nextText1 = thoughtBubbleString_1;
                        BSTpanel2.nextText1 = thoughtBubbleString_2;
                        BSTpanel3.nextText1 = thoughtBubbleString_3;
                    } else {
						//theTextBoxManager.textBox.GetComponent<RectTransform> ().localPosition = new Vector3 (0, 220, 0);
                        //Finally we have a coroutine that starts so as to wait that the camera has zoomed in
                        //Debug.Log("is reached");
                        //thoughtBubble_1.transform.position = thoughtBubble1Pos;
                        //thoughtBubble_2.transform.position = thoughtBubble2Pos;
                        //thoughtBubble_3.transform.position = thoughtBubble3Pos;
                        //theNewThoughtBubble.resetNextBubble (thoughtBubble_1, TB1);
                        //theNewThoughtBubble.resetNextBubble (thoughtBubble_2, TB2);
                        //theNewThoughtBubble.resetNextBubble (thoughtBubble_3, TB3);
                        thoughtBubble_1.GetComponent<NewThoughtBubble>().nextThoughtBubble = hit.collider.gameObject.GetComponent<ThoughtBubble>().TB1;
                        thoughtBubble_2.GetComponent<NewThoughtBubble>().nextThoughtBubble = hit.collider.gameObject.GetComponent<ThoughtBubble>().TB2;
                        thoughtBubble_3.GetComponent<NewThoughtBubble>().nextThoughtBubble = hit.collider.gameObject.GetComponent<ThoughtBubble>().TB3;
                        thoughtBubble_1.GetComponent<NewThoughtBubble>().thoughtBubbleText_1 = hit.collider.gameObject.GetComponent<ThoughtBubble>().thoughtBubbleText_1;
                        thoughtBubble_1.GetComponent<NewThoughtBubble>().thoughtBubbleText_2 = hit.collider.gameObject.GetComponent<ThoughtBubble>().thoughtBubbleText_2;
                        thoughtBubble_1.GetComponent<NewThoughtBubble>().thoughtBubbleText_3 = hit.collider.gameObject.GetComponent<ThoughtBubble>().thoughtBubbleText_3;
                        thoughtBubble_2.GetComponent<NewThoughtBubble>().thoughtBubbleText_1 = hit.collider.gameObject.GetComponent<ThoughtBubble>().thoughtBubbleText_1;
                        thoughtBubble_2.GetComponent<NewThoughtBubble>().thoughtBubbleText_2 = hit.collider.gameObject.GetComponent<ThoughtBubble>().thoughtBubbleText_2;
                        thoughtBubble_2.GetComponent<NewThoughtBubble>().thoughtBubbleText_3 = hit.collider.gameObject.GetComponent<ThoughtBubble>().thoughtBubbleText_3;
                        thoughtBubble_3.GetComponent<NewThoughtBubble>().thoughtBubbleText_1 = hit.collider.gameObject.GetComponent<ThoughtBubble>().thoughtBubbleText_1;
                        thoughtBubble_3.GetComponent<NewThoughtBubble>().thoughtBubbleText_2 = hit.collider.gameObject.GetComponent<ThoughtBubble>().thoughtBubbleText_2;
                        thoughtBubble_3.GetComponent<NewThoughtBubble>().thoughtBubbleText_3 = hit.collider.gameObject.GetComponent<ThoughtBubble>().thoughtBubbleText_3;
                        originalThoughtBubble_1 = thoughtBubble_1.GetComponent<NewThoughtBubble>().nextThoughtBubble;
                        originalThoughtBubble_2 = thoughtBubble_2.GetComponent<NewThoughtBubble>().nextThoughtBubble;
                        originalThoughtBubble_3 = thoughtBubble_3.GetComponent<NewThoughtBubble>().nextThoughtBubble;
                    }
					

					StartCoroutine (waitToDisplayDialogueBox ());

					//theTextBoxManager.enableTextBox ();
				} /*else if (!interactable) {
					
					Debug.Log ("If this shows up, then this is bad");
					
				}*/
			} /*else {
				Debug.Log ("If this shows up, then this is bad");
			}*/


			if (disableObject && !theTextBoxManager.isTextBoxActive) {
				gameObject.SetActive (false);
				notebookSprite.enabled = false;
			}
		}
		if (disableObject && !theTextBoxManager.isTextBoxActive) {
			gameObject.SetActive (false);
			chargerSprite.enabled = false;
			notebookSprite.enabled = false;
		}
		if (ActivateTextAtLine.charger) {
			//Debug.Log ("hello");
		}
	}

	/// <summary>
	/// Hovers  over the object. Checks whether or not we need to display the mini dialogue box next to the interactable object
	/// </summary>
	void hoverOverObject(){
		//creates an other raycast from the mouse
		Ray hover = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitHover;

		//check that it hit something, and then run it again solely for the specific object that got hit
		if (Physics.Raycast (hover, out hitHover) && hitHover.collider.gameObject.name == this.gameObject.name) {
			//if this is the first time the mouse hovered over the object
			if (!hasCanTalk ) {
				//then we instantiate the mini dialogue box next to the character
				canTalk = Instantiate (canTalk, hitHover.transform.position + canTalkDialogueBoxOffset, hitHover.transform.rotation) as GameObject;
				hasCanTalk = true;
			}
			//canTalk.transform.position = transform.position + new Vector3 (0.63f, 1.32f, 0f);
			//we no longer need to instantiate the mini dialogue box, we just set it to active
			canTalk.SetActive (true);
			//once the mouse move away from the character/object, we disable the mini dialogue box
		} else {			
			canTalk.SetActive (false);
		}
	}

	/*void OnTriggerEnter(Collider other){
		if (other.name == "Player1") {

			if (requireButtonPress) {
				waitForPress = true;
				return;
			}
			theTextBoxManager.reloadScript (theText);
			theTextBoxManager.currentLine = startLine;
			theTextBoxManager.endAtLine = endLine;
			theTextBoxManager.enableTextBox ();

			if (destroyWhenActivated) {
				Destroy (gameObject);
			}
		}
	}

	void OnTriggerExit(Collider other){
		if (other.name == "Player1") {
			waitForPress = false;
		}
	}*/

	/// <summary>
	/// Waits to display dialogue box during the zoom in of the camera.
	/// </summary>
	/// <returns>The to display dialogue box.</returns>
	private IEnumerator waitToDisplayDialogueBox()
	{
		yield return new WaitForSeconds(0.4f);
		theTextBoxManager.enableTextBox(isHuman);
        if (isHuman)
        {
            thoughtBubble_1.SetActive (true);
            thoughtBubble_2.SetActive (true);
            thoughtBubble_3.SetActive(true);
        }

		if (destroyWhenActivated)
		{
			disableObject = true;
		}
		//theRotateCamera.back = behind;
		theRotateMouseClick.back = behind;
	}
	private IEnumerator waitToDisplayChargerSprite()
	{
		yield return new WaitForSeconds(0.4f);
		theTextBoxManager.enableTextBox();
		notebookSprite.enabled = true;
		chargerSprite.enabled = true;

		if (destroyWhenActivated)
		{
			disableObject = true;
		}
	}

}
