using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{

    protected PlayerController playerController;
    public bool Passed = false;

    public int tileIndex;

    // Start is called before the first frame update
    void Start()
    {
        LevelController.Instance.AddCheckPoint(this);
        playerController = LevelController.Instance.PlayerController;


        foreach (var c in Physics.OverlapBox(transform.position, Vector3.one * 0.5f))
            if (c.gameObject.CompareTag("Counter"))
                tileIndex = c.GetComponent<TrackTile>().GetTileIndex();

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.Equals(playerController.CollectionCollider))
        {
            Passed = true;
            playerController.CurrentCheckPoint = this;
        }

        foreach(PlayerController ai in LevelController.Instance.AIControllers)
        {
            if (other.Equals(ai.CollectionCollider))
            {
                ai.CurrentCheckPoint = this;
            }
        }
        
    }

    public void ResetCheckPoint()
    {
        Passed = false;
    }



}
