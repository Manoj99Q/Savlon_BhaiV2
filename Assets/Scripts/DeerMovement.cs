using UnityEngine;
using System.Collections;

public class DeerMovement : MonoBehaviour
{
    public float moveDistance = 1.0f; // The distance the object will move up and down.
    public float moveSpeed = 2.0f; // The speed at which the object will move.

    private Vector3 initialPosition;


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
        Destroy(gameObject);
    }
}
