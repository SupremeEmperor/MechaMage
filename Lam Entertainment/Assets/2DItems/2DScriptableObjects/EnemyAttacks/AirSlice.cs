using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Attack", menuName = "Enemy Attacks/Air Slice")]
public class AirSlice : Attack
{
    public enum AttackStages { Not, PrePrep, Prep, Strike, Post }
    [SerializeField]
    float timeBeforeAttack = 1;
    [SerializeField]
    float attackTime = .5f;
    [SerializeField]
    float timeAfterAttack = 1;
    [SerializeField]
    float attackSpeed;
    [SerializeField]
    string animationTrigger = "Attack1";
    public AttackStages attackStages = AttackStages.Not;
    float time = 0;
    bool inUse = false;
    Vector3 targetPosition;
    Vector3 originalPosition;
    Animator anim;

    

    public override float AttackStart(GameObject t, GameObject c, bool r, Animator a)
    {
        attackStages = AttackStages.PrePrep;
        target = t;
        
        caller = c;
        isRight = r;
        totalAttackTime = timeBeforeAttack + timeAfterAttack + attackTime;

        originalPosition = c.transform.position;
        anim = a;
        if(anim != null)
        {
            anim.SetTrigger(animationTrigger);
        }

        return totalAttackTime;
    }

    public override void AttackUse()
    {
        if(caller == null)
        {
            return;
        }
        if (attackStages == AttackStages.PrePrep)
        {
            attackStages = AttackStages.Prep;
            if (caller.GetComponent<TwoDEnemyMovement>())
                caller.GetComponent<TwoDEnemyMovement>().ZeroXVelocity();
            else if (caller.GetComponent<EnemyMovement2D>())
                caller.GetComponent<EnemyMovement2D>().ZeroVelocity();
            time = Time.time;
        }

        if (attackStages == AttackStages.Prep && Time.time - time >= timeBeforeAttack)
        {
            attackStages = AttackStages.Strike;
            time = Time.time;
            targetPosition = target.transform.position;
            caller.transform.LookAt(target.transform);

            caller.GetComponent<Rigidbody>().velocity = caller.transform.forward * attackSpeed;

            
        }

        if (attackStages == AttackStages.Strike && Time.time - time >= attackTime)
        {
            time = Time.time;
            if (caller.GetComponent<TwoDEnemyMovement>() != null)
                caller.GetComponent<TwoDEnemyMovement>().ZeroXVelocity();
            else if (caller.GetComponent<EnemyMovement2D>())
                caller.GetComponent<EnemyMovement2D>().ZeroVelocity();

            caller.GetComponent<Rigidbody>().velocity = caller.transform.forward * -(attackSpeed/2);

            attackStages = AttackStages.Post;
        }

        if (attackStages == AttackStages.Post && Time.time - time >= timeAfterAttack)
        {
            attackStages = AttackStages.Not;
        }
    }

    

    
}
