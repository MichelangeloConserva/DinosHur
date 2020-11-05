using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{

    public GameObject block;

    void SpawnObject()
    {
        block.SetActive(true);
    }


}