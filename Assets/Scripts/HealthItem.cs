using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour {

    public int healthToAdd;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() == null)
            return;

        other.GetComponent<CharacterHealth>().Addhealth(healthToAdd);

        Destroy(gameObject);
    }
}