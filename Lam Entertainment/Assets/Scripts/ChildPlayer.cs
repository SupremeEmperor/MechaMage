using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildPlayer : MonoBehaviour
{
    Transform previousParent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            previousParent = other.gameObject.transform.parent;
            other.gameObject.transform.parent = this.gameObject.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {

            other.gameObject.transform.parent = previousParent;
        }
    }
}
