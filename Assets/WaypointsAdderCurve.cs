using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static TrackCreator;
using static Utils;

public class WaypointsAdderCurve : MonoBehaviour
{

    private GameObject findLink(Vector3 pos)
    {
        RaycastHit hitInfo;
        if (Physics.BoxCast(pos, Vector3.one * 8, Vector3.one, out hitInfo, Quaternion.identity))
        {
            DrawBoxCastOnHit(pos, Vector3.one * 8, Quaternion.identity, Vector3.one, hitInfo.distance, Color.red);
        }
        DrawBoxCastOnHit(pos, Vector3.one * 8, Quaternion.identity, Vector3.one, 10, Color.red);

        RaycastHit rHit;
        if (Physics.BoxCast(pos, Vector3.one * 8, Vector3.one, out rHit))
            return rHit.transform.parent.gameObject;
        Debug.LogError("A CURVE HAS NOT FOUND THE PREVIOUS STRAIGHT LINE");
        return null;
    }


    public void AddWaypoints()
    {

        var previousPos = transform.GetChild(transform.childCount - 2);
        var nextPos = transform.GetChild(transform.childCount - 1);

        GameObject prevStraigth = findLink(previousPos.position);
        GameObject nextStraigth = findLink(nextPos.position);

        for (int i = 0; i < prevStraigth.transform.childCount - 2; i++)  // -2 for the walls
            for (int j = 0; j < transform.GetChild(0).childCount; j++)
                WaypointAdd(prevStraigth.transform.GetChild(i).GetComponent<WaypointChecker>(), transform.GetChild(0).GetChild(j).gameObject);

        for (int k = 0; k < 5; k++)
            for (int i = 0; i < transform.GetChild(k).childCount; i++)
                for (int j = 0; j < transform.GetChild(k + 1).childCount; j++)
                    WaypointAdd(transform.GetChild(k).transform.GetChild(i).GetComponent<WaypointChecker>(), transform.GetChild(k + 1).GetChild(j).gameObject);

        for (int j = 0; j < transform.GetChild(5).childCount; j++)
            for (int i = 0; i < nextStraigth.transform.childCount - 2; i++)  // -2 for the walls
                WaypointAdd(transform.GetChild(5).GetChild(j).GetComponent<WaypointChecker>(), nextStraigth.transform.GetChild(i).gameObject);

    }

}
