using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{

    // Start is called before the first frame update


    public void OnTriggerEnter(Collider other)
    {
        
        if (other.Equals(LevelController.Instance.PlayerController.CollectionCollider))
        {
            LevelController.Instance.FinishLap();
        }
    }
}
