using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    float maxHealth = 99;
    [SerializeField]
    float currentHealth = 99;
    [SerializeField] float invincibilityTime = 1;
    [SerializeField] float damagedTimescale = .75f;
    float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        time = -invincibilityTime;
    }

    // Update is called once per frame
    void Update()
    {
        //What happens when player dies
        if(currentHealth < 0)
        {
            Time.timeScale = 0;
            Destroy(this.gameObject);
        }
        if(Time.time - time < invincibilityTime)
        {
            Time.timeScale = damagedTimescale;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void DealDamage(int dmg)
    {
        if(Time.time - time >= invincibilityTime)
        {
            currentHealth -= dmg;
            time = Time.time;
        }
        
        
    }
}
