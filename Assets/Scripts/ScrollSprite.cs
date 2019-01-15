using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollSprite : MonoBehaviour
{
    public float scrollSpeed;
    public GameObject enviro1;
    public GameObject enviro2;
    private Transform enviroTrans1;
    private Transform enviroTrans2;
    private SpriteRenderer enviroRend1;
    private SpriteRenderer enviroRend2;
    public Sprite enviroGreen;
    public Sprite enviroRed;
    public Sprite enviroBlue;

	void Start ()
    {
        enviroTrans1 = enviro1.GetComponent<Transform>();
        enviroTrans2 = enviro2.GetComponent<Transform>();
        enviroRend1 = enviro1.GetComponent<SpriteRenderer>();
        enviroRend2 = enviro2.GetComponent<SpriteRenderer>();

        int a = Random.Range(0, 3);
        switch (a)
        {
            case 0: enviroRend1.sprite = enviroRed; break;
            case 1: enviroRend1.sprite = enviroGreen; break;
            case 2: enviroRend1.sprite = enviroBlue; break;
        }

        enviroRend2.sprite = enviroRend1.sprite;

    }
	
	// Update is called once per frame
	void Update ()
    {
		if (UIManager.pause && !UIManager.title)
        {
            enviroTrans1.position += Vector3.zero;
            enviroTrans2.position += Vector3.zero;
        }
        else
        {
            enviroTrans1.position += Vector3.down * scrollSpeed;
            enviroTrans2.position += Vector3.down * scrollSpeed;
        }
        if(enviroTrans1.transform.localPosition.y <= -11.55f)
        {
            enviroTrans1.localPosition = enviroTrans2.localPosition + new Vector3(0,20.18f,0);
        }
        if (enviroTrans2.transform.localPosition.y <= -11.55f)
        {
            enviroTrans2.localPosition = enviroTrans1.localPosition + new Vector3(0, 20.18f, 0);
        }
    }
}
