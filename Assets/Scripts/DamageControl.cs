using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DamageControl : MonoBehaviour
{
    public float damage;
    public GameObject chrhealth;
     CharacterHealth chr_script;
    // Use this for initialization
    void Start()
    {
        chr_script = chrhealth.GetComponent<CharacterHealth>();
       

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
            {
            if (this.gameObject.tag == "Hand_R")
            {
                damage = 10F;
                chr_script.TakeSomeDamage(damage);
            }
            if(this.gameObject.tag == "Foot_R")
            {
                damage = 20F;
                chr_script.TakeSomeDamage(damage);
            }
        }
    }
}
