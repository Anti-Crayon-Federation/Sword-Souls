using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 position1;
    public Vector3 position2;
    public float speed=5;
    public float stopTime;
    bool reachedPosition1;
    bool reachedPosition2;
    public bool stopped;
    float stopCountdown;

    private void Start()
    {
        reachedPosition1 = false;
        reachedPosition2 = false;
        stopCountdown = stopTime;
        position1 = transform.GetChild(0).position;
        position2 = transform.GetChild(1).position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == position1)
        {
            reachedPosition1 = true;
            reachedPosition2 = false;
            stopped = true;
            stopCountdown -= Time.deltaTime;
            if (stopCountdown <= 0)
            {
                stopped = false;
                stopCountdown = stopTime;
            }
        }

        if (transform.position == position2)
        {
            reachedPosition2 = true;
            reachedPosition1 = false;
            stopped = true;
            stopCountdown -= Time.deltaTime;
            if (stopCountdown <= 0)
            {
                stopped = false;
                stopCountdown = stopTime;
            }
        }

        if (reachedPosition1 == false && !stopped)
        {
            transform.position = Vector3.MoveTowards(transform.position, position1, speed * Time.deltaTime);
        }

        else if (reachedPosition2 == false && !stopped)
        {
            transform.position = Vector3.MoveTowards(transform.position, position2, speed * Time.deltaTime);
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    collision.gameObject.transform.SetParent(gameObject.transform, true);
    //}
    //
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    collision.gameObject.transform.SetParent(null, true);
    //}
}
