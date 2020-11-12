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

    private float startTime;
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
        

        UpdateUITimers();

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

        if (Checkpoints.TrueForAll(o => o.Passed == true))
        {
            float currentLapStartTime = startTime;
            PlayerController.LapTimes.ForEach(o => currentLapStartTime += o);
            
            float lapTime = Time.time - currentLapStartTime;

            UIController.SetLapTime(PlayerController.CurrentLap, ParseTime(lapTime));
            PlayerController.FinishLap(lapTime);

            Checkpoints.ForEach(o => o.ResetCheckPoint());
        }

    }

    public void SetTime(float time)
    {

        string formattedTime = ParseTime(time);
        UIController.SetTime(formattedTime);
    }

    private void UpdateUITimers()
    {
        // main timer
        SetTime(Time.time - startTime);

        // lap timers
        float currentLapStartTime = startTime;
        PlayerController.LapTimes.ForEach(o => currentLapStartTime += o);

        float lapTime = Time.time - currentLapStartTime;

        UIController.SetLapTime(PlayerController.CurrentLap, ParseTime(lapTime));
    }

    private string ParseTime(float time)
    {
        int minutes = (int)Mathf.Floor(time / 60);
        int seconds = (int)time % 60;
        int fraction = ((int)(time * 100)) % 100;

        return String.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, fraction);
    }

}
