using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NewThoughtBubble : MonoBehaviour {

	// Use this for initialization\
	public GameObject nextThoughtBubble;
	public bool isFinalThought;
	public TextBoxManager theTextBoxManager;
	public GameObject thoughtBubble_1, thoughtBubble_2, thoughtBubble_3, currentHit;
	public Text thoughtBubbleText_1, thoughtBubbleText_2, thoughtBubbleText_3;
	public string thoughtBubbleString_1, thoughtBubbleString_2, thoughtBubbleString_3;
	public string arbThought;
	public int identifier;
    public GameObject endDialogueIndicator;
    public string[] dialogue;
    //public GameObject originalThoughtBubble;

	void Start () {
		theTextBoxManager = FindObjectOfType<TextBoxManager> ();
        //originalThoughtBubble = nextThoughtBubble;
	}
	
	// Update is called once per frame
	void Update () {
        /*if (!theTextBoxManager.isTextBoxActive)
        {
            //Debug.Log("from where is this getting accessed?");
            this.nextThoughtBubble = this.originalThoughtBubble;
        }*/
        
        if (Input.GetKeyDown (KeyCode.Mouse0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				if (hit.collider.gameObject.name == this.gameObject.name && !theTextBoxManager.isTyping) {
					isFinalThought = nextThoughtBubble.GetComponent<NextThoughtBubble> ().isFinalThought;
					if (isFinalThought) {
						thoughtBubble_1.SetActive (false);
						thoughtBubble_2.SetActive (false);
						thoughtBubble_3.SetActive (false);
						theTextBoxManager.disableTextBox ();
					} else {
						thoughtBubbleString_1 = nextThoughtBubble.GetComponent<NextThoughtBubble> ().thoughtBubbleString_1;
						thoughtBubbleString_2 = nextThoughtBubble.GetComponent<NextThoughtBubble> ().thoughtBubbleString_2;
						thoughtBubbleString_3 = nextThoughtBubble.GetComponent<NextThoughtBubble> ().thoughtBubbleString_3;
						arbThought = nextThoughtBubble.GetComponent<NextThoughtBubble> ().arbThought;
						currentHit = hit.collider.gameObject;
						theTextBoxManager.reloadScriptNext (currentHit.GetComponent<NewThoughtBubble> ().arbThought);
						theTextBoxManager.nextThoughtBubble (currentHit);

						if (identifier == 1) {
							thoughtBubble_2.GetComponent<NewThoughtBubble> ().nextThoughtBubble = nextThoughtBubble.GetComponent<NextThoughtBubble> ().otherBubble_1;
							thoughtBubble_3.GetComponent<NewThoughtBubble> ().nextThoughtBubble = nextThoughtBubble.GetComponent<NextThoughtBubble> ().otherBubble_2;
						} else if (identifier == 2) {
							thoughtBubble_1.GetComponent<NewThoughtBubble> ().nextThoughtBubble = nextThoughtBubble.GetComponent<NextThoughtBubble> ().otherBubble_1;
							thoughtBubble_3.GetComponent<NewThoughtBubble> ().nextThoughtBubble = nextThoughtBubble.GetComponent<NextThoughtBubble> ().otherBubble_2;
						} else if (identifier == 3) {
							thoughtBubble_1.GetComponent<NewThoughtBubble> ().nextThoughtBubble = nextThoughtBubble.GetComponent<NextThoughtBubble> ().otherBubble_1;
							thoughtBubble_2.GetComponent<NewThoughtBubble> ().nextThoughtBubble = nextThoughtBubble.GetComponent<NextThoughtBubble> ().otherBubble_2;
						}
						nextThoughtBubble = nextThoughtBubble.GetComponent<NextThoughtBubble> ().nextThoughtBubble;
					}
				}
			}
		}
        
	}
    public void hitButtonDialogue()
    {
        isFinalThought = nextThoughtBubble.GetComponent<NextThoughtBubble>().isFinalThought;
        if (isFinalThought)
        {
            thoughtBubble_1.SetActive(false);
            thoughtBubble_2.SetActive(false);
            thoughtBubble_3.SetActive(false);
            theTextBoxManager.disableTextBox();
        }
        else
        {
            if (theTextBoxManager.doneActivating)
            {
                theTextBoxManager.currentLine = 0;
                theTextBoxManager.done1 = false;
                theTextBoxManager.done2  = false;
                theTextBoxManager.done3 = false;
                theTextBoxManager.doneActivating = false;
                theTextBoxManager.thoughtBubbleText_1.text = "";
                theTextBoxManager.thoughtBubbleText_2.text = "";
                theTextBoxManager.thoughtBubbleText_3.text = "";
                thoughtBubbleString_1 = nextThoughtBubble.GetComponent<NextThoughtBubble>().thoughtBubbleString_1;
                thoughtBubbleString_2 = nextThoughtBubble.GetComponent<NextThoughtBubble>().thoughtBubbleString_2;
                thoughtBubbleString_3 = nextThoughtBubble.GetComponent<NextThoughtBubble>().thoughtBubbleString_3;
                //arbThought = nextThoughtBubble.GetComponent<NextThoughtBubble>().arbThought;
                dialogue = nextThoughtBubble.GetComponent<NextThoughtBubble>().dialogue;
                //currentHit = hit.collider.gameObject;
                thoughtBubble_1.SetActive(false);
                thoughtBubble_2.SetActive(false);
                thoughtBubble_3.SetActive(false);
                theTextBoxManager.reloadScript(gameObject.GetComponent<NewThoughtBubble>().dialogue);
                theTextBoxManager.nextThoughtBubble(gameObject);
                theTextBoxManager.DeActivateResponceBoxes();


                if (identifier == 1)
                {
                    thoughtBubble_2.GetComponent<NewThoughtBubble>().nextThoughtBubble = nextThoughtBubble.GetComponent<NextThoughtBubble>().otherBubble_1;
                    thoughtBubble_3.GetComponent<NewThoughtBubble>().nextThoughtBubble = nextThoughtBubble.GetComponent<NextThoughtBubble>().otherBubble_2;
                }
                else if (identifier == 2)
                {
                    thoughtBubble_1.GetComponent<NewThoughtBubble>().nextThoughtBubble = nextThoughtBubble.GetComponent<NextThoughtBubble>().otherBubble_1;
                    thoughtBubble_3.GetComponent<NewThoughtBubble>().nextThoughtBubble = nextThoughtBubble.GetComponent<NextThoughtBubble>().otherBubble_2;
                }
                else if (identifier == 3)
                {
                    thoughtBubble_1.GetComponent<NewThoughtBubble>().nextThoughtBubble = nextThoughtBubble.GetComponent<NextThoughtBubble>().otherBubble_1;
                    thoughtBubble_2.GetComponent<NewThoughtBubble>().nextThoughtBubble = nextThoughtBubble.GetComponent<NextThoughtBubble>().otherBubble_2;
                }
                nextThoughtBubble = nextThoughtBubble.GetComponent<NextThoughtBubble>().nextThoughtBubble;
           }

        }
    }
	public void resetNextBubble(GameObject hit, GameObject bubble){
		hit.GetComponent<NewThoughtBubble>().nextThoughtBubble = bubble;
	}
}
