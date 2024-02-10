using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private List<GameObject> pooledObjects = new List<GameObject>();
    public float spawnRate;
    private float spawnTimer = 0f;

    private void Start()
    {
        InstantiateObject();
    }
    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if(spawnTimer > spawnRate)
        {
            GameObject obj = GetPooledObject();

            obj.transform.position = RandomSpawnPoint().position;
            obj.transform.rotation = Quaternion.identity;
            
            obj.SetActive(true);
            spawnTimer = 0f;
        }
    }
    public Transform RandomSpawnPoint()
    {
        int randSpawnPoint = Random.Range(0, spawnPoints.Length);
        return spawnPoints[randSpawnPoint].transform;
    }
    public GameObject GetPooledObject()
    {
        int rand = Random.Range(0, 2);
        if(rand == 0 && pooledObjects.Count != 0)
        {
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                if (!pooledObjects[i].activeInHierarchy)
                {
                    return pooledObjects[i];
                }
            }
        }
        else
        {
            return InstantiateObject();
        }
        return null;
    }
    private GameObject InstantiateObject()
    {
        GameObject obj = Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)]);
        obj.SetActive(false);
        pooledObjects.Add(obj);
        return obj;
    }
}
