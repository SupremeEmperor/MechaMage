using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speeder : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    float verticalInput;
    float horizontalInput;
    float steerAngel;
    float currentBreakForce;
    bool isBreaking;

    [SerializeField] float motorForce;
    [SerializeField] float breakForce;
    [SerializeField] float maxSteerAngel;

    [SerializeField] WheelCollider frontRight;
    [SerializeField] WheelCollider frontLeft;
    [SerializeField] WheelCollider backRight;
    [SerializeField] WheelCollider backLeft;
    

    private void FixedUpdate()
    {
       HandleMotor();
       HanndleSteering();
    }

    private void Update()
    {
        GetInput();
        //HandleMotor();
        //HanndleSteering();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor()
    {
        frontRight.motorTorque = verticalInput * motorForce;
        frontLeft.motorTorque = verticalInput * motorForce;
        currentBreakForce = isBreaking ? breakForce : 0f;
        ApplyBreak();
        
    }

    private void ApplyBreak()
    {
        frontRight.brakeTorque = currentBreakForce;
        frontLeft.brakeTorque = currentBreakForce;
        backLeft.brakeTorque = currentBreakForce;
        backRight.brakeTorque = currentBreakForce;
    }

    private void HanndleSteering()
    {
        steerAngel = maxSteerAngel * horizontalInput;
        frontRight.steerAngle = steerAngel;
        frontLeft.steerAngle = steerAngel; 
    }

    public void EmergencyBreak()
    {
        frontLeft.brakeTorque = 1000 * breakForce;
        frontRight.brakeTorque = 1000 * breakForce;
        backLeft.brakeTorque = 1000 * breakForce;
        backRight.brakeTorque = 1000 * breakForce;
    }
}
