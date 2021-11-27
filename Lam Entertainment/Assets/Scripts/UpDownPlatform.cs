using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownPlatform : MonoBehaviour
{
    [SerializeField]
    Transform[] points;
    int currentTarget;

    [SerializeField]
    float moveSpeed = 30;
    float tolerance;
    [SerializeField]
    float delayTime;

    float delayStart;

    [SerializeField]
    bool automatic;
    


    // Start is called before the first frame update
    void Start()
    {
        if(points.Length != 0)
        {
            currentTarget = 0;
        }
        tolerance = moveSpeed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position != points[currentTarget].position)
        {
            MovePlatform();
        }
        else
        {
            UpdateTarget();
        }
    }

    private void UpdateTarget()
    {
        if (automatic)
        {
            if(Time.time - delayStart > delayTime)
            {
                NextPlatform();
            }
        }
    }

    public void NextPlatform()
    {
        currentTarget++;
        if(currentTarget >= points.Length)
        {
            currentTarget = 0;
        }
    }

    private void MovePlatform()
    {
        Vector3 heading = points[currentTarget].position - transform.position;
        transform.position += (heading.normalized) * moveSpeed * Time.deltaTime;
        if(heading.magnitude < tolerance)
        {
            transform.position = points[currentTarget].position;
            delayStart = Time.time;
        }
    }

}
