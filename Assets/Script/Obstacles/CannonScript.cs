using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : IObstacle
{

    
    public GameObject bulletPrefab;

    public float force;
    public Vector3 direction;

    public float forceRandomizer = 0.2f;
    public float spread = 0.5f;

    public Transform spawnPoint;

    public override void Activate()
    {

        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
        CannonBallScript cbs = bullet.GetComponent<CannonBallScript>();

        // Add randomness into force and direction based on forceRandomizer and spread
        float modifiedForce = force * UnityEngine.Random.Range(1 - forceRandomizer, 1 + forceRandomizer);
        Vector3 modifiedDirection = new Vector3(direction.x * UnityEngine.Random.Range(1 - spread, 1 + spread),
                                                direction.y * UnityEngine.Random.Range(1 - spread, 1 + spread),
                                                direction.z * UnityEngine.Random.Range(1 - spread, 1 + spread));

        cbs.AddForce(modifiedDirection * modifiedForce);
        
    }
    public new void Start()
    {

        if (GameController.Instance.TwitchEnabled)
        {
            LevelController.Instance.TwitchController.AddCannon(this);
        } 
        else
        {
            LevelController.Instance.AddObstacle(this);
        }

        direction = Vector3.Normalize(direction);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Activate();
        }    
    }
}
