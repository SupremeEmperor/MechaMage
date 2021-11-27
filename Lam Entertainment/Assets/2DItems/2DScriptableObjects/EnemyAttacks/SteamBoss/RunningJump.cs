using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Attack", menuName = "Enemy Attacks/RunningJump")]
public class RunningJump : Attack
{
    public enum AttackStages { prep, jump, land, end};
    [SerializeField] string animationName = "Attack2";
    [SerializeField] float verticalVelocity;
    [SerializeField] float horizontalVelocity;
    [SerializeField] float prepTime;
    [SerializeField] float jumpTime;
    [SerializeField] float landTime;
    [SerializeField] float endTime;

    AttackStages attackStage = AttackStages.end;
    Rigidbody rb;
    float time;

    public override float AttackStart(GameObject t, GameObject c, bool r, Animator a)
    {
        if (a)
        {
            a.SetTrigger(animationName);
        }

        caller = c;

        if (c)
        {
            if (c.GetComponent<Rigidbody>())
            {
                rb = c.GetComponent<Rigidbody>();
            }
        }

        attackStage = AttackStages.prep;

        time = Time.time;

        return prepTime + jumpTime + landTime + endTime;
    }

    public override void AttackUse()
    {
        if(attackStage == AttackStages.prep)
        {
            rb.velocity = caller.transform.right * horizontalVelocity;

            if (Time.time - time >= prepTime)
            {
                time = Time.time;
                attackStage = AttackStages.jump;
            }
        }

        if(attackStage == AttackStages.jump)
        {
            rb.velocity = (caller.transform.right * horizontalVelocity) + new Vector3(0,verticalVelocity,0);

            if (Time.time - time >= jumpTime)
            {
                time = Time.time;
                attackStage = AttackStages.land;
            }
        }

        if (attackStage == AttackStages.land)
        {
            rb.velocity = (caller.transform.right * horizontalVelocity) + new Vector3(0, -verticalVelocity, 0);

            if (Time.time - time >= landTime)
            {
                time = Time.time;
                attackStage = AttackStages.end;
            }
        }

        if (attackStage == AttackStages.end)
        {
            rb.velocity = new Vector3(0, -verticalVelocity, 0);

            
        }
    }
    
    
}
