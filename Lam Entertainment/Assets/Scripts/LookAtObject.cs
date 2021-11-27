using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtObject : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] bool useController = false;

    // Update is called once per frame
    void Update()
    {
        
            transform.LookAt(new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z));
        
        
        //transform.rotation = Quaternion.Euler(0, 0, transform.rotation.z);
    }

    void UseController(bool c)
    {
        useController = c;
    }
}
