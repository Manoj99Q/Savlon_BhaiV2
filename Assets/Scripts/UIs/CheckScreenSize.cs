using UnityEngine;

public class CheckScreenSize : MonoBehaviour
{
    public GameObject uiPanel; // Reference to the UI Panel you want to activate.

    private void Update()
    {
        // Get the screen dimensions
        float screenHeight = Screen.height;
        float screenWidth = Screen.width;
        // Check if the height is greater than the width
        if (screenHeight > screenWidth)
        {
            // Pause the game (you can modify this logic to pause your specific game)
            Time.timeScale = 0;

            // Activate the UI Panel
            if (uiPanel != null)
            {
                uiPanel.SetActive(true);
                AudioListener.pause = true;
            }
        }
        else
        {
            // Unpause the game (you can modify this logic to unpause your specific game)
            Time.timeScale = 1;

            // Deactivate the UI Panel
            if (uiPanel != null)
            {
                uiPanel.SetActive(false);
                AudioListener.pause = false;
            }
        }
    }
}
