using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{

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

    internal Vector2 GatherInputs()
    {
        // Debug.Log(asc.curTarget);








        return Vector2.zero;


        return BasicAI();
    }


}