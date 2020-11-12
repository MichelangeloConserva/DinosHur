using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using static AIController;

public class AStarController : MonoBehaviour
{
    public static object[] MinArgmin<TKey>(Dictionary<TKey,float> dict)
    {
        int index = 0, minIndex = 0;
        var minDistObj = dict.Keys.ToList().First();
        float minDist = Mathf.Infinity;
        foreach (var kv in dict)
        {
            if (kv.Value < minDist)
            {
                minDistObj = kv.Key;
                minDist = kv.Value;
                minIndex = index;
            }
            index++;
        }
            
        return new object[] { minDistObj, minDist, minIndex };
    }
    // DISTANCE -- COLLECTABLE
    public float[] costWeights = new float[] { 0.5f, 0.5f };

    public float turnCostWeight;
    public int H = 5;

    public Transform lm;

    public WaypointChecker start;
    public WaypointChecker end;

    public List<Vector3> curPath;
    public WaypointChecker curWaypointTarget;

    public Vector3 curTargetPos() { return curWaypointTarget.transform.position; }


    private bool isAboutToTurn = false;
    private List<List<WaypointChecker>> waypointsLayers;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AIAdvisor"))
        {
            Debug.Log("NOaiNO");
        }
    }


    void Start()
    {
        start = lm.GetChild(0).GetChild(0).GetChild(4).GetComponent<WaypointChecker>();
        end = start;
        for (int i = 0; i< H ; i++)
        {
            end = end.nextWaypointsAndDist.Keys.ToList().Last();
        }


        CalculateLayers();
        calculateTarget();
        //CalculateTrajectory();
    }


    private void CalculateLayers()
    {

        waypointsLayers = new List<List<WaypointChecker>>();
        var curWp = start.nextWaypointsAndDist.Keys.ToArray()[0];

        for (int i = 0; i < H; i++)
        {
            var concurrents = concurrentWp(curWp);

            foreach (var wp in concurrents)
                DrawBox(wp.transform.position + Vector3.up * 2, Vector3.one + Vector3.up * 3, Quaternion.identity, Color.black);

            waypointsLayers.Add(concurrents);
            curWp = concurrents.First().nextWaypointsAndDist.Keys.ToArray()[0];
        }
    }

    public void NextTg()
    {
        start = curWaypointTarget;

        CalculateLayers();
        //CalculateTrajectory();
    }


    //private float DistToEnd(WaypointChecker wc)
    //{
    //    float distance = 0;

    //    if (wc.nextWaypointsAndDist.ContainsKey(end))
    //        return distance;

    //    var res = MinArgmin(wc.nextWaypointsAndDist);
    //    WaypointChecker minDistWc = (WaypointChecker)res[0];
    //    float minDist = (float)res[1];

    //    //Debug.Log(wc.nextWaypointsAndDist.Values.ToArray()[0]);
    //    //Debug.Log(wc.nextWaypointsAndDist.Values.ToArray()[1]);
    //    //Debug.Log(wc.nextWaypointsAndDist.Values.ToArray()[2]);
    //    //Debug.Log(wc.nextWaypointsAndDist.Values.ToArray()[3]);

    //    return distance + minDist + DistToEnd(minDistWc);
    //}


    //private float DistToEnd(WaypointChecker wc, List<Vector3> list)
    //{
    //    float distance = 0;

    //    if (wc.nextWaypointsAndDist.ContainsKey(end))
    //        return distance;

    //    var res = MinArgmin(wc.nextWaypointsAndDist);
    //    WaypointChecker minDistWc = (WaypointChecker)res[0];
    //    float minDist = (float)res[1];

    //    //Debug.Log(wc.nextWaypointsAndDist.Values.ToArray()[0]);
    //    //Debug.Log(wc.nextWaypointsAndDist.Values.ToArray()[1]);
    //    //Debug.Log(wc.nextWaypointsAndDist.Values.ToArray()[2]);
    //    //Debug.Log(wc.nextWaypointsAndDist.Values.ToArray()[3]);

    //    if (list.Count < H)
    //        list.Add(minDistWc.transform.position);

    //    return distance + minDist + DistToEnd(minDistWc, list);
    //}


    //private void CalculateTrajectory()
    //{
    //    Dictionary<List<Vector3>, float[]> nextWaypointHeuristicValues = new Dictionary<List<Vector3>, float[]>();

    //    float[] costsSums = new float[] { 0,0,0,0,0 };

    //    foreach( var kv in start.nextWaypointsAndDist)
    //    {
    //        // nextWaypointHeuristicValues.Add(kv.Key, DistToEnd(kv.Key));

    //        List<Vector3> path = new List<Vector3>();
    //        path.Add(kv.Key.transform.position);
    //        float distanceCost = DistToEnd(kv.Key, path) + kv.Value;

    //        var frwd = transform.GetChild(1).forward; frwd.y = 0;
    //        var frwdAction = (path.First() - transform.GetChild(1).position).normalized;

    //        //float angularCost = 0.01f; 
    //        float angularCost = Mathf.Min(turnCostWeight, Vector3.Angle(frwd, frwdAction)) / turnCostWeight + 0.01f;

    //        //Debug.Log(distanceCost);
    //        //Debug.Log(angularCost);
    //        //Debug.Log("------");


    //        // TODO : implement the cost using the path
    //        float enemiesCost = 0.01f;
    //        float obstacleCost = 0.01f;
    //        float collectableCost = 0.01f;

    //        costsSums[0] += distanceCost;
    //        costsSums[1] += angularCost;
    //        costsSums[2] += enemiesCost;
    //        costsSums[3] += obstacleCost;
    //        costsSums[4] += collectableCost;

    //        // the total cost associate with this choice
    //        float[] totalCosts = new float[] { distanceCost, angularCost, enemiesCost, obstacleCost, collectableCost };


    //        nextWaypointHeuristicValues.Add(path, totalCosts);
    //    }

    //    Dictionary<List<Vector3>, float> nextWaypointHeuristicValuesNormalized = new Dictionary<List<Vector3>, float>();



    //    foreach (var kv in nextWaypointHeuristicValues)
    //    {


    //        if (kv.Value[0] < 100)  // if we are nearby the end then the angular cost is decreased
    //        {
    //            kv.Value[2] /= 20;
    //        }

    //        float normCost = 0;
    //        for (int i = 0; i < costsSums.Length; i++)
    //        {
    //            normCost += kv.Value[i] / costsSums[i];
    //        }


    //        nextWaypointHeuristicValuesNormalized.Add(kv.Key, normCost);
    //    }


    //    var res = MinArgmin<List<Vector3>>(nextWaypointHeuristicValuesNormalized);
    //    curPath = (List<Vector3>)res[0];
    //    curWaypointTarget = start.nextWaypointsAndDist.Keys.ToArray()[(int)res[2]];
    //}




    private List<WaypointChecker> concurrentWp(WaypointChecker wp)
    {
        var wps = new List<WaypointChecker>();
        for (int i = 0; i < wp.transform.parent.childCount-2; i++)
            wps.Add(wp.transform.parent.GetChild(i).GetComponent<WaypointChecker>());
        return wps;
    }

    private float normalizedCost(float[] unnormalized, float[] sums, float[] weights)
    {
        var normalizedCost = 0f;
        for (int i = 0; i < sums.Length; i++)
            normalizedCost += unnormalized[i] * weights[i] / sums[i] ;
        return normalizedCost;
    }

    private bool checkCollectable(Vector3 pos)
    {
        if (Physics.OverlapBox(pos, Vector3.one, Quaternion.identity, LayerMask.GetMask("Collectable")).Length > 0)
            return true;
        return false;
    }






    private Dictionary<WaypointChecker, float> GetFrontierNormalized(Dictionary<WaypointChecker, float[]> frontier, float[] costsSums)
    {
        Dictionary<WaypointChecker, float> frontierNormalized = new Dictionary<WaypointChecker, float>();
        foreach (var kv in frontier)
        {
            var cost = normalizedCost(kv.Value, costsSums, costWeights);
            frontierNormalized.Add(kv.Key, cost);
        }
        return frontierNormalized;
    }



    private WaypointChecker bestOfLayer(List<WaypointChecker> waypointsLayers, WaypointChecker lastSelected, float weight)
    {



        Dictionary<WaypointChecker, float[]> curLayer = new Dictionary<WaypointChecker, float[]>();
        float[] costsSums = new float[] { 0, 0, 0, 0, 0 };

        // Find the waypoints in the last H layer
        foreach (var wp in waypointsLayers)
        {
            var otherPos = wp.transform.position;


            var distanceCost = Vector3.Distance(transform.position, otherPos);
            costsSums[0] = distanceCost;

            var collectableCost = 1;
            if (checkCollectable(wp.transform.position))
                collectableCost = 0;
            costsSums[1] = collectableCost;


            var velocity = GetComponent<Rigidbody>().velocity;
            var angularCost = Vector3.Angle(new Vector3(velocity.x, 0, velocity.z).normalized,
                                            new Vector3(otherPos.x, 0, otherPos.z).normalized);
            costsSums[2] = angularCost;

            var dissonancePathCos = 0f;
            if (lastSelected)
                dissonancePathCos = Vector3.Distance(wp.transform.position, lastSelected.transform.position);
            costsSums[3] = dissonancePathCos;


            var beingTooCentralCost = Vector3.Distance(otherPos, wp.transform.parent.GetChild(wp.transform.parent.childCount - 1).position);
            costsSums[4] = beingTooCentralCost;


            //Debug.Log(angularCost);

            var costs = new float[] { distanceCost, collectableCost, angularCost, dissonancePathCos, beingTooCentralCost };
            curLayer.Add(wp, costs);
        }

        Dictionary<WaypointChecker, float> curLayerNormalized = GetFrontierNormalized(curLayer, costsSums);

        foreach (var kv in curLayerNormalized)
        {
            DrawBox(kv.Key.transform.position + Vector3.up * 2, Vector3.one + Vector3.up * (2 + kv.Value), Quaternion.identity, Color.red);
        }

        // Expanding the best node in the frontier
        var res = MinArgmin(curLayerNormalized);
        var best = (WaypointChecker)res[0];
        var cost = (float)res[1];

        DrawBox(best.transform.position + Vector3.up * 2 * cost, Vector3.one + Vector3.up * 1 * cost, Quaternion.identity, Color.blue);

        return best;
    }


    private void calculateTarget()
    {
        WaypointChecker curTarget = null;
        for (int i = waypointsLayers.Count - 1; i >= 0; i--)
        {
            float weigth = (i / waypointsLayers.Count - 1) * 0.6f;


            curTarget = bestOfLayer(waypointsLayers.ElementAt(i), curTarget, weigth);
            //curPath.Add(curTarget.transform.position);
        }
        curWaypointTarget = curTarget;
    }


    void Update()
    {

        //curPath = new List<Vector3>();
        calculateTarget();

        WaypointChecker future = start;
        for (int i = 0; i < 2 * H + 1; i++)
            future = future.nextWaypointsAndDist.Keys.First();
        DrawBox(future.transform.position, Vector3.one * 3, Quaternion.identity, Color.cyan);




        //if (curPath.Count == 0 && Vector3.Distance(transform.position, end.transform.position) > 2f)
        //    CalculateTrajectory();
        // CalculateTrajectory();

            //foreach (var v in curPath)
            //    Debug.DrawLine(transform.GetChild(0).transform.position, v, Color.red);

    }
}
