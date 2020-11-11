using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Image ProgressionBar;

    public Image[] hearts;


    public void SetProgressionBar(float percentage)
    {
        ProgressionBar.fillAmount = percentage;
    }

    public void SetHealth(int currentHealth)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].gameObject.SetActive(true);
            }
            else
            {
                hearts[i].gameObject.SetActive(false);
            }
        }
    }

}
