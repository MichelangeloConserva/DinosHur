using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chariot : MonoBehaviour
{
    public Transform centerOfMass;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = centerOfMass.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
