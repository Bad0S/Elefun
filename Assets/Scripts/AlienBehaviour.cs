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
        Match(Vector3.right);
        Match(Vector3.left);
        Match(Vector3.up);
        Match(Vector3.down);

        /*Debug.DrawRay(transform.position, Vector3.right, Color.green,1);
        Debug.DrawRay(transform.position, Vector3.left, Color.blue, 1);
        if (Physics.Raycast(transform.position, Vector3.right, 1f))
        {
            rightFree = false;
        }
        else
        {
            rightFree = true;
        }
        if (Physics.Raycast(transform.position, Vector3.left, 1f))
        {
            leftFree = false;
        }
        else
        {
            leftFree = true;
        }
        if (stacked)
        {
            if (transform.position.x + 1 < playerTrans.position.x && rightFree)
            {
                transform.position += Vector3.right;
            }
            if (transform.position.x - 1 > playerTrans.position.x && leftFree)
            {
                transform.position += Vector3.left;
            }
        }*/
    }
    /*
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
    private List<GameObject> FindMatch(Vector2 castDir) { // 1
    List<GameObject> matchingTiles = new List<GameObject>(); // 2
    RaycastHit2D hit = Physics2D.Raycast(transform.position, castDir); // 3
    while (hit.collider != null && hit.collider.GetComponent<SpriteRenderer>().sprite == render.sprite) { // 4
        matchingTiles.Add(hit.collider.gameObject);
        hit = Physics2D.Raycast(hit.collider.transform.position, castDir);
    }
    return matchingTiles; // 5
}

         */
    private void Match (Vector3 dir)
    {
        List<GameObject> matchingAliens = new List<GameObject>();
        matchingAliens.Add(gameObject);
        RaycastHit hit;
        Physics.Raycast(transform.position, dir, out hit, 1);
        Debug.DrawRay(transform.position, dir, Color.blue, 1);
        while (hit.collider != null && hit.collider.tag == "Stack")
        {
            matchingAliens.Add(hit.collider.gameObject);
            Physics.Raycast(hit.collider.transform.position, dir, out hit,1);
            Debug.DrawRay(transform.position, dir, Color.blue, 1);
        }
        if (matchingAliens.Count >= 3)
        {
            foreach (GameObject alien in matchingAliens)
            {
                Destroy(alien);
            }
        }
    }
}
