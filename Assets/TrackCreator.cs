using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class TrackCreator : MonoBehaviour
{

    [System.Serializable]
    public class TrackPiece
    {
        public Vector3 startPos, direction;
        public int num;
        public Vector3 rotation;
    }

    [System.Serializable]
    public class TrackPieceCurve
    {
        public Vector3 startPos;
        public Vector3 rotation;
    }


    public static void WaypointAdd(WaypointChecker waypoint, GameObject nextGO)
    {
        waypoint.nextWaypointsAndDist.Add(nextGO.GetComponent<WaypointChecker>(), Vector3.Distance(waypoint.transform.position, nextGO.transform.position)); ;
    }


    public bool IsDebugOn;

    public List<GameObject> waypoints;

    public GameObject straightPiece;
    public GameObject curvePiece;

    public TrackPiece[] trackPieces;
    public TrackPieceCurve[] trackCurves;


    private List<GameObject> track;
    
    void Awake()
    {
        track = new List<GameObject>();
        waypoints = new List<GameObject>();

        UpdateTrack();
    }


    void DeployStraight(Vector3 startPos, int num, Quaternion rotation, Vector3 direction)
    {
        GameObject prev = null;

        for (int i = 0; i < num; i++)
        {
            var cur = Instantiate(straightPiece, startPos + i * direction, rotation, transform.GetChild(0));

            if (prev)
                for (int j = 0; j < prev.transform.childCount-2; j++)   // -2 to take into account of the walls
                    for (int k = 0; k < cur.transform.childCount-2; k++)
                        WaypointAdd(prev.transform.GetChild(j).GetComponent<WaypointChecker>(), cur.transform.GetChild(k).gameObject);

            prev = cur;
            track.Add(cur);
        }

    }

    private void UpdateTrack()
    {
        while (transform.GetChild(0).childCount > 0)
            DestroyImmediate(transform.GetChild(0).GetChild(0).gameObject);


        foreach (GameObject o in track)
            DestroyImmediate(o);
        track = new List<GameObject>();

        foreach (TrackPiece tp in trackPieces)
            DeployStraight(tp.startPos, tp.num, Quaternion.Euler(tp.rotation), tp.direction);


        //Make sure that curves are added after straight lines and not before so that AddWaypoints works
        foreach (TrackPieceCurve tp in trackCurves)
        {
            var curve = Instantiate(curvePiece, tp.startPos, Quaternion.Euler(tp.rotation), transform.GetChild(0));
            curve.GetComponent<WaypointsAdderCurve>().AddWaypoints();
            track.Add(curve);
        }

    }

    void Update()
    {
        // UpdateTrack();
    }
}
