using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class DisplaySingleWord : MonoBehaviour {

    //need an array of scrambled words
    //need an array of 
    public Text thoughtText;
    public string thought, scrambled;
    //public string[] keyWords;
    public int wordIndex;
    public float typingSpeed, newtypingSpeed;
    public gameManager theGameManager;
    public bool edit, isScrambled, hover;

    public static string tentativeDefinition;
    public static int currentEditedIndex;
    public int currentLetter, currentLetterTranslation;
    public bool isTyping, changingLetter;
    int count;
    public static bool disableLetter;
    //public static List<string> keyWords = new List<string>();

    // Use this for initialization
    void Start () {
        scrambled = "";
        theGameManager = FindObjectOfType<gameManager>();
        StartCoroutine(getTheKeyWords());
        //wordIndex = System.Array.FindIndex(theGameManager.getKeyWords(), findWord);
        

	}
	
	// Update is called once per frame
	void Update () {
        
        if (edit)
        {
            //thoughtText.color = Color.blue;
            currentEditedIndex = wordIndex;
            inputWord();
            
            //do the translation
        }else if(!isTyping && isScrambled && !changingLetter && !hover) 
        {
           //Debug.Log(count);
            //scrambled.Substring(Random.Range(0, scrambled.Length - 1), 1);
            //scrambled = scrambled.Replace(scrambled[Random.Range(0, scrambled.Length - 1)].ToString(), gameManager.symbols[Random.Range(0, 23)]);

            StartCoroutine(keepScramble());

        }
        if(currentEditedIndex != -1 && isScrambled && wordIndex == currentEditedIndex)
        {
            //update the word that is beign displayed
            thoughtText.text = tentativeDefinition;
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            edit = false;
            currentEditedIndex = -1;
            if (tentativeDefinition.Trim().Equals(thought.Trim()))
            {
                isScrambled = false;
                thoughtText.text = thought;
                thoughtText.color = Color.green;
                //change the color of the word
            }else
            {
                //thoughtText.color = Color.red;
                //have the word return to scrambled
            }
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
        if(wordIndex != -1)
        {
            isScrambled = true;
            thoughtText.color = Color.red;
        }
        for(int i = 0; i < thought.Length; i++)
        {
            scrambled += gameManager.symbols[Random.Range(0, 23)];
        }
        StartCoroutine(displayWord());
    }

    public IEnumerator displayWord()
    {
        while (isTyping && currentLetter < scrambled.Length - 1)
        {
            thoughtText.text += scrambled[currentLetter];

            currentLetter += 1;
            if (currentLetter == scrambled.Length/2)
            {
                StartCoroutine(showTranslation());
            }
            yield return new WaitForSeconds(typingSpeed);
        }
        if (isScrambled)
        {
            isTyping = false;
        }

    }

    public IEnumerator showTranslation()
    {
        if (!isScrambled)
        {
            while (isTyping && currentLetterTranslation < thought.Length - 1)
            {
                thoughtText.text = thoughtText.text.Remove(currentLetterTranslation, 1);
                thoughtText.text = thoughtText.text.Insert(currentLetterTranslation, thought[currentLetterTranslation].ToString());
                //theText.text.Substring(currentLetterTranslation, currentLetterTranslation + 1) = notScrambled[currentLetterTranslation].ToString();
                currentLetterTranslation += 1;
                yield return new WaitForSeconds(typingSpeed);

            }
            thoughtText.text = thought;
            isTyping = false;
        }
    }
    public IEnumerator keepScramble()
    {
        changingLetter = true;
        scrambled = "";
        for(int i = 0; i < thought.Length; i++)
        {
            scrambled += gameManager.symbols[Random.Range(0, 23)];
        }
        thoughtText.text = scrambled;
        yield return new WaitForSeconds(newtypingSpeed);
        changingLetter = false;
        
    }

    public void editWord()
    {
        if (isScrambled)
        {
            edit = true;
            tentativeDefinition = "";
        }
    }

    public void inputWord()
    {
        if (tentativeDefinition.Length < 10)
        {
            //if the user types in a letter
            if (Input.GetKeyDown(KeyCode.A))
            {
                //then we add that letter to the user definition string
                tentativeDefinition += 'a';
                //we remove the definition offered in the list
                
                //definitionOffered.RemoveAt(currentPage);
                //and add the one with the extra letter in its place
                //definitionOffered.Insert(currentPage, tentativeDefinition);
                
            }
            else if (Input.GetKeyDown(KeyCode.B))
            {
                tentativeDefinition += 'b';
                
                //definitionOffered.RemoveAt(currentPage);
                //and add the one with the extra letter in its place
                //definitionOffered.Insert(currentPage, tentativeDefinition);
                
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                tentativeDefinition += 'c';
                
                //definitionOffered.RemoveAt(currentPage);
                //and add the one with the extra letter in its place
                //definitionOffered.Insert(currentPage, tentativeDefinition);
                
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                tentativeDefinition += 'd';
                
                //definitionOffered.RemoveAt(currentPage);
                //and add the one with the extra letter in its place
                //definitionOffered.Insert(currentPage, tentativeDefinition);
                
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                tentativeDefinition += 'e';
                
                //definitionOffered.RemoveAt(currentPage);
                //and add the one with the extra letter in its place
                //definitionOffered.Insert(currentPage, tentativeDefinition);
                
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                tentativeDefinition += 'f';
                
                //definitionOffered.RemoveAt(currentPage);
                //and add the one with the extra letter in its place
                //definitionOffered.Insert(currentPage, tentativeDefinition);
                
            }
            else if (Input.GetKeyDown(KeyCode.G))
            {
                tentativeDefinition += 'g';
                
                //definitionOffered.RemoveAt(currentPage);
                //and add the one with the extra letter in its place
                //definitionOffered.Insert(currentPage, tentativeDefinition);
                
            }
            else if (Input.GetKeyDown(KeyCode.H))
            {
                tentativeDefinition += 'h';
                
                //definitionOffered.RemoveAt(currentPage);
                //and add the one with the extra letter in its place
                //definitionOffered.Insert(currentPage, tentativeDefinition);
                
            }
            else if (Input.GetKeyDown(KeyCode.I))
            {
                tentativeDefinition += 'i';
                
                //definitionOffered.RemoveAt(currentPage);
                //and add the one with the extra letter in its place
                //definitionOffered.Insert(currentPage, tentativeDefinition);
                
            }
            else if (Input.GetKeyDown(KeyCode.J))
            {
                tentativeDefinition += 'j';
                
                //definitionOffered.RemoveAt(currentPage);
                //and add the one with the extra letter in its place
                //definitionOffered.Insert(currentPage, tentativeDefinition);
                
            }
            else if (Input.GetKeyDown(KeyCode.K))
            {
                tentativeDefinition += 'k';
                
                //definitionOffered.RemoveAt(currentPage);
                //and add the one with the extra letter in its place
                //definitionOffered.Insert(currentPage, tentativeDefinition);
                
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                tentativeDefinition += 'l';
                
                //definitionOffered.RemoveAt(currentPage);
                //and add the one with the extra letter in its place
                //definitionOffered.Insert(currentPage, tentativeDefinition);
                
            }
            else if (Input.GetKeyDown(KeyCode.M))
            {
                tentativeDefinition += 'm';
                
                //definitionOffered.RemoveAt(currentPage);
                //and add the one with the extra letter in its place
                //definitionOffered.Insert(currentPage, tentativeDefinition);
                
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                tentativeDefinition += 'n';
                
                //definitionOffered.RemoveAt(currentPage);
                //and add the one with the extra letter in its place
                //definitionOffered.Insert(currentPage, tentativeDefinition);
                
            }
            else if (Input.GetKeyDown(KeyCode.O))
            {
                tentativeDefinition += 'o';
                
                //definitionOffered.RemoveAt(currentPage);
                //and add the one with the extra letter in its place
                //definitionOffered.Insert(currentPage, tentativeDefinition);
                
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                tentativeDefinition += 'p';
                
                //definitionOffered.RemoveAt(currentPage);
                //and add the one with the extra letter in its place
                //definitionOffered.Insert(currentPage, tentativeDefinition);
                
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                tentativeDefinition += 'q';
                
                //definitionOffered.RemoveAt(currentPage);
                //and add the one with the extra letter in its place
                //definitionOffered.Insert(currentPage, tentativeDefinition);
                //definitionOffered.Insert(currentPage, tentativeDefinition);
                
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                tentativeDefinition += 'r';
                
                //definitionOffered.RemoveAt(currentPage);
                //and add the one with the extra letter in its place
                //definitionOffered.Insert(currentPage, tentativeDefinition);
                
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                tentativeDefinition += 's';
                
                //definitionOffered.RemoveAt(currentPage);
                //and add the one with the extra letter in its place
                //definitionOffered.Insert(currentPage, tentativeDefinition);
                
            }
            else if (Input.GetKeyDown(KeyCode.T))
            {
                tentativeDefinition += 't';
                
                //definitionOffered.RemoveAt(currentPage);
                //and add the one with the extra letter in its place
                //definitionOffered.Insert(currentPage, tentativeDefinition);
                
            }
            else if (Input.GetKeyDown(KeyCode.U))
            {
                tentativeDefinition += 'u';
                
                //definitionOffered.RemoveAt(currentPage);
                //and add the one with the extra letter in its place
                //definitionOffered.Insert(currentPage, tentativeDefinition);
                
            }
            else if (Input.GetKeyDown(KeyCode.V))
            {
                tentativeDefinition += 'v';
                
                //definitionOffered.RemoveAt(currentPage);
                //and add the one with the extra letter in its place
                //definitionOffered.Insert(currentPage, tentativeDefinition);
                
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                tentativeDefinition += 'w';
                
                //definitionOffered.RemoveAt(currentPage);
                //and add the one with the extra letter in its place
                //definitionOffered.Insert(currentPage, tentativeDefinition);
                
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                tentativeDefinition += 'x';
                
                //definitionOffered.RemoveAt(currentPage);
                //and add the one with the extra letter in its place
                //definitionOffered.Insert(currentPage, tentativeDefinition);
                
            }
            else if (Input.GetKeyDown(KeyCode.Y))
            {
                tentativeDefinition += 'y';
                
                //definitionOffered.RemoveAt(currentPage);
                //and add the one with the extra letter in its place
                //definitionOffered.Insert(currentPage, tentativeDefinition);
                
            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                tentativeDefinition += 'z';
                
                //definitionOffered.RemoveAt(currentPage);
                //and add the one with the extra letter in its place
                //definitionOffered.Insert(currentPage, tentativeDefinition);
                
            }

        }
        //if the user presses backspace and the user definition's length is not negative
        if (Input.GetKeyDown(KeyCode.Backspace) && tentativeDefinition.Length > 0)
        {
            //then we remove the last character from the string
            tentativeDefinition = tentativeDefinition.Substring(0, tentativeDefinition.Length - 1);
            //we remove the definition offered at that index 
            
            //definitionOffered.RemoveAt(currentPage);
            //and add the one with the extra letter in its place
            //definitionOffered.Insert(currentPage, tentativeDefinition);
            //moveTyperBackward();
        }
    }
    private void OnDisable()
    {
        changingLetter = false;
        StopCoroutine(keepScramble());
        
    }
    public void accessHover()
    {
        
        //StopCoroutine(keepScramble());
        //changingLetter = false;
        if (isScrambled)
        {
            Debug.Log("Is this getting accessed");
            hover = true;
        }
    }
    public void leaveHover()
    {
        if (isScrambled)
        {
           hover = false;
        }
    }
}
