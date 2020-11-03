using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{

    public DinoForce lDino, rDino;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var torque = Random.Range(0.8f, 1);
        var turnSpeed = Random.Range(-0.1f, 0.1f);



        float turn = Input.GetAxis("Horizontal") * 20f;


        lDino.GetComponent<Transform>().localRotation = Quaternion.Euler(90f, -90f, 90f - turn);
        rDino.GetComponent<Transform>().localRotation = Quaternion.Euler(90f, -90f, 90f - turn);







    }



    //private MotorSimulator ms;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    ms = GetComponent<MotorSimulator>();
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    var torque = Random.Range(0.8f, 1) * ms.enginePower;
    //    var turnSpeed = Random.Range(-0.1f, 0.1f) * ms.turnPower;
    //    ms.MotorControlling(torque, turnSpeed);
    //}


}
