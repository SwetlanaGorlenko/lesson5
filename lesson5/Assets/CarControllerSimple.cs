﻿using UnityEngine;
using System.Collections;

public class CarControllerSimple : MonoBehaviour {
    public WheelCollider wheelFrontLeft;
    public WheelCollider wheelFrontRight;
    public WheelCollider wheelBackLeft;
    public WheelCollider wheelBackRight;

    public float steerMax;
    public float motorMax;
    public float brakeMax;

    float steer = 0.0f;
    float motor = 0.0f;
    float brake = 0.0f;
    

    void Start () {
	
	}

 
    void FixedUpdate()
    {
        steer = Mathf.Clamp(Input.GetAxis("Horizontal"), -1, 1);
        motor = Mathf.Clamp(Input.GetAxis("Vertical"), 0, 1);
        brake = -1 * Mathf.Clamp(Input.GetAxis("Vertical"), -1, 0);

        wheelBackLeft.motorTorque = -1 * motorMax * motor;
        wheelBackRight.motorTorque = -1 * motorMax * motor;
        wheelBackLeft.brakeTorque = brakeMax * brake;
        wheelBackRight.brakeTorque = brakeMax * brake;

        wheelFrontLeft.steerAngle = steerMax * steer;
        wheelFrontRight.steerAngle = steerMax * steer;

    }
}
