using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] private PlayerVitals playerVitals;
    [SerializeField] private int enemyHealth;
    private int playerDMG;
    public Transform player;
    public Transform head;
    Animator anim;
    

    string state = "patrol";
    public GameObject[] waypoints;
    int currentWP = 0;
    public float rotSpeed = 0.2f;
    public float speed = 1.5f;
    float accuracyWP = 5.0f;
    bool inCombat = false;
    public int EnemyHealth
    {
        get
        {
            return enemyHealth;
        }

        set
        {
            enemyHealth = value;
        }
    }


    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if(enemyHealth <= 0)
        {
            NPCDeath();
            return;
        }
        float gravity =(float) 0.0015;
        if(!inCombat)
            this.transform.position += Vector3.down * gravity;

        Vector3 direction = player.position - this.transform.position;
        direction.y = 0;
        float angle = Vector3.Angle(direction, head.up);

        if (state == "patrol" && waypoints.Length > 0)
        {
            anim.SetBool("isIdle", false);
            anim.SetBool("isWalking", true);
            if (Vector3.Distance(waypoints[currentWP].transform.position, transform.position) < accuracyWP)
            {
                currentWP = Random.Range(0, waypoints.Length);
                //currentWP++;
                //if(currentWP >= waypoints.Length)
                //{
                //    currentWP = 0;
                //}
            }

            //rotate towards waypoint
            direction = waypoints[currentWP].transform.position - transform.position;
            this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
            this.transform.Translate(0, 0, Time.deltaTime * speed);
        }


        if (Vector3.Distance(player.position, this.transform.position) < 10 && (angle < 30 || state == "pursuing"))
        {
            state = "pursuing";
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
            //this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);

            anim.SetBool("isIdle", false);
            if (direction.magnitude > 3)
            {
                this.transform.Translate(0, 0, Time.deltaTime * speed);
                anim.SetBool("isWalking", true);
                anim.SetBool("isAttacking", false);
                inCombat = false;
            }
            else /*if (direction.magnitude < 3)*/
            {
                anim.SetBool("isAttacking", true);
                anim.SetBool("isWalking", false);
                inCombat = true;
            }
        }
        else
        {
                anim.SetBool("isAttacking", false);
                anim.SetBool("isWalking", true);
                state = "patrol";
                inCombat = false;
        }

    }

    void Attack()
    {
        int damage = 10;
        playerVitals.healthSlider.value -= damage;
    }

    void NPCDeath()
    {
        anim.SetBool("isWalking", false);
        anim.SetBool("isAttacking", false);
        anim.SetBool("isIdle", false);
        anim.SetBool("isDeath", true);
    }


}
