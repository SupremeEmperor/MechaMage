using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float speed = 200;
    [SerializeField]
    int damage = 5;
    [SerializeField]
    bool pierce = false;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<EnemyHealth>() != null)
        {
            Debug.Log(other.gameObject.name);
            other.gameObject.GetComponent<EnemyHealth>().Damage(damage);
            if (pierce)
            {
                return;
            }
        }
        if (!other.gameObject.GetComponent<Collider>().isTrigger && !other.gameObject.GetComponent<Player>())
        {
            Destroy(this.gameObject);
        }
        
    }
}
