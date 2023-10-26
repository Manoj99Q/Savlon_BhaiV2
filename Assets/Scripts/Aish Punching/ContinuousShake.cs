using System.Collections;
using UnityEngine;

public class ContinuousShake : MonoBehaviour
{
    public float shakeDuration = 1.0f;
    public static float shakeAmount = 0.1f;
    public float decreaseFactor = 1.0f;

    private Vector3 originalPosition;

    private void Start()
    {
        shakeAmount = 0.1f;
        originalPosition = transform.position;
    }

    private void Update()
    {
       
        if (shakeDuration > 0)
        {
            Vector3 randomOffset = Random.insideUnitSphere * shakeAmount;
            transform.position = originalPosition + randomOffset;
            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            // Reset the position and restart the shaking loop
            shakeDuration = 1.0f; // Adjust the duration for your needs
            transform.position = originalPosition;
        }
    }
}
