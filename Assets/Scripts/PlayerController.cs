using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    
    public float walkSpeed;
    public float runSpeed;
    float maxHealth;
    float curHealth;
    public float jumpKickPowah;
    private bool facingright = false;
    bool isAttacking;
    bool isTouchingGround;
    public Slider healthBar;
    Animator anim;
    Rigidbody2D rigidbody2D;

    // Update is called once per frame
    void Start()
    {
        maxHealth = 100F;
        anim = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        Debug.Log("Parent transform: " + transform.position);
        //ResetChildPositions();
        curHealth = maxHealth;

        healthBar.value = CalculateHealth();
      
    }

    void FixedUpdate()
    {

        float speed = 0;
        float horizontal = Input.GetAxis("Horizontal");
        Flip(horizontal);


        if (horizontal != 0F)
        {
       
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = runSpeed;
                
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                speed = walkSpeed;
                anim.Play("NekoWalk");
               
                
            }
            else
            {
                speed = walkSpeed;
            }

            PlayerMovement(horizontal,speed);
          
        }

        else if (horizontal == 0F)
        {
            speed = 0;
        }

        
        //Debug.Log(horizontal);
        anim.SetFloat("Speed", speed);
       

        if (Input.GetKeyDown(KeyCode.A))
        {
            isAttacking = true;
            isTouchingGround = true;
            Debug.Log("Hyökätään");
            anim.SetBool("Hitting", true);

        }


        if (Input.GetKeyDown(KeyCode.Z) && isTouchingGround == true)
        {
      
            Debug.Log("Potkitaan");
            isAttacking = true;
            anim.SetBool("Kicking", true);
            rigidbody2D.AddForce(Vector2.up * jumpKickPowah, ForceMode2D.Impulse);
            isTouchingGround = false;


        }
        
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            Debug.Log("Collision tuli");
        }
        if (coll.gameObject.tag == "Ground")
        {
            isTouchingGround = true;
      
            
        }
   
    }

    float CalculateHealth()
    {
        return curHealth / maxHealth;

    }


    public void TakeSomeDamage(float damage)
    {

        if(curHealth <= 0)
        {          
            Die();
        }

        
        curHealth -= damage;

        healthBar.value = CalculateHealth();
    }

    private void Flip(float horizontal)
    {
        if(( horizontal < 0 && facingright == false || horizontal > 0 && facingright == true))
        {
            facingright = !facingright;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
           
        }

    }

    void PlayerMovement(float direction, float speed)
    {
        Debug.Log("Move");
        transform.Translate(Vector3.right * (speed * direction) * Time.deltaTime);
        //rigidbody2D.AddForce(new Vector2(direction * maxSpeed, 0), ForceMode2D.Force);
        
  

    }
    void AnimationPlayed()
    {
        anim.SetBool("Hitting", false);
        anim.SetBool("Kicking", false);
    }

    void ResetChildPositions()
    {
        int childs = transform.childCount;

        for(int i=0; i<childs; i++)
        {
            if(transform.GetChild(i).IsChildOf(transform))
            {
                transform.GetChild(i).position = transform.position;
            }
        }
    }

    public void Die()
    {
        curHealth = 0;

        Debug.Log("Kuolit!");

        Destroy(gameObject);


    }




    



   
}
