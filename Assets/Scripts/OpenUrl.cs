using UnityEngine;

public class OpenUrl : MonoBehaviour
{
    public string urlToOpen = " ";

    public void OpenURL()
    {
        Application.OpenURL(urlToOpen);
    }
}
