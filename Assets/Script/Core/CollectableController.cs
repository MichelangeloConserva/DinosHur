using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : MonoBehaviour
{
    private List<CollectableScript> collectables = new List<CollectableScript>();
    public int MaximumBoxes = 6;

    private void Update()
    {
        foreach(CollectableScript cs in collectables)
        {
            if (cs.State == CollectableState.INACTIVE)
            {
                StartCoroutine(RespawnCollectable(cs));
            }
        }
    }

    public void AddCollectable(CollectableScript cs)
    {
        collectables.Add(cs);
    }

    private IEnumerator RespawnCollectable(CollectableScript cs)
    {
        cs.State = CollectableState.RESPAWNING;

        yield return new WaitForSeconds(cs.RespawnTime);

        cs.gameObject.SetActive(true);
    }
}
