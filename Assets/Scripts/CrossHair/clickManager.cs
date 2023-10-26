using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;
public class clickManager : MonoBehaviour
{
    public static clickManager Instance;
    int layerMask1;
    int layerMask2;
    private GameObject enemy,deer;
    int combinedLayerMask;
    private float timer;
    [SerializeField] private float shootInterval;
    public bool canShoot;
    AudioSource audio;
    public GameObject CrossHair;
    private bool isOnEnemy; // New variable to track if the crosshair is on an enemy

    [SerializeField] int reload = 30;
    public GameObject gun;

    bool isReloading;
    [SerializeField] TextMeshProUGUI BishnoiCounter,DeerCounter;
    public int bishnoiCounter = 0,deerCounter =0;

    public Transform crossHair;
    private void Awake()
    {
        Instance = this;
        isReloading = false;
        canShoot = true;
        layerMask1 = LayerMask.GetMask("Enemy");
        layerMask2 = LayerMask.GetMask("Cover");
        combinedLayerMask = layerMask1 | layerMask2;
        audio = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        BishnoiCounter.text = bishnoiCounter.ToString();
        DeerCounter.text = deerCounter.ToString();
        if (!PlayerShoot.Instance.hasended)
        {
            CrossHairColour();

            timer += Time.deltaTime;

            if (canShoot && !isReloading && !PlayerShoot.Instance.defence)
            {

                Vector2 mousePosition = crossHair.position;
                RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector2.zero, combinedLayerMask);

                bool foundEnemy = false; // Track if an enemy was found in the hits

                if (hits.Length > 0)
                {
                    hits = hits.OrderByDescending(hit => hit.collider.GetComponent<SpriteRenderer>().sortingOrder).ToArray();

                    foreach (RaycastHit2D hit in hits)
                    {

                        if (hit.collider.tag == "Cover")
                        {

                            //Debug.Log(hit.collider.tag);
                            isOnEnemy = false; // Not on an enemy
                            return;
                        }

                        if (hit.collider.tag == "Enemy")
                        {
                            deer = null;
                            audio.Play();
                            enemy = hit.collider.gameObject;
                            //Debug.Log(enemy.tag);
                            isOnEnemy = true; // On an enemy
                            foundEnemy = true; // Set to true when an enemy is found
                        }
                        if (hit.collider.tag == "Deer")
                        {
                            enemy = null;
                            audio.Play();
                            deer = hit.collider.gameObject;
                            //Debug.Log(enemy.tag);
                            isOnEnemy = true; // On an enemy
                            foundEnemy = true; // Set to true when an enemy is found
                        }
                    }
                }

                if (!foundEnemy)
                {
                    isOnEnemy = false;
                    //Debug.Log("Not on Enemy");
                }

                if (enemy)
                {
                    if (isOnEnemy)
                    {
                        CrosshairRotation.Instance.ShakeX();
                        StartCoroutine(MuzzleFlash());
                        enemy.transform.GetComponent<Enemy>().Onshot();
                        //reload--;
                        canShoot = false; // Disable shooting
                        timer = 0f;
                    }

                }
                if (deer)
                {
                    if (isOnEnemy)
                    {
                        CrosshairRotation.Instance.ShakeX();
                        StartCoroutine(MuzzleFlash());
                        deer.transform.GetComponent<DeerMovement>().OnShot();
                        deerCounter++;
                        //reload--;
                        canShoot = false; // Disable shooting
                        timer = 0f;
                    }

                }
            }

            //if(reload <=0)
            //{
            //    isReloading = true;
            //    Reload();
            //}

            if (timer >= shootInterval)
            {
                canShoot = true; // Enable shooting after the interval
                timer = 0f;
            }
            IEnumerator Reloading()
            {
                Debug.Log("Reloading...");
                PlayerShoot.Instance.Defend();
                // Wait for 2 seconds
                yield return new WaitForSeconds(2f);
                PlayerShoot.Instance.Up();

                // Call your function here (replace MyFunction with the actual function you want to call)
                isReloading = false;
                reload = 30;
                // You can put any code here that you want to execute after the delay
            }

            IEnumerator MuzzleFlash()
            {
                gun.SetActive(true);

                yield return new WaitForSeconds(0.1f);

                gun.SetActive(false);
            }

            void Reload()
            {
                StartCoroutine(Reloading());
            }
        }

    }

    private void CrossHairColour()
    {
        Vector2 mousePosition0 = crossHair.position;
        RaycastHit2D[] hits0 = Physics2D.RaycastAll(mousePosition0, Vector2.zero, combinedLayerMask);
        if (hits0.Length > 0)
        {
            hits0 = hits0.OrderByDescending(hit => hit.collider.GetComponent<SpriteRenderer>().sortingOrder).ToArray();

            foreach (RaycastHit2D hit in hits0)
            {
                if (hit.collider.tag == "Enemy" || hit.collider.tag == "Deer")
                {

                    deer = null;
                    Color cColor = Color.green;
                    CrossHair.GetComponent<SpriteRenderer>().color = cColor;
                }

            }
        }
        else
        {

            Color cColor2 = Color.black;
            CrossHair.GetComponent<SpriteRenderer>().color = cColor2;
        }
    }
}
