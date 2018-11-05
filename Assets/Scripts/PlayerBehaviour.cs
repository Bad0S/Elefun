using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [Range(0.1f,5)]
    public float moveSpeed;
    public bool dead;
    
	void Start ()
    {
		
	}
	
	void Update ()
    {
		if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > -5.1f)
        {
            transform.position += Vector3.left * moveSpeed;
        }
        if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < 5.1f)
        {
            transform.position += Vector3.right * moveSpeed;
        }

        if (dead)
        { moveSpeed = 0; }
	}
}
