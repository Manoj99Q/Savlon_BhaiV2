using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private float threshold;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float coverChangeTime;
    [SerializeField] private GameObject bloodshot;
    [SerializeField] private List<Transform> AttackPoints;
    

    
    private bool spawned;
    private Transform attakcPoint;
    private bool inCombat;
    public bool inCover;

    private int shootingTime;
    private int coverTime;
    public Animator anim;
  
    private float timer;
    private float changeTimer;


    private Transform changePoint;
    private Transform latestPoint;
    EnemyShoot eshoot;

    public bool isMoving;
    


    public Transform hbar;
    public GameObject childBishnoi;


    private enemySpawnManager enemySpawnManager; //refernce for enemyspawnManger script

    public static bool ended;

    private void Awake()
    {
        
        eshoot = gameObject.GetComponent<EnemyShoot>();
        

        enemySpawnManager = enemySpawnManager.Instance; // setting the reference to the script
        List<Transform> AttackPoints = enemySpawnManager.getAttackPoints();
        //Debug.Log(AttackPoints.Count);

        latestPoint = null;
        
        spawned = true;
        inCombat = false;
        
        

       
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        if(!ended)
        {
            if (changeTimer > coverChangeTime)
            {
                isMoving = true;
                inCombat = false;
                SetChangePoint();
                changeTimer = 0;
                timer = 0;
                //anim.SetTrigger("Standing");
                //anim.ResetTrigger("Cover");
                inCover = false;


            }



            if (spawned)
            {

                Vector3 direction = attakcPoint.position - transform.position;

                if (direction.x > 0)
                {
                    Vector3 scale = transform.GetChild(0).localScale;
                    transform.GetChild(0).localScale = new Vector3(1 * Mathf.Abs(scale.x), scale.y, scale.z);


                }
                else
                {

                    Vector3 scale = transform.GetChild(0).localScale;
                    transform.GetChild(0).localScale = new Vector3(-1 * Mathf.Abs(scale.x), scale.y, scale.z);


                }

                direction.Normalize();
                isMoving = true;
                transform.Translate(direction * moveSpeed * Time.deltaTime);
                anim.SetBool("running", true);

                if (Vector3.Distance(transform.position, attakcPoint.position) < threshold)
                {
                    anim.SetBool("running", false);
                    isMoving = false;
                    spawned = false;
                    inCombat = true;
                    inCover = false;
                    SetShootingTime();
                    SetCoverTime();
                }

                return;
            }

            if (inCombat)
            {
                changeTimer += Time.deltaTime;

                timer += Time.deltaTime;

                if (inCover) //is in cover
                {

                    anim.SetBool("crouched", true);
                    Vector3 direction = (changePoint != null ? (changePoint.position) : attakcPoint.position) - new Vector3(0, 1.2f, 0) - transform.position;
                    transform.Translate(direction * moveSpeed * Time.deltaTime);
                    if (timer > coverTime)// go to standing
                    {


                        inCover = false;
                        isMoving = false;
                        SetShootingTime();
                        timer = 0;


                    }

                }

                if (!inCover) //in standing means shooting
                {
                    anim.SetBool("crouched", false);
                    //Debug.Log("Standing");
                    Vector3 direction = (changePoint != null ? (changePoint.position) : attakcPoint.position) - transform.position;
                    transform.Translate(direction * moveSpeed * Time.deltaTime);
                    if (timer > shootingTime) // go to cover
                    {
                        inCover = true;
                        //anim.SetTrigger("Cover");
                        //anim.ResetTrigger("Standing");
                        SetCoverTime();
                        timer = 0;

                    }
                }


            }


            if (!spawned && !inCombat)
            {
                Vector3 direction = changePoint.position - transform.position;

                if (direction.x > 0)
                {
                    Vector3 scale = transform.GetChild(0).localScale;
                    transform.GetChild(0).localScale = new Vector3(1 * Mathf.Abs(scale.x), scale.y, scale.z);


                }
                else
                {

                    Vector3 scale = transform.GetChild(0).localScale;
                    transform.GetChild(0).localScale = new Vector3(-1 * Mathf.Abs(scale.x), scale.y, scale.z);


                }

                direction.Normalize();

                transform.Translate(direction * moveSpeed * Time.deltaTime);
                anim.SetBool("running", true);

                if (Vector3.Distance(transform.position, changePoint.position) < threshold)
                {

                    inCombat = true;
                    anim.SetBool("running", false);
                    isMoving = false;
                    inCover = false;
                    SetShootingTime();
                    SetCoverTime();


                }
            }
        }
        else
        {
            anim.SetBool("running", false);
        }


        
            

           
        
        

    }
    public void GoToAttackPoint(Transform AttackPoint)
    {

        attakcPoint = AttackPoint;
        spawned = true;
        isMoving = true;
        
        
    }
    private void SetShootingTime()
    {
        shootingTime = Random.Range(4,6);

    }
    private void SetCoverTime()
    {
        coverTime = Random.Range(3, 5);
    }

    public void Onshot()
    {
        if(!inCover)
        {
            GameObject newObj = Instantiate(bloodshot, transform.position, Quaternion.identity);
            Debug.Log(newObj.name);
            eshoot.health -= 0.5f;
        }
        
        
        
       
    }

    public void SetChangePoint()
    {
        if(latestPoint!= null)
        {
            enemySpawnManager.AddAttackPoint(latestPoint);
        }
        AttackPoints = enemySpawnManager.getAttackPoints();
        Debug.Log(AttackPoints.Count);
        int ranidx = Random.Range(0, AttackPoints.Count);
        changePoint = AttackPoints[ranidx];
        enemySpawnManager.RemoveAttackPoint(ranidx);
        latestPoint = changePoint;
    }
    public void SetLatestPoint(Transform lp)
    {
        latestPoint = lp;
    }
    private void OnDestroy()
    {
        enemySpawnManager.AddAttackPoint(latestPoint);
    }


}
