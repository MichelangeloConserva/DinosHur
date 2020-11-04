using UnityEngine;
using System.Collections;
using System.Collections.Generic;



[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;
}

public class CarController : MonoBehaviour
{

    public Transform centerOfMass;
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;

    public GameObject dinoLeft, dinoRight;


    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.centerOfMass = centerOfMass.localPosition;
    }


    // finds the corresponding visual wheel
    // correctly applies the transform
    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        //Transform visualWheel = collider.transform.GetChild(0);

        //Vector3 position;
        //Quaternion rotation;
        //collider.GetWorldPose(out position, out rotation);

        //visualWheel.transform.position = position;
        //visualWheel.transform.rotation = rotation;
    }

    public void FixedUpdate()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");


        //rb.AddForceAtPosition(dinoLeft.transform.forward * motor, dinoLeft.transform.position, ForceMode.Acceleration);
        //rb.AddForceAtPosition(dinoRight.transform.forward * motor, dinoRight.transform.position, ForceMode.Acceleration);
        //rb.AddForceAtPosition(dinoLeft.transform.up * 3f, dinoLeft.transform.position, ForceMode.Acceleration);
        //rb.AddForceAtPosition(dinoRight.transform.up * 3f, dinoRight.transform.position, ForceMode.Acceleration);

        //dinoLeft.transform.rotation = Quaternion.Euler(0f, steering, 0f);
        //dinoRight.transform.rotation = Quaternion.Euler(0f, steering, 0f);


        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel);
        }
    }
}