using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    private MotorSimulator ms;

    //Start is called before the first frame update
    void Start()
    {
        ms = GetComponent<MotorSimulator>();
    }

    //Update is called once per frame
    void Update()
    {

    }


}