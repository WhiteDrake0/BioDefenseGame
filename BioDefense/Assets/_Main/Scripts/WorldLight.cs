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

    private float time = 50;

    private bool canDayChange = true;

    public delegate void OnDayChanged();

    public OnDayChanged DayChanged;


    // Update is called once per frame
    void Update()
    {
        if (time > 500)
        {
            time = 0;
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
}
