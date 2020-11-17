using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public static GameController Instance;

    public bool StartConfirmed = false;
    public string PlayerName = "Ben Hur";
    public string PlayerPosition = "1st";

    public List<float> LapTimes = new List<float>();

    public bool TwitchEnabled = false;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
            TwitchEnabled = false;
            DontDestroyOnLoad(this);
        }

        Application.targetFrameRate = 60;
    }

    public void SetLapTimes(List<float> times)
    {
        LapTimes = times;
    }
        
}
