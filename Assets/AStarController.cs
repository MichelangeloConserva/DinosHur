using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AStarController : MonoBehaviour
{
    public static object[] MinArgmin<TKey>(Dictionary<TKey,float> dict)
    {
        var minDistObj = dict.Keys.ToList().First();
        float minDist = Mathf.Infinity;
        foreach (var kv in dict)
            if (kv.Value < minDist)
            {
                minDistObj = kv.Key;
                minDist = kv.Value;
            }
        return new object[] { minDistObj, minDist };
    }


    public int H = 5;

    public Transform lm;

    public WaypointChecker start;
    public WaypointChecker end;

    public List<Vector3> curPath;
    public Vector3 curTarget;

    void Start()
    {
        start = lm.GetChild(0).GetChild(0).GetChild(4).GetComponent<WaypointChecker>();
        end = start;
        for (int i = 0; i< 6 ; i++)
        {
            end = end.nextWaypointsAndDist.Keys.ToList().Last();
        }

        CalculateTrajectory();
    }


    //private float DistToEnd(WaypointChecker wc)
    //{
    //}


    private float DistToEnd(WaypointChecker wc, List<Vector3> list)
    {
        float distance = 0;

        if (wc.nextWaypointsAndDist.ContainsKey(end))
            return distance;

        var res = MinArgmin(wc.nextWaypointsAndDist);
        WaypointChecker minDistWc = (WaypointChecker)res[0];
        float minDist = (float)res[1];

        //Debug.Log(wc.nextWaypointsAndDist.Values.ToArray()[0]);
        //Debug.Log(wc.nextWaypointsAndDist.Values.ToArray()[1]);
        //Debug.Log(wc.nextWaypointsAndDist.Values.ToArray()[2]);
        //Debug.Log(wc.nextWaypointsAndDist.Values.ToArray()[3]);

        if (list.Count < H)
            list.Add(minDistWc.transform.position);

        return distance + minDist + DistToEnd(minDistWc, list);
    }


    public void CalculateTrajectory()
    {
        Dictionary<List<Vector3>, float[]> nextWaypointHeuristicValues = new Dictionary<List<Vector3>, float[]>();

        float[] costsSums = new float[] { 0,0,0,0,0 };

        foreach( var kv in start.nextWaypointsAndDist)
        {
            // nextWaypointHeuristicValues.Add(kv.Key, DistToEnd(kv.Key));

            List<Vector3> path = new List<Vector3>();
            path.Add(kv.Key.transform.position);
            float distanceCost = DistToEnd(kv.Key, path) + kv.Value;

            var frwd = transform.GetChild(1).forward; frwd.y = 0;
            var frwdAction = (path.First() - transform.GetChild(1).position).normalized;
            
            float angularCost = Mathf.Min(50f,Vector3.Angle(frwd, frwdAction)) / 50 + 0.01f;

            //Debug.Log(distanceCost);
            //Debug.Log(angularCost);
            //Debug.Log("------");


            // TODO : implement the cost using the path
            float enemiesCost = 0.01f;
            float obstacleCost = 0.01f;
            float collectableCost = 0.01f;

            costsSums[0] += distanceCost;
            costsSums[1] += angularCost;
            costsSums[2] += enemiesCost;
            costsSums[3] += obstacleCost;
            costsSums[4] += collectableCost;

            // the total cost associate with this choice
            float[] totalCosts = new float[] { distanceCost, angularCost, enemiesCost, obstacleCost, collectableCost };


            nextWaypointHeuristicValues.Add(path, totalCosts);
        }

        Dictionary<List<Vector3>, float> nextWaypointHeuristicValuesNormalized = new Dictionary<List<Vector3>, float>();



        foreach (var kv in nextWaypointHeuristicValues)
        {
            if (kv.Value[0] < 100)  // if we are nearby the end then the angular cost is decreased
                kv.Value[2] /= 20;

            float normCost = 0;
            for (int i = 0; i < costsSums.Length; i++)
            {
                normCost += kv.Value[i] / costsSums[i];
            }


            nextWaypointHeuristicValuesNormalized.Add(kv.Key, normCost);
        }


        var res = MinArgmin<List<Vector3>>(nextWaypointHeuristicValuesNormalized);
        curPath = (List<Vector3>)res[0];
        curTarget = curPath.First();
    }



    // Update is called once per frame
    void Update()
    {

        //if (curPath.Count == 0 && Vector3.Distance(transform.position, end.transform.position) > 2f)
        //    CalculateTrajectory();
        CalculateTrajectory();

        foreach (var v in curPath)
            Debug.DrawLine(transform.position, v, Color.red);



    }
}
