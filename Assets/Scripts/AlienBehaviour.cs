using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlienBehaviour : MonoBehaviour
{
    public Transform playerTrans;
    private Renderer alienRend;
    private Rigidbody alienRb;
    public bool counted;

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
            AddList(FourCast(gameObject));
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

    private List<GameObject> FourCast(GameObject FourCastCenter)
    {
        List<GameObject> FourCastList = new List<GameObject>();
        RaycastHit hit;
        Physics.Raycast(gameObject.transform.position, Vector3.up, out hit, 1);
        Debug.DrawRay(gameObject.transform.position, Vector3.up, Color.magenta, 5);
        if (hit.collider != null)
        { FourCastList.Add(hit.collider.gameObject); }
        Physics.Raycast(gameObject.transform.position, Vector3.down, out hit, 1);
        Debug.DrawRay(gameObject.transform.position, Vector3.down, Color.magenta, 5);
        if (hit.collider != null)
        { FourCastList.Add(hit.collider.gameObject); }
        Physics.Raycast(gameObject.transform.position, Vector3.left, out hit, 1);
        Debug.DrawRay(gameObject.transform.position, Vector3.left, Color.magenta, 5);
        if (hit.collider != null)
        { FourCastList.Add(hit.collider.gameObject); }
        Physics.Raycast(gameObject.transform.position, Vector3.right, out hit, 1);
        Debug.DrawRay(gameObject.transform.position, Vector3.right, Color.magenta, 5);
        if (hit.collider != null)
        { FourCastList.Add(hit.collider.gameObject); }
        return FourCastList;
    }

    private void AddList(List<GameObject> FourCastList)
    {
        List<GameObject> MatchingAliens = new List<GameObject>();
        MatchingAliens.Add(gameObject);
        foreach (GameObject alien in FourCastList)
        {
            if (alien.GetComponent<Renderer>().material.color == gameObject.GetComponent<Renderer>().material.color)
            {
                MatchingAliens.Add(alien);
                FourCast(alien);
            }
        }
        if(MatchingAliens.Count >= 3)
        {
            Debug.Log(MatchingAliens.Count);
            foreach (GameObject alien in MatchingAliens)
            {
                Destroy(alien);
            }
        }
    }
}
