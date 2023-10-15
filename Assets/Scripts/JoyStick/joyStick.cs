using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class joyStick : MonoBehaviour
{
   
    private float deltaX, deltaY;
    Rigidbody2D rb;
    public float moveSpeed = 2.0f; // Adjust the speed as needed

    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (PlayerShoot.Instance.defence)
                    {
                        PlayerShoot.Instance.Up();
                    }
                    deltaX = touchPos.x - transform.position.x;
                    deltaY = touchPos.y - transform.position.y;
                    break;

                case TouchPhase.Moved:
                    rb.MovePosition(new Vector2(touchPos.x - deltaX, touchPos.y - deltaY)*moveSpeed);
                    break;

                case TouchPhase.Ended:
                    rb.velocity = Vector2.zero;
                    if (!PlayerShoot.Instance.defence)
                    {
                        PlayerShoot.Instance.Defend();
                    }
                    break;
            }
        }
    }
}
