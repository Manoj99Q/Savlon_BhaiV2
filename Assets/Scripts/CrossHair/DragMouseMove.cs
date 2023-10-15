using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragMouseMove : MonoBehaviour {

    private Vector3 mousePosition;
    private Rigidbody2D rb;
    private Vector2 direction;
    private float moveSpeed = 100f;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 offset = mousePosition - transform.position;
            direction = (mousePosition - transform.position).normalized;
            transform.position =  new Vector2(mousePosition.x,mousePosition.y) + offset;
            //rb.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed) + offset;
        }
        else {
            rb.velocity = Vector2.zero;
        }
    }
}
