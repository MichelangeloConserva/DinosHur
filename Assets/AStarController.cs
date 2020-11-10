using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarController : MonoBehaviour
{

    public Transform start;
    public Transform end;

    public List<Vector3> path;

    // Start is called before the first frame update
    void Start()
    {
        path = new List<Vector3>();
    }

    private void ResetTrack()
    {
        path = new List<Vector3>();
        path.Add(start.position);
    }




    public void CalculateAStar()
    {







    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
