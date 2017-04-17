using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(Load());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public IEnumerator Load()
    {
        yield return new WaitForSeconds(2.9f);
        gameObject.SetActive(false);

    }
}
