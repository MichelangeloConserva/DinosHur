using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static TrackCreator;
using static Utils;

public class WaypointsAdderCurve : MonoBehaviour
{

    public void AddPreviousConnection(GameObject prevStraigth)
    {
        for (int i = 0; i < prevStraigth.transform.childCount; i++) 
            for (int j = 0; j < transform.GetChild(0).childCount; j++)
                if (prevStraigth.transform.GetChild(j).GetComponent<WaypointChecker>())
                    WaypointAdd(prevStraigth.transform.GetChild(i).GetComponent<WaypointChecker>(), transform.GetChild(0).GetChild(j).gameObject);
    }

    public void AddNextConnection(GameObject nextStraigth)
    {

        for (int j = 0; j < transform.GetChild(3).childCount; j++)
            for (int i = 0; i < nextStraigth.transform.childCount; i++) 
                if (nextStraigth.transform.GetChild(j).GetComponent<WaypointChecker>())
                    WaypointAdd(transform.GetChild(3).GetChild(j).GetComponent<WaypointChecker>(), nextStraigth.transform.GetChild(i).gameObject);
    }

    public void AddInternalWaypoints()
    {
        for (int k = 0; k < 3; k++)
            for (int i = 0; i < transform.GetChild(k).childCount; i++)
                for (int j = 0; j < transform.GetChild(k + 1).childCount; j++)
                    if (transform.GetChild(k).transform.GetChild(i).GetComponent<WaypointChecker>())
                        WaypointAdd(transform.GetChild(k).transform.GetChild(i).GetComponent<WaypointChecker>(), transform.GetChild(k + 1).GetChild(j).gameObject);
    }

}
