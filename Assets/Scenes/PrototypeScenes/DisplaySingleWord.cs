using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class DisplaySingleWord : MonoBehaviour {

    //need an array of scrambled words
    //need an array of 
    public string thought;
    public string[] keyWords;
    //public static List<string> keyWords = new List<string>();

    // Use this for initialization
    void Start () {
        Debug.Log(System.Array.FindIndex(keyWords, findWord));
        

	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public bool findWord(string word)
    {
        if (word.Trim().Equals(thought.Trim()))
        {
            return true;
        }
        return false;
    }
}
