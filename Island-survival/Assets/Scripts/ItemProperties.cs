using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemProperties : MonoBehaviour
{
    [Header("Your Consumables")] public string itemName;

    [SerializeField] private bool food;
    [SerializeField] private bool water;
    [SerializeField] private bool health;
    [SerializeField] private bool sleepingBag;
    [SerializeField] private float value;



    [SerializeField] private PlayerVitals playerVitals;
    [SerializeField] private SleepController sleepController;

    public bool Food
    {
        get
        {
            return food;
        }

        set
        {
            food = value;
        }
    }

    public bool Water
    {
        get
        {
            return water;
        }

        set
        {
            water = value;
        }
    }

    public bool Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;
        }
    }

    public bool SleepingBag
    {
        get
        {
            return sleepingBag;
        }

        set
        {
            sleepingBag = value;
        }
    }

    public float Value
    {
        get
        {
            return value;
        }

        set
        {
            this.value = value;
        }
    }

    public void Interaction(PlayerVitals playerVitals)
    {
        if (Food)
        {
            playerVitals.hungerSlider.value += Value;
            this.gameObject.SetActive(false);
        }
        else if (Water)
        {
            playerVitals.thirstSlider.value += Value;
            this.gameObject.SetActive(false);
        }
        else if (Health)
        {
            playerVitals.healthSlider.value += Value;
            this.gameObject.SetActive(false);
        }
        else if (SleepingBag)
        {
            sleepController.EnableSleepUI();
        }
    }

    public void disableObject(PlayerVitals playerVitals)
    {
        if (Food)
            this.gameObject.SetActive(false);
        else if (Water)
            this.gameObject.SetActive(false);
        else if (Health)
            this.gameObject.SetActive(false);
    }

    void Start () {
        sleepController = GameObject.FindGameObjectWithTag("SleepController").GetComponent<SleepController>();
	}
	
	
	void Update () {
		
	}
}
