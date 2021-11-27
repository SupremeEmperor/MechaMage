using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : ScriptableObject
{
    public GameObject prefab;
    [SerializeField]
    [TextArea(15, 20)]
    public string description;
    protected float totalAttackTime;
    protected bool isRight;
    protected GameObject caller;
    protected GameObject target;

    

    public abstract float AttackStart(GameObject t, GameObject c, bool r, Animator a);

    public abstract void AttackUse();
}
