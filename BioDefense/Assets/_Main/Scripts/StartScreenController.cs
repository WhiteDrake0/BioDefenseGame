using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class StartScreenController : MonoBehaviour
{

    /*private GameObject titleScreen;
    private GameObject optionScrren;*/

    public GameObject btnContainer;
    public GameObject start;

    // Start is called before the first frame update
    void Start()
    {
        /* titleScreen = transform.Find("1 - Title screen").gameObject;
         optionScrren = transform.Find("2 - Options").gameObject;*/

        //btnContainer = transform.Find("BtnContainer").gameObject;
        /*start = transform.Find("TxtPlay").gameObject;
        Debug.Log(start != null);*/


    }

    // Update is called once per frame
    void Update()
    {
        if(start.activeSelf && Input.anyKeyDown)
        {
            start.SetActive(false);
            btnContainer.SetActive(true);
           
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("Office");
    }
}
