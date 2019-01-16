using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [Range(0.1f,5)]
    public float moveSpeed;
    public float moveSpeedInit;
    public bool dead;
    public Camera cam;
    private Animator playerAnim;
    
	void Start ()
    {
        playerAnim = GetComponentInChildren<Animator>();
        moveSpeedInit = moveSpeed;
	}
	
	void Update ()
    {
        if (Input.mousePosition.y <= (Screen.height * 0.89f) && !dead)
        {
            if (cam.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x && Input.mousePosition.y >= (Screen.height * 0.4f) && Input.GetMouseButton(0))
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
        }
        
        if (UIManager.pause && !UIManager.title)
        {
            moveSpeed = 0;
            playerAnim.speed = 0;
        }
        else
        {
            moveSpeed = moveSpeedInit;
            playerAnim.speed = 1;
        }
	}

    public void ShootAnim()
    {
        playerAnim.SetTrigger("Shoot");
    }

    public void DeathAnim()
    {
        if (!dead)
        {
            playerAnim.SetTrigger("Death");
        }
    }
}
