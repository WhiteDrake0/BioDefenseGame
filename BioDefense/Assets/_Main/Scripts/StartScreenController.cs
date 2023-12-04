using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartScreenController : MonoBehaviour
{

    private GameObject titleScreen;
    private GameObject optionScrren;

    // Start is called before the first frame update
    void Start()
    {
        titleScreen = transform.Find("1 - Title screen").gameObject;
        optionScrren = transform.Find("2 - Options").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(titleScreen.activeSelf && Input.anyKeyDown)
        {
            titleScreen.SetActive(false);
            optionScrren.SetActive(true);
           
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("Office");
    }
}
