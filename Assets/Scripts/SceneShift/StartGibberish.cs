using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGibberish : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(Gibberish());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator Gibberish()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("StartBar");
    }
}
