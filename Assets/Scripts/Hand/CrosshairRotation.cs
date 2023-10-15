using System.Collections;
using UnityEngine;

public class CrosshairRotation : MonoBehaviour
{
    public static CrosshairRotation Instance;
    public Transform crosshair; // The crosshair object
    public float rotationSpeed = 5f; // Rotation speed
    public float maxClockwiseRotation = 45f; // Maximum clockwise rotation in degrees
    public float maxCounterclockwiseRotation = -45f; // Maximum counterclockwise rotation in degrees

    public Vector3 targetDirection;

    private bool isShaking = false; // Flag to track if shaking is in progress
    private Vector3 originalPosition; // Original position before shaking

    private void Awake()
    {
        Instance = this;
    }
    void Update()
    {
        
        if (crosshair != null)
        {
            // Get the direction from the current object's position to the crosshair position
            targetDirection = crosshair.position - transform.position;

            // Calculate the desired angle in radians
            float desiredAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

            // Clamp the desired angle within the specified limits
            desiredAngle = Mathf.Clamp(desiredAngle, maxCounterclockwiseRotation, maxClockwiseRotation);

            // Create a Quaternion for the desired rotation on the Z-axis
            Quaternion rotation = Quaternion.Euler(0f, 0f, desiredAngle);

            // Smoothly rotate the object towards the desired rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
    }

    public void ShakeX()
    {
        
            StartCoroutine(ShakeCoroutine());
        
    }

    private IEnumerator ShakeCoroutine()
    {
        originalPosition = transform.localPosition;

        float startTime = Time.time;
        float duration = 0.1f; // Shake duration in seconds
        float shakeMagnitude = 0.1f; // Adjust the magnitude of the shake as needed

        // Calculate the opposite direction based on the current rotation
        Vector3 oppositeDirection = -originalPosition.normalized;

        while (Time.time - startTime < duration)
        {
            // Calculate a random offset in the opposite direction
            Vector3 shakeOffset = oppositeDirection * Random.Range(-shakeMagnitude, shakeMagnitude);

            // Apply the shake offset to the local position
            transform.localPosition = originalPosition + shakeOffset;

            yield return null;
        }

        // Ensure the final position is exactly at the original position
        transform.localPosition = originalPosition;
    }




}
