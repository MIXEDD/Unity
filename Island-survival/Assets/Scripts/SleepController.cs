using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SleepController : MonoBehaviour
{
    [SerializeField] private GameObject sleepUI;
    [SerializeField] private Slider sleepSlider;
    [SerializeField] private Text sleepNumber;

    [SerializeField] private float hourlyRegene;
    [SerializeField] private DisableManager disableManager;
    [SerializeField] private TimeController timeController;

    void Start () {
        disableManager = GameObject.FindGameObjectWithTag("DisableController").GetComponent<DisableManager>();
        timeController = GameObject.FindGameObjectWithTag("DayNightCycle").GetComponent<TimeController>();
    }
	
    public void EnableSleepUI()
    {
        sleepUI.SetActive(true);
        disableManager.DisablePlayer();
    }

    public void UpdateSlider()
    {
        sleepNumber.text = sleepSlider.value.ToString("0");
    }

    public void SleepBtn(PlayerVitals playerVitals)
    {
        float sleepValue = sleepSlider.value * hourlyRegene;
        float currentFatigue = playerVitals.fatigueSlider.value;
        float fatigueIncrease = sleepValue + currentFatigue;
        ChangeDayLight(sleepValue);
        ReduceUIBarValues(sleepValue,playerVitals);
        playerVitals.fatigueSlider.value = fatigueIncrease;
        playerVitals.fatMaxStamina = playerVitals.fatigueSlider.value;
        playerVitals.staminaSlider.value = playerVitals.normMaxStamina;
        playerVitals.fatStage1 = true;
        playerVitals.fatStage2 = true;
        playerVitals.fatStage3 = true;
        sleepSlider.value = 1;
        disableManager.EnablePlayer();
        sleepUI.SetActive(false);
    }

    public float CalculateAmountOfSleepValue(float sleepValue)
    {
        sleepValue = sleepValue / 10000;
        float secondsInFullDay = timeController.SecondsInFullDay;
        float currentTimeOfDay = timeController.CurrentTimeOfDay;
        float singleHourValue = 1 / (secondsInFullDay / 60);
        float hoursInSleep = singleHourValue * sleepValue;
        float updatedSleepValue = currentTimeOfDay + hoursInSleep;
        return updatedSleepValue;
    }

    public void ChangeDayLight(float sleepValue)
    {
        float updatedSleepValue = CalculateAmountOfSleepValue(sleepValue);
        if (updatedSleepValue > 1)
            updatedSleepValue = updatedSleepValue - 1;
        timeController.ChangeSunDayLight(updatedSleepValue);
    }

    public void ReduceUIBarValues(float sleepValue, PlayerVitals playerVitals)
    {
        float updatedSleepValue = CalculateAmountOfSleepValue(sleepValue);
        playerVitals.CalculateThirstBarValue(sleepValue);
        playerVitals.CalculateHungerBarValue(sleepValue);
        

    }



}
