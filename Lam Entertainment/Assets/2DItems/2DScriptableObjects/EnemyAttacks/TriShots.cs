using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Attack", menuName = "Enemy Attacks/Tri Shot")]
public class TriShots : Attack
{
    public enum AttackStages { Not, Aim, Shot, End }
    [SerializeField]
    int numberOfShots = 3;
    int shotsTaken;
    [SerializeField]
    float timeBeforeAttack = .5f;
    [SerializeField]
    float timePerShot = .5f;
    [SerializeField]
    float timeAfterAttack = .5f;
    [SerializeField]
    float positionOffset = 0;
    [SerializeField]
    string animationTrigger = "Attack2";
    public AttackStages attackStages = AttackStages.Not;
    float time = 0;
    bool inUse = false;

    public override float AttackStart(GameObject t, GameObject c, bool r, Animator anim)
    {
        target = t;
        caller = c;
        shotsTaken = 0;
        attackStages = AttackStages.Aim;
        time = Time.time;
        if(anim != null)
        {
            anim.SetTrigger(animationTrigger);
        }
        if(c.GetComponent<EnemyMovement2D>() != null)
        {
            c.GetComponent<EnemyMovement2D>().ZeroVelocity();
        }
        
        return timeBeforeAttack + timeAfterAttack + (numberOfShots * timePerShot);
    }

    public override void AttackUse()
    {
        caller.transform.LookAt(new Vector3(target.transform.position.x, target.transform.position.y, caller.transform.position.z));

        if(attackStages == AttackStages.Aim && Time.time - time >= timeBeforeAttack)
        {
            time = Time.time;
            attackStages = AttackStages.Shot;
        }
        if(attackStages == AttackStages.Shot)
        {
            if(Time.time - time >= timePerShot)
            {
                Vector3 spawnPoint = caller.GetComponent<EnemyMovement2D>().GetShotPosition();
                GameObject Temp = Instantiate(prefab, spawnPoint, caller.transform.rotation);
                Temp.transform.parent = null;
                time = Time.time;
                shotsTaken++;
            }

            if(shotsTaken >= numberOfShots)
            {
                attackStages = AttackStages.End;
            }
        }
    }
}
