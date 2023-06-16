using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> trashList = new List<GameObject>(5);
    public GameObject trashPrefab;
    public Camera mainCamera;
    public float maxDistance = 5f;

    private void Update()
    {
        for (int i = 0; i < trashList.Count; i++)
        {
            if (trashList[i] == null)
            {
                trashList.RemoveAt(i);
                SpawnRandomTrash(trashPrefab);
            }
        }

    }
    public void AddToTrashList(GameObject trash)
    {
        trashList.Add(trash);
    }
    private void SpawnRandomTrash(GameObject trash)
    {
        Vector3 cameraPosition = mainCamera.transform.position;

        float cameraWidth = mainCamera.orthographicSize * 2f * mainCamera.aspect;
        float cameraHeight = mainCamera.orthographicSize * 2f;

        float cameraLeft = cameraPosition.x - cameraWidth / 2f;
        float cameraRight = cameraPosition.x + cameraWidth / 2f;
        float cameraBottom = cameraPosition.y - cameraHeight / 2f;
        float cameraTop = cameraPosition.y + cameraHeight / 2f;

        float minSpawnX = Random.Range(cameraLeft, cameraLeft - maxDistance);
        float maxSpawnX = Random.Range(cameraRight, cameraRight + maxDistance);
        float spawnX;
        int randX = Random.Range(0, 2);
        if (randX == 0)
        {
            spawnX = minSpawnX;
        }
        else
        {
            spawnX = maxSpawnX;
        }
        float minSpawnY = Random.Range(cameraBottom, cameraBottom - maxDistance);
        float maxSpawnY = Random.Range(cameraTop, cameraTop + maxDistance);
        float spawnY;
        int randY = Random.Range(0, 2);
        if (randY == 0)
        {
            spawnY = minSpawnY;
        }
        else
        {
            spawnY = maxSpawnY;
        }

        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);
        GameObject spawnedObject = Instantiate(trash, spawnPosition, Quaternion.identity);

    }
}
