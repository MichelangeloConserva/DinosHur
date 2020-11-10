using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointChecker : MonoBehaviour
{

    public Dictionary<GameObject, float> nextWaypointsAndDist = new Dictionary<GameObject, float>();



    private void Update()
    {
        foreach (GameObject o in nextWaypointsAndDist.Keys)
        {
            if (transform.root.GetComponent<TrackCreator>().IsDebugOn)
                Debug.DrawLine(transform.position, o.transform.position + Vector3.up*3, Color.red);
        }
    }






}
