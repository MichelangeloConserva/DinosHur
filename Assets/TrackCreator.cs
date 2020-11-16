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
        public bool hasFinalCurve = true;
        public bool curveRotated;
        public Vector3 curveModifier;
        public Vector3 startPos, direction;
        public Vector3 rotation;
        public GameObject finalCurve;
        public GameObject firstPiece;
        public GameObject lastPiece;
    }


    public static void WaypointAdd(WaypointChecker waypoint, GameObject nextGO)
    {
        if(waypoint)
            if (nextGO.GetComponent<WaypointChecker>())
                waypoint.nextWaypointsAndDist.Add(nextGO.GetComponent<WaypointChecker>(), Vector3.Distance(waypoint.transform.position, nextGO.transform.position)); ;
    }


    public bool IsDebugOn;

    public List<GameObject> waypoints;

    public GameObject straightPiece;
    public GameObject curvePiece;
    public GameObject counter;

    public TrackPiece[] trackPieces;


    private List<GameObject> track;

    public int trackTileNum = 0;
    
    void Awake()
    {
        track = new List<GameObject>();
        waypoints = new List<GameObject>();

        UpdateTrack();
    }

    private void LinkTwoStraight(GameObject prev, GameObject next)
    {
        for (int j = 0; j < prev.transform.childCount; j++)   // -2 to take into account of the walls
            for (int k = 0; k < next.transform.childCount; k++)
                if(prev.transform.GetChild(j).GetComponent<WaypointChecker>())
                    WaypointAdd(prev.transform.GetChild(j).GetComponent<WaypointChecker>(), next.transform.GetChild(k).gameObject);
    }

    void DeployStraight(TrackPiece tp, Vector3 startPos, int num, Quaternion rotation, Vector3 direction, int index)
    {
        GameObject prev = null;
        for (int i = 0; i < num; i++)
        {
            GameObject cur = Instantiate(straightPiece, startPos + i * direction, rotation, transform.GetChild(0));


            if (rotation.eulerAngles.x != 0)
                for (int ii=0; ii< cur.transform.GetChild(cur.transform.childCount - 1).childCount; ii++)
                    cur.transform.GetChild(cur.transform.childCount - 1).GetChild(ii).rotation = Quaternion.Euler(0, 0, 0);

            for (int ii=0; ii< cur.transform.GetChild(cur.transform.childCount - 1).childCount; ii++)
            {
                cur.transform.GetChild(cur.transform.childCount - 1).GetChild(ii).GetComponent<TrackTile>().SetTileIndex(trackTileNum++);
                //if (rotation.eulerAngles.x != 0)
                //    cur.transform.GetChild(cur.transform.childCount - 1).GetChild(ii).rotation = Quaternion.identity;
            }

            //for (int ii=-4; ii < 5; ii++)
            //{
            //    var cc = Instantiate(counter, Vector3.zero, Quaternion.identity, cur.transform.GetChild(cur.transform.childCount - 1));
            //    cc.transform.localPosition = 8 * (ii / 4f) * cur.transform.forward;
            //    cc.GetComponent<TrackTile>().SetTileIndex(trackTileNum++);
            //}


            if (prev)
                LinkTwoStraight(prev, cur);

            if (i == 0)
            {
                tp.firstPiece = cur;
                if (index > 0)
                {
                    if (trackPieces[index - 1].hasFinalCurve)
                        trackPieces[index - 1].finalCurve.GetComponent<WaypointsAdderCurve>().AddNextConnection(cur);
                    else
                        LinkTwoStraight(trackPieces[index - 1].lastPiece, cur);
                }
            }

            track.Add(prev = cur);
            tp.lastPiece = cur;
        }

        if (tp.hasFinalCurve)
        {

            tp.finalCurve = Instantiate(curvePiece, prev.transform.position + direction.normalized * 30 - Vector3.Cross(direction.normalized, Vector3.up) / 10 + tp.curveModifier, 
                Quaternion.Euler(0, rotation.eulerAngles.y + 90 + (tp.curveRotated ? 90 : 0), 0), transform.GetChild(0));
            if (tp.curveRotated)
                for (int ii = 1; ii < 4; ii++)
                    tp.finalCurve.transform.GetChild(ii).SetAsFirstSibling();
            tp.finalCurve.GetComponent<WaypointsAdderCurve>().AddInternalWaypoints();
            tp.finalCurve.GetComponent<WaypointsAdderCurve>().AddPreviousConnection(prev);
        }

        if ((index == (trackPieces.Length - 1)))
            tp.finalCurve.GetComponent<WaypointsAdderCurve>().AddNextConnection(trackPieces[0].firstPiece);
    }

    private void UpdateTrack()
    {
        trackTileNum = 0;

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
