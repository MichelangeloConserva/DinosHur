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
            Destroy(Instance);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void SetLapTimes(List<float> times)
    {
        LapTimes = times;
    }
        
}
