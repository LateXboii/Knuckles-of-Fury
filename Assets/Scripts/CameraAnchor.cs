using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnchor : MonoBehaviour {


    public Transform player;

    public float distance = 10f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        transform.position = player.position + new Vector3(distance, 0, -1);
	}
}
