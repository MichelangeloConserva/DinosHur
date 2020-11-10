using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    private KartGame.KartSystems.ArcadeKart ak;

    //Start is called before the first frame update
    void Start()
    {
        ak = GetComponent<KartGame.KartSystems.ArcadeKart>();
    }

    private Vector2 BasicAI()
    {
        if (Time.time < 1)
            return new Vector2(0, 0);
        return new Vector2(UnityEngine.Random.Range(-0.4f, 0.4f), 1);
    }


    internal Vector2 GatherInputs()
    {
        return BasicAI();
    }


}