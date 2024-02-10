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
    public BackgroundScaler bgScaler;
    public ObstacleSpawner[] obstacleSpawners;
    //private GameObject player;
    [Space(20f)]

    [Header("Variables")]
    public float bestTimeTimer;
    public int kills = 0;
    public int goldEarned = 0;
    public int score = 0;
    public TMP_Text sceneNameText;
    public string sceneName;
    private Vector3 bossSize = new Vector3(5.29f, 8.04f, 0.2f);
    private void Start()
    {
        // Inicjalizacja referencji do skryptów i obiektów
        camSize = GameObject.FindObjectOfType(typeof(CameraSize)) as CameraSize;
        camFollow = GameObject.FindObjectOfType(typeof(CameraFollow)) as CameraFollow;
        bounds = GameObject.FindObjectOfType(typeof(Boundaries)) as Boundaries;
        menu = GameObject.FindObjectOfType(typeof(Menu)) as Menu;
        bgScaler = GameObject.FindObjectOfType(typeof(BackgroundScaler)) as BackgroundScaler;
        //player = GameObject.FindGameObjectWithTag("Player");
        obstacleSpawners = GameObject.FindObjectsOfType<ObstacleSpawner>();

        // Deaktywacja paska zdrowia bossa na starcie gry
        if (menu)
        {
            menu.DeactiveBossHealthBar();
        }

        // Ustawienie tekstu nazwy sceny i animacja jej pojawienia siê
        sceneNameText.text = sceneName;
        FadeInSceneNameText();
        Invoke("FadeOutSceneNameText", 3f);

        // Wczytanie najlepszego czasu, liczby zniszczonych przeciwników i zebranego z³ota z PlayerPrefs
        bestTimeTimer = PlayerPrefs.GetFloat("BestTimeTimer");
        kills = PlayerPrefs.GetInt("Kills");
        goldEarned = PlayerPrefs.GetInt("GoldEarned");
        score = PlayerPrefs.GetInt("Score");

        // Uruchomienie rutyny zmiany czêstotliwoœci pojawiania siê przeszkód
        StartCoroutine(SpawnRateChanger());
    }
    private void Update()
    {
        bestTimeTimer += Time.deltaTime;

        // Aktualizacja najlepszego czasu, liczby zniszczonych przeciwników i zebranego z³ota w statystykach gracza
        if (bestTimeTimer > playerStats.bestTime)
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

        // Wywo³anie bossa na poziomie 5 w scenie "Universe"
        if (playerStats.level == 5 && SceneManager.GetActiveScene().name == "Universe" && !PlayerPrefs.HasKey("FirstBoss"))
        {
            // Aktywacja paska zdrowia bossa, dostosowanie rozmiaru kamery, wywo³anie bossa i dezaktywacja innych elementów gry
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

            bgScaler.ActivateCortoutine();
            // Zapisanie informacji o pierwszym spotkaniu z bossem w PlayerPrefs
            PlayerPrefs.SetString("FirstBoss", "True");
        }

        // Deaktywacja obiektów Particle System, które ju¿ siê zakoñczy³y
        ParticleSystem[] particles = FindObjectsOfType<ParticleSystem>();
        foreach (ParticleSystem obj in particles)
        {
            if (!obj.GetComponent<ParticleSystem>().IsAlive())
            {
                obj.gameObject.SetActive(false);
            }
        }
    }

    private void FadeInSceneNameText()
    {
        // Animacja pojawienia siê tekstu nazwy sceny
        sceneNameText.DOFade(1f, 2f);
    }
    private void FadeOutSceneNameText()
    {
        // Animacja zanikania tekstu nazwy sceny
        sceneNameText.DOFade(0f, 2f);
        Destroy(sceneNameText.gameObject, 2f);
    }
    IEnumerator SpawnRateChanger()
    {
        // Rutyna zmieniaj¹ca czêstoœæ pojawiania siê przeszkód co 10 sekund
        while (true)
        {
            yield return new WaitForSeconds(30f);
            foreach (ObstacleSpawner obsSpawn in obstacleSpawners)
            {
                obsSpawn.spawnRate -= 0.1f;
            }
        }
    }

    private void SpawnBoss()
    {
        // Spawn bossa w okreœlonej pozycji i rotacji
        Instantiate(bossPrefab, new Vector3(mainCamera.transform.position.x, bounds.spawnPoints[1].transform.position.y + bossSize.y + 10f, 0f), Quaternion.identity);
    }
}
