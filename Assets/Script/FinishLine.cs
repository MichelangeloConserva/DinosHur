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

    void OnTriggerExit(Collider other)
    {

        if (other.Equals(playerController.CollectionCollider))
        {
            LevelController.Instance.FinishLap();
            playerController.GetComponentInChildren<KartGame.KartSystems.ArcadeKart>().counter = 0;
        }

        List<PlayerController> AIControllers = LevelController.Instance.AIControllers;
        foreach(PlayerController ai in AIControllers)
        {
            if (other.Equals(ai.CollectionCollider))
            {
                LevelController.Instance.FinishAILap(ai);
                ai.GetComponentInChildren<KartGame.KartSystems.ArcadeKart>().counter = 0;
            }
        }
    }
}
