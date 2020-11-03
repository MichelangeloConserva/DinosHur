using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoForce : MonoBehaviour
{

    public float pushForce;
    public float maxVelocity;
    public Transform pointOfForce;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        Debug.DrawRay(pointOfForce.position, rb.velocity, Color.red);
    }




}
