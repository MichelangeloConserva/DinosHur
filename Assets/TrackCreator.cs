using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TrackCreator : MonoBehaviour
{
    [System.Serializable]
    public class TrackPiece
    {
        public Vector3 startPos, direction;
        public int num;
        public Vector3 rotation;
    }



    public TrackPiece[] trackPieces;


    public GameObject straightPiece;

    private List<GameObject> track;


    // Start is called before the first frame update
    void Awake()
    {
        track = new List<GameObject>();
    }


    void DeployStraight(Vector3 startPos, int num, Quaternion rotation, Vector3 direction)
    {
        for (int i = 0; i < num; i++)
            track.Add(Instantiate(straightPiece, startPos + i * direction, rotation, transform.GetChild(0)));
    }



    void Update()
    {
        foreach (GameObject o in track)
            DestroyImmediate(o);
        track = new List<GameObject>();

        foreach (TrackPiece tp in trackPieces)
            DeployStraight(tp.startPos, tp.num, Quaternion.Euler(tp.rotation), tp.direction);




    }
}
