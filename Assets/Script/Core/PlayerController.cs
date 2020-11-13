using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public int boxesNecessaryForGun = 6;
    public int playerHealth = 4;

    public GameObject gun;

    public Collider CollectionCollider;
    public Collider ObstacleCollider;

    public Rigidbody dinoRigidbody;
    public Rigidbody vehicleRigidbody;

    public CheckpointScript CurrentCheckPoint { get; set; }
    public int CollectedBoxNum { get; set; } = 0;


    public int CurrentLap { get; set; } = 0;
    public List<float> LapTimes { get; set; } = new List<float>();
    // Start is called before the first frame update
    void Start()
    {
        //CurrentCheckPoint = LevelController.Instance.Checkpoints[0];

        if (transform.GetChild(0).childCount == 3)
            transform.GetChild(0).GetChild(2).gameObject.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RespawnPlayer();
        }

        if (CollectedBoxNum == boxesNecessaryForGun)
        {
            EquipGun();
        }
            
    }

    /// <summary>
    /// Resets player to the last checkpoint
    /// </summary>
    void RespawnPlayer()
    {

        dinoRigidbody.velocity = Vector3.zero;
        dinoRigidbody.angularVelocity = Vector3.zero;
        dinoRigidbody.transform.position = new Vector3(
                                                    CurrentCheckPoint.transform.position.x,
                                                    CurrentCheckPoint.transform.position.y + 10f,
                                                    CurrentCheckPoint.transform.position.z
                                                    );
        dinoRigidbody.transform.rotation = CurrentCheckPoint.transform.rotation;

        vehicleRigidbody.velocity = Vector3.zero;
        vehicleRigidbody.angularVelocity = Vector3.zero;
        vehicleRigidbody.transform.position = new Vector3(
                                                    CurrentCheckPoint.transform.position.x, 
                                                    CurrentCheckPoint.transform.position.y + 10f, 
                                                    CurrentCheckPoint.transform.position.z - 2f
                                                    );
        vehicleRigidbody.transform.rotation = CurrentCheckPoint.transform.rotation;
        
    }



    public void DecreaseHealth()
    {
        if (playerHealth > 0)
        {
            playerHealth--;
            LevelController.Instance.UIController.SetHealth(playerHealth);
        }
    }

    public void IncreaseHealth()
    {
        if (playerHealth < 4)
            playerHealth++;
            LevelController.Instance.UIController.SetHealth(playerHealth);
    }

    public void FinishLap(float time)
    {
        CurrentLap++;
        LapTimes.Add(time);
    }

    public void EquipGun()
    {
        gun.SetActive(true);

        LevelController.Instance.UIController.ShowBullets();
    }

    public void UnequipGun()
    {

        CollectedBoxNum = 0;
        gun.SetActive(false);

        LevelController.Instance.UIController.SetProgressionBar(0);
        LevelController.Instance.UIController.ShowProgressionBar();
        
    }

}
