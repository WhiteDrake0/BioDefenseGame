using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrossFadeManager : MonoBehaviour
{
    public static CrossFadeManager instance;

    private Animator transition;

    private void Awake()
    {
        instance = this;
        transition = gameObject.GetComponent<Animator>();
    }

    public void LoadNextLevel(string name)
    {
        //int sceneIndex = int.Parse(number);

        StartCoroutine(LoadLevel(name));
    }

    private IEnumerator LoadLevel(string name)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(name);
    }
}
