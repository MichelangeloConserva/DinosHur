using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Linq;

public class LevelController : MonoBehaviour
{

    //Singleton
    public static LevelController Instance = null;

    public List<int> rankings = new List<int>();
    public int maxNumTiles;


    public CollectableController CollectableController;
    public ObstacleController ObstacleController;
    

    public PlayerController PlayerController;
    public List<PlayerController> AIControllers;

    public UIController UIController;
    public SoundController SoundController;

    public TwitchController TwitchController;

    public List<CheckpointScript> Checkpoints { get; set; } = new List<CheckpointScript>();

    public List<TrackTile> trackTiles = new List<TrackTile>();
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
        maxNumTiles = GetComponent<TrackCreator>().trackTileNum;

        StartCoroutine(UpdateRankings());

    }

    public void Update()
    {
        

        UpdateUITimers();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }

    private IEnumerator UpdateRankings()
    {
        while (true)
        {
            List<string> positions = CalculatePoisitions();
            UIController.UpdateRankings(positions);

            yield return new WaitForSeconds(1f);

            
        }
    }



    private List<string> CalculatePoisitions()
    {
        rankings = new List<int>();

        List<PlayerController> allRacers = new List<PlayerController>();
        allRacers.AddRange(AIControllers);
        allRacers.Add(PlayerController);
        

        
        //for (int i = 0; i < allRacers.Count - 1; i++)
        //{
        //    for (int j = i + 1; j < allRacers.Count; j++)
        //    {

        //        // if laps are different OR if laps are same but current tile are different
        //        if ((allRacers[i].CurrentLap < allRacers[j].CurrentLap) ||
        //            (allRacers[i].CurrentLap == allRacers[i].CurrentLap && allRacers[i].CurrentTile < allRacers[j].CurrentTile) ||
        //            (allRacers[i].CurrentLap == allRacers[i].CurrentLap && allRacers[i].CurrentTile == allRacers[j].CurrentTile && allRacers[i].TimeEnteredTile > allRacers[i].TimeEnteredTile)
        //            )
        //        {
        //            PlayerController temp = allRacers[i];
        //            allRacers[i] = allRacers[j];
        //            allRacers[j] = temp;
        //        } 

        //    }
        //}



        allRacers.ForEach(o => rankings.Add(o.CurrentLap * maxNumTiles + o.GetComponentInChildren<KartGame.KartSystems.ArcadeKart>().counter));
        allRacers = allRacers.OrderBy(o => o.CurrentLap * maxNumTiles + o.GetComponentInChildren<KartGame.KartSystems.ArcadeKart>().counter).Reverse().ToList();

        List <String> allNames = new List<string>();
        //allRacers.ForEach(o => allNames.Add(o.RacerName + ": " + o.GetComponentInChildren<KartGame.KartSystems.ArcadeKart>().counter.ToString() + " : " + o.CurrentLap.ToString()));
        allRacers.ForEach(o => allNames.Add(o.RacerName));

        return allNames;
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
    public void CollectBox(bool player)
    {
        if (player)
        {
            PlayerController.CollectedBoxNum++;
            UIController.SetProgressionBar((float)PlayerController.CollectedBoxNum / CollectableController.MaximumBoxes);
        }

    }

    public void FinishLap()
    {
        if (Checkpoints.TrueForAll(o => o.Passed == true))
        {


            if (PlayerController.CurrentLap == 3)
            {
                FinishRace();
            }
            float currentLapStartTime = startTime;
            PlayerController.LapTimes.ForEach(o => currentLapStartTime += o);
            
            float lapTime = Time.time - currentLapStartTime;

            UIController.SetLapTime(PlayerController.CurrentLap, ParseTime(lapTime));

            PlayerController.FinishLap(lapTime);

            UIController.ChangeLap(PlayerController.CurrentLap);


            Checkpoints.ForEach(o => o.ResetCheckPoint());
        }

    }

    public void FinishAILap(PlayerController ai)
    {
        ai.CurrentLap++;
    }

    public void SetTime(float time)
    {

        string formattedTime = ParseTime(time);
        if (UIController)
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

        if (UIController)
            UIController.SetLapTime(PlayerController.CurrentLap, ParseTime(lapTime));
    }

    private string ParseTime(float time)
    {
        int minutes = (int)Mathf.Floor(time / 60);
        int seconds = (int)time % 60;
        int fraction = ((int)(time * 100)) % 100;

        return String.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, fraction);
    }

    public void FinishRace()
    {
        GameController.Instance.SetLapTimes(PlayerController.LapTimes);

        List<string> names = CalculatePoisitions();

        int position = 1; ;
        for(int i = 0; i < names.Count; i++)
        {
            if (names[i].Equals(PlayerController.RacerName)){
                position = i + 1;
            }
        }

        string place = "1st";
        switch (position)
        {
            case 1: place = "1st"; break;
            case 2: place = "2nd"; break;
            case 3: place = "3rd"; break;
            case 4: place = "4th"; break;
            case 5: place = "5th"; break;
            case 6: place = "6th"; break;


        }

        GameController.Instance.PlayerPosition = place;
        SceneManager.LoadScene("EndScene");
    }

}
