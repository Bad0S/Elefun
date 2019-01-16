using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public float timer = 3;
    public float timeBetweenSpawn;
    public GameObject alien;
    public float hauteurSpawn;
    public List<ListObject> patterns;

    void Update ()
    {
        if (UIManager.pause == false)
        { timer += Time.deltaTime; }
        if (timer >= timeBetweenSpawn)
        {
            ListObject _listObject = patterns[Random.Range(0, patterns.Count)];
            for (int i = 0; i < _listObject.cubes.Count; i++)
            {
                GameObject _cube = Instantiate(alien);
                _cube.transform.position = new Vector3(_listObject.cubes[i].posX, _listObject.cubes[i].posY, 0);
            }
            timer = 0;
        }
	}
}
