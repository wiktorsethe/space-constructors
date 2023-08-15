using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
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
            int randSpawnPoint = Random.Range(0, spawnPoints.Length);
            obj.transform.position = spawnPoints[randSpawnPoint].transform.position;
            obj.transform.rotation = Quaternion.identity;
            
            obj.SetActive(true);
            spawnTimer = 0f;
        }
    }
    public GameObject GetPooledObject()
    {
        bool active = false;
        foreach (GameObject obj in pooledObjects)
        {
            if (!obj.activeSelf)
            {
                goto Next;
            }
            else if (obj.activeSelf)
            {
                active = true;
            }
        }

        if (active)
        {
            InstantiateObject();
        }

    Next:
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }


        return null;
    }
    private void InstantiateObject()
    {
        GameObject obj = Instantiate(obstaclePrefab);
        obj.SetActive(false);
        pooledObjects.Add(obj);
    }
}
