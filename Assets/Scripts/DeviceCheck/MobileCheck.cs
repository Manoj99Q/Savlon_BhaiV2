using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MobileCheck : MonoBehaviour
{
    // Link to the JavaScript function that returns whether the game is being played on a mobile browser

    public static bool _isMobile;

  
    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern bool IsMobile();

    public bool isMobile()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
            return IsMobile();
#endif
        return false;
    }

    void Start()
    {

        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            if (isMobile())
            {
                _isMobile = true;
            }
            else
            {
                _isMobile = false;
            }
            
        }
        
    }

    
}
