using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSActions : MonoBehaviour {


    [SerializeField] private PlayerVitals playerVitals;
    [SerializeField] private Patrol patrol;
    Animator animator;

    private Transform fps;
    private Transform enemy;
    private float range = 10.0f;


    void Start () {
        animator = GetComponent<Animator>();
    }
	
	
	void Update () {

        //if (enemy)
        //    Debug.Log(enemy.name + " is " + Distance().ToString() + " units from " + fps.name);
        //else
        //    Debug.Log("Player not found!");


        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            animator.SetBool("isHandWalk", true);
            animator.SetBool("isHandIdle", false);
            animator.SetBool("isPunch", false);
            animator.SetBool("isPickUp", false);
        } else
        {
            animator.SetBool("isHandWalk", false);
            animator.SetBool("isHandIdle", true);
            animator.SetBool("isPunch", false);
            animator.SetBool("isPickUp", false);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetBool("isHandWalk", false);
            animator.SetBool("isHandIdle", false);
            animator.SetBool("isPunch", true);
            animator.SetBool("isPickUp", false);
            AttackNPC();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetBool("isHandWalk", false);
            animator.SetBool("isHandIdle", false);
            animator.SetBool("isPunch", false);
            animator.SetBool("isPickUp", true);
        }

    }

    void AttackNPC()
    {
        int dmg = 10;
        float distance = Distance();
        if(distance <= 3)
            patrol.EnemyHealth -= dmg;
    }

    private void Awake()
    {
        fps = this.transform;
        enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
    }
    private float Distance()
    {
        return Vector3.Distance(fps.position, enemy.position);
    }

}
