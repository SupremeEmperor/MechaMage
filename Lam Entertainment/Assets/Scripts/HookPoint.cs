using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookPoint : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && Input.GetButtonDown("Jump"))
        {
            player.GetComponent<ThirdPersonMovement>().SetGrapple(false);
            player.GetComponentInParent<Transform>().gameObject.transform.parent = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("WhatIsPlayer"))
        {
            player = other.gameObject;
            other.gameObject.GetComponent<ThirdPersonMovement>().SetGrapple(true);
            player.GetComponentInParent<Transform>().gameObject.transform.parent = this.gameObject.transform;
            Debug.Log("Parent");
        }
    }
}
