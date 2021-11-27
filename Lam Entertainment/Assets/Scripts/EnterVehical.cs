using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterVehical : MonoBehaviour
{
    [SerializeField]
    Transform exitSpot;
    GameObject Hold;
    bool insideVehical = false;

    private void Update()
    {
        if(insideVehical == true)
        {
            if (Input.GetButtonDown("Exit"))
            {
                Debug.Log("Exit");
                insideVehical = false;
                Hold.transform.position = exitSpot.position;
                Hold.transform.rotation = exitSpot.rotation;
                Hold.SetActive(true);
                Hold.transform.parent = null;
                this.gameObject.GetComponentInParent<SpeederController>().enabled = false;
                //this.gameObject.GetComponent<SpeederController>().EmergencyBreak();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("WhatIsPlayer"))
        {
            if (Input.GetButtonDown("Enter"))
            {
                insideVehical = true;
                Hold = other.gameObject;
                Hold.SetActive(false);
                Hold.transform.position = this.transform.position;
                Hold.transform.parent = this.gameObject.transform;
                this.gameObject.GetComponentInParent<SpeederController>().enabled = true;
            }
        }
    }
}
