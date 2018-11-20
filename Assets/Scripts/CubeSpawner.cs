using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour {

    public List<ListObject> paterns;
    public GameObject cubePrefab;
    public float timer = 4f;

	void Start ()
    {
        Spawn();
	}

    void Spawn()
    {
        ListObject _listObject = paterns[Random.Range(0, paterns.Count)];
        for (int i = 0; i < _listObject.cubes.Count; i++)
        {
            GameObject _cube = Instantiate(cubePrefab);
            _cube.transform.position = new Vector3(_listObject.cubes[i].posX, _listObject.cubes[i].posY, 0);
        }
        StartCoroutine(RestartSpawn());

    }

    IEnumerator RestartSpawn()
    {
        yield return new WaitForSeconds(timer);
        Spawn();
    }
	

	void Update ()
    {
		

	}
}
