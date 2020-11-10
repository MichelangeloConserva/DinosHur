using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : MonoBehaviour
{
    private List<ICollectable> collectables = new List<ICollectable>();
    public int MaximumBoxes = 6;

    private void Update()
    {

        //Check for collectables that need to be respawned
        foreach(ICollectable cs in collectables)
        {
            if (cs.State == CollectableState.INACTIVE)
            {
                StartCoroutine(RespawnCollectable(cs));
            }
        }
    }

    public void AddCollectable(ICollectable cs)
    {
        collectables.Add(cs);
    }

    private IEnumerator RespawnCollectable(ICollectable cs)
    {
        cs.State = CollectableState.RESPAWNING;

        yield return new WaitForSeconds(cs.RespawnTime);

        cs.gameObject.SetActive(true);
        cs.State = CollectableState.ACTIVE;

    }
}
