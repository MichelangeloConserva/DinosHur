using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartCollectable : ICollectable
{
    public override void Collect(bool player)
    {
        if (player == true)
        {
            LevelController.Instance.PlayerController.IncreaseHealth();
        }

        LevelController.Instance.PlaySound(SoundType.CollectBox, transform.position);

        State = CollectableState.INACTIVE;
        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();   
    }

}
