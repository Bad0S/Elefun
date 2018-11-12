﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlienBehaviour : MonoBehaviour
{
    public Transform playerTrans;
    private Renderer alienRend;
    private Rigidbody alienRb;
    public bool counted;
    public bool stacked;
    public bool leftFree;
    public bool rightFree;
    public Material redMat;
    public Material greenMat;
    public Material blueMat;
    public bool destroyed;
    private ScoreManager scoreManager;

    private void Start()
    {
        scoreManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ScoreManager>();
        playerTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        alienRend = gameObject.GetComponent<Renderer>();
        alienRb = gameObject.GetComponent<Rigidbody>();
        int a = Random.Range(0, 3);
        switch(a)
        {
            case 0: alienRend.material = redMat; break;
            case 1: alienRend.material = greenMat; break;
            case 2: alienRend.material = blueMat; break;
        }
    }

    List<GameObject> MatchingAliens;

    private void Update()
    {
        DetectFree();
        if (Input.GetKeyDown(KeyCode.Return) && !destroyed)
        {
            MatchingAliens = new List<GameObject>();
            MatchingAliens.Add(gameObject);
            AddList(FourCast(gameObject));
            DestroyAliens();
        }
        if (Input.GetKeyDown(KeyCode.Q) && stacked )
        {
            MoveLeft();
        }
        if (Input.GetKeyDown(KeyCode.D) && stacked)
        {
            MoveRight();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Stack")
        {
            stacked = true;
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

        Physics.Raycast(FourCastCenter.transform.position, Vector3.up, out hit, 1);
        Debug.DrawRay(FourCastCenter.transform.position, Vector3.up, Color.magenta, 5);
        if (hit.collider != null)
            if(!MatchingAliens.Contains(hit.collider.gameObject))
        { FourCastList.Add(hit.collider.gameObject);}

        Physics.Raycast(FourCastCenter.transform.position, Vector3.down, out hit, 1);
        Debug.DrawRay(FourCastCenter.transform.position, Vector3.down, Color.magenta, 5);
        if (hit.collider != null)
            if (!MatchingAliens.Contains(hit.collider.gameObject))
            { FourCastList.Add(hit.collider.gameObject); }

        Physics.Raycast(FourCastCenter.transform.position, Vector3.left, out hit, 1);
        Debug.DrawRay(FourCastCenter.transform.position, Vector3.left, Color.magenta, 5);
        if (hit.collider != null)
            if (!MatchingAliens.Contains(hit.collider.gameObject))
            { FourCastList.Add(hit.collider.gameObject); }

        Physics.Raycast(FourCastCenter.transform.position, Vector3.right, out hit, 1);
        Debug.DrawRay(FourCastCenter.transform.position, Vector3.right, Color.magenta, 5);
        if (hit.collider != null)
            if (!MatchingAliens.Contains(hit.collider.gameObject))
            { FourCastList.Add(hit.collider.gameObject); }

        return FourCastList;
    }

    private void AddList(List<GameObject> FourCastList)
    {

        foreach (GameObject alien in FourCastList)
        {
            if (alien.GetComponent<Renderer>().material.color == gameObject.GetComponent<Renderer>().material.color)
            {
                MatchingAliens.Add(alien);
                AddList(FourCast(alien));
            }
        }
    
    }

    private void DestroyAliens()
    {
        if (MatchingAliens.Count >= 3)
        {
            Debug.Log(MatchingAliens.Count);
            scoreManager.Scoring(MatchingAliens.Count);
            foreach (GameObject alien in MatchingAliens)
            {
                Destroy(alien);
                alien.GetComponent<AlienBehaviour>().destroyed = true;
            }
        }
    }

    private void DetectFree()
    {
        RaycastHit hitLeft;
        Physics.Raycast(gameObject.transform.position, Vector3.left, out hitLeft, 1);
        if(hitLeft.collider == null)
        { leftFree = true; }
        else
        { leftFree = false; }
        RaycastHit hitRight;
        Physics.Raycast(gameObject.transform.position, Vector3.right, out hitRight, 1);
        if (hitRight.collider == null)
        { rightFree = true; }
        else
        { rightFree = false; }
    }

    public void MoveLeft()
    {
        if (leftFree)
        {
            transform.position += Vector3.left;
        }
    }
    public void MoveRight()
    {
        if (rightFree)
        {
            transform.position += Vector3.right;
        }
    }
}
