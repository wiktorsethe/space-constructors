using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniverseLevelGenerator : MonoBehaviour
{
    public List<GameObject> terrainSpawnPoints = new List<GameObject>();
    public GameObject[] levelParts;
    private Camera mainCamera;
    private float bufferDistance = 15f;
    private void Start()
    {
        mainCamera = Camera.main;
        Instantiate(levelParts[0], terrainSpawnPoints[0].transform.position, Quaternion.identity);
        terrainSpawnPoints.RemoveAt(0);
    }
    private void Update()
    {
        for (int i = 0; i < terrainSpawnPoints.Count; i++)
        {
            if (!IsObjectOutsideScreen(terrainSpawnPoints[i]))
            {
                int randInt = Random.Range(1, levelParts.Length);
                Instantiate(levelParts[randInt], terrainSpawnPoints[i].transform.position, Quaternion.identity);
                terrainSpawnPoints.RemoveAt(i);
            }
        }
    }
    private bool IsObjectOutsideScreen(GameObject spawnpoint)
    {
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(spawnpoint.transform.position);
        bool isOutsideScreen = screenPoint.x < -bufferDistance ||
                               screenPoint.x > 1 + bufferDistance ||
                               screenPoint.y < -bufferDistance ||
                               screenPoint.y > 1 + bufferDistance;

        return isOutsideScreen;
    }
}
