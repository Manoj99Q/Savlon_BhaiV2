using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DODL : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
