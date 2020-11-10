using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CollectableScript : ICollectable
{
    public new void Start()
    {
        base.Start();
    }
    public new void Update()
    {
        base.Update();
       
    }
    public override void Collect()
    {
        LevelController.Instance.CollectBox();
        LevelController.Instance.PlaySound(SoundType.CollectBox, transform.position);

        State = CollectableState.INACTIVE;
        gameObject.SetActive(false);
    }
}
