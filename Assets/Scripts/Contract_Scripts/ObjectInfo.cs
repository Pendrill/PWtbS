using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectInfo : MonoBehaviour {

    public string Name;
    public string originalScene, endScene;
    public Vector3 originalPosition, endPosition;
    public bool inInv, droppedInEnd;
    public int InvIndex;

    MeshRenderer objectMeshRenderer;
    PickUpObject theObjectPickUpScript;

    public void Start()
    {
        objectMeshRenderer = GetComponent<MeshRenderer>();
        theObjectPickUpScript = GetComponent<PickUpObject>(); 
    }
    public void Update()
    {
        if( inInv || ((!originalScene.Trim().Equals( SceneManager.GetActiveScene().name.Trim()) && !endScene.Trim().Equals(SceneManager.GetActiveScene().name.Trim())) && !droppedInEnd))
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
        }
    }
}
