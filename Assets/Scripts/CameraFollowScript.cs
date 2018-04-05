using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour {

    public float xMargin = 1f;


    public float xSmooth = 8f;
    public float ySmooth = 8f;
    public Vector2 maxXandY;
    public Vector2 minXandY;

    private Transform player;
	// Use this for initialization
	private void Awake () {

        player = GameObject.FindGameObjectWithTag("Player").transform;
	}

    private bool CheckXMargin()
    {
        return (transform.position.x - player.position.x) < xMargin;
    }
	
	// Update is called once per frame
	void Update ()
    {
        TrackPlayer();
	}

    private void TrackPlayer()
    {
        float targetX = transform.position.x;
        float targetY = transform.position.y;

        if (CheckXMargin())
        {
            targetX = Mathf.Lerp(transform.position.x, player.position.x, xSmooth * Time.deltaTime);
        }






        targetX = Mathf.Clamp(targetX, minXandY.x, maxXandY.x);
        targetY = Mathf.Clamp(targetY, minXandY.y, maxXandY.y);

        
    }

}
