using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [Header("Lists and Objects")]
    public Camera mainCamera;
    public PlayerStats playerStats;
    private Menu menu;
    public GameObject bossPrefab;
    private CameraSize camSize;
    private CameraFollow camFollow;
    private Boundaries bounds;
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
        camSize = GameObject.FindObjectOfType(typeof(CameraSize)) as CameraSize;
        camFollow = GameObject.FindObjectOfType(typeof(CameraFollow)) as CameraFollow;
        bounds = GameObject.FindObjectOfType(typeof(Boundaries)) as Boundaries;
        menu = GameObject.FindObjectOfType(typeof(Menu)) as Menu;
        obstacleSpawners = GameObject.FindObjectsOfType<ObstacleSpawner>();
        menu.DeactiveBossHealthBar();
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

        if(playerStats.level == 5 && SceneManager.GetActiveScene().name == "Universe" && !PlayerPrefs.HasKey("FirstBoss"))
        {
            menu.ActiveBossHealthBar();
            Instantiate(bossPrefab, new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y + mainCamera.orthographicSize + 5f, 0f), Quaternion.identity);
            float newSize = mainCamera.orthographicSize * 3f;
            camSize.CamSize(newSize, 4f);
            camFollow.enabled = false;
            foreach(ObstacleSpawner script in obstacleSpawners)
            {
                script.enabled = false;
            }
            bounds.Check();
            PlayerPrefs.SetString("FirstBoss", "True");
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
