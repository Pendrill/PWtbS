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

    public void Update()
    {
        if( inInv || ((!originalScene.Trim().Equals( SceneManager.GetActiveScene().name.Trim()) && !endScene.Trim().Equals(SceneManager.GetActiveScene().name.Trim())) && !droppedInEnd))
        {
            gameObject.SetActive(false);
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
