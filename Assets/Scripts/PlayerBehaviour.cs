using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [Range(0.1f,5)]
    public float moveSpeed;
    
	void Start ()
    {
		
	}
	
	void Update ()
    {
		if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > -6.9f)
        {
            transform.position += Vector3.left * moveSpeed;
        }
        if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < 6.9f)
        {
            transform.position += Vector3.right * moveSpeed;
        }
	}
}
