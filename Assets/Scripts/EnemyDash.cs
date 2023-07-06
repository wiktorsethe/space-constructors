using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class EnemyDash : MonoBehaviour
{
    [Header("Other Scripts")]
    private ExpBar expBar;
    private GameManager gameManager;
    public PlayerStats playerStats;
    [Space(20f)]
    [Header("Variables")]
    [SerializeField] private int experience;
    [SerializeField] private int gold;
    private float inTarget = 5;
    private float moveSpeed = 7f;
    public float dashSpeed = 0f;
    [Space(20f)]
    [Header("GameObjects and Rest")]
    private GameObject player;
    [SerializeField] private GameObject miningTextPrefab;
    [SerializeField] private string target;
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
    Vector3 vectorToTarget;
    private bool hasDirection = false;
    private bool isInState = false;
    public bool triggerTouch = false;
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
        dashSpeed += Time.deltaTime;
        Debug.Log(triggerTouch);
        if (hideTimer > 2f)
        {
            healthBarCanvas.SetActive(false);
        }
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance <= inTarget && !isInState)
        {

            transform.Find("EnemyShipImage").transform.rotation = Quaternion.LookRotation(Vector3.forward, vectorToTarget);
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, player.gameObject.GetComponent<ShipMovement>().moveSpeed * Time.deltaTime);
            isInState = true;

        }
        else if (distance > inTarget && !isInState)
        {
            Vector3 vectorToTarget = player.transform.position - transform.position;
            //transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime); //ciekawe rzeczy jak weümiemy .right
            transform.Find("EnemyShipImage").GetComponent<PolygonCollider2D>().isTrigger = false;
            transform.Find("EnemyShipImage").transform.rotation = Quaternion.LookRotation(Vector3.forward, vectorToTarget);
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
        else if (isInState)
        {
            if (dashSpeed > 4f)
            {
                if (!hasDirection)
                {
                    vectorToTarget = player.transform.position - transform.position;
                    hasDirection = true;
                }
                transform.Find("EnemyShipImage").GetComponent<PolygonCollider2D>().isTrigger = true;
                transform.rotation = Quaternion.LookRotation(Vector3.forward, vectorToTarget);
                transform.Translate(Vector3.up * player.gameObject.GetComponent<ShipMovement>().moveSpeed * Time.deltaTime);
                if (distance > inTarget && isInState && triggerTouch)
                {
                    dashSpeed = 0f;
                    hasDirection = false;
                    triggerTouch = false;
                }
            }

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
}
