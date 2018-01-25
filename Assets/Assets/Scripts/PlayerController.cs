using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    public float moveSpeed;
    private bool attacking = false;
    public float jumpForce;
    public bool animation_lool;
    public Sprite isku;
    public Sprite TESTIHAHMO;
    Animator anim;

    Rigidbody2D rb;
    
	// Update is called once per frame
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
    }

    void Update ()
    {
       
        if (Input.GetKey (KeyCode.RightArrow))
        {
           rb.velocity = new Vector2(moveSpeed, 0);

        }
        if(Input.GetKey (KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-moveSpeed, 0); 
        }
        if (Input.GetKey (KeyCode.Space))
        {
            rb.velocity = new Vector2(0, jumpForce);
            
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
           
            Debug.Log("Hyökätään");
            attacking = true;
            anim.SetBool("Iskee", true);
            
           
        }
       
        if(Input.GetKeyDown(KeyCode.S) && !attacking)
        {
            Debug.Log("PUNCH");
            attacking = true;

        }
        if (Input.GetKeyDown(KeyCode.D) && !attacking)
        {
            Debug.Log("PUNCH");
            attacking = true;
           
        }

       
    }

   void OnCollisionEnter2D(Collision2D col)

    {
        if(coll.gameObject.tag == "Enemy")
        {
            Destroy();
        }
        

    }

    public void AnimationPlayed()
    {
        anim.SetBool("Iskee", false);
    }
}
