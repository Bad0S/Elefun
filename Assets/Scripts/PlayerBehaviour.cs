﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [Range(0.1f,5)]
    public float moveSpeed;
    public bool dead;
    public Camera cam;
    
	void Start ()
    {
	}
	
	void Update ()
    {
        if (cam.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x && Input.mousePosition.y >= (Screen.height / 2 - 100) && Input.GetMouseButton(0))
        {
            if (transform.position.x > -5.1f)
            {
                transform.position += Vector3.left * moveSpeed;
            }
        }
        if (cam.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x && Input.mousePosition.y >= (Screen.height/2 - 100) && Input.GetMouseButton(0))
        {
            if (transform.position.x < 5.1f)
            {
                transform.position += Vector3.right * moveSpeed;
            }
        }

        if (dead)
        { moveSpeed = 0; }
	}
}
