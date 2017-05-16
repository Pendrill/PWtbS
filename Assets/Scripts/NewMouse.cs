using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewMouse : MonoBehaviour {

    public GameObject WorldObject;
    public RectTransform UI_Element, CanvasRect;
    public Canvas CanvasGO;
    public Image Idle, Object, Talk, moveCameraR, moveCameraL;
    TextBoxManager theTextBoxManager;
    public float theScreenWidth, theScreenHeight, offsetScreenPostition;
    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;
        theTextBoxManager = FindObjectOfType<TextBoxManager>();

        offsetScreenPostition = 50;
        //gets/sets the screen width and height
        theScreenWidth = Screen.width;
        theScreenHeight = Screen.height;

    }
	// Update is called once per frame
	void Update () {
        // Vector3 temp = Input.mousePosition;
        //temp.z = 10f;
        //this.GetComponent<RectTransform>().anchoredPosition3D = Camera.main.ScreenToWorldPoint(temp);

        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(CanvasGO.transform as RectTransform, Input.mousePosition, CanvasGO.worldCamera, out pos);
        transform.position = CanvasGO.transform.TransformPoint(pos);

        if (Input.mousePosition.x > theScreenWidth - offsetScreenPostition) {
            moveCameraR.gameObject.SetActive(true);
            moveCameraL.gameObject.SetActive(false);
            Idle.gameObject.SetActive(false);
            Object.gameObject.SetActive(false);
            Talk.gameObject.SetActive(false);
        }
        else if(Input.mousePosition.x < 0 + offsetScreenPostition){
            moveCameraL.gameObject.SetActive(true);
            moveCameraR.gameObject.SetActive(false);
            Idle.gameObject.SetActive(false);
            Object.gameObject.SetActive(false);
            Talk.gameObject.SetActive(false);
        }

        else if (SceneManager.GetActiveScene().name.Trim().Equals("MainMap".Trim()) || !theTextBoxManager.isTextBoxActive)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "Spirit")
                {
                    Idle.gameObject.SetActive(false);
                    Object.gameObject.SetActive(false);
                    Talk.gameObject.SetActive(true);
                    moveCameraR.gameObject.SetActive(false);
                    moveCameraL.gameObject.SetActive(false);
                }
                else if(hit.collider.gameObject.tag == "Object")
                {
                    Idle.gameObject.SetActive(false);
                    Object.gameObject.SetActive(true);
                    Talk.gameObject.SetActive(false);
                    moveCameraR.gameObject.SetActive(false);
                    moveCameraL.gameObject.SetActive(false);
                }
                else
                {
                    Idle.gameObject.SetActive(true);
                    Object.gameObject.SetActive(false);
                    Talk.gameObject.SetActive(false);
                    moveCameraR.gameObject.SetActive(false);
                    moveCameraL.gameObject.SetActive(false);
                }

            }else
            {
                Idle.gameObject.SetActive(true);
                Object.gameObject.SetActive(false);
                Talk.gameObject.SetActive(false);
                moveCameraR.gameObject.SetActive(false);
                moveCameraL.gameObject.SetActive(false);
            }
        }
        else
        {
            Idle.gameObject.SetActive(true);
            Object.gameObject.SetActive(false);
            Talk.gameObject.SetActive(false);
            moveCameraR.gameObject.SetActive(false);
            moveCameraL.gameObject.SetActive(false);
        }

            /*Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(Input.mousePosition);
            Vector2 WorldObject_ScreenPosition = new Vector2(
            ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
            ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));

            //now you can set the position of the ui element
            UI_Element.anchoredPosition3D = WorldObject_ScreenPosition;*/
        }
}
