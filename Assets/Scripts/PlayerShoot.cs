using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    public static PlayerShoot Instance;

    public float health;
    public bool defence = false;

    public Transform hbar;
    public Slider hSlider;
    public Sprite[] HealthFaces;
    public Image HealthFace;
    Vector3 localScale;

    public Image hitEffectPanel; // Reference to the hit effect panel

    public GameObject canvas;

    public Sprite SavlonDefending;
    public SpriteRenderer Savlon;
    public GameObject hand;
    
    Sprite tempSprite;

    Vector3 ogPosition;

    public Sprite[] PlayerEnd;
    public GameObject CrossHair;
    public bool hasended;
    bool ended;
    public GameObject endScreen,winscreen;


    //Audio

    public AudioSource audioSource1, audioSourceEnd,audioSourceWin;

    public float fadeDuration = 2.0f;

    private float startVolume1;
    private float startVolume2;

    public int levelID;

    public GameObject Tutorial, InputGameObj;

    int tutComplete;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        tutComplete = PlayerPrefs.GetInt("TutComplete");
        InputGameObj.GetComponent<joyStick>().enabled = false;
        InputGameObj.GetComponent<CrossHairMovement>().enabled = false;
        Tutorial.SetActive(false);
        startVolume1 = audioSource1.volume;
        startVolume2 = audioSourceEnd.volume;
        Enemy.ended = false;
        ogPosition = transform.position;
        tempSprite = Savlon.sprite;
        localScale = hbar.localScale;
        health = 1;
        
        if (MobileCheck._isMobile)
        {
            if (tutComplete == 0)
            {
                Tutorial.SetActive(true);
            }
            InputGameObj.GetComponent<joyStick>().enabled = true;


        }
        else
        {
            InputGameObj.GetComponent<CrossHairMovement>().enabled = true;

        }
    }

    void Update()
    {
        
        localScale.x = health;
        hbar.localScale = localScale;

        hSlider.value = health;
        if (health <= 0 && !hasended)
        {
            hasended = true;
            ended = true;
            if (ended)
            {
                StartCoroutine(FadeOutAndIn(audioSourceEnd));
                Enemy.ended = true;
                ended = false;
                CrossHair.SetActive(false);
                clickManager.Instance.canShoot = false;
                hand.gameObject.SetActive(false);
                Savlon.sprite = PlayerEnd[0];
                LeanTween.moveLocalY(gameObject, -7f, 5f).setOnComplete(()=>{ endScreen.SetActive(true); });
            }


            //Destroy(gameObject);
        }

        if (clickManager.Instance.bishnoiCounter == 30)
        {
            if (!Enemy.ended)
            {


                int levelPassed = PlayerPrefs.GetInt("LevelPassed", levelID);
                if (levelPassed != 2)
                {
                    PlayerPrefs.SetInt("LevelPassed", levelID);
                }
                LeanTween.delayedCall(1.5f, () => { WinScreen(); });
                
            }
           
            

        }

    }

    private void WinScreen()
    {
        Up();
        StartCoroutine(FadeOutAndIn(audioSourceWin));
        Enemy.ended = true;
        ended = false;
        CrossHair.SetActive(false);
        clickManager.Instance.canShoot = false;
        hand.gameObject.SetActive(false);
        Savlon.sprite = PlayerEnd[1];
        LeanTween.delayedCall(2f, () => { winscreen.SetActive(true); });
    }

    private IEnumerator FadeOutAndIn(AudioSource audio)
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
        if (!audio.isPlaying)
        {
            audio.Play();
        }

        // Fade in audioSource2
        elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            audio.volume = Mathf.Lerp(0, startVolume2, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        audio.volume = startVolume2;
    }

    public void HitEffect()
    {
        health -= 0.15f;
        if(health<0.75)
        {
            HealthFace.sprite = HealthFaces[0];
        }
        if (health<0.35)
        {
            HealthFace.sprite = HealthFaces[1];
        }
        CrossHairMovement.Instance.movementDefence = false;
        // Activate the hit effect panel
        canvas.SetActive(true);
        //defence = true;
        // Start a coroutine to deactivate it after 0.1 seconds
        StartCoroutine(ActivateHitEffect());
        //LeanTween.delayedCall(0.1f,()=> StartCoroutine(Defence()));
        
    }

    IEnumerator ActivateHitEffect()
    {
        float fadeDuration = 1f;
        Color startColor = hitEffectPanel.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 0f);
        float startTime = Time.time;

        while (Time.time - startTime < fadeDuration)
        {
            float t = (Time.time - startTime) / fadeDuration;
            hitEffectPanel.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
        }

        // Ensure the alpha is set to the target alpha
        hitEffectPanel.color = targetColor;

        // Wait for 0.1 seconds
        yield return new WaitForSeconds(0.5f);


        // Deactivate the hit effect panel
        canvas.SetActive(false);
        targetColor = new Color(startColor.r, startColor.g, startColor.b, 1f);
        hitEffectPanel.color = targetColor;

    }



    public void  Defend()
    {
       
        defence = true;
        tempSprite = Savlon.sprite;
        hand.SetActive(false);
        Savlon.sprite = SavlonDefending;
        gameObject.transform.position = new Vector3(transform.position.x, -4.67f, transform.position.z);
        
    }

    public void Up()
    {
        if(defence)
        {
            defence = false;
            hand.SetActive(true);
            Savlon.sprite = SavlonDefending;

            gameObject.transform.position = ogPosition;
            Savlon.sprite = tempSprite;
        }
            

    }




   

}
