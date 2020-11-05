using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoForce : MonoBehaviour
{

    public float frequency;
    public float range;

    private void Start()
    {
    }


    private void Update()
    {

        var pos = transform.localPosition;
        pos.y = Mathf.Sin(frequency * Time.time) * range + 0.1f;
        transform.localPosition = pos;

        var rot = transform.localRotation.eulerAngles;
        rot.x = Mathf.Cos(frequency * Time.time) * range*30;
        transform.localRotation = Quaternion.Euler(rot);


    }




}
