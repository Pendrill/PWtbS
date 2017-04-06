using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour {
    public GameObject currentObjectInSlot;
    public string OriginalLocation;
    public Button drop;
    
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		if(drop.gameObject.activeInHierarchy && Input.GetMouseButtonDown(0) && !drop.GetComponent<Drop>().hoverOverDrop)
        {
            drop.gameObject.SetActive(false);
        }
	}
    public void invSlotPressed()
    {
        drop.gameObject.SetActive(true);
        drop.GetComponent<RectTransform>().anchoredPosition3D = GetComponent<RectTransform>().anchoredPosition3D + new Vector3(73, 0, 0);
        drop.GetComponent<Drop>().currentObjectInSlot = currentObjectInSlot;

       

    }

    
}
