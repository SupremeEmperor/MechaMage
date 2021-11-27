using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDoorInteractor : MonoBehaviour
{
    [SerializeField]
    Animator anim;
    [SerializeField]
    float doorOpenTime = 3;
    float time = 0;
    bool doorIsOpen;
    bool isLocked = false;

    private void Update()
    {
        if(Time.time - time > doorOpenTime && doorIsOpen)
        {
            //close Door
            anim.SetTrigger("CloseDoor");
            doorIsOpen = false;
        }
        if(doorIsOpen && isLocked)
        {
            anim.SetTrigger("CloseDoor");
            doorIsOpen = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Bullet>())
        {
            if (!doorIsOpen && !isLocked)
            {
                time = Time.time;
                doorIsOpen = true;
                anim.SetTrigger("OpenDoor");
            }
            
        }
    }

    public void SetLock(bool l)
    {
        isLocked = l;
    }
}
