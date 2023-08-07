using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class EnemyShaman : MonoBehaviour
{
    [Header("Other Scripts")]
    private ExpBar expBar;
    public PlayerStats playerStats;
    private GameManager gameManager;
    [Space(20f)]
    [Header("Variables")]
    [SerializeField] private int experience; // ??
    [SerializeField] private int gold;
    private float spawnTimer = 0f;
    private float moveSpeed = 2f;
    private float inTarget = 13;
    [Space(20f)]
    [Header("GameObjects and Rest")]
    private GameObject player;
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private GameObject enemyWarriorPrefab;
    private List<GameObject> minions = new List<GameObject>();
    [SerializeField] private GameObject miningTextPrefab;
    [SerializeField] private Animator animator;
    private bool facingRight;
    private float previousX;
    [Space(20f)]
    [Header("Health System")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject healthBarCanvas;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Gradient healthGradient;
    [SerializeField] private Image fillBar;
    [SerializeField] private int maxHealth;
    private int currentHealth;
    [SerializeField] private float Angle;
    private float hideTimer = 0f;

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

        float velocityX = transform.position.x - previousX;
        previousX = transform.position.x;

        if (hideTimer > 2f)
        {
            healthBarCanvas.SetActive(false);
        }
        spawnTimer += Time.deltaTime;
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance >= (inTarget - 1f) && distance < 30f && spawnTimer >= 2.5f)
        {
            if (velocityX > 0 && facingRight)
            {
                Flip();
            }
            if (velocityX < 0 && !facingRight)
            {
                Flip();
            }
            //Vector3 vectorToTarget = player.transform.position - transform.position;
            //transform.Find("EnemyShamanImage").transform.rotation = Quaternion.LookRotation(Vector3.forward, vectorToTarget); // rotacja do przegadania bo pewnie bedzie tylko L/R
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
        else if(distance < inTarget)
        {
            if(minions.Count == 0)
            {
                SpawnMinions();
                Invoke("EnableMinions", 0.4f);
                animator.SetTrigger("Play");
                spawnTimer = 0f;
            }
        }
    }
    private void SpawnMinions()
    {
        for(int i=0; i<3; i++)
        {
            GameObject minion = Instantiate(enemyWarriorPrefab, spawnPoints[i].transform.position, Quaternion.identity);
            minions.Add(minion);
            minion.SetActive(false);
        }
    }
    private void EnableMinions()
    {
        foreach(GameObject minion in minions)
        {
            minion.SetActive(true);
        }
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
            spawnTimer = -10f;
            GetComponent<LootBag>().InstantiateLoot(transform.position);
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
        text.GetComponent<TMP_Text>().text = amount.ToString();
    }
    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facingRight = !facingRight;
    }
}
