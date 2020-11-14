using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{ 
    public Text Timer;

    public GameObject ProgressionBarUI;
    public Image ProgressionBar;
    
    public Image[] Hearts;

    public GameObject BulletUI;
    public Image[] Bullets;


    public Text[] Laps;
    public Text[] LapTimes;

    public GameObject LivesNotification;
    
    public void SetProgressionBar(float percentage)
    {
        ProgressionBar.fillAmount = percentage;
    }

    public void SetHealth(int currentHealth)
    {
        for (int i = 0; i < Hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                Hearts[i].gameObject.SetActive(true);
            }
            else
            {
                Hearts[i].gameObject.SetActive(false);
            }
        }
    }

    public void SetBullets(int currentBullets)
    {
        for (int i = 0; i < Bullets.Length; i++)
        {
            if (i < currentBullets)
            {
                Bullets[i].gameObject.SetActive(true);
            }
            else
            {
                Bullets[i].gameObject.SetActive(false);
            }
        }
    }

    public void SetTime(string time)
    {
        Timer.text = time;
    }

    public void ChangeLap(int lap)
    {
        for (int i = 0; i < Laps.Length; i++)
        {
            if (i <= lap)
            {
                Laps[i].gameObject.SetActive(true);
                LapTimes[i].gameObject.SetActive(true);
            }
            else
            {
                LapTimes[i].gameObject.SetActive(false);
                Laps[i].gameObject.SetActive(false);
            }
        }
    }

    public void SetLapTime(int lap, string time)
    {
        LapTimes[lap].text = time;
    }

    public void ShowBullets()
    {
        BulletUI.SetActive(true);
        ProgressionBarUI.SetActive(false);
    }

    public void ShowProgressionBar()
    {
        BulletUI.SetActive(false);
        ProgressionBarUI.SetActive(true);
    }

    public void ShowLivesNotification(float time)
    {
        LivesNotification.SetActive(true);
        StartCoroutine(HideLivesNotification(time));
    }

    private IEnumerator HideLivesNotification(float time)
    {
        yield return new WaitForSeconds(time);
        LivesNotification.SetActive(false);
    }
    

}
