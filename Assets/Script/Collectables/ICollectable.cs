using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CollectableState { INACTIVE, RESPAWNING, ACTIVE };
public abstract class ICollectable : MonoBehaviour
{
    public float RespawnTime = 5f;
    public float RotationSpeed = 200f;

    protected Collider playerCollider;
   
    public CollectableState State { get; set; }

    public abstract void Collect();

    public void Start()
    {
        State = CollectableState.ACTIVE;
        playerCollider = LevelController.Instance.PlayerController.CollectionCollider;

        LevelController.Instance.AddCollectable(this);
       
    }
    public void Update()
    {
        transform.Rotate(Time.deltaTime * RotationSpeed / 2, Time.deltaTime * RotationSpeed, 0);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.Equals(playerCollider))
        {
            Collect();
        }
    }
}
