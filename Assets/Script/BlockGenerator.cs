using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{

    //public GameObject spawnPosition;

    public GameObject block;

    public float minWait;
    public float maxWait;

    private bool isSpawning;

    //public float wait;

    void Awake()
    {
        isSpawning = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        //block.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (!isSpawning)
        {
            //block.SetActive(true);
            float timer = Random.Range(minWait, maxWait);
            Invoke("SpawnObject", timer);
            isSpawning = true;
        }

    }


    void SpawnObject()
    {
        //GameObject newBlock = Instantiate(block);
        block.SetActive(true);
        block.transform.position = transform.position;
        isSpawning = false;
    }


    //if (//5 seconds has passed?)
    //{
    //    GameObject newBlock = Instantiate(block);
    //    newBlock.transform.position = spawnPosition.transform.position;
    //}



}