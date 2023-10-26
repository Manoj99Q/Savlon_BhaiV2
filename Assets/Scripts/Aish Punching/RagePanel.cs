using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RagePanel : MonoBehaviour
{
    public static RagePanel Instance;

    // the image you want to fade, assign in the inspector
    public Image img;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        img = GetComponent<Image>();
    }

    public void FadeIn()
    {
        // Fades the image in over 1 second and stops at alpha 0.5f
        StartCoroutine(FadeInImage());
    }

    IEnumerator FadeInImage()
    {
        float duration = 1.0f;
        float targetAlpha = 0.35f;
        Color startColor = img.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            img.color = Color.Lerp(startColor, targetColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final alpha is exactly 0.5f
        img.color = targetColor;
    }
}
