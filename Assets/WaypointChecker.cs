using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointChecker : MonoBehaviour
{

    public Dictionary<WaypointChecker, float> nextWaypointsAndDist = new Dictionary<WaypointChecker, float>();


    private void Update()
    {
        foreach (WaypointChecker wc in nextWaypointsAndDist.Keys)
        {
            if (transform.root.GetComponent<TrackCreator>().IsDebugOn)
                Debug.DrawLine(transform.position, wc.transform.position + Vector3.up*3, Color.red);

        }
    }






}
