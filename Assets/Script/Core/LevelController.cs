using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    //Singleton
    public static LevelController Instance = null;

    public CollectableController CollectableController;
    public ObstacleController ObstacleController;
    public TwitchController TwitchController;

    public PlayerController PlayerController;
    public UIController UIController;
    public SoundController SoundController;

    public List<CheckpointScript> Checkpoints;

    public float startTime;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } 
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        startTime = Time.time;
    }

    public void Update()
    {
        SetTime(Time.time - startTime);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void AddCollectable(ICollectable cs)
    {
        CollectableController.AddCollectable(cs);
    }

    public void AddObstacle(IObstacle obstacle)
    {
        ObstacleController.AddObstacle(obstacle);
    }

    public void AddCheckPoint(CheckpointScript checkpoint)
    {
        Checkpoints.Add(checkpoint);
    }

    public void PlaySound(SoundType soundType, Vector3 position, float volume = 1f)
    {
        SoundController.PlaySound(soundType, position, volume);
    }

    public void PlayMusic(MusicType musicType, float volume = 1f)
    {
        SoundController.PlayMusic(musicType, volume);
    }

    /// <summary>
    /// Increase the number of collected boxes and set the progression bar in the UI.
    /// </summary>
    public void CollectBox()
    {
        PlayerController.CollectedBoxNum++;
        UIController.SetProgressionBar((float)PlayerController.CollectedBoxNum / CollectableController.MaximumBoxes);

    }

    public void FinishLap()
    {

        // check if all checkpoints have been passed
        bool passedAll = true;
        foreach(CheckpointScript cs in Checkpoints)
        {
            if (cs.Passed == false)
            {
                passedAll = false;
            }
        }

        
        if (passedAll == true)
        {

            float currentLapStartTime = startTime;

            foreach(float previousLapTime in PlayerController.LapTimes) {
                currentLapStartTime += previousLapTime;
            }

            float lapTime = Time.time - currentLapStartTime;
            string formattedTime = ParseTime(lapTime);

            UIController.SetLapTime(PlayerController.CurrentLap, formattedTime);
            PlayerController.FinishLap(lapTime);

            foreach (CheckpointScript cs in Checkpoints)
            {
                cs.Passed = false;
            }
        }


    }

    public void SetTime(float time)
    {

        string formattedTime = ParseTime(time);
        UIController.SetTime(formattedTime);
    }

    private string ParseTime(float time)
    {
        int minutes = (int)Mathf.Floor(time / 60);
        int seconds = (int)time % 60;
        int fraction = ((int)(time * 100)) % 100;

        return String.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, fraction);
    }

}
