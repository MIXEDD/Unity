using UnityEngine;

public class InventoryUI : MonoBehaviour {


    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.I)) {
            Debug.Log("presesd i");
           // gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}
