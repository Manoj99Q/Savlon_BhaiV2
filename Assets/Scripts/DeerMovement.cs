using UnityEngine;
using System.Collections;

public class DeerMovement : MonoBehaviour
{
    public float moveDistance = 1.0f; // The distance the object will move up and down.
    public float moveSpeed = 2.0f; // The speed at which the object will move.

    private Vector3 initialPosition;

    public GameObject Blood;

    public float shakeAmount = 0.1f;
    public GameObject Deer;

    public AudioSource audio;

    private void Start()
    {
        initialPosition = transform.position;

        // Start the looping movement.
        MoveUpAndDown();
    }

    private void MoveUpAndDown()
    {
        // Tween the object's position using LeanTween.
        LeanTween.moveY(gameObject, initialPosition.y + moveDistance, 1.0f / moveSpeed)
            .setEase(LeanTweenType.easeInOutSine)
            .setOnComplete(() =>
            {
                // When the first part of the movement is complete, move down.
                LeanTween.moveY(gameObject, initialPosition.y - moveDistance, 1.0f / moveSpeed)
                    .setEase(LeanTweenType.easeInOutSine)
                    .setOnComplete(() =>
                    {
                        // After moving down, restart the loop.
                        MoveUpAndDown();
                    });
            });
    }

    public void OnShot()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        audio.Play();
        StartCoroutine(ShakeObject(0.15f));
        GameObject blood =  Instantiate(Blood, transform.position, Quaternion.identity);
        Destroy(blood, 0.15f);
        Destroy(Deer,0.15f);
        Destroy(gameObject, 1f);
    }

    private IEnumerator ShakeObject(float duration)
    {
        Vector3 originalPosition = Deer.transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            // Generate a random offset within the specified range.
            Vector3 randomOffset = Random.insideUnitSphere * shakeAmount;
            randomOffset.z = 0f; // Ensure the object doesn't move in the z-axis.

            if(gameObject!=null)
            {
                // Apply the offset to the object's position.
                Deer.transform.localPosition = originalPosition + randomOffset;
            }
           

            // Increment the elapsed time.
            elapsed += Time.deltaTime;

            yield return null;
        }

        // Reset the object's position when the shake is done.
        Deer.transform.localPosition = originalPosition;
    }
}
