using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{

    protected PlayerController playerController;
    public bool Passed = false;
    // Start is called before the first frame update
    void Start()
    {
        LevelController.Instance.AddCheckPoint(this);
        playerController = LevelController.Instance.PlayerController;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.Equals(playerController.CollectionCollider))
        {
            Passed = true;
            playerController.CurrentCheckPoint = this;
        }
    }

    public void ResetCheckPoint()
    {
        Passed = false;
    }



}
