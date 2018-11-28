using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public float timer;
    public float timeBetweenSpawn;
    public GameObject alien;
    public float hauteurSpawn;
	
	void Update ()
    {
        if (UIManager.pause == false)
        { timer += Time.deltaTime; }
        if (timer >= timeBetweenSpawn)
        {
            Instantiate(alien, new Vector3(0, hauteurSpawn, 0), Quaternion.identity);
            timer = 0;
        }
	}
}
