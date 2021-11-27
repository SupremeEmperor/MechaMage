using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    Rigidbody rb;
    [SerializeField]
    int damage = 10;
    [SerializeField]
    float speed = 10;
    [SerializeField]
    float lifeTime = 3;
    [SerializeField]
    PlayerFinder playerFinder;
    [SerializeField]
    float knockBackSpeed = 3;
    float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
        time = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Time.time - time >= lifeTime)
        {
            Destroy(this.gameObject);
        }
        if (playerFinder.GetSeenPlayer())
        {
            float xKnockback;
            float yKnockback;
            if (playerFinder.GetPlayer().transform.position.x < transform.position.x)
            {
                xKnockback = -knockBackSpeed / 2;
            }
            else
            {
                xKnockback = knockBackSpeed / 2;
            }
            if (playerFinder.GetPlayer().transform.position.y < transform.position.y)
            {
                yKnockback = -knockBackSpeed / 2;
            }
            else
            {
                yKnockback = knockBackSpeed / 2;
            }
            playerFinder.GetPlayer().GetComponent<Rigidbody>().AddForce(new Vector3(xKnockback, yKnockback, 0));
            if (playerFinder.GetPlayer().GetComponent<Health>())
            {
                playerFinder.GetPlayer().GetComponent<Health>().DealDamage(damage);
            }
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.GetComponent<Enemy>() && !playerFinder.GetSeenPlayer())
        {
            Debug.Log(other.gameObject.name);
            Destroy(gameObject);
        }
    }
}
