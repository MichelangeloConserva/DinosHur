using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRespawn : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
     
        if (other.Equals(LevelController.Instance.PlayerController.CollectionCollider))
        {
            LevelController.Instance.PlayerController.RespawnPlayer();
        }

        foreach(PlayerController ai in LevelController.Instance.AIControllers)
        {

            if (other.Equals(ai.CollectionCollider))
            {
                ai.RespawnPlayer();
            }
        }
    }
}
