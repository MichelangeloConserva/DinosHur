using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{

    private MotorSimulator ms;

    // Start is called before the first frame update
    void Start()
    {
        ms = GetComponent<MotorSimulator>();
    }

    // Update is called once per frame
    void Update()
    {
        var torque = Random.Range(0.8f, 1) * ms.enginePower;
        var turnSpeed = Random.Range(-0.1f, 0.1f) * ms.turnPower;
        ms.MotorControlling(torque, turnSpeed);
    }
}
