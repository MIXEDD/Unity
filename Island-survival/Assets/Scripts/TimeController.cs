using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    [SerializeField] private Light sun;
    [SerializeField] private float secondsInFullDay;// = 120f;

    [Range(0, 1)] [SerializeField] private float currentTimeOfDay = 0;
    private float timeMultiplier = 1f;
    private float sunInitialIntensity;

    public float SecondsInFullDay
    {
        get
        {
            return secondsInFullDay;
        }

        set
        {
            secondsInFullDay = value;
        }
    }

    public float CurrentTimeOfDay
    {
        get
        {
            return currentTimeOfDay;
        }

        set
        {
            currentTimeOfDay = value;
        }
    }

    void Start()
    {
        sunInitialIntensity = sun.intensity;
    }

    private void Update()
    {
        UpdateSun();

        CurrentTimeOfDay += (Time.deltaTime / SecondsInFullDay) * timeMultiplier;

        if(CurrentTimeOfDay >= 1)
        {
            CurrentTimeOfDay = 0;
        }
    }

    public void ChangeSunDayLight(float currentTimeOfDay)
    {
        CurrentTimeOfDay = currentTimeOfDay;
        UpdateSun();
    }

    void UpdateSun()
    {
        sun.transform.localRotation = Quaternion.Euler((CurrentTimeOfDay * 360f) - 90, 170, 0);

        float intensityMultiplier = 1;

        if (CurrentTimeOfDay <= 0.23f || CurrentTimeOfDay >= 0.75f)
        {
            intensityMultiplier = 0;
        }
        else if(CurrentTimeOfDay <= 0.25f)
        {
            intensityMultiplier = Mathf.Clamp01((CurrentTimeOfDay - 0.23f) * (1 / 0.02f));
        }
        else if(CurrentTimeOfDay >= 0.73f)
        {
            intensityMultiplier = Mathf.Clamp01(1 - ((CurrentTimeOfDay - 0.73f) * (1 / 0.02f)));
        }

        sun.intensity = sunInitialIntensity * intensityMultiplier;
    }

}
