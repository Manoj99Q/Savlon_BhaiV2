using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawnManager : MonoBehaviour
{
    public static enemySpawnManager Instance;
    // Start is called before the first frame update
    [SerializeField] private GameObject Enemy,Deer;
    [SerializeField] private Transform Dspot1, Dspot2;

    [SerializeField] private int Maxenemies;
    [SerializeField] private float SpawnInterval;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private List<Transform> AttackPoints;
    private List<Transform> AdjustedAttackPoints;
    private List<GameObject> currEnemies;
    private float timer;

    public int maxenemies = 30;
    private bool isSpawning = false;
    private float spawnTimer = 0.0f;



    Camera mainCamera; // Assuming you have a main camera in your scene
    private float Width;
    private float Height;
    private void Awake()
    {
        
        Instance = this;
        timer = 0;
        mainCamera = Camera.main;
        Width = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x * 2;
        Height = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y * 2;
        //Debug.Log(Height + " " + Width);
        currEnemies = new List<GameObject>();
        AdjustedAttackPoints = new List<Transform>();
        AdjustAttackPoints();


    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(AttackPoints.Count);
        if(maxenemies >0)
        {
            timer += Time.deltaTime;

            if (!isSpawning && maxenemies > 0 && !PlayerShoot.Instance.hasended)
            {
                spawnTimer += Time.deltaTime;

                // Check if it's time to spawn a deer.
                if (spawnTimer >= Random.Range(3.0f, 6.0f)) // Random spawn interval between 3 to 6 seconds.
                {
                    isSpawning = true;
                    spawnTimer = 0.0f;

                    // Randomly choose a spawn point.
                    Transform spawnPoint = Random.Range(0, 2) == 0 ? Dspot1 : Dspot2;
                    Vector3 deerSpawnPosition = spawnPoint.position;

                    GameObject newDeer = Instantiate(Deer, deerSpawnPosition, Quaternion.identity);
                    newDeer.SetActive(true);
                    if(spawnPoint.position.x>0)
                    {
                        newDeer.transform.localScale = new Vector3(-1, 1, 1);
                    }
                    else
                    {
                        newDeer.transform.localScale = new Vector3(1, 1, 1);
                    }

                    // Calculate the direction from the spawn point to the opposite spot.
                    Vector3 targetDirection = spawnPoint == Dspot1 ? (Dspot2.position - deerSpawnPosition).normalized : (Dspot1.position - deerSpawnPosition).normalized;

                    // Set the deer's movement speed.
                    float moveSpeed = 5.0f; // Set your desired movement speed.

                    // Move the deer using Translate.
                    StartCoroutine(MoveDeer(newDeer, targetDirection, moveSpeed));
                }
            }

            if (timer > SpawnInterval)
            {
                

                if (currEnemies.Count < Maxenemies)
                {


                    Vector3 spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Length)].position; // Set the position where you want to spawn the object.
                                                                                                       //Vector3 spawnPosition = new Vector2(0, 0); // Set the position where you want to spawn the object.

                    Quaternion spawnRotation = Quaternion.identity; // Set the rotation (Quaternion.identity means no rotation).

                    GameObject newEnemy = Instantiate(Enemy, spawnPosition, spawnRotation);
                    maxenemies--;
                    newEnemy.SetActive(true);
                    int randidx;
                    randidx = Random.Range(0, AdjustedAttackPoints.Count);
                    newEnemy.GetComponent<Enemy>().GoToAttackPoint(AdjustedAttackPoints[randidx]);
                    newEnemy.GetComponent<Enemy>().SetLatestPoint(AdjustedAttackPoints[randidx]);
                    RemoveAttackPoint(randidx);
                    currEnemies.Add(newEnemy);
                }

                timer = 0;
            }
        }
        


    }
    public void KilledEnemy(GameObject enemy)
    {
        currEnemies.Remove(enemy);
        
    }

    private IEnumerator MoveDeer(GameObject deer, Vector3 direction, float speed)
    {
        float startTime = Time.time;

        while (Time.time - startTime < 5.0f) // Move for 5 seconds.
        {
            if(deer!=null)
            {
                deer.transform.Translate(direction * speed * Time.deltaTime);
                
            }
            yield return null;
        }

        // Destroy the deer after moving for 5 seconds.
        Destroy(deer);
        isSpawning = false;
    }


    public List<Transform> getAttackPoints() // return AttackPoints array to give acces to other scripts
    {
        return AdjustedAttackPoints;
    }
    private void AdjustAttackPoints()
    {
        Debug.Log(AttackPoints.Count);
        foreach(Transform atkpoint in AttackPoints)
        {
            AdjustedAttackPoints.Add(atkpoint);
            Vector3 newPosition = atkpoint.position + new Vector3(1.5f,0,0);
            GameObject newObject = new GameObject();
            newObject.transform.position = newPosition;
            AdjustedAttackPoints.Add(newObject.transform);


             newPosition = atkpoint.position + new Vector3(-1.5f, 0, 0);
             newObject = new GameObject();
            newObject.transform.position = newPosition;
            AdjustedAttackPoints.Add(newObject.transform);

        }

        //foreach(Transform atkPoint in AdjustedAttackPoints)
        //{
        //    Debug.Log(atkPoint.position);
        //}
    }

    public void AddAttackPoint(Transform atkpoint)
    {
        AdjustedAttackPoints.Add(atkpoint);
    }

    public void RemoveAttackPoint(int idx)
    {
        AdjustedAttackPoints.RemoveAt(idx);
    }
}
