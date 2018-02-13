using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
    
    public Transform target;
    Transform myTransform;
    int direction;
    public int damage;
    Animator anim;
    public float moveSpeed;
    private float distanceToPlayer;
    

	// Use this for initialization
	void Start ()
    {
        

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        myTransform = GetComponent<Transform>();



    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector2.Distance(target.position, transform.position);
        Debug.Log(distance);
        if(distance < 4.0F)
        {
            anim.SetBool("walk", false);
            anim.SetBool("lyonti", true);

        }
        else if (distance < 15.0f )
        {
            myTransform.position += myTransform.right * -moveSpeed * Time.deltaTime;
            anim.SetBool("walk", true);
        }

         

    }
    void OnCollisionEnter2D(Collision2D col)
    {
       if(col.gameObject.tag == "Player")
        {
            //Distance
        }
    }

    void KnockedOut()
    {

    }

    private void Attack()
    {
        

       
       
    }
}
