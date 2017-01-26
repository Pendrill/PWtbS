using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextImporter : MonoBehaviour {

	//this is the block of text
	public TextAsset textFile;
	public string[] textLines;
	// Use this for initialization
	void Start () {
		if (textFile != null) {
			textLines = (textFile.text.Split('\n'));
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
