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

        Transform visualWheel = transform.GetChild(0);


    }

    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
            return;

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }

    private void Update()
    {

        float motor = 50 * Input.GetAxis("Vertical");
        float steering = 30 * Input.GetAxis("Horizontal");

        wc.motorTorque = motor;
        //wc.steerAngle = 0.5f;
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
