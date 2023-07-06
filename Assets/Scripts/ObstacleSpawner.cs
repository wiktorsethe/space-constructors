using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private float spawnRate;
    private float spawnTimer = 0f;
    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if(spawnTimer > spawnRate)
        {
            int randSpawnPoint = Random.Range(0, spawnPoints.Length);
            int randPrefab = Random.Range(0, obstaclePrefabs.Length);
            Instantiate(obstaclePrefabs[randPrefab], spawnPoints[randSpawnPoint].transform.position, Quaternion.identity);
            spawnTimer = 0f;
        }
    }
}
