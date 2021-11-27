using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement2D : MonoBehaviour
{
    public enum MoveOptions {Fly, Walk, Stationary}
    //general data
    [SerializeField] bool isFlying;
    [SerializeField] Attack attack;
    [SerializeField] float moveSpeed;
    [SerializeField] PlayerFinder followArea;
    [SerializeField] PlayerFinder attackArea;
    [SerializeField] Rigidbody rb;
    [SerializeField] Animator anim;

    //Movement data
    Transform currentMoveGoal;
    [SerializeField] Transform[] searchPath;
    [SerializeField] Transform rightBound;
    [SerializeField] Transform leftBound;
    [SerializeField] Transform topBound;
    [SerializeField] Transform bottomBound;
    [SerializeField] Transform center;
    [SerializeField] Transform shotPosition;

    float tolerance;
    float attackTimeStart = 0;
    float attackTimeTotal = 0;
    int nextPoint = 0;
    bool isRight = true;
    bool closeEnough = false;
    bool retreat = false;
    [SerializeField]
    float retreatTime = .5f;
    float retreatTimeStart = 0;
    public MoveOptions moveOptions;



    private void Start()
    {
        if(shotPosition == null)
        {
            shotPosition = this.transform;
        }
        if(center == null)
        {
            center = transform;
        }
        if(searchPath.Length == 0)
        {
            searchPath = new Transform[]{ center};
        }
        if(rb == null)
        {
            rb = this.GetComponent<Rigidbody>();
        }
        tolerance = moveSpeed * Time.deltaTime;
        currentMoveGoal = searchPath[0];
    }

    private void FixedUpdate()
    {
        //If the enemy is not currently attacking run what is in here
        if(Time.time - attackTimeStart >= attackTimeTotal)
        {
            //if not retreating and player in follow area move towards player
            if (!retreat)
            {
                if (followArea.GetSeenPlayer())
                {
                    currentMoveGoal = followArea.GetPlayer().transform;
                }
            }
            else 
            {
                //if retreat time is over stop retreating
                if (Time.time - retreatTimeStart >= retreatTime)
                {
                    retreat = false;
                }
            }

            //if not close enough move towards the target
            if (!closeEnough)
            {
                    Move();
            }
            else //if close enough change the target
            {
                    UpdateTarget();
            }

            //Starts an attack if the player is in the Attack area.
            if (attackArea.GetSeenPlayer())
            {
                    attackTimeTotal = attack.AttackStart(attackArea.GetPlayer(), this.gameObject, isRight, anim);
                    attackTimeStart = Time.time;
            }
            //if out of bounds change target
            if (transform.position.x >= rightBound.position.x || transform.position.x <= leftBound.position.x
             || transform.position.y >= topBound.position.y || transform.position.y <= bottomBound.position.y)
            {
                if (!retreat)
                {
                    UpdateTarget();
                    retreatTimeStart = Time.time;
                    retreat = true;
                }
            }
        }
        else
        {
            //if the attack has started the attack continues.
            attack.AttackUse();
        }
        
    }

    //Changes the target 
    private void UpdateTarget()
    {
        Debug.Log("Update Target");
        nextPoint++;
        if (nextPoint >= searchPath.Length)
        {
            nextPoint = 0;
            
        }
        
        currentMoveGoal = searchPath[nextPoint];
        closeEnough = false;
        if(moveOptions == MoveOptions.Walk)
        {
            if(currentMoveGoal.position.x > transform.position.x)
            {
                if (!isRight)
                {
                    transform.Rotate(new Vector3(0, 180, 0));
                }
                isRight = true;
            }
            else
            {
                if (isRight)
                {
                    transform.Rotate(new Vector3(0, 180, 0));
                }
                isRight = false;
            }
        }

    }

    //Moves towards the current target
    private void Move()
    {
        if (moveOptions == MoveOptions.Walk)
        {
            GroundMove();
        }
        if(MoveOptions.Fly == moveOptions)
        {
            AirMove();
        }
        if(moveOptions == MoveOptions.Stationary)
        {
            transform.LookAt(new Vector3(currentMoveGoal.position.x, currentMoveGoal.position.y, transform.position.z));
        }
        
    }

    //Moves through the air
    private void AirMove()
    {
        transform.LookAt(new Vector3(currentMoveGoal.position.x, currentMoveGoal.position.y, transform.position.z));

        rb.velocity = transform.forward * moveSpeed;
        Vector3 heading = currentMoveGoal.position - transform.position;

        if (heading.magnitude < tolerance)
        {
            transform.position = currentMoveGoal.position;
            closeEnough = true;
            ZeroVelocity();
        }
    }

    //Moves while on the ground
    private void GroundMove()
    {
        Debug.Log(currentMoveGoal);
        
        
        
        rb.velocity = transform.right * moveSpeed + new Vector3(0,rb.velocity.y,0);

        float heading = currentMoveGoal.position.x - transform.position.x;
        if (isRight)
        {
            if (heading < tolerance)
            {
                closeEnough = true;
                ZeroVelocity();
            }
        }
        else
        {
            if (heading > tolerance)
            {
                closeEnough = true;
                ZeroVelocity();
            }
        }
        
    }

    

    //Returns the position of the place where the shot came from
    public Vector3 GetShotPosition()
    {
        return shotPosition.position;
    }

    //Zero's out the of the enemy
    public void ZeroVelocity()
    {
        if(gameObject.GetComponent<Rigidbody>() != null)
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    }
}
