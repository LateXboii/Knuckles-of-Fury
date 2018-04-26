using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialBarScript : MonoBehaviour {
    public Slider slider;
    public bool CanUse = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        slider.value = Mathf.MoveTowards(slider.value, 100.0f, 0.0005f);

        if (slider.value == 1f)
        {
            CanUse = true;
        }

        if (CanUse && Input.GetKeyDown(KeyCode.S))
        {
            slider.value = 0f;
        }
	}
}
