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

    private GameObject dinos;
    private AStarController asc;
    private KartGame.KartSystems.ArcadeKart ak;

    //Start is called before the first frame update
    void Start()
    {
        ak = GetComponent<KartGame.KartSystems.ArcadeKart>();
        asc = transform.parent.GetComponent<AStarController>();
        dinos = transform.GetChild(1).gameObject;
    }

    private Vector2 BasicAI()
    {
        if (Time.time < 1)
            return new Vector2(0, 0);
        return new Vector2(UnityEngine.Random.Range(-0.4f, 0.4f), 1);
    }


    private float AngleToTurn()
    {
        var heading = asc.curPath.Last() - dinos.transform.position;
        var cross = Vector3.Cross(dinos.transform.forward, heading.normalized);

        //if (Mathf.Abs(cross.y) < minTurnToTurn)
        //    return 0;
        return Mathf.Clamp(cross.y* turner, -1,1);
    }


    internal Vector2 GatherInputs()
    {

        if (Vector3.Distance(asc.curTargetPos(), dinos.transform.position) < 5)
            asc.NextTg();
        if (Vector3.Distance(asc.curTargetPos(), dinos.transform.position) > Vector3.Distance(asc.curPath.Last(), dinos.transform.position))
            asc.NextTg();



        Debug.DrawLine(dinos.transform.position, asc.curPath.Last() + Vector3.up, Color.blue);
        Debug.DrawRay(dinos.transform.position, dinos.transform.forward + Vector3.up, Color.blue);



        var angle = AngleToTurn();
        var speed = 1 - Mathf.Min(Mathf.Abs(angle), 20f) / 20f;


        Debug.Log(speed);
        Debug.Log(Vector3.Distance(asc.curTargetPos(), dinos.transform.position));

        //var angle = Input.GetAxis("Horizontal");
        //var speed = Input.GetAxis("Vertical");



        return new Vector2(angle, speed * slow);
        return BasicAI();
    }


}