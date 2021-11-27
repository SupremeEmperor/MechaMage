using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Attack Holder", menuName = "Enemy Attacks/Attack Holder")]
public class EnemyAttackHolder : Attack
{
    [SerializeField]
    float[] chanceToAttack;
    [SerializeField]
    Attack[] attacks;
    int currentAttack;
    // Start is called before the first frame update
    

    public override float AttackStart(GameObject t, GameObject c, bool r, Animator a)
    {
        float rand = Random.Range(0f, 100f);
        for (int i = 0; i < attacks.Length && i < chanceToAttack.Length; i++)
        {
            if (rand < chanceToAttack[i])
            {
                currentAttack = i;
                return attacks[i].AttackStart(t, c, r, a);
            }
        }
        currentAttack = 0;
        return attacks[0].AttackStart(t, c, r, a);
    }

    public override void AttackUse()
    {
        attacks[currentAttack].AttackUse();
    }

    
}
