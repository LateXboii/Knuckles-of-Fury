using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageControl : MonoBehaviour {

    public int damage;
    GameObject player;
    CharacterHealth chr_health;
	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("Neko");
        chr_health = player.GetComponent<CharacterHealth>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.gameObject.CompareTag("Player"))
        {
            if (gameObject.tag == "Hand_R")
            {
                damage = 20;
                chr_health.TakeSomeDamage(damage);
            }

            if (gameObject.tag == "Foot_R")
            {
                damage = 30;
                chr_health.TakeSomeDamage(damage);
            }
        }
    }
}
