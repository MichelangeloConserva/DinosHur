using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackTile : MonoBehaviour
{

    public int TileIndex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.Equals(LevelController.Instance.PlayerController.CollectionCollider))
        {
            LevelController.Instance.PlayerController.CurrentTile = TileIndex;
            LevelController.Instance.PlayerController.TimeEnteredTile = Time.time;
        }

        foreach(PlayerController ai in LevelController.Instance.AIControllers)
        {
            if (other.Equals(ai.CollectionCollider))
            {
                ai.CurrentTile = TileIndex;
                ai.TimeEnteredTile = Time.time;
            }
        }
    }
}
