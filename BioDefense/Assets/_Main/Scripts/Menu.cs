using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI currentUI;
    [SerializeField] TextMeshProUGUI WaveNum;
    [SerializeField] TextMeshProUGUI time;
    [SerializeField] Animator animator;

    private bool isMenuOpen = true;

    public void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        animator.SetBool("MenuOpen", isMenuOpen);
    }

    private void OnGUI()
    {
        currentUI.text = LevelManager.main.materials.ToString();
        WaveNum.text = EnemySpawner.currentWave.ToString();
        float mappedTime = MapToTime(WorldLight.time);
        string formattedTime = FormatTime(mappedTime);
        time.text = formattedTime;
    }

    public void SetSelected()
    {

    }

    // Define your interval
    private float minValue = 50f;
    private float maxValue = 500f;

    // Define your time range
    private float minTime = 1f;  // in hours
    private float maxTime = 24f; // in hours

    // Function to map the interval to the time range
    private float MapToTime(float value)
    {
        // Ensure the value is within the specified interval
        value = Mathf.Clamp(value, minValue, maxValue);

        // Map the value to the time range
        float mappedTime = Mathf.Lerp(minTime, maxTime, Mathf.InverseLerp(minValue, maxValue, value));

        return mappedTime;
    }

    // Function to format time as Hours:Minutes:Seconds
    private string FormatTime(float timeInHours)
    {
        int hours = Mathf.FloorToInt(timeInHours);
        int minutes = Mathf.FloorToInt((timeInHours - hours) * 60f);
        //int seconds = Mathf.FloorToInt(((timeInHours - hours) * 60f - minutes) * 60f);

        return string.Format("{0:D2}:{1:D2}", hours, minutes);
    }

}

