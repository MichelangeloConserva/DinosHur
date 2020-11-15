using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using static AIController;

public class AStarController : MonoBehaviour
{

    private static Vector3 GetVector3Down(Vector3 v) { return v - Vector3.up * v.y; }

    public static object[] MinArgmin<TKey>(Dictionary<TKey, float> dict)
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



    public float[] costWeights = new float[] { 0.5f, 0.5f };

    public GameObject Player;

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
    private KartGame.KartSystems.ArcadeKart ak;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("AIAdvisor"))
            GetComponent<Rigidbody>().AddForceAtPosition((other.transform.position - transform.position).normalized * 2 - Vector3.up*5, transform.position + transform.forward - Vector3.up, ForceMode.Acceleration);
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("AIAdvisor"))
            isAboutToTurn = false;
    }

    void Start()
    {
        ak = GetComponent<KartGame.KartSystems.ArcadeKart>();

        start = lm.GetChild(0).GetChild(1).GetChild(4).GetComponent<WaypointChecker>();
        end = start;
        for (int i = 0; i < H; i++)
            end = end.nextWaypointsAndDist.Keys.ToList().Last();

        CalculateLayers();
        calculateTarget();
    }

    private void CalculateLayers()
    {
        waypointsLayers = new List<List<WaypointChecker>>();
        var curWp = start.nextWaypointsAndDist.Keys.ToArray()[0];

        for (int i = 0; i < H; i++)
        {
            var concurrents = concurrentWp(curWp);
            waypointsLayers.Add(concurrents);
            curWp = concurrents.First().nextWaypointsAndDist.Keys.ToArray()[0];
        }
    }

    public void NextTg()
    {
        start = curWaypointTarget;
        CalculateLayers();
    }

    private List<WaypointChecker> concurrentWp(WaypointChecker wp)
    {
        var wps = new List<WaypointChecker>();
        for (int i = 0; i < wp.transform.parent.childCount; i++)
            if (wp.transform.parent.GetChild(i).GetComponent<WaypointChecker>())
                wps.Add(wp.transform.parent.GetChild(i).GetComponent<WaypointChecker>());
        return wps;
    }

    private float normalizedCost(float[] unnormalized, float[] sums, float[] weights)
    {
        var normalizedCost = 0f;
        for (int i = 0; i < sums.Length; i++)
            normalizedCost += unnormalized[i] * weights[i] / (sums[i] > 0 ? sums[i] : 0.001f);
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

    private WaypointChecker bestOfLayer(List<WaypointChecker> waypointsLayers, Vector3 future)
    {
        Dictionary<WaypointChecker, float[]> curLayer = new Dictionary<WaypointChecker, float[]>();
        float[] costsSums = new float[] { 0, 0, 0, 0, 0 };

        // Find the waypoints in the last H layer
        foreach (var wp in waypointsLayers)
        {
            var otherPos = wp.transform.position;


            var distanceCost = Vector3.Distance(transform.position, otherPos);
            costsSums[0] = distanceCost;

            var collectableCost = 1f;
            if (checkCollectable(wp.transform.position))
                collectableCost = 0f;
            costsSums[1] = collectableCost;


            var angularCost = 0f;
            if (!isAboutToTurn)
            {
                var velocity = GetVector3Down(GetComponent<Rigidbody>().velocity);
                velocity = velocity.magnitude > 3 ? velocity : transform.forward;
                angularCost = Vector3.Angle(velocity, otherPos - transform.position);
            }
            costsSums[2] = Mathf.Max(angularCost, 0.001f);


            var dissonancePathCos = Vector3.Distance(wp.transform.position, future);
            costsSums[3] = dissonancePathCos;


            var collisionCost = 0f; // Vector3.Distance(otherPos, wp.transform.parent.GetChild(wp.transform.parent.childCount - 1).position);
            //if (ProspectiveCollision(otherPos))
            //    collisionCost = 1f;
            costsSums[4] = collisionCost;


            var costs = new float[] { distanceCost, collectableCost, angularCost, dissonancePathCos, collisionCost };
            curLayer.Add(wp, costs);

            var str = "";
            foreach (var cc in costs)
                str += cc.ToString() + "-";
            //Debug.Log(str);



        }

        Dictionary<WaypointChecker, float> curLayerNormalized = GetFrontierNormalized(curLayer, costsSums);

        foreach (var kv in curLayerNormalized)
        {
            DrawBox(kv.Key.transform.position + Vector3.up * 2, Vector3.one + Vector3.up * (2 + kv.Value), Quaternion.identity, Color.red);
        }

        // Expanding the best node in the frontier
        var res = MinArgmin(curLayerNormalized);
        var best = (WaypointChecker)res[0];
        var cost = Mathf.Max((float)res[1], 5);

        DrawBox(best.transform.position + Vector3.up * 2 * cost, Vector3.one + Vector3.up * 1 * cost, Quaternion.identity, Color.blue);

        return best;
    }


    private void calculateTarget()
    {
        WaypointChecker future = start;
        for (int i = 0; i < H + 2; i++)
            future = future.nextWaypointsAndDist.Keys.First();

        WaypointChecker curTarget = null;
        for (int i = waypointsLayers.Count - 1; i >= 0; i--)
        {
            float weigth = (i / waypointsLayers.Count - 1) * 0.6f;
            curTarget = bestOfLayer(waypointsLayers.ElementAt(i), future.transform.position);
        }
        curWaypointTarget = curTarget;
    }

    public void ResetPathfinding()
    {
        
        RaycastHit[] hits =  Physics.RaycastAll(transform.position, Vector3.down);
        foreach(RaycastHit hit in hits)
        {
            WaypointChecker wc = hit.collider.gameObject.GetComponentInChildren<WaypointChecker>();
            if ( wc != null)
            {
                //recalculate the way
                start = wc.gameObject.GetComponentInChildren<WaypointChecker>();
                end = start;

                for (int i = 0; i < H; i++)
                    end = end.nextWaypointsAndDist.Keys.ToList().Last();

                CalculateLayers();
                calculateTarget();
            }
        }
    }


    void Update()
    {
        calculateTarget();

        WaypointChecker future = start;
        for (int i = 0; i < 2 * H + 1; i++)
            future = future.nextWaypointsAndDist.Keys.First();
        DrawBox(future.transform.position, Vector3.one * 3, Quaternion.identity, Color.cyan);


        //foreach (var concurrents in waypointsLayers)
        //    foreach (var wp in concurrents)
        //        DrawBox(wp.transform.position + Vector3.up * 2, Vector3.one , Quaternion.identity, Color.black);


        //if (curPath.Count == 0 && Vector3.Distance(transform.position, end.transform.position) > 2f)
        //    CalculateTrajectory();
        // CalculateTrajectory();

        //foreach (var v in curPath)
        //    Debug.DrawLine(transform.GetChild(0).transform.position, v, Color.red);

    }
}
