using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    [SerializeField]
    int damage = 1;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Damage");
        if (other.gameObject.layer == LayerMask.NameToLayer("WhatIsEnemy"))
        {
            other.gameObject.GetComponent<EnemyHealth>().Damage(damage);
            
        }
    }
}
