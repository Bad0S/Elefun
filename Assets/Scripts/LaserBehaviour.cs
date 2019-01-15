using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{
    private SpriteRenderer laserRend;
    public AnimationCurve animCurve;
    public float dist;
    private Vector2 beginLerp;
    private Vector2 endLerp;
    public Vector3 targetPoint;
    public bool shoot;
    private Vector3 direction;
    private float angleRotation;

	void Start ()
    {
        laserRend = GetComponent<SpriteRenderer>();
	}

    private void Update()
    {
        if (shoot)
        {
            direction = (targetPoint - transform.position).normalized;
            angleRotation = Mathf.Atan2(direction.y, direction.x);
            transform.rotation = Quaternion.Euler(0,0,angleRotation);

            dist = Vector3.Distance(transform.position, targetPoint);
            beginLerp = new Vector2(0, 0);
            endLerp = new Vector2(3.16f, dist);
            laserRend.size = Vector2.Lerp(beginLerp, endLerp, animCurve.Evaluate(Time.time));
        }
    }
}
