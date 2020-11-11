using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotator : MonoBehaviour
{
    Rigidbody vehicleRigidbody;

    void Start()
    {
        vehicleRigidbody = LevelController.Instance.PlayerController.vehicleRigidbody;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
