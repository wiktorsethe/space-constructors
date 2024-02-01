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
            // SprawdŸ, czy obiekt jest poza widokiem kamery
            if (!IsObjectOutsideScreen(terrainSpawnPoints[i]))
            {
                // Losuj element poziomu i zainstancjuj go na miejscu spawnu terenu
                int randInt = Random.Range(0, levelParts.Length);
                Instantiate(levelParts[randInt], terrainSpawnPoints[i].transform.position, Quaternion.identity);

                // Usuñ punkt spawnu terenu z listy
                terrainSpawnPoints.RemoveAt(i);
            }
        }
    }
    private bool IsObjectOutsideScreen(GameObject spawnpoint)
    {
        // Konwertuj pozycjê obiektu na wspó³rzêdne ekranowe
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(spawnpoint.transform.position);

        // SprawdŸ, czy obiekt jest poza widokiem kamery
        bool isOutsideScreen = screenPoint.x < -bufferDistance ||
                               screenPoint.x > 1 + bufferDistance ||
                               screenPoint.y < -bufferDistance ||
                               screenPoint.y > 1 + bufferDistance;

        return isOutsideScreen;
    }
}
