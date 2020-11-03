using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(WheelCollider))]
public class Wheel : MonoBehaviour
{
    public Transform tire;
    public float steerAngle;


    private WheelCollider wc;

    // Use this for initialization
    private void Awake()
    {
        wc = GetComponent<WheelCollider>();
    }

    public void Move(float torque)
    {
        wc.motorTorque = torque;
    }

    public void Turn(float turnSpeed)
    {
        wc.steerAngle = steerAngle = turnSpeed;
        tire.eulerAngles = new Vector3(0f, wc.steerAngle, 0f);
    }

    public void Brake(float brake)
    {
        wc.brakeTorque = brake;
    }
}
