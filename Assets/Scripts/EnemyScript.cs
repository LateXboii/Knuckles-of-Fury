using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
    
    public Transform target;
    Transform myTransform;
    PlayerController playah;
    public float damage;
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

    float maxHealth;
    float curHealth;

    //float healthRegeneration;
    //float healthRegenTimer;

    

    void Start()
    {
        maxHealth = 100F;
        curHealth = maxHealth;
       

        //healthRegeneration = 2;
        //healthRegenTimer = 0;
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
        if (playerObject != null)
        {
            distanceFromTarget = Vector3.Distance(playerObject.transform.position, gameObject.transform.position);
            Idle();

        }
        else if (playerObject == null)
        {
            Die();
            
        }
        
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

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            damage = Random.Range(10F, 30F);
            coll.gameObject.GetComponent<PlayerController>().TakeSomeDamage(damage);
        }
    }


    void Chase()
    {
        float movementDistance = moveSpeed * Time.deltaTime;
        if(target != null)
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

    void Die()
    {
<<<<<<< HEAD
        if (playerObject != null)
            playah.Die();
=======
        anim.SetBool("EnemyPunch", true);
>>>>>>> 584b55a901b18acc2aa1117ccae2c8fa90d7a7e8
    }

    
     
    /*public void Fleeing()
    {
        Vector3 awayFromPlayer = transform.position - playerObject.transform.position;
        awayFromPlayer.Normalize();

        transform.LookAt(transform.position + awayFromPlayer);
        /*if (curHealth < 5)
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
    }*/
    
}
