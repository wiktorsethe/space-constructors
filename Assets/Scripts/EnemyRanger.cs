using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnemyRanger : MonoBehaviour
{
    private ExpBar expBar;
    public float health;
    public int experience; // ??
    public int gold;
    public GameManager gameManager;
    public PlayerStats playerStats;
    private GameObject player;
    public float inTarget = 7;

    public float bulletSpeed = 10f;
    public string target = "Player";
    private float shootTimer = 0f;

    public GameObject bulletPrefab;
    public Transform firePoint;

    private float moveSpeed = 2f;

    [Header("Health System")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject healthBarCanvas;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Gradient healthGradient;
    [SerializeField] private Image fillBar;
    public int maxHealth;
    private int currentHealth;
    [SerializeField] private float Angle;
    private void Start()
    {
        expBar = GameObject.FindObjectOfType(typeof(ExpBar)) as ExpBar;
        gameManager = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (health <= 0)
        {
            expBar.SetExperience(experience);
            playerStats.gold += gold;
            gameManager.goldEarned += gold;
            gameManager.kills += 1;
            Destroy(gameObject);
        }
        float distance = Vector2.Distance(transform.position, player.transform.position);
        shootTimer += Time.deltaTime;
        if (distance < inTarget)
        {
            if (shootTimer < 2f)
            {
                Vector3 vectorToTarget = player.transform.position - transform.position;
                transform.rotation = Quaternion.LookRotation(Vector3.forward, vectorToTarget);
            }

            if (shootTimer >= 3f)
            {
                FireBullet();
                shootTimer = 0f;
            }
        }
        else if (distance >= (inTarget - 1f) && distance < 30f)
        {
            Vector3 vectorToTarget = player.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, vectorToTarget); // rotacja do przegadania bo pewnie bedzie tylko L/R
            //transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime); //ciekawe rzeczy jak weŸmiemy .right
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }

    }

    void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<ShootingBullet>().target = target;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 bulletVelocity = firePoint.up * bulletSpeed;
        rb.velocity = bulletVelocity;
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
}
