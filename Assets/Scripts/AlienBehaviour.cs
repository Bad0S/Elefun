using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlienBehaviour : MonoBehaviour
{
    public Transform playerTrans;
    private Renderer alienRend;
    public bool counted;
    public bool stacked;
    public bool leftFree;
    public bool rightFree;
    public Material redMat;
    public Material greenMat;
    public Material blueMat;
    public bool destroyed;
    private ScoreManager scoreManager;
    public float scaleDecalage;
    private float mousePressPosX;
    private float mouseReleasPosX;
    private Camera cam;

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        scoreManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ScoreManager>();
        playerTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        alienRend = gameObject.GetComponent<Renderer>();
        int a = Random.Range(0, 3);
        switch(a)
        {
            case 0: alienRend.material = redMat; break;
            case 1: alienRend.material = greenMat; break;
            case 2: alienRend.material = blueMat; break;
        }
        int b = Random.Range(0, 7);
        switch(b)
        {
            case 0: transform.position = new Vector3(-4.8f, transform.position.y, 0); break;
            case 1: transform.position = new Vector3(-3.2f, transform.position.y, 0); break;
            case 2: transform.position = new Vector3(-1.6f, transform.position.y, 0); break;
            case 3: transform.position = new Vector3(0, transform.position.y, 0); break;
            case 4: transform.position = new Vector3(1.6f, transform.position.y, 0); break;
            case 5: transform.position = new Vector3(3.2f, transform.position.y, 0); break;
            case 6: transform.position = new Vector3(4.8f, transform.position.y, 0); break;
        }
    }

    List<GameObject> MatchingAliens;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePressPosX = cam.ScreenToWorldPoint(Input.mousePosition).x;
        }

        DetectFree();
        if (Input.GetMouseButtonUp(0) && Input.mousePosition.y < Screen.height / 2)
        {
            mouseReleasPosX = cam.ScreenToWorldPoint(Input.mousePosition).x;
            //if (!destroyed && mouseReleasPosX - mousePressPosX > -0.5f && mouseReleasPosX - mousePressPosX < 0.5f)
            //{
                
            //}
            if(mouseReleasPosX - mousePressPosX < -0.5f && stacked)
            {
                MoveLeft();
            }
            if (mouseReleasPosX - mousePressPosX > 0.5f && stacked)
            {
                MoveRight();
            }
        }
    }

    private void OnMouseDown()
    {
        MatchingAliens = new List<GameObject>();
        MatchingAliens.Add(gameObject);
        AddList(FourCast(gameObject));
        DestroyAliens();
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
        UIManager.pause = true;
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
            transform.position += Vector3.left * scaleDecalage;
        }
    }
    public void MoveRight()
    {
        if (rightFree)
        {
            transform.position += Vector3.right * scaleDecalage;
        }
    }
}
