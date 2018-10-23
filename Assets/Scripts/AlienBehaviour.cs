using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlienBehaviour : MonoBehaviour
{
    public Transform playerTrans;
    public bool stacked;
    public bool rightFree;
    public bool leftFree;

    private void Start()
    {
        playerTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, Vector3.right, 1);
        if (Physics.Raycast(transform.position, Vector3.right, 1f))
        {
            rightFree = false;
        }
        if (Physics.Raycast(transform.position, Vector3.left, 1f))
        {
            leftFree = false;
        }
        if (stacked)
        {
            if (transform.position.x - 1 < playerTrans.position.x && rightFree)
            {
                transform.position += Vector3.right;
            }
            if (transform.position.x - 1 < playerTrans.position.x && leftFree)
            {
                transform.position += Vector3.left;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(EndGame(collision.gameObject));
        }
        if (collision.gameObject.tag == "Stack")
        {
            stacked = true;
        }
    }
    private IEnumerator EndGame(GameObject obj)
    {
        Destroy(obj);
        yield return new WaitForSecondsRealtime(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
