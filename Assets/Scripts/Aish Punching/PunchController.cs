using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
public class PunchController : MonoBehaviour
{
    public GameObject leftDefense;
    public GameObject leftPunch;
    public GameObject rightDefense;
    public GameObject rightPunch;
    public GameObject AishFace; // Reference to the head object

    private bool isLeftPunching = false;
    private bool isRightPunching = false;

    [SerializeField] float duration;
    [SerializeField] float shakeDuration = 0.2f;
    [SerializeField] float shakeIntensity = 15f;
    [SerializeField] Sprite[] Faces;
    public Slider AishHealth;
    public float health;

    public float HealthDecreament;

    private Quaternion originalRotation; // Store the original rotation of the head

    public Transform[] HitSpots;

    public AudioClip[] hitClips;

    public GameObject Hit;

    AudioSource audio;

    public GameObject finishHer;

    public GameObject EndScreen;
    private void Start()
    {
        audio = GetComponent<AudioSource>();
        health = 1;
        originalRotation = AishFace.transform.rotation;
    }

    private void Update()
    {

        AishHealth.value = health;
        if (health > 0.75f && health<1f)
        {
            
        }

        else if(health > 0.5 && health < 0.75f)
        {
            
            AishFace.GetComponent<Image>().sprite = Faces[0];
        }
        else if (health > 0.25f && health < 0.5f)
        {
            RagePanel.Instance.FadeIn();
            ContinuousShake.shakeAmount = 0.5f;
            if(finishHer!=null)
            {
                finishHer.SetActive(true);
                Destroy(finishHer, 1.7f);
            }
            
            AishFace.GetComponent<Image>().sprite = Faces[1];
        }
        else if (health > 0f && health < 0.25f)
        {
            
            
            AishFace.GetComponent<Image>().sprite = Faces[2];
        }


        if(health<=0)
        {
            EndScreen.SetActive(true);
            FadeAudio.Instance.Fade();
            ContinuousShake.shakeAmount = 0f;
            
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            RightPunch();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            LeftPunch();
        }
    }

    public void LeftPunch()
    {
        
        if (!isLeftPunching && !isRightPunching)
        {
            isLeftPunching = true;
            leftDefense.SetActive(false);
            leftPunch.SetActive(true);
            StartCoroutine(ShakeHeadAndReset());
        }
    }

    public void RightPunch()
    {
        
        if (!isRightPunching && !isLeftPunching)
        {
            isRightPunching = true;
            rightDefense.SetActive(false);
            rightPunch.SetActive(true);
            StartCoroutine(ShakeHeadAndReset());
        }
    }

    private IEnumerator ShakeHeadAndReset()
    {
        audio.Play();
        int randomIndex = UnityEngine.Random.Range(0, HitSpots.Length); // Generate a random index within the array's bounds
        Transform hitPos = HitSpots[randomIndex]; // Select a random Transform from HitSpots
        GameObject hitPref = Instantiate(Hit, hitPos.position, hitPos.transform.rotation);
        //hitPref.transform.localScale = new Vector2(Hit.transform.localScale.x, Hit.transform.localScale.y)*2;
        Destroy(hitPref, 1f);
        health -= HealthDecreament;
        // Shake the head
        float elapsedTime = 0;
        while (elapsedTime < shakeDuration)
        {
            float t = elapsedTime / shakeDuration;
            float shakeAngle = Mathf.Sin(t * Mathf.PI * 2) * shakeIntensity;
            AishFace.transform.rotation = originalRotation * Quaternion.Euler(0, 0, shakeAngle);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Reset the head to its original rotation
        AishFace.transform.rotation = originalRotation;

        // Perform the punch action (LeftPunch or RightPunch)
        

        // Wait for the regular punch duration
        yield return new WaitForSeconds(duration);

        // Clean up
        leftPunch.SetActive(false);
        rightPunch.SetActive(false);
        leftDefense.SetActive(true);
        rightDefense.SetActive(true);
        isLeftPunching = false;
        isRightPunching = false;
    }
}