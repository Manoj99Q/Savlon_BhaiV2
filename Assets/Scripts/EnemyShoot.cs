using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public float shootInterval = 2f; // Adjust the interval between enemy shots as needed
    private float timer = 0f;
    public float health = 1;

    public bool defence = false;

    public Transform hbar;
    Vector3 localScale;
    Enemy enemy;

    public GameObject muzzle, exclaimation;
    AudioSource audio;
    public AudioSource bulletHit;

    public Transform muzzleSpot;

    public int randomShootVal;
    public Animator anim;

    

    void Start()
    {
        // Start shooting at random intervals
        enemy = gameObject.GetComponent<Enemy>();
        audio = gameObject.GetComponent<AudioSource>();
        timer = Random.Range(0f, shootInterval);
        localScale = hbar.localScale;
        
    }

    void Update()
    {
        

        localScale.x = health;
        hbar.localScale = localScale;

        if (timer <= 0f )
        {
            if(enemy.isMoving==false && enemy.inCover == false && !PlayerShoot.Instance.hasended && !enemy.spawned)
            {
                
                ShootPlayer();
                anim.SetTrigger("shooting");
                timer = shootInterval;
            }
            
        }
       
            timer -= Time.deltaTime;

        if (health == 0)
        {
            if (InputTut.Instance != null)
            {
                InputTut.Instance.Deactivate();
            }
            health = 0.01f;
            enemySpawnManager.Instance.KilledEnemy(gameObject);
            clickManager.Instance.bishnoiCounter++;

            Destroy(gameObject, 0.15f);
        }
    }

    void ShootPlayer()
    {
        

            if (PlayerShoot.Instance != null)
            {
                // Enemy is shooting, deactivate defense
                defence = false;

                
                
                    //GameObject muzzlePref = Instantiate(muzzle, muzzleSpot.position, Quaternion.identity);
                    //muzzlePref.transform.parent = gameObject.transform;
                    audio.Play();
                    //Destroy(muzzlePref, 0.25f);
                    

            // Reduce player's health by 10 when not defending
            int shoot = Random.Range(0, randomShootVal);
                    if(shoot == 1)
                    {
                        
                         StartCoroutine(PerformActionsCoroutine());
                     }
                //Debug.Log("Player's health reduced by 10. Current health: " + PlayerShoot.Instance.health);
            }
            
        
    }
    private IEnumerator PerformActionsCoroutine()
    {
        bulletHit.Play();
        FlickerChildren.Instance.Flicker();
        exclaimation.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        if (gameObject != null)
        {
            if (PlayerShoot.Instance.defence == false)
            {
                PlayerShoot.Instance.HitEffect();
            }
            exclaimation.SetActive(false);
        }
    }
}
