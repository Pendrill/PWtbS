using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PickUpObject : MonoBehaviour {
	public TextBoxManager theTextBoxManager;
	public MoveCameraDialogue MoveCameraDialogue;
	public TranslatorManager theTranslatorManager;
	public objectPickupManager theObjectPickupManager;
	public Image chargerSprite, notebookSprite;
	public int startLine, endLine;
	public TextAsset theText;
    public Vector3 specificOffset;
    // Use this for initialization
    void Start () {
		theTranslatorManager = FindObjectOfType<TranslatorManager> ();
		theTextBoxManager = FindObjectOfType<TextBoxManager> ();
		MoveCameraDialogue = FindObjectOfType<MoveCameraDialogue> ();
		theObjectPickupManager = FindObjectOfType<objectPickupManager> ();
	}
	
	// Update is called once per frame
	void Update () {
        if (SceneManager.GetActiveScene().name.Trim().Equals("Classroom".Trim()) || SceneManager.GetActiveScene().name.Trim().Equals("StudentBedroom".Trim()))
        {
            notebookSprite = GameObject.FindGameObjectWithTag("notebookSprite").GetComponent<Image>();
            chargerSprite = GameObject.FindGameObjectWithTag("chargerSprite").GetComponent<Image>();
            theTextBoxManager = GameObject.FindGameObjectWithTag("TextBoxManager").GetComponent<TextBoxManager>();
            MoveCameraDialogue = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MoveCameraDialogue>();

        }
        if (!SceneManager.GetActiveScene().name.Trim().Equals("Location Selection".Trim()) && Input.GetKeyDown (KeyCode.Mouse0) && !theTextBoxManager.isTextBoxActive && MoveCameraDialogue.transform.position == MoveCameraDialogue.OriginalCameraPosition && !theTranslatorManager.panelIsActive) {
			//we create a ray cast that is emmited forward from the position of the mouse
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			//if there is a hit
			if (Physics.Raycast (ray, out hit)) {
				//we only want to run this for the specific object that got hit (as this script will be attached to many objects)
				if (hit.collider.gameObject.name == this.gameObject.name ) {
					Debug.Log (hit.collider.gameObject.name);
					if(hit.collider.gameObject.name.Equals("notebook")){
						//ActivateTextAtLine.notebook = true;
						notebookSprite.enabled = true;
					}else if(hit.collider.gameObject.name.Equals("charger")){
						//ActivateTextAtLine.charger = true;                       
						chargerSprite.enabled = true;
					}
					//otherwise we need to zoom in the camera towards the object that got hit by the raycast
					MoveCameraDialogue.moveTowardObject (hit.transform.gameObject, specificOffset);
					//and then we need to update the dialogue text, start, and end line
					theObjectPickupManager.reloadScript (hit.transform.gameObject.GetComponent<PickUpObject> ().theText, hit.transform.gameObject);
					theObjectPickupManager.currentLine = hit.transform.gameObject.GetComponent<PickUpObject> ().startLine;
					theObjectPickupManager.endAtLine = hit.transform.gameObject.GetComponent<PickUpObject> ().endLine;
                    theObjectPickupManager.textBox.GetComponent<RectTransform>().localPosition = new Vector3(0, -220, 0);
                    //Finally we have a coroutine that starts so as to wait that the camera has zoomed in
                    StartCoroutine (waitToDisplayDialogueBox ());
				}

			}
		}
	}
	private IEnumerator waitToDisplayDialogueBox()
	{
		yield return new WaitForSeconds(0.2f);
		theObjectPickupManager.enableTextBox();
//		if (destroyWhenActivated)
//		{
//			disableObject = true;
//		}
	}
}
