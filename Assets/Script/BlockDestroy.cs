using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDestroy : MonoBehaviour
{


    void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.transform.root.CompareTag("vehicle"))
        {
            //Destroy(gameObject);
            collision.transform.root.GetComponent<BlockProgress>().blockCollectionCounter++;
            gameObject.SetActive(false);
        }
    }


}
