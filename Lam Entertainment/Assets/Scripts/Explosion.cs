using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    int damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyHealth>() != null)
        {
            other.gameObject.GetComponent<EnemyHealth>().Damage(damage);
            
        }
        

    }
}
