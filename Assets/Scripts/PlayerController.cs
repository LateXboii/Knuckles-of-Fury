using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    public float moveSpeed;
    private bool attacking = false;
    public float jumpForce;
    public bool animation_lool;
    public float health;
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
       
        if(Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("KICk");
            attacking = true;
            anim.SetBool("Potkia", true);

        }
        if (Input.GetKeyDown(KeyCode.D) && !attacking)
        {
            Debug.Log("PUNCH");
            attacking = true;
           
        }
       
        

       
    }



    public void AnimationPlayed()
    {
        anim.SetBool("Iskee", false);
        anim.SetBool("Potkia", false);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            Debug.Log("Trigger tuli");
        }
    }

    void TakeSomeDamage( int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Debug.Log("Dead)");
        }
    }
}
