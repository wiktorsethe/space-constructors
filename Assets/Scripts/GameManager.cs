using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [Header("Lists and Objects")]
    public Camera mainCamera;
    public PlayerStats playerStats;
    private ObstacleSpawner[] obstacleSpawners;
    [Space(20f)]

    [Header("Variables")]
    public float bestTimeTimer;
    public int kills = 0;
    public int goldEarned = 0;
    public TMP_Text sceneNameText;
    public string sceneName;
    private void Start()
    {
        obstacleSpawners = GameObject.FindObjectsOfType<ObstacleSpawner>();
        sceneNameText.text = sceneName;
        FadeInSceneNameText();
        Invoke("FadeOutSceneNameText", 3f);
        bestTimeTimer = PlayerPrefs.GetFloat("BestTimeTimer");
        kills = PlayerPrefs.GetInt("Kills");
        goldEarned = PlayerPrefs.GetInt("GoldEarned");
        StartCoroutine(SpawnRateChanger());
    }
    private void Update()
    {
        bestTimeTimer += Time.deltaTime;
        
        if(bestTimeTimer > playerStats.bestTime)
        {
            playerStats.bestTime = bestTimeTimer;
            playerStats.todayBestTime = bestTimeTimer; //poraw jak nie dziala
        }
        if(kills > playerStats.mostKills)
        {
            playerStats.mostKills = kills;
            playerStats.todayMostKills = kills;
        }
        if(goldEarned > playerStats.mostGoldEarned)
        {
            playerStats.mostGoldEarned = goldEarned;
            playerStats.todayMostGoldEarned = goldEarned;
        }
    }

    private void FadeInSceneNameText()
    {
        sceneNameText.DOFade(1f, 2f);
    }
    private void FadeOutSceneNameText()
    {
        sceneNameText.DOFade(0f, 2f);
        Destroy(sceneNameText.gameObject, 2f);
    }
    IEnumerator SpawnRateChanger()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            foreach (ObstacleSpawner obsSpawn in obstacleSpawners)
            {
                obsSpawn.spawnRate -= 0.1f;
            }
        }
    }
}
