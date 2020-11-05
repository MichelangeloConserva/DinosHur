using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChariotSpawner : MonoBehaviour
{

    public GameObject chariot;
    public List<GameObject> AIChariots = new List<GameObject>();



    void Start()
    {

        AIChariots.Add(Instantiate(chariot, new Vector3(-5.8f, 2, 0), Quaternion.identity, transform));
        AIChariots.Add(Instantiate(chariot, new Vector3(-11f, 2, 0), Quaternion.identity, transform));
        AIChariots.Add(Instantiate(chariot, new Vector3(5.8f, 2, 0), Quaternion.identity, transform));
        AIChariots.Add(Instantiate(chariot, new Vector3(11f, 2, 0), Quaternion.identity, transform));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
