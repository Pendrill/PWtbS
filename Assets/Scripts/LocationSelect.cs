using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocationSelect : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 10000000))
        {
            if((hit.transform.tag == "CLASSROOM"))
            {
                //Light up or Grow bigger, etc kind of visual feedback that ur hovering

                if (Input.GetMouseButtonDown(0))
                {
                    SceneManager.LoadScene("CLASSROOM");
                }
            }
            if ((hit.transform.tag == "BEDROOM"))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    SceneManager.LoadScene("StudentBedroom");
                }
            }
        }
    }
}

