using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AStarController : MonoBehaviour
{

    public Transform lm;

    public WaypointChecker start;
    public WaypointChecker end;

    public List<Vector3> path;


    void Start()
    {
        start = lm.GetChild(0).GetChild(0).GetChild(4).GetComponent<WaypointChecker>();
        end = start;
        for (int i = 0; i< 130 ; i++)
        {
            foreach (WaypointChecker wc in end.nextWaypointsAndDist.Keys)
            {
                if (Random.Range(0, 1) > 0.3)
                {
                    end = wc;
                    break;
                }
                end = wc;
            }
        }

        ResetTrack();
        CalculateAStar();
    }

    private void ResetTrack()
    {
        path = new List<Vector3>();
        path.Add(start.transform.position);
    }





    private float DistToEnd(WaypointChecker wc)
    {
        float distance = 0;

        if (wc.nextWaypointsAndDist.ContainsKey(wc))
            return distance;

        WaypointChecker minDistWc = wc.nextWaypointsAndDist.Keys.ToList().First();
        float minDist = Mathf.Infinity;
        foreach(var kvwc in wc.nextWaypointsAndDist)
            if (kvwc.Value < minDist)
            {
                minDistWc = kvwc.Key;
                minDist = kvwc.Value;
            }

        return distance + minDist + DistToEnd(minDistWc);
    }


    public void CalculateAStar()
    {

        foreach( var kv in start.nextWaypointsAndDist)
        {
            Debug.Log(DistToEnd(kv.Key));
        }



    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
