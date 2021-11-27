using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Attack", menuName = "Enemy Attacks/Slam")]
public class Slam : Attack
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
    public AttackStages attackStages = AttackStages.Not;
    float time;
    bool inUse;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        totalAttackTime = timeBeforeAttack + timeAfterAttack + attackTime;
    }

    // Update is called once per frame
    public override void AttackUse()
    {
        CombatSlam();
    }

    private void CombatSlam()
    {
        if (attackStages == AttackStages.PrePrep)
        {
            attackStages = AttackStages.Prep;
            if(caller.GetComponent<TwoDEnemyMovement>())
                caller.GetComponent<TwoDEnemyMovement>().ZeroXVelocity();
            time = Time.time;
        }

        if (attackStages == AttackStages.Prep && Time.time - time >= timeBeforeAttack)
        {
            attackStages = AttackStages.Strike;
            time = Time.time;
            if (isRight)
            {
                caller.GetComponent<Rigidbody>().velocity = caller.transform.right * attackSpeed;

            }
            if (!isRight)
            {
                caller.GetComponent<Rigidbody>().velocity = caller.transform.right * -attackSpeed;

            }
        }

        if (attackStages == AttackStages.Strike && Time.time - time >= attackTime)
        {
            time = Time.time;
            if(caller.GetComponent<TwoDEnemyMovement>())
                caller.GetComponent<TwoDEnemyMovement>().ZeroXVelocity();
            attackStages = AttackStages.Post;
        }

        if (attackStages == AttackStages.Post && Time.time - time >= timeAfterAttack)
        {
            attackStages = AttackStages.Not;
        }
    }

    public override float AttackStart(GameObject target,  GameObject _caller, bool _isRight, Animator a) 
    {
        attackStages = AttackStages.PrePrep;
        caller = _caller;
        time = 0;
        totalAttackTime = timeBeforeAttack + timeAfterAttack + attackTime;
        return totalAttackTime;
    }
}
