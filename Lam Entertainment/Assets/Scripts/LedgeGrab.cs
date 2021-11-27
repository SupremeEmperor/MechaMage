using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeGrab : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    bool grabedLedge = false;

    //Ledge climb tools
    bool isClimb = false;
    [SerializeField]
    float climbSpeed = 5;
    [SerializeField]
    float climbVerticalMovement = 2;
    [SerializeField]
    float climbHorizontalMovement = 1;
    float startingX;
    float startingY;
    Vector3 climbPosition;
    //Ledge Jump Tools
    bool isLedgeJump = false;
    [SerializeField]
    float ledgeJumpSpeedVertical = 1;
    [SerializeField]
    float ledgeJumpSpeedHorizontal = 1;
    [SerializeField]
    float ledgeJumpTime = .5f;
    [SerializeField]
    float boxHeight = .5f;
    


    float time = 0;

    private void Update()
    {
        if (isLedgeJump)
        {
            if(player != null)
            {
                if(player.GetComponent<Player>() != null)
                {
                    if (player.GetComponent<TwoDMovement>().GetLookRight())
                    {
                        player.GetComponent<Rigidbody>().velocity
                            = new Vector3(-ledgeJumpSpeedHorizontal, ledgeJumpSpeedVertical, 0);
                    }
                    else
                    {
                        player.GetComponent<Rigidbody>().velocity
                            = new Vector3(ledgeJumpSpeedHorizontal, ledgeJumpSpeedVertical, 0);
                    }
                        

                }
            }

            if(Time.time - time >= ledgeJumpTime)
            {
                isLedgeJump = false;
                player.GetComponent<TwoDMovement>().enabled = true;
                player.GetComponent<Rigidbody>().useGravity = true;
            }
        }

        if (isClimb)
        {
            if(transform.position.y < startingY + climbVerticalMovement)
            {
                    player.GetComponent<Rigidbody>().velocity = new Vector3(0,climbSpeed,0);
            }else
            {
                if (player.GetComponent<TwoDMovement>().GetLookRight())
                {
                    if(transform.position.x < startingX + climbHorizontalMovement)
                        player.GetComponent<Rigidbody>().velocity = new Vector3(climbSpeed, 0, 0);
                    else
                    {
                        isClimb = false;
                        player.GetComponent<TwoDMovement>().enabled = true;
                        player.GetComponent<Rigidbody>().useGravity = true;
                    }
                }
                else
                {
                    if (transform.position.x > startingX - climbHorizontalMovement)
                        player.GetComponent<Rigidbody>().velocity = new Vector3(-climbSpeed, 0, 0);
                    else
                    {
                        isClimb = false;
                        player.GetComponent<TwoDMovement>().enabled = true;
                        player.GetComponent<Rigidbody>().useGravity = true;
                    }
                }
            }
        }

        
    }

    private void LateUpdate()
    {
        
        if (grabedLedge)
        {
            player.GetComponent<TwoDMovement>().ZeroVelocity();
            if (Input.GetButtonDown("Jump"))
            {
                grabedLedge = false;
                isLedgeJump = true;
                time = Time.time;
            }
            if (Input.GetAxis("Vertical") > 0)
            {
                grabedLedge = false;
                isClimb = true;
                startingX = transform.position.x;
                startingY = transform.position.y;
            }
            if (Input.GetAxis("Vertical") < 0)
            {
                grabedLedge = false;
                player.GetComponent<TwoDMovement>().enabled = true;
                player.GetComponent<Rigidbody>().useGravity = true;
            }
        }
    }

    
    private void OnTriggerStay(Collider other)
    {

        if(other.gameObject.layer == LayerMask.NameToLayer("WhatIsGround"))
        {
            player.GetComponent<TwoDMovement>().ZeroXVelocity();
            if (Input.GetAxis("Horizontal") != 0 && Input.GetButton("Jump") && !player.GetComponentInChildren<GroundCheck>().IsGrounded() && !isLedgeJump)
            {
                isLedgeJump = true;
                player.GetComponent<TwoDMovement>().enabled = false;
                player.GetComponent<Rigidbody>().useGravity = false;
                time = Time.time;
            }
        }
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("WhatIsLedge"))
        {
            if(other.gameObject.GetComponent<BoxCollider>() != null)
            {
                if (player.GetComponent<Rigidbody>().velocity.y < 0
                && gameObject.GetComponent<BoxCollider>().center.y > other.gameObject.GetComponent<BoxCollider>().center.y + boxHeight)
                {
                    if (player.GetComponent<TwoDMovement>().GetLookRight()
                        && Input.GetAxis("Horizontal") > 0)
                    {
                        grabedLedge = true;
                        player.GetComponent<TwoDMovement>().enabled = false;
                        player.GetComponent<Rigidbody>().useGravity = false;
                    }
                    //Debug.Log(player.GetComponent<TwoDMovement>().GetLookRight() + " and " + (Input.GetAxis("Horizontal") < 0));
                    if (!player.GetComponent<TwoDMovement>().GetLookRight()
                        && (Input.GetAxis("Horizontal") < 0))
                    {
                        grabedLedge = true;
                        player.GetComponent<TwoDMovement>().enabled = false;
                        player.GetComponent<Rigidbody>().useGravity = false;
                    }
                }
            }
            
        }
    }
}
