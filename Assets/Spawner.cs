using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public float spawnInterval = 5f;
    public float spawnY = 6f;
    public float spawnXMin = -4f;
    public float spawnXMax = 4f;

    void Start()
    {
        InvokeRepeating("SpawnObstacle", 1f, spawnInterval);
    }

    void SpawnObstacle()
    {
        int randomIndex = Random.Range(0, obstaclePrefabs.Length);
        float randomX = Random.Range(spawnXMin, spawnXMax);
        Vector2 spawnPos = new Vector2(randomX, spawnY);
        Instantiate(obstaclePrefabs[randomIndex], spawnPos, Quaternion.identity);
    }
}