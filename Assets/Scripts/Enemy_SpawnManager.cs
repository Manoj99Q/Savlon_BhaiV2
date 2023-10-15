using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab; // The enemy prefab to spawn
    [SerializeField] private Transform[] spawnPoints; // Array of spawn points
    [SerializeField] private int maxEnemies; // Maximum number of enemies to spawn

    public List<GameObject> currentEnemies = new List<GameObject>();
    private List<int> availableSpawnPoints = new List<int>(); // List of available spawn point indices

    void Start()
    {
        InitializeSpawnPoints();
        SpawnEnemies(maxEnemies);
    }

    void Update()
    {
        currentEnemies.RemoveAll(enemy => enemy == null);

        if (currentEnemies.Count == 0)
        {
            InitializeSpawnPoints(); // Reset available spawn points
            SpawnEnemies(maxEnemies);
        }
    }

    void InitializeSpawnPoints()
    {
        // Populate the list of available spawn points with all spawn point indices
        availableSpawnPoints.Clear();
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            availableSpawnPoints.Add(i);
        }
    }

    void SpawnEnemies(int numEnemies)
    {
        int spawnPointsCount = spawnPoints.Length;

        if (numEnemies > spawnPointsCount)
        {
            Debug.LogWarning("The number of enemies to spawn exceeds the number of spawn points.");
            numEnemies = spawnPointsCount;
        }

        for (int i = 0; i < numEnemies; i++)
        {
            // Choose a random available spawn point
            int randomIndex = Random.Range(0, availableSpawnPoints.Count);
            int spawnPointIndex = availableSpawnPoints[randomIndex];

            // Remove the selected spawn point from the available list
            availableSpawnPoints.RemoveAt(randomIndex);

            // Instantiate the enemy at the chosen spawn point
            Transform spawnPoint = spawnPoints[spawnPointIndex];
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            currentEnemies.Add(newEnemy);
        }
    }
}
