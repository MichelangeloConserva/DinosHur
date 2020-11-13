using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public static GameController Instance;

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

        LapTimes.Add(11111111);
        LapTimes.Add(22222222);
        LapTimes.Add(33333333);
        LapTimes.Add(44444444);
    }

    public void SetLapTimes(List<float> times)
    {
        LapTimes = times;
    }
        
}
