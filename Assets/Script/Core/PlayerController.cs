using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public int boxesNecessaryForGun = 6;

    public GameObject gun;

    public Collider CollectionCollider;
    public Collider ObstacleCollider;

    public Rigidbody dinoRigidbody;
    public Rigidbody vehicleRigidbody;

    public Transform CurrentCheckPoint { get; set; }

    public int CollectedBoxNum { get; set; } = 0;
    // Start is called before the first frame update
    void Start()
    {
        CurrentCheckPoint = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RespawnPlayer();
        }

        if (CollectedBoxNum == boxesNecessaryForGun)
            gun.SetActive(true);

    }

    /// <summary>
    /// Resets player to the last checkpoint
    /// </summary>
    void RespawnPlayer()
    {

        dinoRigidbody.velocity = Vector3.zero;
        dinoRigidbody.angularVelocity = Vector3.zero;
        dinoRigidbody.transform.position = new Vector3(
                                                    CurrentCheckPoint.position.x,
                                                    CurrentCheckPoint.position.y + 10f,
                                                    CurrentCheckPoint.position.z
                                                    );
        dinoRigidbody.transform.rotation = CurrentCheckPoint.rotation;

        vehicleRigidbody.velocity = Vector3.zero;
        vehicleRigidbody.angularVelocity = Vector3.zero;
        vehicleRigidbody.transform.position = new Vector3(
                                                    CurrentCheckPoint.position.x, 
                                                    CurrentCheckPoint.position.y + 10f, 
                                                    CurrentCheckPoint.position.z - 2f
                                                    );
        vehicleRigidbody.transform.rotation = CurrentCheckPoint.rotation;
        
    }


}
