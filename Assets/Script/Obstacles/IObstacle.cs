
using System;
using UnityEngine;
/// <summary>
/// Base interface for every obstacle
/// </summary>
public abstract class IObstacle : MonoBehaviour
{
    public float Cooldown = 2f;
    public abstract void Activate();

    public void Start()
    {
        LevelController.Instance.AddObstacle(this);
    }



}
