using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour {
    public GameObject notebook;
    public GameObject charger;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKey(KeyCode.P)){
            if (Input.GetKeyDown(KeyCode.R))
            {
                notebook.GetComponent<ObjectInfo>().currentScene = "Classroom";
                notebook.GetComponent<ObjectInfo>().inInv = false;
                charger.GetComponent<ObjectInfo>().currentScene = "Classroom";
                charger.GetComponent<ObjectInfo>().inInv = false;
                QuestManager.currentContract = 0;
                SceneManager.LoadScene("TitleScreen");

            }

        }
    }
}
