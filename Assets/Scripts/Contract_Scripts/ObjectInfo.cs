using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectInfo : MonoBehaviour {

    public string Name;
    public string originalScene;
    public bool inInv;
    public int InvIndex;

    public void Update()
    {
        if( inInv && originalScene.Trim().Equals( SceneManager.GetActiveScene().name.Trim()))
        {
            gameObject.SetActive(false);
        }
    }
}
