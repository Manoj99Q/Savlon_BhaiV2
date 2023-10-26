using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAudio : MonoBehaviour
{
    public static FadeAudio Instance;
    public AudioSource audioSource1, audioSourceEnd, audioSourceWin;

    public float fadeDuration = 0.1f;

    private float startVolume1;
    private float startVolume2;

    bool hasplayed;

    private void Awake()
    {
        hasplayed = false;
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        startVolume1 = audioSource1.volume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Fade()
    {
        if(!hasplayed)
        {
            StartCoroutine(FadeOutAndIn());
            hasplayed = true;
        }
        
    }

    public IEnumerator FadeOutAndIn()
    {
        // Fade out audioSource1
        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            audioSource1.volume = Mathf.Lerp(startVolume1, 0, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        audioSource1.volume = 0;

        // Stop audioSource1 (optional)
        audioSource1.Stop();

        // Play audioSource2 (assuming it's not already playing)
        if (!audioSourceWin.isPlaying)
        {
            audioSourceWin.Play();
        }

    }
}
