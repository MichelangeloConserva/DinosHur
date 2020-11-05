using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDestroy : MonoBehaviour
{

    //public GameObject block;



    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "vehicle")
        {

            //Destroy(gameObject);
            gameObject.SetActive(false);

        }

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
