using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploreThoughts : MonoBehaviour {

    /// <summary>
    /// What do i need this script to do//
    /// 
    /// ok so basically, i am going to click on the human
    /// Once i have clicked on them, we are going to zoom in
    /// as we are zooming in we need to fade to black
    /// after having zoomed in, the words start to come in (if posible have it do the cool thing that happens for the arbs)
    /// the scrambled words should scramble
    /// when the player enters a word, it gets replaced however, once they press enter if it is incorect it returns to the scrabled word and shakes a bit
    /// if the word is correct have like a particle effect and an unlock sound
    /// </summary>



    public TextBoxManager theTextBoxManager;
    public MoveCameraDialogue MoveCameraDialogue;
    public GameObject currentHit;
    public Vector3 specificOffset;

    public int humanNumber;
    //we porbs need the other game object that woudld be the black bar that would fade in as you zoom in here.


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !theTextBoxManager.isTextBoxActive && MoveCameraDialogue.transform.position == MoveCameraDialogue.OriginalCameraPosition)
        {
            //we create a ray cast that is emmited forward from the position of the mouse
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            //if there is a hit
            if (Physics.Raycast(ray, out hit))
            {
                //we only want to run this for the specific object that got hit (as this script will be attached to many objects)
                if (hit.collider.gameObject.name == this.gameObject.name)
                {
                    currentHit = hit.collider.gameObject;
                    //Edits need to made to the the move camera script as to make sure that it zooms in faster ( Don't know if we need the camera rotation lerp anymore)
                    MoveCameraDialogue.moveTowardObject(currentHit, specificOffset);
                    //I think that we need to make sure whether or not the player is the only one in the scene. Have a specific number
                    //associated to the button layout it needs based on the number of interactable humans there are in the scene.

                    //we need to load the human number into the the new translator manager or whatever. The number 0 is the default meaning nothing needs to be done
                    //as there is only one interactable human in the scene. Other numbers will be dealt accordingly.

                    //We might also need a new script for the specific button stuff, though i think we should be able to do all that in the inspector.

                    // !!! have a specofoc script on the button that would load in the dialogue into them once they appear.!!!

                }
            }
      }

    }
}
