using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private bool attacking = false;
    public float walkSpeed = 5F;
    public float runSpeed = 18F;
    private bool facingright = false;
    public float health;
    Animator anim;
   
    Rigidbody2D rigidbody2D;

    

    // Update is called once per frame
    void Start()
    {
        
        anim = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
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

        Debug.Log(speed);
        //Debug.Log(horizontal);
        anim.SetFloat("Speed", speed);
        
        


        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetBool("Hitting", true);
            Debug.Log("Hyökätään");
            attacking = true;

        }


        if (Input.GetKeyDown(KeyCode.Z))
        {
      
            Debug.Log("Potkitaan");
            attacking = true;

        }

        
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            Debug.Log("Collision tuli");
        }
    }

    void TakeSomeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Debug.Log("Dead)");
        }
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
        transform.Translate(Vector3.right * (speed * direction) * Time.deltaTime);
        //rigidbody2D.AddForce(new Vector2(direction * maxSpeed, 0), ForceMode2D.Force);
        Debug.Log(speed);
  

    }
    void AnimationPlayed()
    {
        anim.SetBool("Hitting", false);
    }

   
}
