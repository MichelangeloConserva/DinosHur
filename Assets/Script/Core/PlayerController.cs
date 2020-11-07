using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Collider CollectionCollider;
    public Collider ObstacleCollider;

    public int NumCollectedBoxes { get; set; } = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RespawnPlayer();
        }
    }


    void RespawnPlayer()
    {

    }


}
