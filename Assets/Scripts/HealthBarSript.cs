using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarSript : MonoBehaviour {

    public Slider enemyhealthbar;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 healthbarpos = Camera.main.WorldToScreenPoint(this.transform.position);
        enemyhealthbar.transform.position = healthbarpos;
	}
}
