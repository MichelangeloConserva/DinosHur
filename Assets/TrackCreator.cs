using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TrackCreator : MonoBehaviour
{

    public GameObject straightPiece;

    public List<GameObject> track;


    // Start is called before the first frame update
    void Awake()
    {
        track = new List<GameObject>();
    }


    void DeployStraight(Vector3 startPos, int num, Vector3 rotation)
    {
        for (int i =0; i<num; i++)
            track.Add(Instantiate(straightPiece, startPos + ))
    }



    void Update()
    {
        foreach (GameObject o in track)
            Destroy(o);


        DeployStraight(Vector3.zero, 30, Vector3.zero);





    }
}
