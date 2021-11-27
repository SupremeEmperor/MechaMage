using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDEnemyMovement : MonoBehaviour
{
    [SerializeField]
    Transform left;
    [SerializeField]
    Transform right;
    [SerializeField]
    float speed;
    [SerializeField]
    bool isRight = false;
    [SerializeField]
    GameObject body;
    [SerializeField]
    bool lookRight = true;
    [SerializeField]
    GameObject attackArea;
    float waitTime;
    float time;
    [SerializeField]
    Attack attack;
    [SerializeField]
    GameObject chaseRange;
    [SerializeField]
    bool lookAtTarget;

    //Movement Box
    [SerializeField]
    Transform maxX;
    [SerializeField]
    Transform minX;
    [SerializeField]
    Transform maxY;
    [SerializeField]
    Transform minY;
    [SerializeField]
    Transform resetPoint;


    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        waitTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //If player is in the attack area attack
        if (attackArea.GetComponent<Target>().PlayerIsHere() && Time.time - time >= waitTime)
        {
            waitTime = attack.AttackStart(this.gameObject, this.gameObject, isRight, null);
            time = Time.time;
        }

        //If the time of the attack is not over continue the attack
        if (Time.time - time < waitTime)
        {
            attack.AttackUse();
            
        }

        //If you pass the limits of your movement stop moveing
        if (transform.position.x < left.position.x || transform.position.x > right.position.x)
        {
            ZeroXVelocity();
        }

        //If the time of the attack is over continue on with movement
        if (Time.time - time >= waitTime)
        {
            if (transform.position.x < left.position.x && !isRight)
            {
                isRight = true;
            }
            else if (transform.position.x > right.position.x && isRight)
            {
                isRight = false;
            }
            if (isRight)
            {
                gameObject.GetComponent<Rigidbody>().velocity = gameObject.transform.right * speed;

            }
            if (!isRight)
            {
                gameObject.GetComponent<Rigidbody>().velocity = gameObject.transform.right * -speed;

            }
        }
        
      
        SpinBody();
        
    }

    
    //if the Enemy is facing the opisite direction it is supposed to it looks the correct direction
    private void SpinBody()
    {
        if (lookRight && !isRight)
        {
            body.transform.Rotate(new Vector3(0, 180, 0));
            lookRight = false;
        }
        if (!lookRight && isRight)
        {
            body.transform.Rotate(new Vector3(0, 180, 0));
            lookRight = true;
        }
    }

    public void ZeroXVelocity()
    {
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, gameObject.GetComponent<Rigidbody>().velocity.y, gameObject.GetComponent<Rigidbody>().velocity.z);
    }

    
}
