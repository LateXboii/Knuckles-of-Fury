using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageControl : MonoBehaviour {

    public int damage;
    public GameObject enemi;
    EnemyScript enmi_script;
	// Use this for initialization
	void Start ()
    {
        enmi_script = enemi.GetComponent<EnemyScript>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
                {
            if (this.gameObject.tag == "Arm_left")
            {
                damage = 20;
                enmi_script.TookDamage(damage);
            }
        }
        if (this.gameObject.tag == "Paw_left")
        {
            damage = 20;
            enmi_script.TookDamage(damage);
        }
    }
}
