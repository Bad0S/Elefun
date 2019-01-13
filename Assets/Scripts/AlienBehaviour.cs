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
    private Vector2 mousePressPos;
    private Vector2 mouseReleasPos;
    private Camera cam;
    private Rigidbody alienBody;
    private PlayerBehaviour playerScript;
    private float limitHeight;

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        scoreManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ScoreManager>();
        playerTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
        alienRend = gameObject.GetComponent<Renderer>();
        alienBody = gameObject.GetComponent<Rigidbody>();
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

        Debug.Log(Screen.height * 0.4f);
    }

    List<GameObject> MatchingAliens;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePressPos = cam.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(mousePressPos);
            //if (Input.mousePosition.y < Screen.height * 0.4f)
        }

        DetectFree();
        if (Input.GetMouseButtonUp(0))
        {
            mouseReleasPos = cam.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(mouseReleasPos);
            if (mousePressPos.y < 6.9 && mouseReleasPos.y < 6.9)
            {
                if (mouseReleasPos.x - mousePressPos.x < -0.5f && stacked)
                {
                    MoveLeft();
                }
                if (mouseReleasPos.x - mousePressPos.x > 0.5f && stacked)
                {
                    MoveRight();
                }
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
        playerScript.DeathAnim();
        playerTrans.gameObject.GetComponent<PlayerBehaviour>().dead = true;
        yield return new WaitForSecondsRealtime(1f);
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
            playerScript.ShootAnim();
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
            StartCoroutine(moveLeftLoop());
        }
    }
    public void MoveRight()
    {
        if (rightFree)
        {
            StartCoroutine(moveRightLoop());
        }
    }

    IEnumerator moveLeftLoop()
    {
        transform.position += Vector3.left * scaleDecalage;
        Debug.Log("Move Left");
        yield return new WaitForSecondsRealtime(0.01f);
        if (leftFree)
        {
            StartCoroutine(moveLeftLoop());
        }
    }

    IEnumerator moveRightLoop()
    {
        transform.position += Vector3.right * scaleDecalage;
        Debug.Log("Move Right");
        yield return new WaitForSecondsRealtime(0.01f);
        if (rightFree)
        {
            StartCoroutine(moveRightLoop());
        }
    }
}
