using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TranslationCheck : MonoBehaviour {
    public GameObject singleWord;
    private bool go;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        StartCoroutine(waitAFrame());
        if (!singleWord.GetComponent<DisplaySingleWord>().isScrambled && go) 
        {
            SceneManager.LoadScene("StartBar");
        }
	}

    private IEnumerator waitAFrame()
    {
        yield return new WaitForEndOfFrame();
        go = true;
        
    }
}
