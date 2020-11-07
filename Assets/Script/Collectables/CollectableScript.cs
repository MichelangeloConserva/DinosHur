using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CollectableState { INACTIVE, RESPAWNING, ACTIVE};
public class CollectableScript : MonoBehaviour
{
    public float RotationSpeed = 200f;
    public float RespawnTime = 5f;

    private Collider playerCollider;

    public CollectableState State { get; set; }

    private void Start()
    {
        LevelController.Instance.AddCollectable(this);
        playerCollider = LevelController.Instance.PlayerController.CollectionCollider;
    }
    void Update()
    {
        transform.Rotate(Time.deltaTime * RotationSpeed / 2, Time.deltaTime * RotationSpeed, 0);
    }

    public void OnTriggerEnter(Collider other)
    {
        
        if (other.Equals(playerCollider))
        {
            LevelController.Instance.CollectBox();

            //other.transform.root.GetComponent<BlockProgress>().blockCollectionCounter++;
            
            State = CollectableState.INACTIVE;
            gameObject.SetActive(false);

        }

    }
}
