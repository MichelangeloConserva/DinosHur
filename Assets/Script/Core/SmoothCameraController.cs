using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraController : MonoBehaviour
{


    public float distance = 10f;
    public float height = 5f;
    public float rotationDampening = 2f;
    public float heightDampening = 2f;
    public float speedFOVModifier = 4f;
    public float speedHeightModifier = 1.5f;
    public float speedDistanceModifier = 1.5f;
    public float defaultFOV = 60f;
    public readonly float maxAcceleration = 30f;
    private float rotationVector;
    private Rigidbody vehicleRigidbody;
    private Transform vehicleTransform;


    private void Start()
    {
        vehicleRigidbody = LevelController.Instance.PlayerController.vehicleRigidbody;
        vehicleTransform = vehicleRigidbody.transform;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 localVelocity = vehicleTransform.InverseTransformDirection(vehicleRigidbody.velocity);
        
        if (localVelocity.z < -1f)
        {
            rotationVector = vehicleTransform.eulerAngles.y + 180;
        } 
        else
        {
            rotationVector = vehicleTransform.eulerAngles.y;
        }

        float acceleration = vehicleRigidbody.velocity.magnitude;
        
        float speedFOV = Mathf.Pow(acceleration, 2) / maxAcceleration * speedFOVModifier * Time.deltaTime;

       
        Camera.main.fieldOfView = defaultFOV + speedFOV;
                        
    }


    private void LateUpdate()
    {
        float wantedAngle = rotationVector;
        float myAngle = transform.eulerAngles.y;

        float speedHeightDecrease = vehicleRigidbody.velocity.magnitude * speedHeightModifier / maxAcceleration;
        float wantedHeight = vehicleTransform.position.y + height - speedHeightDecrease;
        float myHeight = transform.position.y;

        float speedDistanceIncrease = vehicleRigidbody.velocity.magnitude * speedDistanceModifier / maxAcceleration;
/*        float wantedDistance = vehicleTransform.position.z + distance + speedDistanceIncrease;
        float myDistance = transform.position.z;*/

        LevelController.Instance.UIController.SetDebugText(speedDistanceIncrease.ToString());

        myAngle = Mathf.LerpAngle(myAngle, wantedAngle, rotationDampening * Time.deltaTime);
        myHeight = Mathf.LerpAngle(myHeight, wantedHeight, heightDampening * Time.deltaTime);

        Quaternion currentRotation = Quaternion.Euler(0, myAngle, 0);
        transform.position = vehicleTransform.position;
        transform.position -= currentRotation * Vector3.forward * (distance + speedDistanceIncrease);

        Vector3 temp = transform.position;
        temp.y = myHeight;
        transform.position = temp;

        transform.LookAt(vehicleTransform);

    }
}
