using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class How2PlayScript : MonoBehaviour
{
    public GameObject TitleUI;
    public GameObject InGameUI;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.SetActive(false);
            TitleUI.GetComponent<Transform>().localScale = Vector3.one;
            InGameUI.GetComponent<Transform>().localScale = Vector3.one;
        }
    }
}
