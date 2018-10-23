using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public float timer;
    public float timeBetweenSpawn;
    public GameObject alien;
    private float posX;
	void Start ()
    {
		
	}
	
	void Update ()
    {
        timer += Time.deltaTime;
        if (timer >= timeBetweenSpawn)
        {
            posX = Random.Range(-5f, 5.9f);
            posX = Mathf.FloorToInt(posX);
            Instantiate(alien, new Vector3(posX,15 , 10), Quaternion.identity );
            timer = 0;
        }
	}
}
