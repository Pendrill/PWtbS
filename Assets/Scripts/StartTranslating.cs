using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTranslating : MonoBehaviour {
    public int count;
    public GameObject[] sets;
    public GameObject diamondIndicator;
	// Use this for initialization
	void Start () {
        sets[count].SetActive(true);
        count += 1;
	}
	
	// Update is called once per frame
	void Update () {
		/*for (int i = 0; i< sets.Length; i++)
        {
            if(count == i)
            {
                sets[count].SetActive(true);
                break;
            }else
            {
                sets[count].SetActive(false);
            }
        }*/
        if (Input.GetKeyDown(KeyCode.Mouse0) && count!= 4)
        {
            sets[count].SetActive(true);
            if(count != 0)
            {
                sets[count - 1].SetActive(false);
            }
            count += 1;
        }
	}
}
