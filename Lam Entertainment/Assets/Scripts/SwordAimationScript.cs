using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAimationScript : MonoBehaviour
{
    [SerializeField]
    Animator Anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Anim.GetCurrentAnimatorStateInfo(0).IsName("Nuetral_Sword_Wait"))
        {
            Anim.SetInteger("AttackNumber", 0);
        }

        if (Input.GetButtonDown("Fire2"))
        {
            Anim.SetInteger("AttackNumber", Anim.GetInteger("AttackNumber") + 1);
        }

        
    }

    
}
