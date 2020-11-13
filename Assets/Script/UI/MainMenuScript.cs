using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{

    public GameObject Controls;
    public GameObject MenuItems;
    public GameObject Rules;
    public Text Loading;


    public void StartRace()
    {
        //SceneManager.LoadScene(1);

        
        Controls.SetActive(false);
        MenuItems.SetActive(false);
        Rules.SetActive(false);
        Loading.gameObject.SetActive(true);


        StartCoroutine(LoadYourAsyncScene());
    }


    public void ExitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }


    IEnumerator LoadYourAsyncScene()
    {

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MarkoTrack");
        //asyncLoad.allowSceneActivation = false;

        string text = "...Loading...";
        int currentIndex = 0;

        while (!asyncLoad.isDone)
        {

            Loading.text = text.Substring(0, currentIndex);
            currentIndex = (currentIndex + 1) % (text.Length + 1);
            yield return new WaitForSeconds(0.2f);

        }
        
    }

}
