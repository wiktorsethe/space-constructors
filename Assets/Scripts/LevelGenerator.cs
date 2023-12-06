using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("Other Scripts")]
    private Camera mainCamera;
    [Space(20f)]
    [Header("Lists")]
    public List<GameObject> terrainSpawnPoints = new List<GameObject>();
    public GameObject[] levelParts;
    [Space(20f)]
    [Header("Variables")]
    private float bufferDistance = 3f;
    private void Start()
    {
        mainCamera = Camera.main;
    }
    private void Update()
    {
        for (int i = 0; i < terrainSpawnPoints.Count; i++)
        {
            if (!IsObjectOutsideScreen(terrainSpawnPoints[i]))
            {
                int randInt = Random.Range(0, levelParts.Length);
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
