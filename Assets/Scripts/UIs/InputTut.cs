using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTut : MonoBehaviour
{
    public static InputTut Instance;
    public bool deactivate;
    public float time;
    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
    }
    void Deactivate()
    {
        LeanTween.delayedCall(time, () => { gameObject.SetActive(false); });
    }

    // Update is called once per frame
    void Update()
    {
        if(deactivate)
        {
            Deactivate();
        }
    }

    
}
