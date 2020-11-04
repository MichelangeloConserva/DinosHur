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
    public float Grip = .95f;
    public float CoastingDrag = 4f;
    public float maxSpeed = 10f;

    public GameObject dinoLeft, dinoRight;


    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.centerOfMass = centerOfMass.localPosition;
    }



    public void FixedUpdate()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        Move(motor, steering);


        //foreach (AxleInfo axleInfo in axleInfos)
        //{
        //    if (axleInfo.steering)
        //    {
        //        axleInfo.leftWheel.steerAngle = turnInput;
        //        axleInfo.rightWheel.steerAngle = turnInput;
        //    }
        //    if (axleInfo.motor)
        //    {
        //        axleInfo.leftWheel.motorTorque = accelInput;
        //        axleInfo.rightWheel.motorTorque = accelInput;
        //    }
        //}

    }

    private void Move(float accelInput, float turnInput)
    {
        // manual acceleration curve coefficient scalar
        Vector3 localVel = transform.InverseTransformVector(rb.velocity);


        bool isBraking = accelInput < 0;

        Quaternion turnAngle = Quaternion.AngleAxis(turnInput, rb.transform.up);
        Vector3 fwd = turnAngle * rb.transform.forward;

        Vector3 movement = fwd * accelInput; //* finalAcceleration * GroundPercent;


        float finalAccelPower = accelInput;


        float currentSpeed = rb.velocity.magnitude;
        bool wasOverMaxSpeed = currentSpeed >= maxSpeed;

        if (wasOverMaxSpeed && !isBraking) movement *= 0;


        Vector3 adjustedVelocity = rb.velocity + movement * Time.deltaTime;
        adjustedVelocity.y = rb.velocity.y;


        // Should only happend on
        adjustedVelocity = Vector3.ClampMagnitude(adjustedVelocity, maxSpeed);

        // coasting is when we aren't touching accelerate
        bool isCoasting = Mathf.Abs(accelInput) < .01f;

        if (isCoasting)
        {
            Vector3 restVelocity = new Vector3(0, rb.velocity.y, 0);
            adjustedVelocity = Vector3.MoveTowards(adjustedVelocity, restVelocity, Time.deltaTime * CoastingDrag);
        }

        rb.velocity = adjustedVelocity;




        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = turnInput;
                axleInfo.rightWheel.steerAngle = turnInput;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = accelInput;
                axleInfo.rightWheel.motorTorque = accelInput;
            }
        }
    }





}