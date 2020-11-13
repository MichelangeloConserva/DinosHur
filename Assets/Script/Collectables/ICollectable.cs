using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CollectableState { INACTIVE, RESPAWNING, ACTIVE };
public abstract class ICollectable : MonoBehaviour
{
    public float RespawnTime = 5f;
    public float RotationSpeed = 200f;

    protected List<Collider> chariotColliders = new List<Collider>();
   
    public CollectableState State { get; set; }

    public abstract void Collect();

    public void Start()
    {
        State = CollectableState.ACTIVE;

        chariotColliders.Add(LevelController.Instance.PlayerController.CollectionCollider);
        foreach(PlayerController pc in LevelController.Instance.AIControllers)
        {
            chariotColliders.Add(pc.CollectionCollider);
        }

        LevelController.Instance.AddCollectable(this);
       
    }
    public void Update()
    {
        transform.Rotate(Time.deltaTime * RotationSpeed / 2, Time.deltaTime * RotationSpeed, 0);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (chariotColliders.Contains(other))
        {
            Collect();
        }
    }
}
