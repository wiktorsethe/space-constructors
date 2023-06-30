using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Lists and Objects")]
    public Camera mainCamera;
    public PlayerStats playerStats;
    [Space(20f)]

    [Header("Variables")]
    public float maxDistance = 5f;
    public float delay = 2f;
    public float trashTimer = 0f;
    public float bestTimeTimer = 0f;
    public int kills = 0;
    public int goldEarned = 0;
    private void Update()
    {
        bestTimeTimer += Time.deltaTime;
        if(bestTimeTimer > playerStats.bestTime)
        {
            playerStats.bestTime = bestTimeTimer;
        }
        if(kills > playerStats.mostKills)
        {
            playerStats.mostKills = kills;
        }
        if(goldEarned > playerStats.mostGoldEarned)
        {
            playerStats.mostGoldEarned = goldEarned;
        }
    }

}
