using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GunScript : MonoBehaviour
{

    public GameObject bulletPrefab;
    public Rigidbody vehicleRigibody;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    void Fire()
    {

        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.parent.rotation);
        BulletScript bulletScript = bullet.GetComponent<BulletScript>();


        bulletScript.SetVelocity(transform.TransformDirection(Vector3.forward), vehicleRigibody.velocity);
        

    }
}
