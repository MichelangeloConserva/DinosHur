using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private List<IObstacle> recurringObstacles = new List<IObstacle>();
    public float Randommness = 1f;
    public bool TwitchEnabled = true;
    // Update is called once per frame


    public IEnumerator ActivateObstacle(IObstacle obstacle) {

        while (true)
        {
            yield return new WaitForSeconds(obstacle.Cooldown + UnityEngine.Random.Range(-Randommness, Randommness));
            obstacle.Activate();
        }
    }

    public void AddObstacle(IObstacle obstacle)
    {
        recurringObstacles.Add(obstacle);
        StartCoroutine(ActivateObstacle(obstacle));
    }



    




}
