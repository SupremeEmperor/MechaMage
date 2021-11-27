using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCrawl2D : MonoBehaviour
{
    public enum WalkPath { up, down, left, right };
    [SerializeField] float speed;
    [SerializeField] WalkPath walkPath;
    [SerializeField] bool clockWise;
    [SerializeField] float rayLength = 1;
    [SerializeField] LayerMask layer;
    [SerializeField] float turnTime;
    [SerializeField] Transform DownRayStartPoint;
    Rigidbody rb;
    float time = 0;
    Vector3 startPosition;
    WalkPath startPath;

    bool grounded;
    bool wall;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = .25f;
        rb = GetComponent<Rigidbody>();
        time = Time.time;
        grounded = true;
        wall = false;
        startPath = walkPath;
        startPosition = transform.position;
    }
    private void Update()
    {
        if(Time.time - time >= turnTime)
            CastRay();
        
    }

    private void CastRay()
    {
        Ray downRay = new Ray(DownRayStartPoint.position, -transform.up);
        Ray forwardRay = new Ray(transform.position, transform.forward);
        Debug.DrawRay(DownRayStartPoint.position, -transform.up, Color.red, 0 , false);
        Debug.DrawRay(transform.position, transform.forward, Color.blue, 0, false);
        if (!Physics.Raycast(downRay, rayLength, layer))
        {
            time = Time.time;
            
            grounded = false;
        }
        else
        {
            grounded = true;
        }
        if (Physics.Raycast(forwardRay, rayLength, layer))
        {
            time = Time.time;
            wall = true;
        }
        else
        {
            wall = false;
        }

    }

    // Update is called once per frame
    void LateUpdate()
    {
        

        if (rb)
        {
            rb.velocity = (transform.forward * speed) + (transform.up * (-.5f * speed));
        }
        if (clockWise)
        {
            if (walkPath == WalkPath.down)
            {
                transform.rotation = Quaternion.Euler(90, 90, 0);
                if (wall)
                {
                    walkPath = WalkPath.right;
                    wall = false;
                }
                if (!grounded)
                {
                    walkPath = WalkPath.left;
                    grounded = true;
                }
                return;

            }
            if (walkPath == WalkPath.up)
            {
                transform.rotation = Quaternion.Euler(-90, 90, 0);
                if (wall)
                {
                    walkPath = WalkPath.left;
                    wall = false;
                }
                if (!grounded)
                {
                    walkPath = WalkPath.right;
                    grounded = true;
                }
                return;
            }
            if (walkPath == WalkPath.left)
            {
                transform.rotation = Quaternion.Euler(0, -90, -180);
                if (wall)
                {
                    walkPath = WalkPath.down;
                    wall = false;
                }
                if (!grounded)
                {
                    walkPath = WalkPath.up;
                    grounded = true;
                }
                return;
            }
            if (walkPath == WalkPath.right)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
                if (wall)
                {
                    walkPath = WalkPath.up;
                    wall = false;
                }
                if (!grounded)
                {
                    walkPath = WalkPath.down;
                    grounded = true;
                }
                return;
            }
        }
        else
        {
            if (walkPath == WalkPath.down)
            {
                transform.rotation = Quaternion.Euler(90, -90, 0);
                if (wall)
                {
                    walkPath = WalkPath.left;
                    wall = false;
                }
                if (!grounded)
                {
                    walkPath = WalkPath.right;
                    grounded = true;
                }
                return;

            }
            if (walkPath == WalkPath.up)
            {
                transform.rotation = Quaternion.Euler(-90, -90, 0);
                if (wall)
                {
                    walkPath = WalkPath.right;
                    wall = false;
                }
                if (!grounded)
                {
                    walkPath = WalkPath.left;
                    grounded = true;
                }
                return;
            }
            if (walkPath == WalkPath.left)
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
                if (wall)
                {
                    walkPath = WalkPath.up;
                    wall = false;
                }
                if (!grounded)
                {
                    walkPath = WalkPath.down;
                    grounded = true;
                }
                return;
            }
            if (walkPath == WalkPath.right)
            {
                transform.rotation = Quaternion.Euler(0, 90, 180);
                if (wall)
                {
                    walkPath = WalkPath.down;
                    wall = false;
                }
                if (!grounded)
                {
                    walkPath = WalkPath.up;
                    grounded = true;
                }
                return;
            }
        }
        

        

    }
}
