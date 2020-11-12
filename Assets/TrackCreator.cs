using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class TrackCreator : MonoBehaviour
{

    [System.Serializable]
    public class TrackPiece
    {
        public int num;
        public bool curveRotated;
        public Vector3 startPos, direction;
        public Vector3 rotation;
        public GameObject finalCurve;
        public GameObject firstPiece;
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


    private List<GameObject> track;
    
    void Awake()
    {
        track = new List<GameObject>();
        waypoints = new List<GameObject>();

        UpdateTrack();
    }


    void DeployStraight(TrackPiece tp, Vector3 startPos, int num, Quaternion rotation, Vector3 direction, int index)
    {
        GameObject prev = null;
        for (int i = 0; i < num; i++)
        {
            var cur = Instantiate(straightPiece, startPos + i * direction, rotation, transform.GetChild(0));
            if (prev)
                for (int j = 0; j < prev.transform.childCount-2; j++)   // -2 to take into account of the walls
                    for (int k = 0; k < cur.transform.childCount-2; k++)
                        WaypointAdd(prev.transform.GetChild(j).GetComponent<WaypointChecker>(), cur.transform.GetChild(k).gameObject);

            if (i == 0)
            {
                tp.firstPiece = cur;
                if (index > 0)
                    trackPieces[index - 1].finalCurve.GetComponent<WaypointsAdderCurve>().AddNextConnection(cur);
            }

            track.Add(prev = cur);
        }

        tp.finalCurve = Instantiate(curvePiece, prev.transform.position + direction.normalized * 30 - Vector3.Cross(direction.normalized, Vector3.up) / 10, Quaternion.Euler(0, rotation.eulerAngles.y + 90 + (tp.curveRotated ? 90 : 0), 0), transform.GetChild(0));

        if (tp.curveRotated)
            for (int ii=1; ii<4; ii++)
                tp.finalCurve.transform.GetChild(ii).SetAsFirstSibling();

        tp.finalCurve.GetComponent<WaypointsAdderCurve>().AddInternalWaypoints();
        tp.finalCurve.GetComponent<WaypointsAdderCurve>().AddPreviousConnection(prev);

        if ((index == (trackPieces.Length - 1)))
            tp.finalCurve.GetComponent<WaypointsAdderCurve>().AddNextConnection(trackPieces[0].firstPiece);
    }

    private void UpdateTrack()
    {
        while (transform.GetChild(0).childCount > 0)
            DestroyImmediate(transform.GetChild(0).GetChild(0).gameObject);


        foreach (GameObject o in track)
            DestroyImmediate(o);
        track = new List<GameObject>();

        for (int i=0; i<trackPieces.Length; i++)
            DeployStraight(trackPieces[i], trackPieces[i].startPos, trackPieces[i].num, Quaternion.Euler(trackPieces[i].rotation), trackPieces[i].direction, i);


    }

    private void Update()
    {
        if (!Application.isPlaying)
            UpdateTrack();

    }

}
