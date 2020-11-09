using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Image ProgressionBar;

    public Text DebugText;

    public void SetProgressionBar(float percentage)
    {
        ProgressionBar.fillAmount = percentage;
    }

    public void SetDebugText(string text)
    {
        DebugText.text = text;
    }
}
