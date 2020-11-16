using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preloader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MainMenuScript>().PreloadScene();
    }


}
