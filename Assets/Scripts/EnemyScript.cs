using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour {
    
    public Transform target;
    public float maxHealth;
    public float curHealth;

    
    Transform myTransform;
    GameObject other;
    GameObject hand_R;
    GameObject foot_R;
    BoxCollider2D handCol;
    BoxCollider2D footCol;
    Animator anim;
    public Slider enemyhealthbar;
    public float moveSpeed;
    public float distanceFromTarget;
    public GameObject playerObject;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool grounded;
    bool hasSpottedPlayer;
    float reactionTime;
    float attacTimer = 0f;
    private bool faceRight = false;

   

    
 
    //float healthRegeneration;
    //float healthRegenTimer;

    

    void Start()
    {
        maxHealth = 200;
        curHealth = maxHealth;

        enemyhealthbar.value = CalculateHealth();

        other = GameObject.Find("CharacterHealth");
        hand_R = GameObject.Find("Hand_R");
        foot_R = GameObject.Find("Foot_R");
        handCol = hand_R.GetComponent<BoxCollider2D>();
        footCol = foot_R.GetComponent <BoxCollider2D>();
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

        if (distanceFromTarget < 4.5f ) {
            anim.SetBool("EnemyWalk", false);
            handCol.enabled = true;
            footCol.enabled = true;
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
            other.GetComponent<CharacterHealth>().Die();
    }

 
    void Attack()
    {
        int r = (int)Random.Range(1, 3);
        anim.SetInteger("RandomATK", r);
    }
    public void RandomATKNull()
    { 
        anim.SetInteger("RandomATK", 0);
        anim.Play("EnemyIdle");
    }

    public void TookDamage(float damage)
    {
        if(curHealth <= 0F)
        {
            
        }

        curHealth -= damage;

        enemyhealthbar.value = CalculateHealth();


    }

    

    float CalculateHealth()
    {
        return curHealth / maxHealth;
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
