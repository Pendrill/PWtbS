using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemZoom : MonoBehaviour {

    public Vector3 originalPosition, endDestination, originalRotation ;
    public bool moveObjectTowardsPlayer, once, canPickUp;
    public float time, Offset;
    public GameObject Cam;
    public float rotationsPerMinute= 10.0f;
    public int startLine, endLine;
    public TextAsset theText;
    public objectExamineManager theObjectExamineManager;
    public TextBoxManager theTextBoxManager;
    public static bool itemGotSelected;

    // Use this for initialization
    void Start () {
        originalPosition = transform.position;
        theObjectExamineManager = FindObjectOfType<objectExamineManager>();
        theTextBoxManager = FindObjectOfType<TextBoxManager>();
        once = true;
        originalRotation = transform.eulerAngles;
        //Cam.transform.position= new Vector3 (Cam.transform.position.x, Cam.transform.position.y, Offset);
	}
	
	// Update is called once per frame
	void Update () {
        if (moveObjectTowardsPlayer)
        {
            time += Time.deltaTime;
            transform.position = Vector3.Lerp(originalPosition, endDestination , time);
        }else if(!itemGotSelected && !moveObjectTowardsPlayer)
        {
            //itemGotSelected = false;
            once = true;
            time -= Time.deltaTime;
            transform.position = Vector3.Lerp(originalPosition, endDestination, time);
            transform.rotation = Quaternion.Lerp(Quaternion.Euler(originalRotation), transform.rotation, time);

        }
        if(time > 1)
        {
            transform.Rotate(0, 6.0f * rotationsPerMinute * Time.deltaTime, 0);
            if (once)
            {
                once = false;
                theObjectExamineManager.reloadScript(theText, gameObject, canPickUp);
                theObjectExamineManager.currentLine = startLine;
                theObjectExamineManager.endAtLine = endLine;
                theObjectExamineManager.enableTextBox();
                //StartCoroutine(waitToDisplayDialogueBox());
            }
        }
        if(!once && !theObjectExamineManager.isTextBoxActive)
        {
            moveObjectTowardsPlayer = false;
            theObjectExamineManager.zoomingIn = false;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && !itemGotSelected && !theTextBoxManager.isTextBoxActive)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.name == this.gameObject.name)
                {
                    theTextBoxManager.textBox.GetComponent<RectTransform>().anchoredPosition3D = theTextBoxManager.OriginalPanelPosition;
                    itemGotSelected = true;
                    moveObjectTowardsPlayer = true;
                    theObjectExamineManager.zoomingIn = true;

                    //Offset += Cam.transform.forward;
                    //Cam.transform.position = new Vector3(Cam.transform.position.x, Cam.transform.position.y, Offset);
                    endDestination = new Vector3(0, 0, 0);
                    endDestination += Camera.main.transform.position + Camera.main.transform.forward * Offset;
                    //endDestination = Cam.transform.position;
                }
            }
        }
        if (time > 1f)
        {
            time = 1f;
        }
        else if (time < 0f)
        {
            time = 0f;
        }
    }
    private IEnumerator waitToDisplayDialogueBox()
    {
        yield return new WaitForSeconds(0.2f);
        theObjectExamineManager.enableTextBox();
        //		if (destroyWhenActivated)
        //		{
        //			disableObject = true;
        //		}
    }
}
