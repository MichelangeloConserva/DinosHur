using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public static GameController Instance;

    public bool StartConfirmed = false;

    public List<float> LapTimes = new List<float>();

    public bool TwitchEnabled = false;
    void Awake()
    {
        if (Instance != null)
        {
            
            GameObject.Destroy(Instance);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        Application.targetFrameRate = 60;
    }

    public void SetLapTimes(List<float> times)
    {
        LapTimes = times;
    }
        
}
