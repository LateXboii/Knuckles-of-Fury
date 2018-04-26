using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    GameObject paw_left;
    GameObject arm_left;
    BoxCollider2D armCol;
    BoxCollider2D pawCol;
    public float walkSpeed;
    public float runSpeed;
    public float jumpKickPower;
    float jumpPower = 27;
    bool facingright = false;
    bool isTouchingGround;
    bool isJumping;
   



    Animator anim;
    Rigidbody2D rb2D;

    // Update is called once per frame
    void Start()
    {

        arm_left = GameObject.Find("Arm_left");
        paw_left = GameObject.Find("Paw_left");
        armCol = arm_left.GetComponent<BoxCollider2D>();
        pawCol = paw_left.GetComponent<BoxCollider2D>();
        armCol.enabled = false;
        pawCol.enabled = false;
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
            PlayerActionsDisabled();

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
        anim.SetFloat("Speed", speed);

        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayerActionsEnabled();
            anim.SetBool("Hitting", true);
        }

        if (Input.GetKeyDown(KeyCode.Z) && isTouchingGround == true)
        {
            PlayerActionsEnabled();
            anim.SetBool("Kicking", true);
            rb2D.AddForce(Vector2.up * jumpKickPower, ForceMode2D.Impulse);
            isTouchingGround = false;
            

        }

        if (Input.GetKeyDown(KeyCode.Space) && isTouchingGround == true)
        {
            PlayerActionsDisabled();
            anim.SetBool("Jumping", true);
            rb2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isTouchingGround = false;
            
            
            
            
            
        }

    }


    void OnCollisionEnter2D(Collision2D coll)
    {

       
        if (coll.gameObject.tag == "Ground")
        {
            isTouchingGround = true;

        }


    }


    private void Flip(float horizontal)
    {
        if ((horizontal < 0 && facingright == false || horizontal > 0 && facingright == true))
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

        for (int i = 0; i < childs; i++)
        {
            if (transform.GetChild(i).IsChildOf(transform))
            {
                transform.GetChild(i).position = transform.position;
            }
        }
    }

    public void PlayerActionsDisabled()
    {
        armCol.enabled = false;
        pawCol.enabled = false;
    }

    public void PlayerActionsEnabled()
    {
        armCol.enabled = true;
        pawCol.enabled = true;
    }

    


   








}
