using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConfirmExit : MonoBehaviour {
    public GameObject dialoguePanel;
    public Text theText;
    public Button Confirm, Reject;
    public string LeaveDialogue, scrambled;
    public bool isDialoguePanelActive, isTyping, coroutineIsHappening, isBar, attemptToLeave;
    public int currentLetter, currentLetterTranslation;
    public float typingSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (isTyping)
            {
                isTyping = false;
            }
            else if(attemptToLeave){
                
                stay();
            }else
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.name == this.gameObject.name)
                    {
                        isDialoguePanelActive = true;
                    }
                }
            }
        }
        if (!isDialoguePanelActive)
        {
            return;
        }else if(!coroutineIsHappening)
        {
            coroutineIsHappening = true;
            dialoguePanel.SetActive(true);
            StartCoroutine(displayExitDialogue(LeaveDialogue));
        }
   }

    public void ActivateButtons()
    {
        Confirm.gameObject.SetActive(true);
        Reject.gameObject.SetActive(true);
    }
    public void Leave()
    {
        if (isBar)
        {
            if (QuestManager.currentContract == 0)
            {
                Confirm.gameObject.SetActive(false);
                Reject.gameObject.SetActive(false);
                StartCoroutine(displayExitDialogue("I need to pickup my contract first"));
                attemptToLeave = true;
            }
        } else {
            Confirm.gameObject.SetActive(false);
            Reject.gameObject.SetActive(false);
            dialoguePanel.SetActive(false);
            isDialoguePanelActive = false;
            coroutineIsHappening = false;
            attemptToLeave = false;
            SceneManager.LoadScene("MainMap");
        }
        
    }
    public void stay()
    {
        Confirm.gameObject.SetActive(false);
        Reject.gameObject.SetActive(false);
        dialoguePanel.SetActive(false);
        isDialoguePanelActive = false;
        coroutineIsHappening = false;
        attemptToLeave = false;
    }
    public IEnumerator displayExitDialogue(string LeaveDialogue)
    {
        scrambled = "";
        for (int i = 0; i < LeaveDialogue.Length; i++)
        {
            scrambled += gameManager.symbols[Random.Range(0, 23)];
        }
        theText.text = "";
        isTyping = true;
        currentLetter = 0;
        while(currentLetter<LeaveDialogue.Length && isTyping)
        {
            theText.text += scrambled[currentLetter];
            currentLetter += 1;
            if (currentLetter == scrambled.Length / 2)
            {
                StartCoroutine(displayExitDialogueTranslation(LeaveDialogue));
            }
            yield return new WaitForSeconds(typingSpeed);
        }
        

    }
    public IEnumerator displayExitDialogueTranslation(string LeaveDialogue)
    {
        currentLetterTranslation = 0;
        while (currentLetterTranslation < LeaveDialogue.Length && isTyping)
        {
            theText.text = theText.text.Remove(currentLetterTranslation, 1);
            theText.text = theText.text.Insert(currentLetterTranslation, LeaveDialogue[currentLetterTranslation].ToString());
            currentLetterTranslation += 1;
            yield return new WaitForSeconds(typingSpeed);
        }
        theText.text = LeaveDialogue;
        isTyping = false;
        if (!attemptToLeave)
        {
            ActivateButtons();
        }
    }
}
