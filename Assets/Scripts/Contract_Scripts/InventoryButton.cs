using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour {
    public GameObject currentObjectInSlot;
    public string OriginalLocation;
    public Button drop;
    public int index;
    public int reference;
    
	// Use this for initialization
	void Start () {
        Time.timeScale = 1;
    }
	
	// Update is called once per frame
	void Update () {
		if(drop.gameObject.activeInHierarchy && Input.GetMouseButtonDown(0) && !drop.GetComponent<Drop>().hoverOverDrop)
        {
            drop.gameObject.SetActive(false);
        }
        if(currentObjectInSlot == null)
        {
            reference = 0;
        }
        else
        {
            reference = currentObjectInSlot.GetComponent<ObjectInfo>().reference ;
        }
	}
    public void invSlotPressed()
    {
        Debug.Log("Button Got Pressed");
        drop.gameObject.SetActive(true);
        drop.GetComponent<RectTransform>().anchoredPosition3D = GetComponent<RectTransform>().anchoredPosition3D + new Vector3(73, 0, 0);
        drop.GetComponent<Drop>().currentObjectInSlot = currentObjectInSlot;
        drop.GetComponent<Drop>().InvIndex = index;
        drop.GetComponent<Drop>().invButton = this.gameObject;

       

    }

    
}
