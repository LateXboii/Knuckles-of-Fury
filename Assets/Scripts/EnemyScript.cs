using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{

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
    GameObject objectname;
    public Slider enemyhealthbar;
    public float moveSpeed;
    public float distanceFromTarget;
    public GameObject playerObject;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    bool grounded;
    bool hasSpottedPlayer;

    private bool facingright = false;
    bool isDead = false;
    bool takingDamage = false;
   
    


    void Start()
    {
        grounded = true;
        curHealth = maxHealth;

        enemyhealthbar.value = CalculateHealth();
        if(name == "Enemi3")
        {
            hand_R = GameObject.Find("Right_Hand");
            foot_R = GameObject.Find("Right_Foot");
        }
        else if(name == "Enemi1")
        {
            hand_R = GameObject.Find("Hand_R");
            foot_R = GameObject.Find("Foot_R");
        }
        other = GameObject.Find("CharacterHealth");
        handCol = hand_R.GetComponent<BoxCollider2D>();
        footCol = foot_R.GetComponent<BoxCollider2D>();
        handCol.enabled = false;
        footCol.enabled = false;

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        myTransform = GetComponent<Transform>();
        hasSpottedPlayer = false;

        
    }

    void FixedUpdate()
    {
        if (groundCheck != null)
            grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);


    }

    void Update()
    {

        if (playerObject != null)
        {

            distanceFromTarget = Vector2.Distance(playerObject.transform.position, gameObject.transform.position);
       

        }
        else if (playerObject == null)
        {
            Die();

        }

        if (distanceFromTarget < 50)
        {
            Chase();
        }
    }


    void Chase()
    {
        float movementDistance = moveSpeed * Time.deltaTime;
        Debug.Log("movementDistance: " + movementDistance);
        if (target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, movementDistance);
            
   
        }

        if (transform.position.x > target.position.x && facingright)
        {

            Flip();
        }

        if (transform.position.x < target.position.x && !facingright)
        {
            Flip();

        }

        if (distanceFromTarget > 10.0f )
        {
            

            handCol.enabled = false;
            footCol.enabled = false;
            anim.SetInteger("RandomATK", 0);
            anim.SetBool("EnemyWalk", true);
            moveSpeed = 16;
            


        }

        if (distanceFromTarget < 5.3f )
        {
            

            anim.SetBool("EnemyWalk", false);
            if (anim.GetInteger("RandomATK") == 0)
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
        if (!takingDamage)
        {
            int r = (int)Random.Range(1, 3);
            anim.SetInteger("RandomATK", r);
        }
    }
    public void RandomATKNull()
    {

        anim.SetInteger("RandomATK", 0);
        anim.Play("EnemyIdle");


    }

    public void TookDamage(float damage)
    {
        if (curHealth <= 50F)
        {
            Death();
        }

        if (!isDead)
        {
            anim.SetInteger("RandomATK", 0);
            takingDamage = true;
            anim.SetBool("EnemyHit2", true);
        }

        curHealth -= damage;
        enemyhealthbar.value = CalculateHealth();

    }


    private void Flip()
    {
        facingright = !facingright;
        Vector2 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


    float CalculateHealth()
    {
        return curHealth / maxHealth;
    }


    void Death()
    {
        Destroy(gameObject);
    }

    public void EnemyColliderActivate()
    {
        handCol.enabled = true;
        footCol.enabled = true;
    }

    public void EnemyColliderDisable()
    {
        handCol.enabled = false;
        handCol.enabled = false;
    }


    void EnemyAnimationPlayed()
    {
        anim.SetBool("EnemyHit2", false);
        takingDamage = false;

    }

    void ResetChildPositions()
    {
        int childs = transform.childCount;

        for (int i = 0; i < childs; i++)
        {
            if (transform.GetChild(i).IsChildOf(transform))
            {
                transform.GetChild(i).position = transform.position;
            }
        }
    }



    /*public void Fleeing()
    {
        Vector3 awayFromPlayer = transform.position - playerObject.transform.position;
        awayFromPlayer.Normalize();

        transform.LookAt(transform.position + awayFromPlayer);
        if (curHealth < 5)
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
     aa   }*/
}



