using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GunScript : MonoBehaviour, IWeapon
{

    public GameObject BulletPrefab;
    private Rigidbody vehicleRigibody;

    public int MaxAmmo = 6;
    private int currentAmmo = 0;

    public float FiringCooldown = 0.3f;

    private bool ready = true;

    // Start is called before the first frame update
    void Start()
    {
        vehicleRigibody = LevelController.Instance.PlayerController.vehicleRigidbody;   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && ready == true)
        {
            Use();  
        }
    }

    public void OnEnable()
    {
        currentAmmo = MaxAmmo;
        ready = true;
        LevelController.Instance.UIController.SetBullets(currentAmmo);
    }

    private IEnumerator Ready()
    {
        yield return new WaitForSeconds(FiringCooldown);
        ready = true;
    }


    public void Use()
    {
        GameObject bullet = Instantiate(BulletPrefab, transform.position, transform.parent.rotation);

        BulletScript bulletScript = bullet.GetComponent<BulletScript>();
        bulletScript.SetVelocity(transform.TransformDirection(Vector3.forward), vehicleRigibody.velocity);

        LevelController.Instance.PlaySound(SoundType.LaserGunFire, transform.position, 0.1f);

        currentAmmo--;
        ready = false;

        LevelController.Instance.UIController.SetBullets(currentAmmo);

        if (currentAmmo == 0)
        {
            LevelController.Instance.PlayerController.UnequipGun();
        } else
        {
            StartCoroutine(Ready());
        }
    }
}
