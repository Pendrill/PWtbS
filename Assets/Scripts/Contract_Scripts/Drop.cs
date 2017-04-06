using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Drop : MonoBehaviour {
   // public string Name;
    public string originalScene;
    //public bool inInv;
    public int InvIndex;
    public GameObject currentObjectInSlot;
    //public string OriginalLocation;
    public objectPickupManager theObjectPickupManager;
    //public Button drop;
    // Use this for initialization
    public bool hoverOverDrop;
    void Start () {
        theObjectPickupManager = FindObjectOfType<objectPickupManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void drop()
    {
        Debug.Log("drop happens");
        originalScene = currentObjectInSlot.GetComponent<ObjectInfo>().originalScene;
        if (originalScene.Trim().Equals(SceneManager.GetActiveScene().name.Trim()))
        {
            currentObjectInSlot.SetActive(true);
            currentObjectInSlot.GetComponent<ObjectInfo>().inInv = false;
            InvIndex = currentObjectInSlot.GetComponent<ObjectInfo>().InvIndex;
            theObjectPickupManager.InventorySlot[currentObjectInSlot.GetComponent<ObjectInfo>().InvIndex].GetComponent<Image>().sprite = null;
            theObjectPickupManager.slotOpen[InvIndex] = false;
            hoverOverDrop = false;
            gameObject.SetActive(false);
        }
    }
    public void hoverOver()
    {
        hoverOverDrop = true;
    }
}
