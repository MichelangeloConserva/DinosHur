using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraController : MonoBehaviour
{

    public Transform car;

    public float distance = 10f;
    public float height = 5f;
    public float rotationDampening = 2f;
    public float heightDampening = 2f;
    public float zoomRatio = 4f;
    public float defaultFOV = 60f;

    private float rotationVector;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 localVelocity = car.InverseTransformDirection(car.GetComponent<Rigidbody>().velocity);
        
        if (localVelocity.z < -0.5f)
        {
            rotationVector = car.eulerAngles.y + 100;
        } 
        else
        {
            rotationVector = car.eulerAngles.y;
        }

        float acceleration = car.GetComponent<Rigidbody>().velocity.magnitude;
        Camera.main.fieldOfView = defaultFOV + acceleration * zoomRatio * Time.deltaTime;
                        
    }


    private void LateUpdate()
    {
        float wantedAngle = rotationVector;

        float wantedHeight = car.position.y + height;
        float myAngle = transform.eulerAngles.y;
        float myHeight = transform.position.y;

        myAngle = Mathf.LerpAngle(myAngle, wantedAngle, rotationDampening * Time.deltaTime);
        myHeight = Mathf.LerpAngle(myHeight, wantedHeight, heightDampening * Time.deltaTime);

        Quaternion currentRotation = Quaternion.Euler(0, myAngle, 0);
        transform.position = car.position;
        transform.position -= currentRotation * Vector3.forward * distance;

        Vector3 temp = transform.position;
        temp.y = myHeight;
        transform.position = temp;

        transform.LookAt(car);

    }
}
