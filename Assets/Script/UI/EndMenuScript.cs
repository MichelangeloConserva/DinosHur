using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMenuScript : MonoBehaviour
{

    public Text raceMessage;
    public Text[] laps;
    // Start is called before the first frame update
    void Start()
    {
        FillLapTimes();
        FillRaceTime();
    }

    // Update is called once per frame
    public void FillRaceTime()
    {
        float totalTime = 0;
        for (int i = 0; i < laps.Length; i++)
        {
            totalTime += GameController.Instance.LapTimes[i];
        }

        raceMessage.text = "Congratulations, you have finished the race in " + ParseTime(totalTime);


    }
    public void FillLapTimes()
    {
        for (int i = 0; i < laps.Length; i++) {
            laps[i].text = (i + 1).ToString() + " - " +  ParseTime(GameController.Instance.LapTimes[i]);
        }
    }


    private string ParseTime(float time)
    {
        int minutes = (int)Mathf.Floor(time / 60);
        int seconds = (int)time % 60;
        int fraction = ((int)(time * 100)) % 100;

        return String.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, fraction);
    }


}
