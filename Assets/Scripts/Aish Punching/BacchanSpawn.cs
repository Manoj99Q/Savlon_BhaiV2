using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacchanSpawn : MonoBehaviour
{
    public GameObject spritePrefab;
    public float spawnInterval = 2.0f; // Time between sprite spawns
    public float spriteLifeTime = 2.0f; // Time the sprite stays visible
    public float spriteRotationSpeed = 30.0f; // Rotation speed of the sprite

    private Vector3 screenCenter;
    private float timer;

    private void Start()
    {
        screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        timer = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0;
            SpawnSprite();
        }
    }

    private void SpawnSprite()
    {
        // Randomly determine the spawn edge (top, bottom, left, or right)
        Vector3 spawnPosition;
        float randomEdge = Random.Range(0, 4);

        if (randomEdge < 1)
        {
            // Spawn at the top edge
            spawnPosition = new Vector3(Random.Range(0, Screen.width), Screen.height, 0);
        }
        else if (randomEdge < 2)
        {
            // Spawn at the bottom edge
            spawnPosition = new Vector3(Random.Range(0, Screen.width), 0, 0);
        }
        else if (randomEdge < 3)
        {
            // Spawn at the left edge
            spawnPosition = new Vector3(0, Random.Range(0, Screen.height), 0);
        }
        else
        {
            // Spawn at the right edge
            spawnPosition = new Vector3(Screen.width, Random.Range(0, Screen.height), 0);
        }

        // Create the sprite object
        GameObject sprite = Instantiate(spritePrefab, spawnPosition, Quaternion.identity);

        // Rotate the sprite to face the center of the screen
        Vector3 directionToCenter = (screenCenter - sprite.transform.position).normalized;
        float angle = Mathf.Atan2(directionToCenter.y, directionToCenter.x) * Mathf.Rad2Deg;
        sprite.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Destroy the sprite after a set time
        Destroy(sprite, spriteLifeTime);
    }

    private void UpdateSpriteRotation()
    {
        GameObject[] sprites = GameObject.FindGameObjectsWithTag("SpriteTag");

        foreach (GameObject sprite in sprites)
        {
            sprite.transform.Rotate(Vector3.forward * spriteRotationSpeed * Time.deltaTime);
        }
    }
}
