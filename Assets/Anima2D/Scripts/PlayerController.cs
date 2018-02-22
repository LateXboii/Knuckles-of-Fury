using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private bool attacking = false;
    public float walkSpeed = 10F;
    public float runSpeed = 18F;
    private bool facingright = false;
    private bool running = false;
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
        float speed;
        float horizontal = Input.GetAxis("Horizontal");
         Flip(horizontal);


        if (horizontal != 0F)
        {
       
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = runSpeed;
                anim.SetBool("Idle", false);
                anim.SetBool("Walking", false);
                anim.SetBool("Running", true);
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                speed = walkSpeed;
                anim.SetBool("Running", false);
                anim.SetBool("Walking", true);
            }
            else
            {
                speed = walkSpeed;
                anim.SetBool("Idle", false);
                anim.SetBool("Walking", true);
            }

            PlayerMovement(horizontal,speed);
          
        }
        else if(Input.GetKey(KeyCode.LeftShift))
        {
            if (horizontal < 0F)
            {
                speed = runSpeed;
                anim.SetBool("Idle", false);
                anim.SetBool("Running", true);
                PlayerMovement(horizontal, speed);
            }
            else
            {
                anim.SetBool("Running", false);
                anim.SetBool("Idle", true);
            }
        }
        else if (horizontal == 0)
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Running", false);
            anim.SetBool("Idle", true);
        }

        
        //Debug.Log(horizontal);
        anim.SetFloat("Speed", Mathf.Abs(horizontal));
        
        


        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetBool("Hitting", true);
            Debug.Log("Hyökätään");
            attacking = true;

        }


        if (Input.GetKeyDown(KeyCode.Z))
        {
            anim.SetBool("Kicking", true);
            Debug.Log("Potkitaan");
            attacking = true;

        }

        
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            Debug.Log("Trigger tuli");
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
        anim.SetBool("Kicking", false);
        anim.SetBool("Running", false);
    }

   
}
