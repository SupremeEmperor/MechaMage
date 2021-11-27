using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLock : MonoBehaviour
{
    [SerializeField]
    BulletDoorInteractor door;
    [SerializeField]
    bool theLock;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            door.SetLock(theLock);
            Destroy(this.gameObject);
        }
    }
}
