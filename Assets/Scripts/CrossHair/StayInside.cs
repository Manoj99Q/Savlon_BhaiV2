using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInside : MonoBehaviour {

	public float x, y;
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -x, x),
            Mathf.Clamp(transform.position.y, -y, y), transform.position.z);
	}
}
