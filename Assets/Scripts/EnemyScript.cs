using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
    
    public Transform target;
    Transform myTransform;
    int direction;
    public int damage;
    Animator anim;
    public float moveSpeed;
    public float distanceFromTarget;
    public GameObject playerObject;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool grounded;
    bool hasSpottedPlayer;
    float reactionTime;
    private bool idlespeed;

    float healthRegeneration;
    float healthRegenTimer;

    int hp;

    void Start()
    {
        hp = 10;
        healthRegeneration = 2;
        healthRegenTimer = 0;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        myTransform = GetComponent<Transform>();
        hasSpottedPlayer = false;
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    void Update()
    {
        distanceFromTarget = Vector3.Distance(playerObject.transform.position, gameObject.transform.position);
        Idle();
        if (grounded & hasSpottedPlayer)
        {
            Chase();
        }
    }

    public void Idle()
    {
        if (distanceFromTarget < 50)
        {
            SawPlayer();
        }
    }

    public void SawPlayer()
    {
        if (!hasSpottedPlayer)
        {
            hasSpottedPlayer = true;
        }

        if (reactionTime < 0)
        {
            reactionTime = 50;
            Chase();
        }
       /* {
            reactionTime -= 1;
        }*/

    }


    void OnCollisionEnter2D(Collision2D col)
    {
       if(col.gameObject.tag == "Player") {
            Debug.Log("Attack");
            Attack();
        }
    }

    void Chase()
    {
            float movementDistance = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, movementDistance);


        if (distanceFromTarget > 9.0f) {
            anim.SetBool("EnemyWalk", true);
            moveSpeed = 20;
            
        }

        if (distanceFromTarget < 9.0f) {
            anim.SetBool("EnemyWalk", false);
            moveSpeed = 0;
            Attack();
        }
    }

void Attack()
    {
        anim.SetBool("EnemyPunch", true);
    }

    public void Fleeing()
    {
        Vector3 awayFromPlayer = transform.position - playerObject.transform.position;
        awayFromPlayer.Normalize();

        transform.LookAt(transform.position + awayFromPlayer);
        if (hp < 5)
        {
            float dampening = 30;
            transform.position = new Vector3(transform.position.x + awayFromPlayer.x / dampening,
                                                            transform.position.y,
                                                            transform.position.z + awayFromPlayer.z / dampening);
            healthRegenTimer += Time.deltaTime;

            if (healthRegenTimer >= healthRegeneration)
            {
                Debug.Log("Enemy new hp: " + hp);
                hp++;
                healthRegenTimer = 0;
            }
        }
        else
        {
            Chase();
        }
    }
}
