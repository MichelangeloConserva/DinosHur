using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : CheckpointScript
{

    // Start is called before the first frame update
    private void Start()
    {
        LevelController.Instance.PlayerController.CurrentCheckPoint = this;
    }

    public new void OnTriggerEnter(Collider other)
    {
        
        if (other.Equals(LevelController.Instance.PlayerController.CollectionCollider))
        {
            LevelController.Instance.FinishLap();
        }
    }
}
