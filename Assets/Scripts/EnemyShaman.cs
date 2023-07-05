using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class EnemyShaman : MonoBehaviour
{
    private ExpBar expBar;
    public float health;
    public int experience; // ??
    public int gold;
    public GameManager gameManager;
    public PlayerStats playerStats;
    private GameObject player;
    private float spawnTimer = 0f;
    private float moveSpeed = 2f;
    public float inTarget = 7;
    public GameObject[] spawnPoints;
    public GameObject enemyWarriorPrefab;
    public List<GameObject> minions = new List<GameObject>();

    [Header("Health System")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject healthBarCanvas;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Gradient healthGradient;
    [SerializeField] private Image fillBar;
    public int maxHealth;
    private int currentHealth;
    [SerializeField] private float Angle;
    private float hideTimer = 0f;

    [SerializeField] private GameObject miningTextPrefab;
    private void Start()
    {
        expBar = GameObject.FindObjectOfType(typeof(ExpBar)) as ExpBar;
        gameManager = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
        player = GameObject.FindGameObjectWithTag("Player");

        canvas.worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        currentHealth = maxHealth;
        SetMaxHealth(maxHealth);
        healthBarCanvas.SetActive(false);
    }
    private void Update()
    {
        fillBar.color = healthGradient.Evaluate(healthBar.normalizedValue);
        hideTimer += Time.deltaTime;
        if (hideTimer > 2f)
        {
            healthBarCanvas.SetActive(false);
        }
        spawnTimer += Time.deltaTime;
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance >= (inTarget - 1f) && distance < 30f && spawnTimer >= 2.5f)
        {
            Vector3 vectorToTarget = player.transform.position - transform.position;
            transform.Find("EnemyShamanImage").transform.rotation = Quaternion.LookRotation(Vector3.forward, vectorToTarget); // rotacja do przegadania bo pewnie bedzie tylko L/R
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
        else if(distance < inTarget)
        {
            if(minions.Count == 0)
            {
                SpawnMinions();
            }
        }
    }
    private void SpawnMinions()
    {
        for(int i=0; i<3; i++)
        {
            minions.Add(Instantiate(enemyWarriorPrefab, spawnPoints[i].transform.position, Quaternion.identity));
        }
        spawnTimer = 0f;
    }
    public void SetMaxHealth(int health)
    {
        healthBar.maxValue = health;
        healthBar.value = health;
        fillBar.color = healthGradient.Evaluate(1f);
    }
    public void SetHealth()
    {
        DOTween.To(() => healthBar.value, x => healthBar.value = x, currentHealth, 1.5f);
    }
    public void CollisionDetected()
    {
        healthBarCanvas.SetActive(true);
        hideTimer = 0f;
        currentHealth -= 10;
        SetHealth();
        if (currentHealth <= 0)
        {
            expBar.SetExperience(experience);
            playerStats.gold += gold;
            gameManager.goldEarned += gold;
            gameManager.kills += 1;
            Destroy(gameObject, 2f);
        }
        if (miningTextPrefab)
        {
            ShowMiningText(10);
        }
    }
    private void ShowMiningText(int amount)
    {
        var text = Instantiate(miningTextPrefab, transform.position, Quaternion.identity);
        text.GetComponent<TMP_Text>().text = "+ " + amount.ToString();
    }
}
