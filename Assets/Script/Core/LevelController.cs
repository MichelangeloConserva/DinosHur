using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    //Singleton
    public static LevelController Instance = null;

    public CollectableController CollectableController;
    public TwitchController TwitchController;

    public PlayerController PlayerController;
    public UIController UIController;
   
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

    public void AddCollectable(CollectableScript cs)
    {
        CollectableController.AddCollectable(cs);
    }

    /// <summary>
    /// Increase the number of collected boxes and set the progression bar in the UI.
    /// </summary>
    public void CollectBox()
    {
        PlayerController.CollectedBoxNum++;
        UIController.SetProgressionBar((float)PlayerController.CollectedBoxNum / CollectableController.MaximumBoxes);

        
    }
}
