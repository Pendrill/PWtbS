using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class DisplaySingleWord : MonoBehaviour {

    //need an array of scrambled words
    //need an array of 
    public string thought;
    //public string[] keyWords;
    public int wordIndex;
    public gameManager theGameManager;
    public bool edit, isScrambled;

    public static string tentativeDefinition;
    public static int currentEditedIndex;
    //public static List<string> keyWords = new List<string>();

    // Use this for initialization
    void Start () {
        theGameManager = FindObjectOfType<gameManager>();
        StartCoroutine(getTheKeyWords());
        //wordIndex = System.Array.FindIndex(theGameManager.getKeyWords(), findWord);
        

	}
	
	// Update is called once per frame
	void Update () {
        if (edit)
        {
            currentEditedIndex = wordIndex;
            //do the translation
        }
        if(currentEditedIndex != -1 && isScrambled && wordIndex == currentEditedIndex)
        {
            //update the word that is beign displayed
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            edit = false;
            currentEditedIndex = -1;
            //check whether the word is correct or not;
            //if it is uncheck isScrambled
        }
	}
    public bool findWord(string word)
    {
        if (word.Trim().Equals(thought.Trim()))
        {
            return true;
        }
        return false;
    }

    //We need to make sure that the key words have the time to insert themselves into the array before we attempt to access it.
    public IEnumerator getTheKeyWords()
    {
        yield return new WaitForEndOfFrame();
        wordIndex = System.Array.FindIndex(theGameManager.getKeyWords(), findWord);
    }

    public void editWord()
    {
        if (isScrambled)
        {
            edit = true;
        }
    }
}
