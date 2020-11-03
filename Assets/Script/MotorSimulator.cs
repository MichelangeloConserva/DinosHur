using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class MotorSimulator : MonoBehaviour
{

    [Header("Settings")]
    public float enginePower = 400f;
    public float turnPower = 10f;
    public bool humanControll;
    public float maxSpeed;

    public Wheel[] wheel;
    public Transform centerOfMass;
    public Rigidbody rbody;

    void Awake()
    {
        rbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rbody.centerOfMass = centerOfMass.localPosition;
    }

    void Update()
    {
        if (humanControll)
        {
            float torque = Input.GetAxis("Vertical") * enginePower;
            float turnSpeed = Input.GetAxis("Horizontal") * turnPower;
            MotorControlling(torque, turnSpeed);
        }

        Debug.DrawRay(transform.position, rbody.velocity, Color.red);



    }


    public void Move(float torque)
    {
        // Eliminating braking force if present
        //wheel[0].Brake(0);
        //wheel[1].Brake(0);

        //front wheel drive
        wheel[0].Move(torque);
        wheel[1].Move(torque);

        //front wheel steering
        //wheel[0].Turn(turnSpeed);
        //wheel[1].Turn(turnSpeed);
    }

    public void Brake(float brakeTorque)
    {
        // Eliminating front force if present
        //wheel[0].Move(0);
        //wheel[1].Move(0);

        //front wheel drive
        wheel[0].Brake(brakeTorque);
        wheel[1].Brake(brakeTorque);
    }

    public void MotorControlling(float torque, float turnSpeed)
    {

        if (torque > 0)
        {
            wheel[0].Brake(0);
            wheel[1].Brake(0);

            //front wheel drive
            wheel[0].Move(Mathf.Abs(torque));
            wheel[1].Move(Mathf.Abs(torque));
        }
        else
        {
            //front wheel drive
            wheel[0].Brake(Mathf.Abs(torque));
            wheel[1].Brake(Mathf.Abs(torque));
            //wheel[0].Move(0);
            //wheel[1].Move(0);
        }

        //front wheel steering
        wheel[0].Turn(turnSpeed);
        wheel[1].Turn(turnSpeed);
    }


}
