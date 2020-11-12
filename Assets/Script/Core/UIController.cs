using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{ 
    public Text Timer;

    public Image ProgressionBar;
    public Image[] Hearts;

    public Text[] LapTimes;
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

    public void SetTime(string time)
    {
        Timer.text = time;
    }

    public void SetLapTime(int lap, string time)
    {
        LapTimes[lap].text = time;
    }

    

}
