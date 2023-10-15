using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairMovement : MonoBehaviour
{
    public static CrossHairMovement Instance;
    [SerializeField] private Camera mainCam;
    public bool movementDefence = true;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        movementDefence = true;
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



        Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        transform.position = mousePos;
    }
}
