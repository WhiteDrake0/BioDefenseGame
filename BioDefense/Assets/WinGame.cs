using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinGame : MonoBehaviour
{
    public string sceneToLoad; // Name of the scene to load

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();

        if (button != null)
        {
            button.onClick.AddListener(ChangeScene);
        }
        else
        {
            Debug.LogError("Button component not found on GameObject with SceneChangeButton script.");
        }
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

}
