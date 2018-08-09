using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class PlayerVitals : MonoBehaviour
{
    public Slider healthSlider;
    public int maxHealth;
    public int healthFallRate;
    
    public Slider thirstSlider;
    public int maxThirst;
    public int thirstFallRate;

    public Slider hungerSlider;
    public int maxHunger;
    public int hungerFallRate;

    public Slider staminaSlider;
    public int normMaxStamina;
    public float fatMaxStamina;
    private int staminaFallrate;
    public int staminaFallMult;
    private int staminaRegainRate;
    public int staminaRegainMult;

    
    private bool gameStarted = false;

    //[Header("Temperature Settings")]
    //public float freezingTemp;
    //public float currentTemp;
    //public float normalTemp;
    //public float heatTemp;
    //public Text tempNumber;
    //public Image tempBG;

    //Fatigue variables
    public Slider fatigueSlider;
    public int maxFatigue;
    public int fatigueFallRate;
    public bool fatStage1 = true;
    public bool fatStage2 = true;
    public bool fatStage3 = true;

    private CharacterController charController;
    private FirstPersonController playerController;
    private Object lockHungerSlider = new Object();
    private Object lockThirstSlider = new Object();

    [SerializeField] private DisableManager disableManager;

    public bool GameStarted
    {
        get
        {
            return gameStarted;
        }

        set
        {
            gameStarted = value;
        }
    }

    private void Start()
    {
        gameStarted = true;
        fatigueSlider.maxValue = maxFatigue;
        fatigueSlider.value = maxFatigue;

        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;

        thirstSlider.maxValue = maxThirst;
        thirstSlider.value = maxThirst;

        hungerSlider.maxValue = maxHunger;
        hungerSlider.value = maxHunger;

        staminaSlider.maxValue = normMaxStamina;
        staminaSlider.value = normMaxStamina;
        staminaFallrate = 1;
        staminaRegainRate = 1;

        charController = GetComponent<CharacterController>();
        playerController = GetComponent<FirstPersonController>();
        disableManager = GameObject.FindGameObjectWithTag("DisableController").GetComponent<DisableManager>();

}

    //void UpdateTemp()
    //{
    //    tempNumber.text = currentTemp.ToString("00.0");
    //}

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            disableManager.DisablePlayer();
            SceneManager.LoadScene("menu");
        }

        //Fatigue section
        if (fatigueSlider.value <= 60 && fatStage1)
        {
            fatMaxStamina = 80;
            staminaSlider.value = fatMaxStamina;
            fatStage1 = false;
        }
        else if (fatigueSlider.value <= 40 && fatStage2)
        {
            fatMaxStamina = 60;
            staminaSlider.value = fatMaxStamina;
            fatStage2 = false;
        }
        else if (fatigueSlider.value <= 20 && fatStage3)
        {
            fatMaxStamina = 20;
            staminaSlider.value = fatMaxStamina;
            fatStage3 = false;
        }

        if (fatigueSlider.value >= 0)
        {
            fatigueSlider.value -= Time.deltaTime + fatigueFallRate;
        }
        else if (fatigueSlider.value <= 0)
        {
            fatigueSlider.value = 0;
        }
        else if (fatigueSlider.value >= maxFatigue)
        {
            fatigueSlider.value = maxFatigue;
        }

        //// Temperature section
        //if (currentTemp <= freezingTemp)
        //{
        //    tempBG.color = Color.blue;
        //    UpdateTemp();
        //}
        //else if(currentTemp >= heatTemp - 0.1)
        //{
        //    tempBG.color = Color.red;
        //    UpdateTemp();
        //}
        //else
        //{
        //    tempBG.color = Color.green;
        //    UpdateTemp();
        //}


        //Health controller
        if(hungerSlider.value <= 0 && thirstSlider.value <= 0)
        {
            healthSlider.value -= Time.deltaTime / healthFallRate * 2;
        }
        else if(hungerSlider.value <= 0 || thirstSlider.value <= 0)
        {
            healthSlider.value -= Time.deltaTime / healthFallRate;
        }
        else if(healthSlider.value <= 0)
        {
            CharacterDeath();
        }

        //Hunger controller
        if(hungerSlider.value >= 0)
        {
            hungerSlider.value -= Time.deltaTime / hungerFallRate;
        }
        else if(hungerSlider.value <= 0)
        {
            hungerSlider.value = 0;
        }
        else if(hungerSlider.value >= maxHunger)
        {
            hungerSlider.value = maxHunger;
        }

        //Thirst controller
        if (thirstSlider.value >= 0)
        {
            thirstSlider.value -= Time.deltaTime / thirstFallRate;
        }
        else if (thirstSlider.value <= 0)
        {
            thirstSlider.value = 0;
        }
        else if (thirstSlider.value >= maxThirst)
        {
            thirstSlider.value = maxThirst;
        }

        //Stamina controller section
        if(charController.velocity.magnitude > 0 && Input.GetKey(KeyCode.LeftShift))
        {
            staminaSlider.value -= Time.deltaTime / staminaFallrate * staminaFallMult;
            
            //if(staminaSlider.value > 0)
            //{
            //    currentTemp += Time.deltaTime / 18;
            //}
        }
        else
        {
            staminaSlider.value += Time.deltaTime / staminaRegainRate * staminaRegainMult;

            //if(currentTemp >= normalTemp)
            //{
            //    currentTemp -= Time.deltaTime / 10;
            //}
        }

        if(staminaSlider.value >= fatMaxStamina)
        {
            staminaSlider.value = fatMaxStamina;
        }
        else if (staminaSlider.value <= 0)
        {
            staminaSlider.value = 0;
            playerController.m_RunSpeed = playerController.m_WalkSpeed;
        }
        else if(staminaSlider.value >= 0)
        {
            playerController.m_RunSpeed = playerController.m_RunSpeedNorm;
        }
    }

    public void CalculateHungerBarValue(float sleepValue)
    {
        sleepValue = sleepValue / 10000;
        float deltaValue = Time.deltaTime;
        float hungerBarReductionValue = Time.deltaTime / hungerFallRate;
        while(deltaValue < 1)
        {
            hungerBarReductionValue += Time.deltaTime / hungerFallRate;
            deltaValue += Time.deltaTime;
        }
        hungerBarReductionValue = hungerBarReductionValue * 20;
        hungerBarReductionValue = hungerBarReductionValue * sleepValue;
        lock(hungerSlider)
            hungerSlider.value -= hungerBarReductionValue;

    }


    public void CalculateThirstBarValue(float sleepValue)
    {
        sleepValue = sleepValue / 10000;
        float deltaValue = Time.deltaTime;
        float thirstBarReductionValue = Time.deltaTime / thirstFallRate;
        
        while (deltaValue < 1)
        {
            thirstBarReductionValue += Time.deltaTime / thirstFallRate;
            deltaValue += Time.deltaTime;
        }
        thirstBarReductionValue = thirstBarReductionValue * 20;
        thirstBarReductionValue = thirstBarReductionValue * sleepValue;
        lock(thirstSlider)
            thirstSlider.value -= thirstBarReductionValue;

    }

    public void Eat(int value)
    {
        hungerSlider.value += value;
    }

    public void Drink(int value)
    {
        thirstSlider.value += value;
    }

    public void UseMedicine(int value)
    {
        healthSlider.value += value;
    }

    void CharacterDeath()
    {
        disableManager.DisablePlayer();
        SceneManager.LoadScene("death");
    }

}
