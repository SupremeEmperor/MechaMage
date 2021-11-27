using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDMovement : MonoBehaviour
{
    [SerializeField]
    GameObject GroundCheck;
    [SerializeField]
    float maxSpeed = 20;
    [SerializeField]
    float acceleration = 20;
    [SerializeField]
    float jumpHeight = 20;
    [SerializeField]
    float dashTime = .5f;
    [SerializeField]
    float dashSpeed = 10;
    [SerializeField]
    float dashCooldown = .1f;

    [SerializeField]
    GameObject body;
    [SerializeField]
    GameObject gun;


    bool jump;
    float horizontal;
    Rigidbody rb;
    bool lookRight = true;
    bool hasJumped;
    [SerializeField]
    bool canDoubleJump = false;
    [SerializeField]
    bool ableToDash = false;
    bool secondJump = false;
    bool isDashing = false;
    bool dash = false;
    float time = 0;
    bool canDash = false;
    bool canJump = true;
    


    // Start is called before the first frame update
    void Start()
    {
        hasJumped = false;
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (!hasJumped)
        {
            jump = Input.GetButton("Jump");
        }
        if (Input.GetButtonUp("Jump"))
        {
            hasJumped = false;
        }

        if (canDash && !isDashing)
        {
            dash = Input.GetButton("Dash");
        }

        SpinBody();

        if (GroundCheck.GetComponent<GroundCheck>().IsGrounded())
        {
            secondJump = false;
            canJump = true;
        }
    }

    private void SpinBody()
    {
        if(lookRight && horizontal < 0)
        {
            body.transform.Rotate(new Vector3(0,0,180));
            gun.transform.Rotate(new Vector3(0, 180, 0));
            lookRight = false;
        }
        if (!lookRight && horizontal > 0)
        {
            body.transform.Rotate(new Vector3(0, 0, 180));
            gun.transform.Rotate(new Vector3(0, 180, 0));
            lookRight = true;
        }
    }

    private void FixedUpdate()
    {
        if (!isDashing)
        {
            Hoizontal();
            Jump();
        }

        Dash();
    }

    private void Dash()
    {
        if (dash && canDash && !isDashing)
        {
            
            isDashing = true;
            time = Time.time;
            dash = false;
            if (lookRight)
            {
                rb.velocity = new Vector3(dashSpeed, 0, 0);
            }
            else
            {
                rb.velocity = new Vector3(-dashSpeed, 0, 0);
            }
        }
        if(Time.time - time >= dashTime && isDashing)
        {
            ZeroVelocity();
            isDashing = false;
            time = Time.time;
            canDash = false;
        }
        if(!canDash && Time.time - time >= dashCooldown && ableToDash)
        {
            canDash = true;
        }

    }

    private void Hoizontal()
    {
        if(horizontal == 0 && GroundCheck.GetComponent<GroundCheck>().IsGrounded())
        {
            ZeroXVelocity();
        }
        rb.AddForce(transform.forward * horizontal * acceleration);
        if(rb.velocity.x > maxSpeed)
        {
            rb.velocity = new Vector3(maxSpeed, rb.velocity.y,rb.velocity.z);
        }
        if (rb.velocity.x < -maxSpeed)
        {
            rb.velocity = new Vector3(-maxSpeed, rb.velocity.y, rb.velocity.z);
        }
    }

    private void Jump()
    {
        if(jump && canJump)
        {
            //ZeroYVelocity();
            canJump = false;
            this.gameObject.GetComponent<Rigidbody>().
                AddForce(transform.up * jumpHeight);

            hasJumped = true;
            jump = false;
        }else if(canDoubleJump && jump && !secondJump && rb.velocity.y < 0)
        {
            ZeroYVelocity();
            secondJump = true;
            jump = false;
            this.gameObject.GetComponent<Rigidbody>().
                AddForce(transform.up * jumpHeight);
        }
    }

    public void ZeroVelocity()
    {
        rb.velocity = new Vector3(0, 0, 0);
    }

    public void ZeroXVelocity()
    {
        rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
    }

    public void ZeroYVelocity()
    {
        
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
    }

    public bool GetLookRight()
    {
        return lookRight;
    }

    public void SetDoubleJump(bool b)
    {
        canDoubleJump = b;
    }
}
