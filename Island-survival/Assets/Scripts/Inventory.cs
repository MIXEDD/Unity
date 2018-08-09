using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour {

    [SerializeField] private GameObject Survival_Inventory;
    [SerializeField] private Text foodText;
    [SerializeField] private Text food;
    [SerializeField] private Text foodVal;
    [SerializeField] private Text waterText;
    [SerializeField] private Text water;
    [SerializeField] private Text waterVal;
    [SerializeField] private Text medicineText;
    [SerializeField] private Text medicine;
    [SerializeField] private Text medicineVal;
    [SerializeField] private Text panel_message;
    [SerializeField] private DisableManager disableManager;
    [SerializeField] private PlayerVitals playerVitals;
    private bool inventoryOpen = false;

    private int foodCount, waterCount, medicineCount;
    private int foodValue, waterValue, medicideValue;

    public int FoodCount
    {
        get
        {
            return foodCount;
        }

        set
        {
            foodCount = value;
        }
    }

    public int WaterCount
    {
        get
        {
            return waterCount;
        }

        set
        {
            waterCount = value;
        }
    }

    public int MedicineCount
    {
        get
        {
            return medicineCount;
        }

        set
        {
            medicineCount = value;
        }
    }

    public int FoodValue
    {
        get
        {
            return foodValue;
        }

        set
        {
            foodValue = value;
        }
    }

    public int WaterValue
    {
        get
        {
            return waterValue;
        }

        set
        {
            waterValue = value;
        }
    }

    public int MedicideValue
    {
        get
        {
            return medicideValue;
        }

        set
        {
            medicideValue = value;
        }
    }

    public Text Food
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

    public Text FoodVal
    {
        get
        {
            return foodVal;
        }

        set
        {
            foodVal = value;
        }
    }

    public Text Water
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

    public Text WaterVal
    {
        get
        {
            return waterVal;
        }

        set
        {
            waterVal = value;
        }
    }

    public Text Medicine
    {
        get
        {
            return medicine;
        }

        set
        {
            medicine = value;
        }
    }

    public Text MedicineVal
    {
        get
        {
            return medicineVal;
        }

        set
        {
            medicineVal = value;
        }
    }

    public Text FoodText
    {
        get
        {
            return foodText;
        }

        set
        {
            foodText = value;
        }
    }

    public Text WaterText
    {
        get
        {
            return waterText;
        }

        set
        {
            waterText = value;
        }
    }

    public Text MedicineText
    {
        get
        {
            return medicineText;
        }

        set
        {
            medicineText = value;
        }
    }

    // Use this for initialization
    void Start () {
        disableManager = GameObject.FindGameObjectWithTag("DisableController").GetComponent<DisableManager>();
        FoodCount = Convert.ToInt32(Food.text.ToString());
        WaterCount = Convert.ToInt32(Water.text.ToString());
        MedicineCount = Convert.ToInt32(Medicine.text.ToString());
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.I)) {
            SetInventoryActive(inventoryOpen);
        }
    }

    void SetInventoryActive(bool inventoryStatus)
    {
        if (!inventoryStatus)
        {
            inventoryOpen = true;
            Survival_Inventory.SetActive(inventoryOpen);
            disableManager.DisablePlayer();
        }
        else
        {
            inventoryOpen = false;
            Survival_Inventory.SetActive(inventoryOpen);
            disableManager.EnablePlayer();
            panel_message.text = null;
        }
    }

    public void IncrementConsumableCounter(Text consumableType, int cosumableValue)
    {
       if(consumableType == FoodText)
        {
            foodCount += 1;
            food.text = Convert.ToString(foodCount);
            foodVal.text = Convert.ToString(cosumableValue);
        }
        else if (consumableType == WaterText)
        {
            waterCount += 1;
            water.text = Convert.ToString(waterCount);
            waterVal.text = Convert.ToString(cosumableValue);
        }
        else if (consumableType == MedicineText)
        {
            medicineCount += 1;
            medicine.text = Convert.ToString(medicineCount);
            medicineVal.text = Convert.ToString(cosumableValue);
        }
    }

    public void DecreaseConsumableCounter(Text consumableType)
    {
        int value;
        int consumableCount = Convert.ToInt32(consumableType.text);
        if (consumableCount == 0)
        {
            panel_message.text = "You do not have that item.";
            return;
        }
        else
          consumableCount -= 1;


        if (consumableType == Food)
        {             
            foodCount = consumableCount;
            food.text = Convert.ToString(foodCount);
            value = Convert.ToInt32(foodVal.text);
            playerVitals.Eat(value);
            if (consumableCount == 0)
                foodVal.text = "0";
        }
        else if (consumableType == Water)
        {
            Debug.Log("here");
            waterCount = consumableCount;
            water.text = Convert.ToString(waterCount);
            value = Convert.ToInt32(waterVal.text);
            playerVitals.Drink(value);
            if (consumableCount == 0)
                waterVal.text = "0";
        }
        else if (consumableType == Medicine)
        {
            medicineCount = consumableCount;
            medicine.text = Convert.ToString(medicineCount);
            value = Convert.ToInt32(medicineVal.text);
            playerVitals.UseMedicine(value);
            if (consumableCount == 0)
                medicineVal.text = "0";
        }
    }

}
