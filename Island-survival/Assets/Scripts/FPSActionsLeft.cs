using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSActionsLeft : MonoBehaviour {


    [SerializeField] private PlayerVitals playerVitals;
    [SerializeField] private Patrol patrol;
    Animator animator;
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            animator.SetBool("isHandWalk", true);
            animator.SetBool("isHandIdle", false);
            animator.SetBool("isPunch", false);
            animator.SetBool("isPickUp", false);
        }
        else
        {
            animator.SetBool("isHandWalk", false);
            animator.SetBool("isHandIdle", true);
            animator.SetBool("isPunch", false);
            animator.SetBool("isPickUp", false);
        }
    }
}
