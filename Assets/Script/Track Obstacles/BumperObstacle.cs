﻿using System;
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

    private BumperState bumperState = BumperState.INACTIVE;


    private Vector3 startPosition;
    private Vector3 endPosition;
    
    private Vector3 direction;

    private float distance;
    private float currentDistance;

    public void Start()
    {
        startPosition = transform.GetChild(0).position;
        endPosition = transform.GetChild(1).position;

        distance = Vector3.Distance(startPosition, endPosition);
    }
    public void Update()
    {

        currentDistance = Vector3.Distance(transform.position, startPosition);
        
        if (Input.GetKey(KeyCode.Space) && bumperState == BumperState.INACTIVE)
        {
            // activate bumper
            direction = Vector3.Normalize(endPosition - startPosition);
            rb.velocity = direction * speed;
            bumperState = BumperState.ACTIVE;
        } 

        // check if bumper should stop
        if (currentDistance > distance && bumperState == BumperState.ACTIVE)
        {
            //retract bumper
            direction = Vector3.Normalize(startPosition - transform.position);
            rb.velocity = direction * speed * retractionFactor;
            bumperState = BumperState.RETRACTING;
        }

        if ( currentDistance < 0.1f && bumperState == BumperState.RETRACTING)
        {
            // reset position
            rb.velocity = new Vector3(0, 0, 0);
            transform.position = startPosition;
            bumperState = BumperState.INACTIVE;
        }

    }
    public void Activate()
    {
        
    }
}
