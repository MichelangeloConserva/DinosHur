using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : CheckpointScript
{

    // Start is called before the first frame update
    private void Start()
    {
        playerController = LevelController.Instance.PlayerController;
        playerController.CurrentCheckPoint = this;
        foreach(PlayerController pc in LevelController.Instance.AIControllers)
        {
            pc.CurrentCheckPoint = this;
        }
    }

    public new void OnTriggerExit(Collider other)
    {
        
        if (other.Equals(playerController.CollectionCollider))
            LevelController.Instance.FinishLap();

        List<PlayerController> AIControllers = LevelController.Instance.AIControllers;
        foreach(PlayerController ai in AIControllers)
        {
            if (other.Equals(ai.CollectionCollider))
                LevelController.Instance.FinishAILap(ai);
        }
    }
}
