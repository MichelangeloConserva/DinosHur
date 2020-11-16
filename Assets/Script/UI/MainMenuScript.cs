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

    public Text RulesText;
    public Text RulesHeading;

    public Text TwitchText;

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

    public void ToggleTwitch()
    {
        if (GameController.Instance.TwitchEnabled == true)
        {
            GameController.Instance.TwitchEnabled = false;
            TwitchText.text = "Twitch Disabled";
        }
        else
        {
            GameController.Instance.TwitchEnabled = true;
            TwitchText.text = "Twitch Enabled";
        }
    }
    public void TwitchHover()
    {
        RulesHeading.text = "Twitch:";
        RulesText.text = "- Twitch is a streaming platform.\n\n - To play with your friend have them go to twitch.tv/lightsider23\n\n - To fire the cannons have them write \"FIRE\" in the chat.";
    }

    public void TwitchLeave()
    {
        RulesHeading.text = "Rules:";
        RulesText.text = "- Finish 4 laps before the opponents to win the race\n\n- Collect 6 boxes to receive a gun\n\n- Avoid the obstacles on the track\n\n- Have your friends control the cannons at twitch.tv";
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
