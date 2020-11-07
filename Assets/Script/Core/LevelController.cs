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

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCollectable(CollectableScript cs)
    {
        CollectableController.AddCollectable(cs);
    }

    public void CollectBox()
    {
        PlayerController.NumCollectedBoxes++;
        UIController.SetProgressionBar((float)PlayerController.NumCollectedBoxes / CollectableController.MaximumBoxes);

        
    }
}
