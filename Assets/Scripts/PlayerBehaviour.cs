using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [Range(0.1f,5)]
    public float moveSpeed;
    public bool dead;
    public Camera cam;
    private Animator playerAnim;
    
	void Start ()
    {
        playerAnim = GetComponentInChildren<Animator>();
	}
	
	void Update ()
    {
        if (cam.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x && Input.mousePosition.y >= (Screen.height *0.4f) && Input.GetMouseButton(0))
        {
            if (transform.position.x > -5.1f && UIManager.pause == false)
            {
                transform.position += Vector3.left * moveSpeed;
            }
        }
        if (cam.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x && Input.mousePosition.y >= (Screen.height * 0.4f) && Input.GetMouseButton(0))
        {
            if (transform.position.x < 5.1f && UIManager.pause == false)
            {
                transform.position += Vector3.right * moveSpeed;
            }
        }
        
        if (UIManager.pause == true)
        {
            moveSpeed = 0;
        }
        else
        {
            moveSpeed = 0.15f;
        }
	}

    public void ShootAnim()
    {
        playerAnim.SetTrigger("Shoot");
    }

    public void DeathAnim()
    {
        playerAnim.SetTrigger("Death");
    }
}
