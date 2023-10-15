using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject deer;
    
    [SerializeField] private int Maxenemies;
    [SerializeField] private float SpawnInterval;
    private List<GameObject> currEnemies;
    private float timer;



    Camera mainCamera; // Assuming you have a main camera in your scene
    private float Width;
    private float Height;
    private void Awake()
    {
        timer = 0;
         mainCamera = Camera.main;
        Width = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x * 2;
        Height = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y * 2;
        Debug.Log(Height + " " + Width);
        currEnemies = new List<GameObject>();
        Debug.Log("Here");

    }

    // Update is called once per frame
    void Update()
    {
        
        timer += Time.deltaTime;

        
        if(timer > SpawnInterval)
        {
            if(currEnemies.Count < Maxenemies)
            {

                
                Vector3 spawnPosition = new Vector2(Random.Range(-Width/2,Width/2), Random.Range(-Height/2,Height/2)); // Set the position where you want to spawn the object.
                //Vector3 spawnPosition = new Vector2(0, 0); // Set the position where you want to spawn the object.

                Quaternion spawnRotation = Quaternion.identity; // Set the rotation (Quaternion.identity means no rotation).

                GameObject newEnemy =Instantiate(deer, spawnPosition, spawnRotation);
                newEnemy.SetActive(true);
                currEnemies.Add(newEnemy);
            }

            timer = 0;
        }


    }
}
