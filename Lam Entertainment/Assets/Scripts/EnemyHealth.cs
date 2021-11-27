using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    
    [SerializeField]
    int health = 5;
    [SerializeField]
    int currentHealth = 0;
    [SerializeField]
    float deathTime = 0;
    [SerializeField] GameObject killAlso;
    [SerializeField] Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
        if (!anim)
        {
            anim = this.gameObject.GetComponent<Animator>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0)
        {
            if(anim != null)
                anim.SetTrigger("Death");
            GameObject.Destroy(this.gameObject, deathTime);
            GameObject.Destroy(killAlso, deathTime);
        }
    }

    public virtual void Damage(int d)
    {
        currentHealth = currentHealth - d;
    }

    public int getCurrentHealth()
    {
        return currentHealth;
    }

    public int getMaxHealth()
    {
        return health;
    }

}
