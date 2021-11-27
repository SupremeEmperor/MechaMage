using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glowOverDamage : MonoBehaviour
{
    [SerializeField]
    EnemyHealth EH;
    [SerializeField]
    Material mat;
    [SerializeField]
    float flashTime;
    [SerializeField]
    GameObject flashObject;
    int lastHealth;
    float time = 0;
    bool canFlash = true;
    bool start;
    // Start is called before the first frame update
    void Start()
    {
        lastHealth = EH.getCurrentHealth();
        flashObject.SetActive(false);
        canFlash = true;
        start = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            lastHealth = EH.getCurrentHealth();
            start = false;
        }
        

        if(lastHealth > EH.getCurrentHealth() && canFlash)
        {
            flashObject.SetActive(true);
            time = Time.time;
            lastHealth = EH.getCurrentHealth();
            canFlash = false;
        }
        if(Time.time - time >= flashTime)
        {
            canFlash = true;
            flashObject.SetActive(false);
        }
    }
}
