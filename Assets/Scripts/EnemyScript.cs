using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour {
    
    public Transform target;
    Transform myTransform;
    GameObject other;
    GameObject hand_R;
    BoxCollider2D handCol;
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
        

        other = GameObject.Find("GameManager");
        hand_R = GameObject.Find("Hand_R");
        handCol = hand_R.GetComponent<BoxCollider2D>();
        //handCol.enabled = false;
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
        {
            reactionTime -= 1;
        }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

            damage = 5F;
            other.gameObject.GetComponent<GameManager>().TakeSomeDamage(damage);

    }



    void Chase()
    {
        float movementDistance = moveSpeed * Time.deltaTime;
        if(target != null)
            transform.position = Vector3.MoveTowards(transform.position, target.position, movementDistance);


        if (distanceFromTarget > 8.0f) {
            
            anim.SetInteger("RandomATK", 0);
            anim.SetBool("EnemyWalk", true);
            moveSpeed = 20;
        }

        if (distanceFromTarget < 8.0f) {
            anim.SetBool("EnemyWalk", false);
            handCol.enabled = true;
            if(anim.GetInteger("RandomATK") == 0)
            {
                int r = (int)Random.Range(1, 3);
                anim.SetInteger("RandomATK", r);
            }
            
            moveSpeed = 0;
        }
    }

    void Die()
    {
        if (playerObject != null)
            other.GetComponent<GameManager>().Die();
    }

 
    void Attack()
    {
        int r = (int)Random.Range(1, 3);
        anim.SetInteger("RandomATK", r);
    }
    void RandomATKNull()
    { 
        anim.SetInteger("RandomATK", 0);
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
