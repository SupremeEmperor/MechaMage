using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeederController : MonoBehaviour
{
    
    [SerializeField]
    Transform cam;

    [SerializeField]
    float speed = 6f;

    //Jump Affectors
    [SerializeField]
    float gravity = 9.8f;
    [SerializeField]
    float jumpSpeed = 6;
    [SerializeField]
    float jumpLeewayTime = .5f;
    float lastTimeTouchedGround;
    float jumpWait = .5f;
    float jumpTime;
    //Look Affectors
    [SerializeField]
    float turnSmoothTime = 0.1f;
    Vector3 moveDirection;
    [SerializeField]
    GameObject GroundCheck;
    float turnSmoothVelocity;

    float directionY;
    float angel;





    [SerializeField]
    float waitTime = 1;
    float timeLanded;
    float energyCharge = 1;
    [SerializeField]
    int maxEnergyCharge = 1;
    
    bool jump;

    private void Start()
    {
        angel = 0;
        jump = false;
        jumpTime = Time.time;
        if (cam == null)
        {
            cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (cam != null)
        {
            
                
            HorizontalControl();

            VerticalControl();
                
                
            jump = false;
        }


    }

    private void Update()
    {
        if(cam == null)
        {
            cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        }

        
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    public void HorizontalControl()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        

        if (horizontal != 0)
        {
            
            if(horizontal > 0)
            {
                angel = Mathf.SmoothDampAngle(transform.eulerAngles.y, transform.eulerAngles.y + 90,
                    ref turnSmoothVelocity, turnSmoothTime);
            }else
            {
                angel = Mathf.SmoothDampAngle(transform.eulerAngles.y, transform.eulerAngles.y - 90,
                    ref turnSmoothVelocity, turnSmoothTime);
            }
            transform.rotation = Quaternion.Euler(transform.rotation.x, angel, transform.rotation.z);
        }

        if(vertical != 0)
        {
            moveDirection = Quaternion.Euler(0f, angel, 0f) * Vector3.forward * vertical;
            this.gameObject.GetComponent<Rigidbody>().AddForce(moveDirection.normalized * speed);
        }

        //Vector3 direction = new Vector3(horizontal, 0f, 0f).normalized;

        /*
        if (direction.magnitude >= 0.1f)
        {
            float targetAngel = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angel = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngel, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(transform.rotation.x, angel, transform.rotation.z);

            moveDirection = Quaternion.Euler(0f, angel, 0f) * Vector3.right * -1;
            this.gameObject.GetComponent<Rigidbody>().AddForce(moveDirection.normalized * speed);
            //controller.Move((moveDirection.normalized * speed) * Time.deltaTime);
        }
        */
    }

    public void VerticalControl()
    {
        if (IsGrounded())
        {
            
            directionY = 0;
            
        }

        directionY -= gravity * Time.deltaTime;
        if (directionY < -gravity)
        {
            directionY = -gravity;
        }
        //controller.Move(new Vector3(0f, directionY, 0f));
    }

    

    

    

    private bool IsGrounded()
    {
        return GroundCheck.GetComponent<GroundCheck>().IsGrounded();
        //if (GroundCheck.GetComponent<GroundCheck>().IsGrounded())
        //{
        //lastTimeTouchedGround = Time.time;
        //}
        //return controller.isGrounded;
    }

    
}

