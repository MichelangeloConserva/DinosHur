using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;
using UnityEngine;


public enum BumperState { INACTIVE = 0, ACTIVE, RETRACTING};
public class BumperObstacle : MonoBehaviour, IObstacle
{
    // Start is called before the first frame update
    public GameObject bumperPrefab;
    public Rigidbody rb;

    public float speed = 10;
    public float retractionFactor = 0.5f;
    public float distance = 10;

    private BumperState bumperState;

    private Vector3 startPosition;
    private Vector3 endPosition;

    public void Start()
    {
        startPosition = transform.position;
        endPosition = new Vector3(startPosition.x + distance, startPosition.y, startPosition.z);
        bumperState = BumperState.INACTIVE;

    }
    public void Update()
    {

        if (Input.GetKey(KeyCode.Space) && bumperState == BumperState.INACTIVE)
        {
            // activate bumper
            rb.velocity = new Vector3(speed, 0, 0);
            bumperState = BumperState.ACTIVE;
        } 

        // check if bumper should stop
        if (transform.position.x > endPosition.x && bumperState == BumperState.ACTIVE)
        {
            //retract bumper
            rb.velocity = new Vector3(-speed * retractionFactor, 0, 0);
            bumperState = BumperState.RETRACTING;
        }

        //
        if (transform.position.x < startPosition.x && bumperState == BumperState.RETRACTING)
        {
            rb.velocity = new Vector3(0, 0, 0);
            bumperState = BumperState.INACTIVE;
        }

    }
    public void Activate()
    {
        
    }
}
