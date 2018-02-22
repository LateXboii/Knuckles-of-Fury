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

        Chase();

    }
    void OnCollisionEnter2D(Collision2D col)
    {
       if(col.gameObject.tag == "Player")
        {
            //Distance
        }
    }

    void Chase()
    {
        float movementDistance = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, movementDistance);
    }

    private void Attack()
    {
        

       
       
    }


}
