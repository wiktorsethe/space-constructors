using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] terrainSpawnPoints;
    public GameObject[] levelParts;

    private void Start()
    {
        for (int i = 0; i < terrainSpawnPoints.Length; i++)
        {
            int randInt = Random.Range(0, levelParts.Length);
            Instantiate(levelParts[randInt], terrainSpawnPoints[i].transform.position, Quaternion.identity);
        }
    }
}
