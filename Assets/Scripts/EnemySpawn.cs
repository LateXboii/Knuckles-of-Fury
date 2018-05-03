using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {

    public GameObject[] enemies;
    
    public int numberOfenemies;
    public float spawnTime;
    private bool isDead = false;

    
    private int currentenemies;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!isDead)
        {
            if (currentenemies >= numberOfenemies)
            {
                int enemies = FindObjectsOfType<EnemyScript>().Length;
                if (enemies <= 0)
                {
                    
                    gameObject.SetActive(false);
                }
            }

        }
	}

    public void SpawnRandom()
    {
        if (!isDead)
        {

            bool positionX = Random.Range(0, 2) == 0 ? true : false;
            Vector2 spawnPosition;
            spawnPosition.x = Random.Range(197f, 210f);

            if (positionX)
            {
                spawnPosition = new Vector3(spawnPosition.x, 2.85f, 0);
            }

            else
            {
                spawnPosition = new Vector3(spawnPosition.x, 2.85f, 0);
            }

            Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPosition, Quaternion.identity);
            currentenemies++;
            if (currentenemies <= numberOfenemies)
            {
                Invoke("SpawnEnemy", spawnTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            GetComponent<BoxCollider2D>().enabled = false;
            SpawnRandom();
        }
    }
}
