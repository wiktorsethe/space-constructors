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
    public Menu menu;
    public GameObject bossPrefab;
    public CameraSize camSize;
    public CameraFollow camFollow;
    public Boundaries bounds;
    public ObstacleSpawner[] obstacleSpawners;
    private GameObject player;
    [Space(20f)]

    [Header("Variables")]
    public float bestTimeTimer;
    public int kills = 0;
    public int goldEarned = 0;
    public TMP_Text sceneNameText;
    public string sceneName;
    private Vector3 bossSize = new Vector3(5.29f, 8.04f, 0.2f);
    private void Start()
    {
        camSize = GameObject.FindObjectOfType(typeof(CameraSize)) as CameraSize;
        camFollow = GameObject.FindObjectOfType(typeof(CameraFollow)) as CameraFollow;
        bounds = GameObject.FindObjectOfType(typeof(Boundaries)) as Boundaries;
        menu = GameObject.FindObjectOfType(typeof(Menu)) as Menu;
        player = GameObject.FindGameObjectWithTag("Player");
        obstacleSpawners = GameObject.FindObjectsOfType<ObstacleSpawner>();
        if (menu)
        {
            menu.DeactiveBossHealthBar();
        }
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
            float newSize = mainCamera.orthographicSize * 3f;

            camSize.CamSize(newSize, 4f);

            Invoke("SpawnBoss", 5f);

            camFollow.enabled = false;
            foreach(ObstacleSpawner script in obstacleSpawners)
            {
                script.enabled = false;
            }
            bounds.Check();
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("EnemyShip");
            foreach(GameObject enemy in enemies)
            {
                Destroy(enemy);
            }
            GameObject[] meteorites = GameObject.FindGameObjectsWithTag("Meteorite");
            foreach (GameObject meteorite in meteorites)
            {
                Destroy(meteorite);
            }
            PlayerPrefs.SetString("FirstBoss", "True");
        }

        ParticleSystem[] particles = FindObjectsOfType<ParticleSystem>();
        foreach (ParticleSystem obj in particles)
        {
            if (!obj.GetComponent<ParticleSystem>().IsAlive())
            {
                //obj.gameObject.SetActive(false);
            }
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

    private void SpawnBoss()
    {

        Renderer[] playerRenderers = player.GetComponentsInChildren<Renderer>();
        if (playerRenderers.Length > 0)
        {
            Bounds combinedBounds = playerRenderers[0].bounds;
            for (int i = 1; i < playerRenderers.Length; i++)
            {
                combinedBounds.Encapsulate(playerRenderers[i].bounds);
            }
            Vector3 size = combinedBounds.size;
            Vector3 center = combinedBounds.center;

            Vector3 gap = new Vector3(size.x / bossSize.x, size.y / bossSize.y, bossSize.z);

            GameObject boss = Instantiate(bossPrefab, new Vector3(mainCamera.transform.position.x, bounds.spawnPoints[1].transform.position.y - bossSize.y * gap.y, 0f), Quaternion.identity);

            if (gap.x <= gap.y)
            {
                boss.transform.localScale = new Vector3(gap.y, gap.y, boss.transform.localScale.z);
            }
            else
            {
                boss.transform.localScale = new Vector3(gap.x, gap.x, boss.transform.localScale.z);
            }
        }
    }
}
