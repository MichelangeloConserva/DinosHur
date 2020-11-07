using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Image ProgressionBar;
    // Start is called before the first frame update

    public void SetProgressionBar(float percentage)
    {
        Debug.Log("test");
        ProgressionBar.fillAmount = percentage;
    }
}
