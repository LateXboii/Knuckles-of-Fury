using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public float walkSpeed;
    public float runSpeed;
    public float jumpKickPowah;
    public float jumpPower;
    private bool facingright = false;
    bool isAttacking;
    bool isTouchingGround;
    private Slider slider;
    GameObject playerSpecialSlider;

    Animator anim;
    Rigidbody2D rb2D;

    // Update is called once per frame
    void Start()
    {
        playerSpecialSlider = GameObject.Find("Special Bar");
        anim = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        Debug.Log("Parent transform: " + transform.position);
        //ResetChildPositions();
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

            PlayerMovement(horizontal, speed);

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
            rb2D.AddForce(Vector2.up * jumpKickPowah, ForceMode2D.Impulse);
            isTouchingGround = false;


        }

        if (Input.GetKeyDown(KeyCode.Space) && isTouchingGround == true)
        {
            anim.SetBool("Jumping", true);
            rb2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isTouchingGround = false;

        }

        if (Input.GetKeyDown(KeyCode.S) && playerSpecialSlider.GetComponent<SpecialBarScript>().CanUse)
        {
            rb2D.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            rb2D.AddForce(Vector2.right * 10000, ForceMode2D.Impulse);
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
        anim.SetBool("Jumping", false);
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

   




    



   
}
