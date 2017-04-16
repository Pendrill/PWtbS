﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewMouse : MonoBehaviour {

    public GameObject WorldObject;
    public RectTransform UI_Element, CanvasRect;
    public Canvas CanvasGO;
    public Image Idle, Object, Talk;
    TextBoxManager theTextBoxManager;
    // Use this for initialization
    void Start()
    {
      // Cursor.visible = false;
        theTextBoxManager = FindObjectOfType<TextBoxManager>();

    }
	// Update is called once per frame
	void Update () {
        // Vector3 temp = Input.mousePosition;
        //temp.z = 10f;
        //this.GetComponent<RectTransform>().anchoredPosition3D = Camera.main.ScreenToWorldPoint(temp);

        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(CanvasGO.transform as RectTransform, Input.mousePosition, CanvasGO.worldCamera, out pos);
        transform.position = CanvasGO.transform.TransformPoint(pos);

        if (!theTextBoxManager.isTextBoxActive)
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
                }else if(hit.collider.gameObject.tag == "Object")
                {
                    Idle.gameObject.SetActive(false);
                    Object.gameObject.SetActive(true);
                    Talk.gameObject.SetActive(false);
                }
                else
                {
                    Idle.gameObject.SetActive(true);
                    Object.gameObject.SetActive(false);
                    Talk.gameObject.SetActive(false);
                }

            }else
            {
                Idle.gameObject.SetActive(true);
                Object.gameObject.SetActive(false);
                Talk.gameObject.SetActive(false);
            }
        }
        else
        {
            Idle.gameObject.SetActive(true);
            Object.gameObject.SetActive(false);
            Talk.gameObject.SetActive(false);
        }

            /*Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(Input.mousePosition);
            Vector2 WorldObject_ScreenPosition = new Vector2(
            ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
            ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));

            //now you can set the position of the ui element
            UI_Element.anchoredPosition3D = WorldObject_ScreenPosition;*/
        }
}