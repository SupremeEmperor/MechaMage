using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageToPlayer : MonoBehaviour
{
    
    [SerializeField]
    float knockbackTime = .5f;
    [SerializeField]
    float knockbackForce = 600;
    [SerializeField]
    int dmg = 10;
    GameObject gm;
    float time;
    [SerializeField]
    bool singleUse = false;

    bool hit = false;

    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.GetComponent<TwoDMovement>() != null)
        {
            hit = true;
            gm = collision.gameObject;
            //this.gameObject.GetComponent<TwoDEnemyMovement>().ZeroXVelocity();
            gm.GetComponent<TwoDMovement>().ZeroVelocity();
            gm.GetComponent<TwoDMovement>().enabled = false;
            //Fix This
            float xKnockback;
            float yKnockback;
            if(gm.transform.position.x < transform.position.x)
            {
                xKnockback = -knockbackForce / 2;
            }
            else
            {
                xKnockback = knockbackForce / 2;
            }
            if (gm.transform.position.y < transform.position.y)
            {
                yKnockback = -knockbackForce / 2;
            }
            else
            {
                yKnockback = knockbackForce / 2;
            }
            gm.GetComponent<Rigidbody>().AddForce(new Vector3(xKnockback, yKnockback, 0));
            //please
            collision.gameObject.GetComponent<Health>().DealDamage(dmg);
            time = Time.time;
            if (singleUse)
            {
                if (GetComponent<Collider>())
                {
                    GetComponent<Collider>().enabled = false;
                }
                if (GetComponent<MeshRenderer>())
                {
                    GetComponent<MeshRenderer>().enabled = false;
                }
            }
        }
    }

    private void Update()
    {
        if(gm != null)
        {
            if(gm.GetComponent<TwoDMovement>() != null)
            {
                if(Time.time - time > knockbackTime)
                {
                    hit = false;
                    gm.GetComponent<TwoDMovement>().enabled = true;
                    if (singleUse)
                    {
                        if(transform.parent.gameObject != null)
                        {
                            Destroy(transform.parent.gameObject);
                        }
                        else
                        {
                            Destroy(this.gameObject);
                        }
                    }
                }
            }
        }
    }

    private void OnDestroy()
    {
        if(gm != null)
        {
            if (gm.GetComponent<TwoDMovement>() != null && hit == true)
            {
                gm.GetComponent<TwoDMovement>().enabled = true;
            }
        }
    }

}
