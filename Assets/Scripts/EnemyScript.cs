using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{

    public Transform target;
    public float maxHealth;
    public float curHealth;


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
   

    private bool facingright = false;
    bool isDead = false;
    bool takinghits;





    void Start()
    {
        takinghits = false;
        curHealth = maxHealth;

        enemyhealthbar.value = CalculateHealth();
        if (name == "Enemi3")
        {
            hand_R = GameObject.Find("Right_Hand");
            foot_R = GameObject.Find("Right_Foot");
        }
        else if (name == "Enemi1")
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
        else if(distanceFromTarget > 50)
        {
            DisableWalk();
        }

    }


    void Chase()
    {
        float movementDistance = moveSpeed * Time.deltaTime;
        if (target != null)
        {
            transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), new Vector2(target.position.x, transform.position.y), movementDistance);
        }

        if (transform.position.x > target.position.x && facingright)
        {

            Flip();
        }

        if (transform.position.x < target.position.x && !facingright)
        {
            Flip();

        }

        if (distanceFromTarget > 8.0f)
        {
            takinghits = false;
            handCol.enabled = false;
            footCol.enabled = false;
            anim.SetInteger("RandomATK", 0);
            anim.SetBool("EnemyWalk", true);
            moveSpeed = 20;

        }


        if (distanceFromTarget < 4.8f)
        {
            if (!takinghits)
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
    }

    void Die()
    {
        if (playerObject != null)
            other.GetComponent<CharacterHealth>().Die();
    }


 
    public void RandomATKNull()
    {

        anim.SetInteger("RandomATK", 0);
        anim.Play("EnemyIdle");
        takinghits = false;

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
            takinghits = true;
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
        isDead = true;
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
        takinghits = false;

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

    public void DisableWalk()
    {
        anim.SetBool("EnemyWalk", false);
        anim.Play("EnemyIdle");
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



