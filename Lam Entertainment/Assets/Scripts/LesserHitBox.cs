using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LesserHitBox : EnemyHealth
{
    [SerializeField]
    EnemyHealth mainHealth;
    // Start is called before the first frame update
    void Start()
    {
        if(mainHealth == null)
        {
            if (gameObject.GetComponentInParent<EnemyHealth>())
            {
                mainHealth = gameObject.GetComponentInParent<EnemyHealth>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Damage(int d)
    {
        Debug.Log("Lesser Hitbox: Has been Hit");
        if (mainHealth != null)
        {
            mainHealth.Damage(d);
        }
    }
}
