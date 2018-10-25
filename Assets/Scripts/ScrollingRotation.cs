using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingRotation : MonoBehaviour
{
    [Range(0.1f,10)]
    public float scrollSpeed;
	void Update ()
    {
        transform.Rotate(new Vector3(scrollSpeed, 0, 0));
	}
}
