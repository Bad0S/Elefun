using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlienBehaviour : MonoBehaviour
{
    public int RGB;
    public Sprite redSprite;
    public Sprite greenSprite;
    public Sprite blueSprite;
    private SpriteRenderer childRend;

    public Transform playerTrans;
    public bool counted;
    public bool stacked;
    public bool leftFree;
    public bool rightFree;
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
        childRend = GetComponentInChildren<SpriteRenderer>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        scoreManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ScoreManager>();
        playerTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
        alienBody = gameObject.GetComponent<Rigidbody>();
        int a = Random.Range(0, 3);
        switch(a)
        {
            case 0: childRend.sprite = redSprite;RGB = 0; break;
            case 1: childRend.sprite = greenSprite; RGB = 1; break;
            case 2: childRend.sprite = blueSprite; RGB = 2; break;
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
    List<GameObject> MovingAliens;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePressPos = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        DetectFree();
        if (Input.GetMouseButtonUp(0))
        {
            mouseReleasPos = cam.ScreenToWorldPoint(Input.mousePosition);
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
        if (UIManager.pause == true)
        {
            alienBody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        }
        else
        {
            alienBody.constraints = RigidbodyConstraints.FreezeRotation;
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
        playerScript.DeathAnim();
        playerTrans.gameObject.GetComponent<PlayerBehaviour>().dead = true;
        yield return new WaitForSecondsRealtime(3f);
        UIManager.pause = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    #region 
    private void OnMouseDown()
    {
        MatchingAliens = new List<GameObject>();
        MatchingAliens.Add(gameObject);
        AddList(FourCast(gameObject));
        DestroyAliens();
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
            if (alien.GetComponent<AlienBehaviour>() != null)
            {
                if (alien.GetComponent<AlienBehaviour>().RGB == RGB)
                {
                    MatchingAliens.Add(alien);
                    AddList(FourCast(alien));
                }
            }
        }
    
    }

    private void DestroyAliens()
    {
        if (MatchingAliens.Count >= 3)
        {
            playerScript.ShootAnim();
            scoreManager.Scoring(MatchingAliens.Count);
            foreach (GameObject alien in MatchingAliens)
            {
                Destroy(alien);
                alien.GetComponent<AlienBehaviour>().destroyed = true;
            }
        }
    }
    #endregion
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
    #region
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

    private GameObject MoveCast(GameObject moveCastCenter, int leftOrRightCast)
    {
        if (leftOrRightCast == 0)
        {
            RaycastHit hitRight;
            Physics.Raycast(moveCastCenter.transform.position, Vector3.right, out hitRight, 1);
            Debug.DrawRay(moveCastCenter.transform.position, Vector3.right, Color.magenta, 5);
            if (hitRight.collider != null && hitRight.collider.tag == "Stack")
            {
                return hitRight.collider.gameObject;
            }
            else
            {
                return null;
            }
        }
        else
        {
            RaycastHit hitLeft;
            Physics.Raycast(moveCastCenter.transform.position, Vector3.left, out hitLeft, 1);
            Debug.DrawRay(moveCastCenter.transform.position, Vector3.left, Color.magenta, 5);
            if (hitLeft.collider != null && hitLeft.collider.tag == "Stack")
            {
                return hitLeft.collider.gameObject;
            }
            else
            {
                return null;
            }
        }
    }

    private void AddListMove(GameObject movingAlien, int leftOrRight)
    {
        if (movingAlien != null)
        {
            MovingAliens.Add(movingAlien);
            if (leftOrRight == 0)
            {
                AddListMove(MoveCast(movingAlien, 0), 0);
            }
            else
            {
                AddListMove(MoveCast(movingAlien, 1), 1);
            }
        }
    }

    IEnumerator moveLeftLoop()
    {
            MovingAliens = new List<GameObject>();
            MovingAliens.Add(gameObject);
            AddListMove(MoveCast(gameObject, 0), 0);
            foreach (GameObject alienToMoveLeft in MovingAliens)
            {
                alienToMoveLeft.transform.position += Vector3.left * scaleDecalage;
            }
        yield return new WaitForSecondsRealtime(0.01f);
        if (leftFree)
        {
            StartCoroutine(moveLeftLoop());
        }
    }

    IEnumerator moveRightLoop()
    {
            MovingAliens = new List<GameObject>();
            MovingAliens.Add(gameObject);
            AddListMove(MoveCast(gameObject, 1), 1);

            foreach (GameObject alienToMoveLeft in MovingAliens)
            {
                alienToMoveLeft.transform.position += Vector3.right * scaleDecalage;
            }
        yield return new WaitForSecondsRealtime(0.01f);
        if (rightFree)
        {
            StartCoroutine(moveRightLoop());
        }
    }
    #endregion
}
