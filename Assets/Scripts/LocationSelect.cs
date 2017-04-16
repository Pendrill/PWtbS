using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocationSelect : MonoBehaviour
{
    public Transform school, apartment, spiritBar, mart, subway;
    public Vector3 OGschool, OGapartment, OGSpiritBar, OGMart, OGsubway;
    // Use this for initialization
    void Start()
    {
        OGschool = school.localScale;
        OGapartment = apartment.localScale;
        OGSpiritBar = spiritBar.localScale;
        OGMart = mart.localScale;
        OGsubway = subway.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if((hit.transform.tag == "CLASSROOM"))
            {
                //Light up or Grow bigger, etc kind of visual feedback that ur hovering
                hit.transform.localScale = new Vector3 (1.3f,1.3f,1.3f);
                apartment.localScale = OGapartment;
                mart.localScale = OGMart;
                if (Input.GetMouseButtonDown(0))
                {
                    SceneManager.LoadScene("CLASSROOM");
                }
            }
            if ((hit.transform.tag == "BEDROOM"))
            {
                hit.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                school.localScale = OGschool;
                mart.localScale = OGMart;
                if (Input.GetMouseButtonDown(0))
                {
                    SceneManager.LoadScene("StudentBedroom");
                }
            }
            if ((hit.transform.tag == "Mart"))
            {
                hit.transform.localScale = new Vector3(1.3f, 1.3f, transform.localScale.z);
                school.localScale = OGschool;
                apartment.localScale = OGapartment;
                if (Input.GetMouseButtonDown(0))
                {
                    SceneManager.LoadScene("Mart");
                }
            }
            if ((hit.transform.tag == "SpiritBar"))
            {
                hit.transform.localScale = new Vector3(1.3f, 1.3f, transform.localScale.z);
                if (Input.GetMouseButtonDown(0))
                {
                    SceneManager.LoadScene("BarDesignTest");
                }
            }
            if ((hit.transform.tag == "Subway"))
            {
                hit.transform.localScale = new Vector3(1.00078f , 1.08f, 0.9f);
                if (Input.GetMouseButtonDown(0))
                {
                    SceneManager.LoadScene("Subway");
                }
            }
        }
        else
        {
            school.localScale = OGschool;
            apartment.localScale = OGapartment;
            spiritBar.localScale = OGSpiritBar;
            mart.localScale = OGMart;
            subway.localScale = OGsubway;
        }
    }
}

