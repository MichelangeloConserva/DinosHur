using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIController : MonoBehaviour
{

    public float slow = 0.5f;
    public float turner = 2f;
    public float minTurnToTurn = 0.01f;

    private AStarController asc;
    private KartGame.KartSystems.ArcadeKart ak;

    //Start is called before the first frame update
    void Start()
    {
        ak = GetComponent<KartGame.KartSystems.ArcadeKart>();
        asc = transform.parent.GetComponent<AStarController>();
    }

    private Vector2 BasicAI()
    {
        if (Time.time < 1)
            return new Vector2(0, 0);
        return new Vector2(UnityEngine.Random.Range(-0.4f, 0.4f), 1);
    }


    private float AngleToTurn()
    {

        var rb = GetComponent<Rigidbody>().velocity;

        if (rb.magnitude > 2)
        {

            var ch = transform.parent.GetChild(0).transform.forward;

            var heading = asc.curPath.Last() - transform.position;
            var h = new Vector3(heading.x, 0, heading.z).normalized;
            var z = Vector3.Lerp(new Vector3(rb.x, 0, rb.z).normalized, new Vector3(ch.x, 0, ch.z).normalized, 0.5f);

            var cross = Vector3.Cross(z, h);

            return Mathf.Clamp(cross.y * turner, -1, 1);
        }
        else
        {
            var heading = asc.curPath.Last() - transform.position;
            var cross = Vector3.Cross(transform.forward, heading.normalized);
            return Mathf.Clamp(cross.y * turner, -1, 1);
        }



        //var rb = GetComponent<Rigidbody>().velocity;

        //if (rb.magnitude > 5)
        //{

        //    var heading = asc.curPath.Last() - transform.position;
        //    var h = new Vector3(heading.x, 0, heading.z).normalized;
        //    var z = new Vector3(rb.x, 0, rb.z).normalized;
        //    var cross = Vector3.Cross(z, h);

        //    return Mathf.Clamp(cross.y * turner, -1, 1);
        //}
        //else
        //{
        //    var heading = asc.curPath.Last() - transform.position;
        //    var cross = Vector3.Cross(transform.forward, heading.normalized);
        //    return Mathf.Clamp(cross.y * turner, -1, 1);
        //}


        //var heading = asc.curPath.Last() - dinos.transform.position;
        //var cross = Vector3.Cross(dinos.transform.forward, heading.normalized);

        //var heading = asc.curPath.Last() - transform.position;
        //var h = new Vector3(heading.x, 0, heading.z);
        //var z = new Vector3(rb.x, 0, rb.z);

        //var cross = Vector3.Cross(h, z);

        //if (Mathf.Abs(cross.y) < minTurnToTurn)
        //    return 0;
        //return Mathf.Clamp(cross.y* turner, -1,1);
    }


    internal Vector2 GatherInputs()
    {

        if (Vector3.Distance(asc.curTargetPos(), transform.position) < 5)
            asc.NextTg();
        if (Vector3.Distance(asc.curTargetPos(), transform.position) > Vector3.Distance(asc.curPath.Last(), transform.position))
            asc.NextTg();



        Debug.DrawLine(transform.position, asc.curPath.Last() + Vector3.up, Color.blue);
        Debug.DrawRay(transform.position, transform.forward + Vector3.up, Color.blue);
        Debug.DrawRay(transform.position, GetComponent<Rigidbody>().velocity.normalized * 10 + Vector3.up, Color.green);
        Debug.DrawRay(transform.position, GetComponent<Rigidbody>().angularVelocity.normalized * 10 + Vector3.up, Color.green);
        Debug.DrawRay(transform.position, Vector3.Lerp(new Vector3(GetComponent<Rigidbody>().velocity.x, 0, GetComponent<Rigidbody>().velocity.z).normalized, 
                                                              new Vector3(transform.parent.GetChild(0).transform.forward.x, 0, transform.parent.GetChild(0).transform.forward.z).normalized, 0.5f) * 10 + Vector3.up, Color.black);


        var angle = AngleToTurn();

        var speed = 1 - Mathf.Abs(angle);
        if (speed < 0.1f)
        {
            speed = -1f;
            angle = Mathf.Sign(angle);
        }

        //Debug.Log("Speed: " + speed.ToString() + "---Angle: " + angle.ToString());


        //var angle = Input.GetAxis("Horizontal");
        //var speed = Input.GetAxis("Vertical");

        //Debug.Log(speed);
        //Debug.Log(angle);


        return new Vector2(angle, speed * slow);
        return BasicAI();
    }


}