using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDefence : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= -2f)
        {
            if (!PlayerShoot.Instance.defence)
            {
                PlayerShoot.Instance.Defend();
            }

        }
        else
        {
            if (PlayerShoot.Instance.defence)
            {
                PlayerShoot.Instance.Up();
            }
        }
    }
}
