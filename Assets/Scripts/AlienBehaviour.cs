using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlienBehaviour : MonoBehaviour
{
    public Transform playerTrans;
    private Renderer alienRend;
    private Rigidbody alienRb;
    private bool counted;
    List<GameObject> MatchingAliens = new List<GameObject>();

    private void Start()
    {
        playerTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        alienRend = gameObject.GetComponent<Renderer>();
        alienRb = gameObject.GetComponent<Rigidbody>();
        int a = Random.Range(0, 3);
        switch(a)
        {
            case 0: alienRend.material.color = Color.red; break;
            case 1: alienRend.material.color = Color.green; break;
            case 2: alienRend.material.color = Color.blue; break;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Stack")
        {
            //alienRb.isKinematic = true;
            Match(gameObject);
        }
        if (collision.collider.tag == "Player")
        {
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        playerTrans.gameObject.GetComponent<PlayerBehaviour>().dead = true;
        yield return new WaitForSecondsRealtime(1.5f);
        playerTrans.gameObject.GetComponent<PlayerBehaviour>().dead = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Match(GameObject startRay)
    {
        MatchingAliens.Add(startRay);
        counted = true;
        RaycastHit hit;
        Physics.Raycast(startRay.transform.position, Vector3.up, out hit, 1);
        Debug.DrawRay(startRay.transform.position, Vector3.up, Color.magenta, 1);
        if (hit.collider != null && hit.collider.gameObject.GetComponent<AlienBehaviour>().counted == false && hit.collider.gameObject.GetComponent<Renderer>().material.color == alienRend.material.color)
        {
            Match(hit.collider.gameObject);
        }
        if (MatchingAliens.Count >= 2)
        {
            Debug.Log(MatchingAliens.Count);
            Debug.Log(alienRend.material.color);
            foreach (GameObject alien in MatchingAliens)
            {
                Destroy(alien);
            }
        }
    }
}
