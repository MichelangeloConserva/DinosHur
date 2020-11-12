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

    private List<String> lapTimes = new List<String>();

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

    public void SetTime(float time)
    {

        int minutes = (int) Mathf.Floor(time / 60);
        int seconds = (int)time % 60;
        int fraction = ((int)(time * 100)) % 100;
        
        string format = String.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, fraction);
        UIController.SetTime(format);
    }
}
