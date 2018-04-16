using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DamageControl : MonoBehaviour
{
    public float damage;
    GameObject enemy;
    EnemyScript enmiscript;
   

    // Use this for initialization
    void Start()
    {
        enemy = GameObject.Find("Enemi 1");
        enmiscript = enemy.GetComponent<EnemyScript>();
      
       

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
            {
            if (gameObject.tag == "Arm_left")
            {
                damage = 20F;
                enmiscript.TookDamage(damage);

            }
            if(gameObject.tag == "Paw_left")
            {
                damage = 40F;
                enmiscript.TookDamage(damage);
            }
        }
    }
}
