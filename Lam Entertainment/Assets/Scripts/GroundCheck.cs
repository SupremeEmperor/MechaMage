using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField]
    float groundDistance = 0.4f;
    [SerializeField]
    LayerMask groundMask;
    [SerializeField]
    Transform gc1, gc2, gc3, gc4;

    public bool IsGrounded()
    {
        if (Physics.CheckSphere(gc1.position, groundDistance, groundMask))
        {
            return true;
        } else if (Physics.CheckSphere(gc2.position, groundDistance, groundMask))
        {
            return true;
        } else if (Physics.CheckSphere(gc3.position, groundDistance, groundMask))
        {
            return true;
        } else if (Physics.CheckSphere(gc4.position, groundDistance, groundMask))
        {
            return true;
        }
        
        return Physics.CheckSphere(transform.position,groundDistance,groundMask);

    }

}
