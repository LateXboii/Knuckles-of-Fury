using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CharacterHealth : MonoBehaviour {

    float maxHealth;
    float curHealth;
    Animator anim;
    public Slider healthBar;

    
    // Use this for initialization
    void Start()
    {

        maxHealth = 10000F;

        curHealth = maxHealth;

        anim = GetComponent<Animator>();

        healthBar.value = CalculateHealth();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    void OnCollisionEnter2D(Collision2D coll)
    {


    }

    float CalculateHealth()
    {
        return curHealth / maxHealth;

    }

    public void Addhealth(int healthToAdd)
    {
        curHealth += healthToAdd;

        healthBar.value = CalculateHealth();
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

