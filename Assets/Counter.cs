using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    private KartGame.KartSystems.ArcadeKart ak;
    private int lastTileIndex;

    void Start()
    {
        ak = GetComponentInParent<KartGame.KartSystems.ArcadeKart>();
    }

    private void OnTriggerExit(Collider other)
    {


        if (other.gameObject.CompareTag("Counter"))
        {
            int curTileIndex = other.GetComponent<TrackTile>().GetTileIndex();

            if (curTileIndex > lastTileIndex)
                ak.counter++;
            else if (curTileIndex < lastTileIndex)
                if (ak.counter > 0)
                    ak.counter--;

            lastTileIndex = curTileIndex;
        }
    }
}
