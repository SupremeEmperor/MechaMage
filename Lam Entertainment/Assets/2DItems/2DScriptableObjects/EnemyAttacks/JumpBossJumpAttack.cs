using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Attack", menuName = "Enemy Attacks/JumpBoss")]
public class JumpBossJumpAttack : Attack
{
    public enum AttackStages { not, prep, jump, wait, land, end };
    [SerializeField] string animationName = "Attack1";
    [SerializeField] float verticalSpeed;
    [SerializeField] float horizontalSpeed;
    [SerializeField] float dropSpeed;
    [SerializeField] int numberOfJumps = 1;
    [SerializeField] float prepTime;
    [SerializeField] float jumpTime;
    [SerializeField] float waitTime;
    [SerializeField] float landTime;
    [SerializeField] float endTime;
    [SerializeField] AttackStages attackstage = AttackStages.not;

    Animator anim;
    float time;
    Rigidbody rb;
    float currentJump;
    float xVelocity;


    public override float AttackStart(GameObject t, GameObject c, bool r, Animator a)
    {
        target = t;

        caller = c;

        isRight = r;
        if (a)
        {
            a.SetTrigger(animationName);
        }

        attackstage = AttackStages.prep;

        if (caller.GetComponent<EnemyMovement2D>())
        {
            caller.GetComponent<EnemyMovement2D>().ZeroVelocity();
        }

        time = Time.time;

        if (caller.transform.position.x > target.transform.position.x)
        {
            xVelocity = -horizontalSpeed;
        }
        else
        {
            xVelocity = horizontalSpeed;
        }

        if (caller.GetComponent<Rigidbody>())
        {
            rb = caller.GetComponent<Rigidbody>();
        }

        
        return prepTime + (jumpTime * numberOfJumps)+ (waitTime * numberOfJumps) + (landTime * numberOfJumps) + endTime;
    }

    public override void AttackUse()
    {
        if(attackstage == AttackStages.prep & Time.time - time > prepTime)
        {
            time = Time.time;
            attackstage = AttackStages.jump;
            if (rb)
            {
                rb.velocity = new Vector3(0, dropSpeed, 0);
            }
            currentJump = 1;
        }

        if (attackstage == AttackStages.jump)
        {
            if (rb)
            {
                
                rb.velocity = new Vector3(xVelocity,verticalSpeed,0);
                
            }
            if(Time.time - time >= jumpTime)
            {
                time = Time.time;
                attackstage = AttackStages.wait;
            }
        }

        if(attackstage == AttackStages.wait)
        {
            if (rb)
            {
                rb.velocity = new Vector3(0, 0, 0);
                rb.useGravity = false;
            }

            if (Time.time - time >= waitTime)
            {
                if (rb)
                {
                    rb.useGravity = true;
                }
                time = Time.time;
                attackstage = AttackStages.land;
            }
        }

        if(attackstage == AttackStages.land)
        {
            if (rb)
            {
                rb.velocity = new Vector3(0, dropSpeed, 0);
            }

            if(Time.time - time >= landTime)
            {
                time = Time.time;
                if(currentJump < numberOfJumps)
                {
                    currentJump++;
                    attackstage = AttackStages.jump;
                }
                else
                {
                    attackstage = AttackStages.end;
                }
            }
        }

        if(attackstage == AttackStages.end)
        {
            if (rb)
            {
                rb.velocity = new Vector3(0, dropSpeed, 0);
            }
        }
    }
}
