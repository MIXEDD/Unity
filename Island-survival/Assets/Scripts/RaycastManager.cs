using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastManager : MonoBehaviour
{
    private GameObject raycastedObj;

    [Header("Raycast Settings")]
    [SerializeField] private float rayLength = 10;
    [SerializeField] private LayerMask newLayerMask;

    [Header("References")]
    [SerializeField] private PlayerVitals playerVitals;
    [SerializeField] private Image crossHair;
    [SerializeField] private Text itemNameText;
    [SerializeField] private int itemValue;
    [SerializeField] private Inventory inventory;


    void Start () {
		
	}
	
	void Update () {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if(Physics.Raycast(transform.position,fwd,out hit, rayLength, newLayerMask.value))
        {
            if (hit.collider.CompareTag("Consumable"))
            {
                CrossHairActive();
                raycastedObj = hit.collider.gameObject;
                ItemProperties properties = raycastedObj.GetComponent<ItemProperties>();
                itemNameText.text = raycastedObj.GetComponent<ItemProperties>().itemName;
                itemValue = Convert.ToInt32(raycastedObj.GetComponent<ItemProperties>().Value);
                //update UI name
                if (Input.GetMouseButtonDown(0))
                {
                    properties.Interaction(playerVitals);
                }
                else if (Input.GetKeyDown(KeyCode.F))
                {
                    properties.disableObject(playerVitals);
                    if (properties.Food)
                        inventory.IncrementConsumableCounter(inventory.FoodText,itemValue);
                    else if (properties.Water)
                        inventory.IncrementConsumableCounter(inventory.WaterText, itemValue);                        
                    else if (properties.Health)
                        inventory.IncrementConsumableCounter(inventory.MedicineText, itemValue);     
                }
            }
        }
        else
        {
            CrosshairNormal();
            //item name back to normal
            itemNameText.text = null;
        }
	}

    void CrossHairActive()
    {
        crossHair.color = Color.red;
    }

    void CrosshairNormal()
    {
        crossHair.color = Color.white;

    }

}
