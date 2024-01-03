using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WorldLight : MonoBehaviour
{
    [SerializeField] private Gradient lightColor;
    [SerializeField] private GameObject light;

    private int days;

    public int Days => days;

    public static float time = 50;

    private bool canDayChange = true;

    public delegate void OnDayChanged();

    public OnDayChanged DayChanged;

    private LevelManager level;

    private void Start()
    {
        level = LevelManager.main;
    }


    // Update is called once per frame
    void Update()
    {
        
        if (time > 500)
        {
            //Pause the current scene
            //Time.timeScale = 0.0f;
            /* Debug.Log(level.materials);
             PlayerPrefs.SetInt("Materials", level.materials);
             PlayerPrefs.SetInt("Days", days);
             PlayerPrefs.Save*/
            //CrossFadeManager.instance.LoadNextLevel("GameHub");
            StopGame();
            //time = 0;
        }

        if((int)time ==250 && canDayChange)
        {
            canDayChange = false;
            DayChanged();
            days++;
        }

        if((int)time  == 255){
            canDayChange = true;
        }

        time += Time.deltaTime;
        light.GetComponent<Light2D>().color = lightColor.Evaluate(time * 0.002f);
    }

    private void StopGame()
    {
        //Pause the current scene
        //Time.timeScale = 0.0f;
        CrossFadeManager.instance.LoadNextLevel("GameHub");
    }
}
