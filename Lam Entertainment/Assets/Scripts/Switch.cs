using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField]
    Animator anim;
    [SerializeField]
    Action action;
    Light light;
    private void Start()
    {
        light = gameObject.GetComponent<Light>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("WhatIsPlayer"))
        {
            light.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("WhatIsPlayer"))
        {
            light.enabled = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("WhatIsPlayer"))
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                anim.SetTrigger("Switch");
                if(action != null)
                    action.StartAction(true);
            }
                
        }
    }
}
