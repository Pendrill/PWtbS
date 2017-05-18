using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ObjectInfo : MonoBehaviour {

    public string Name;
    public string originalScene, endScene, currentScene;
    public Vector3 originalPosition, endPosition;
    public bool inInv, droppedInEnd;
    public int InvIndex;
    public Image objectSprite;
    public SpriteRenderer objectMeshRenderer;
    PickUpObject theObjectPickUpScript;
    public int reference;
    public objectPickupManager theObjectPickUpManager;
    bool checkIfThere;
    public bool droppedOffAtEnd;
    public GameObject referenceObject;
    
    

    public void Start()
    {
        objectMeshRenderer = GetComponent<SpriteRenderer>();
        theObjectPickUpScript = GetComponent<PickUpObject>();
        theObjectPickUpManager = FindObjectOfType<objectPickupManager>();
    }
    public void Update()
    {
        currentScene = referenceObject.GetComponent<ObjectInfo>().currentScene;
        inInv = referenceObject.GetComponent<ObjectInfo>().inInv;
        if (inInv)
        {
            Debug.Log("Is object Info getting accessed?");
            objectMeshRenderer.enabled = false;
            currentScene = "inventory";
        }
        else if (currentScene.Trim().Equals(SceneManager.GetActiveScene().name.Trim()) && !inInv)
        {
            objectMeshRenderer.enabled = true;
        }else if(!currentScene.Trim().Equals(SceneManager.GetActiveScene().name.Trim()) && !inInv)
        {
            objectMeshRenderer.enabled = false;
        }
        /*for (int i = 0; i < theObjectPickUpManager.InventorySlot.Length; i++)
        {
            if (droppedOffAtEnd && theObjectPickUpManager.InventorySlot[i].GetComponent<InventoryButton>().currentObjectInSlot == null && objectPickupManager.droppedInEndRef[i] == reference)
            {
                //Debug.Log(1);
                checkIfThere = false;
                droppedInEnd = true;
            }
            else if (!droppedOffAtEnd && objectPickupManager.droppedInEndRef[i] == reference)
            {
               // Debug.Log("In classroom this should show up");
                checkIfThere = true;
                break;
            }
            else if (theObjectPickUpManager.InventorySlot[i].GetComponent<InventoryButton>().reference != 0 && theObjectPickUpManager.InventorySlot[i].GetComponent<InventoryButton>().currentObjectInSlot != null && theObjectPickUpManager.InventorySlot[i].GetComponent<InventoryButton>().currentObjectInSlot.name.Trim().Equals(name.Trim()))
            {
                //Debug.Log(i);
                theObjectPickUpManager.InventorySlot[i].GetComponent<InventoryButton>().index = i;
                checkIfThere = true;
                break;
                //objectMeshRenderer.enabled = false;
            } 
            
            else if (theObjectPickUpManager.InventorySlot[i].GetComponent<InventoryButton>().currentObjectInSlot == null)
            {
               // Debug.Log(4);
                //objectMeshRenderer.enabled = true;
                checkIfThere = false;
            }

        }
        if (droppedOffAtEnd && !droppedInEnd)
        {
            //checkIfThere = false;
            objectMeshRenderer.enabled = false;
            GetComponent<ItemZoom>().enabled = false;
            return;
        }
        if (checkIfThere)
        {
            GetComponent<ItemZoom>().enabled = false;
            objectMeshRenderer.enabled = false;
        }
        else
        {
            objectMeshRenderer.enabled = true;
            GetComponent<ItemZoom>().enabled = true;
        }*/
        /*if( inInv || ((!originalScene.Trim().Equals( SceneManager.GetActiveScene().name.Trim()) && !endScene.Trim().Equals(SceneManager.GetActiveScene().name.Trim())) && !droppedInEnd))
        {
            objectMeshRenderer.enabled = false;
            theObjectPickUpScript.enabled = false;
            //gameObject.SetActive(false);
        }else if (!inInv && !droppedInEnd && originalScene.Trim().Equals(SceneManager.GetActiveScene().name.Trim()))
        {
            objectMeshRenderer.enabled = true;
            theObjectPickUpScript.enabled = true;
        }else if(!inInv && droppedInEnd && endScene.Trim().Equals(SceneManager.GetActiveScene().name.Trim()))
        {
            objectMeshRenderer.enabled = true;
            theObjectPickUpScript.enabled = true;
        }
        if (Name.Trim().Equals("charger".Trim()))
        {
            if( inInv || !droppedInEnd)
            {
                ROOM_MANAGER.complete = false;
                return;
            }
        }else if (Name.Trim().Equals("notebook".Trim()))
        {
            if (inInv || !droppedInEnd)
            {
                ROOM_MANAGER.complete = false;
                return;
            }
        }*/
    }
}
