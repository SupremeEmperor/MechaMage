using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Attack", menuName = "Enemy Attacks/SteamDash")]
public class ChargeDash : Attack
{
    public enum AttackStages { not, prep, dash, end };
    [SerializeField] string animationName = "Attack1";
    [SerializeField] float riseSpeed;
    [SerializeField] float dashSpeed;
    [SerializeField] float prepTime;
    [SerializeField] float dashTime;
    [SerializeField] float endTime;

    AttackStages attackStage = AttackStages.not;
    float time;
    Rigidbody rb = null;
    



    public override float AttackStart(GameObject t, GameObject c, bool r, Animator a)
    {
        isRight = r;
        target = t;
        caller = c;
        time = Time.time;

        //Play Animation
        if (a)
        {
            a.SetTrigger(animationName);
        }

        if (caller)
        {
            if (caller.GetComponent<Rigidbody>())
            {
                rb = caller.GetComponent<Rigidbody>();
            }
        }

        if (caller.GetComponent<EnemyMovement2D>())
        {
            caller.GetComponent<EnemyMovement2D>().ZeroVelocity();
        }

        //Start first affect

        attackStage = AttackStages.prep;

        return prepTime + dashTime + endTime;
    }

    public override void AttackUse()
    {
        if(attackStage == AttackStages.prep)
        {
            if (rb)
            {
                rb.velocity = new Vector3(0,riseSpeed,0);
            }
            if(Time.time - time >= prepTime)
            {
                //end First affect
                time = Time.time;
                attackStage = AttackStages.dash;
            }
        }
        if (attackStage == AttackStages.dash)
        {
            if (rb)
            {
                rb.velocity = caller.transform.right * dashSpeed;
            }
            if (Time.time - time >= dashTime)
            {
                //end second affect
                time = Time.time;
                attackStage = AttackStages.end;
            }
        }

    }

    
}
