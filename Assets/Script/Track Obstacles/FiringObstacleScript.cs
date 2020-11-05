using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringObstacleScript : MonoBehaviour, IObstacle
{

    
    public GameObject bulletPrefab;
    public float force;


    public Vector3 direction;
    public Boolean randomize = true;

    public Transform spawnPoint;
    public void Activate()
    {

        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
        CannonBallScript cbs = bullet.GetComponent<CannonBallScript>();


        if (randomize)
        {

            float modifiedForce = force * UnityEngine.Random.Range(0.8f, 1.2f);
            Vector3 modifiedDirection = new Vector3(direction.x * UnityEngine.Random.Range(0.5f, 1.5f),
                                                  direction.y * UnityEngine.Random.Range(0.5f, 1.5f),
                                                  direction.z * UnityEngine.Random.Range(0.5f, 1.5f));

            cbs.AddForce(modifiedDirection * modifiedForce);
        }
        else
        {
            cbs.AddForce(direction * force);
        }
        
    }
    public void Start()
    {
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
