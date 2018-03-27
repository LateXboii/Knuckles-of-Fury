using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    float maxHealth;
    float curHealth;
    public Slider healthBar;

    // Use this for initialization
    void Start ()
    {
        maxHealth = 100F;

        curHealth = maxHealth;

        healthBar.value = CalculateHealth();

    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		if(Input.GetKeyDown(KeyCode.X))
        {
            TakeSomeDamage(30F);
        }
	}

   void OnCollisionEnter2D(Collision2D coll)
    {
        
        
    }

    float CalculateHealth()
    {
        return curHealth / maxHealth;

    }

    public void TakeSomeDamage(float damage)
    {

        if (curHealth <= 0F)
        {
            Die();
        }



        curHealth -= damage;

        healthBar.value = CalculateHealth();
    }

    public void Die()
    {
        
        curHealth = 0;
        SceneManager.LoadScene(0);
        


    }
}
