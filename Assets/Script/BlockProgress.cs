using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockProgress : MonoBehaviour
{

    public int blockCollectionCounter;

    public Image progressionBar;

    public GameObject gun;


    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "block")
        {

            blockCollectionCounter++;

        }

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       

        if (blockCollectionCounter < 6)    
        {
            progressionBar.fillAmount = blockCollectionCounter / 6.0f;
        }
        else if(blockCollectionCounter == 6)
        {
            progressionBar.fillAmount = 1;
            gun.SetActive(true);
        }
        else
        {
            blockCollectionCounter = 1;
            progressionBar.fillAmount = blockCollectionCounter / 6.0f;
        }

    }
}
