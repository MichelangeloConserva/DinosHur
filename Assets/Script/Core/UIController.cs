using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Image ProgressionBar;


    public void SetProgressionBar(float percentage)
    {
        ProgressionBar.fillAmount = percentage;
    }

}
